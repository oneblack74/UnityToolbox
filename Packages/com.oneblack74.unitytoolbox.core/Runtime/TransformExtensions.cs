using UnityEngine;

namespace oneBlack74.UnityToolbox.Core
{
    public static class TransformExtensions
    {
        // Resets local position, rotation and scale
        public static void ResetLocal(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public static void SetPositionX(this Transform transform, float x)
            => transform.position = transform.position.WithX(x);

        public static void SetPositionY(this Transform transform, float y)
            => transform.position = transform.position.WithY(y);

        public static void SetPositionZ(this Transform transform, float z)
            => transform.position = transform.position.WithZ(z);
    }
}
