using UnityEngine;

namespace oneBlack74.UnityToolbox.Core
{
    public static class VectorExtensions
    {
        public static Vector3 WithX(this Vector3 v, float x) => new(x, v.y, v.z);
        public static Vector3 WithY(this Vector3 v, float y) => new(v.x, y, v.z);
        public static Vector3 WithZ(this Vector3 v, float z) => new(v.x, v.y, z);

        public static Vector2 WithX(this Vector2 v, float x) => new(x, v.y);
        public static Vector2 WithY(this Vector2 v, float y) => new(v.x, y);

        public static Vector2 ToVector2(this Vector3 v) => new(v.x, v.y);
        public static Vector3 ToVector3(this Vector2 v, float z = 0f) => new(v.x, v.y, z);
    }
}
