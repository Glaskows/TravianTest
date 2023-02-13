using System;
using Travian.Math;
using UnityEngine;

namespace Travian.WorldMap
{
    [Serializable]
    public struct Tile
    {
        public readonly HexCoordinate Coordinate;
        // Height of tile in multiples of Thickness
        public readonly int Height;
        public readonly TileType Type;
        
        public const float Width = 1f;
        public const float Thickness = 0.1f;
        public static readonly float Size = Width / Mathf.Sqrt(3);

        public Tile(HexCoordinate coordinate)
        {
            Coordinate = coordinate;
            Height = 0;
            Type = TileType.Water;
        }

        public Tile(HexCoordinate coordinate, int height, TileType type)
        {
            Coordinate = coordinate;
            Height = height;
            Type = type;
        }

        public Vector3 GetWorldPosition()
        {
            Vector2 pos = Coordinate2Vector(Coordinate);
            return new Vector3(pos.x, Height * Thickness, pos.y);
        }

        public static HexCoordinate Vector2Coordinate(Vector2 pos)
        {
            float qfrac = (Mathf.Sqrt(3f) / 3f * pos.x - 1f / 3f * pos.y) / Size;
            float rfrac = 2f / 3f * pos.y / Size;
            return Fractional2Coordiante(qfrac, rfrac, -qfrac - rfrac);
        }

        public static Vector2 Coordinate2Vector(HexCoordinate coord)
        {
            float x = Size * ((Mathf.Sqrt(3f) * coord.q) + (0.5f * Mathf.Sqrt(3) * coord.r));
            float y = Size * 1.5f * coord.r;
            return new Vector2(x, y);
        }

        private static HexCoordinate Fractional2Coordiante(float qfrac, float rfrac, float sfrac)
        {
            int q = Mathf.RoundToInt(qfrac);
            int r = Mathf.RoundToInt(rfrac);
            int s = Mathf.RoundToInt(sfrac);

            float dq = Mathf.Abs(qfrac - q);
            float dr = Mathf.Abs(rfrac - r);
            float ds = Mathf.Abs(sfrac - s);

            if (dq > dr && dq > ds)
                q = -r - s;
            else if (dr > ds)
                r = -q - s;
            else
                s = -q - r;

            return new HexCoordinate(q, r, s);
        }
    }
}
