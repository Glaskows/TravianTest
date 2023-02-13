using System;
using System.Collections.Generic;
using Travian.Math;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Travian.WorldMap
{
    [Serializable]
    public struct Map
    {
        public readonly Tile[] Tiles;

        public Map(int radius, int maxHeight)
        {
            Random.InitState((int)DateTime.Now.Ticks);

            Tiles =
                SetTileTypeBasedOnHeight(
                    DoFellOff(
                        DoPerlinHeight(
                            GenerateRange(radius),
                            maxHeight),
                        HexCoordinate.Zero, 1.8f)
                );
        }

        public Vector3 GetWorldTilePosition(HexCoordinate coordinate)
        {
            foreach (Tile tile in Tiles)
            {
                if (tile.Coordinate == coordinate)
                {
                    return tile.GetWorldPosition();
                }
            }

            return Vector3.zero;
        }

        public Tile? GetTileAt(HexCoordinate coordinate)
        {
            int idx = Array.FindIndex(Tiles, tile => tile.Coordinate == coordinate);
            if (idx <= 0)
                return null;

            return Tiles[idx];
        }

        private static Tile[] SetTileTypeBasedOnHeight(Tile[] tiles)
        {
            for (int i = 0; i < tiles.Length; ++i)
            {
                Tile tile = tiles[i];
                TileType type;
                if (tile.Height == 0)
                {
                    type = TileType.Water;
                }
                else if (tile.Height == 1)
                {
                    type = Random.value < 0.4f ? TileType.Sand : TileType.Grass;
                }
                else if (tile.Height == 2)
                {
                    type = TileType.Grass;
                }
                else if (tile.Height < 5)
                {
                    type = Random.value < 0.4f ? TileType.Grass : TileType.Forest;
                }
                else if (tile.Height < 9)
                {
                    type = TileType.Forest;
                }
                else
                {
                    type = TileType.Rock;
                }

                tiles[i] = new Tile(tile.Coordinate, tile.Height, type);
            }

            return tiles;
        }

        private static Tile[] DoFellOff(Tile[] tiles, HexCoordinate center, float power)
        {
            // using a simple gausian curve
            // p(x) = exp(-(x-mu)^2/(2*sigma^2))/sqrt(2*pi*sigma^2)

            Vector3 centerPos = new Tile(center).GetWorldPosition();

            for (int i = 0; i < tiles.Length; ++i)
            {
                Tile tile = tiles[i];
                Vector3 tilePos = tile.GetWorldPosition();
                Vector3 planePos = new Vector3(tilePos.x, 0f, tilePos.z);
                float distance = Vector3.Magnitude(planePos - centerPos);

                float factor = Mathf.Exp(
                                   -distance * distance /
                                   (2f * power * power))
                               / (Mathf.Sqrt(2f * Mathf.PI) * power);
                tiles[i] = new Tile(tile.Coordinate, (int) (tile.Height * factor), tile.Type);
            }

            return tiles;
        }

        private static Tile[] DoPerlinHeight(Tile[] tiles, int maxHeight)
        {
            // perlin implementation symmetrical on axis, need to move it to first quadrant
            float minX = float.MaxValue;
            float minZ = float.MaxValue;
            foreach (Tile tile in tiles)
            {
                Vector3 pos = tile.GetWorldPosition();
                minX = Mathf.Min(minX, pos.x);
                minZ = Mathf.Min(minZ, pos.z);
            }

            minX += 1000f * Random.value;
            minZ += 1000f * Random.value;
            
            for (int i = 0; i < tiles.Length; ++i)
            {
                Tile tile = tiles[i];
                Vector3 worldPos = tile.GetWorldPosition();
                float noise = Mathf.PerlinNoise(minX + worldPos.x / 5f, minZ + worldPos.z / 5f);
                int height = Mathf.RoundToInt(noise * maxHeight);
                tiles[i] = new Tile(tile.Coordinate, height, TileType.Grass);
                tiles[i] = new Tile(tile.Coordinate, height, TileType.Grass);
            }

            return tiles;
        }

        private static Tile[] GenerateRange(int radius)
        {
            List<Tile> tiles = new List<Tile>();
            for (int q = -radius + 1; q < radius; ++q)
            {
                int startr = Mathf.Max(-radius + 1, -q - radius + 1);
                int endr = Mathf.Min(radius, -q + radius);
                for (int r = startr; r < endr; ++r)
                {
                    int s = -q - r;
                    HexCoordinate pos = new HexCoordinate(q, r, s);
                    tiles.Add(new Tile(pos));
                }
            }

            return tiles.ToArray();
        }
    }
}