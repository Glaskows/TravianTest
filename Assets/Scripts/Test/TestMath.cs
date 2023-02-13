using Travian.Math;
using Travian.WorldMap;
using UnityEngine;

namespace Travian.Test
{
    public class TestMath : MonoBehaviour
    {
        public GameObject tile3d;
        public HexCoordinate[] coordinates;

        public void Start()
        {
            foreach (HexCoordinate coordinate in coordinates)
            {
                Tile tile = new Tile(coordinate);
                GameObject go = Instantiate(tile3d, tile.GetWorldPosition(), Quaternion.identity, transform);
                go.name = tile.Coordinate.ToString();
            }
        }
    }
}