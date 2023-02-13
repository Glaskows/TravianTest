using System;
using UnityEngine;

namespace Travian.Math
{
    [Serializable]
    public struct HexCoordinate
    {
        public readonly int q;
        public readonly int r;
        public readonly int s;

        public static readonly HexCoordinate Zero = new (0, 0, 0);

        public HexCoordinate(int _q, int _r, int _s)
        {
            q = _q;
            r = _r;
            s = _s;
        }

        public override string ToString() => $"({q},{r},{s})";

        public bool Equals(HexCoordinate other)
        {
            return this == other;
        }

        public override bool Equals(object other)
        {
            return other is HexCoordinate coordinate && Equals(coordinate);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(q, r);
        }

        public static HexCoordinate operator +(HexCoordinate a)
        {
            return a;
        }

        public static HexCoordinate operator -(HexCoordinate a)
        {
            return new HexCoordinate(-a.q, -a.r, -a.s);
        }

        public static HexCoordinate operator +(HexCoordinate a, HexCoordinate b)
        {
            return new HexCoordinate(a.q + b.q, a.r + b.r, a.s + b.s);
        }

        public static HexCoordinate operator -(HexCoordinate a, HexCoordinate b)
        {
            return new HexCoordinate(a.q - b.q, a.r - b.r, a.s - b.s);
        }

        public static HexCoordinate operator *(HexCoordinate a, int scalar)
        {
            return new HexCoordinate(a.q * scalar, a.r * scalar, a.s * scalar);
        }

        public static HexCoordinate operator /(HexCoordinate a, int scalar)
        {
            if (scalar == 0)
            {
                Debug.LogError("Division by 0 is not allowed, returning 0 as failsafe");
                return Zero;
            }

            return new HexCoordinate(a.q / scalar, a.r / scalar, a.s / scalar);
        }

        public static bool operator ==(HexCoordinate a, HexCoordinate b)
        {
            return a.q == b.q && a.r == b.r && a.s == b.s;
        }

        public static bool operator !=(HexCoordinate a, HexCoordinate b)
        {
            return !(a == b);
        }

        public static int Distance(HexCoordinate a, HexCoordinate b)
        {
            HexCoordinate d = b - a;
            return (Mathf.Abs(d.q) + Mathf.Abs(d.r) + Mathf.Abs(d.s)) / 2;
        }
    }
}