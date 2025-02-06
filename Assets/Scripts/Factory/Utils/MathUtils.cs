using UnityEngine;

namespace Factory.Utils
{
    public static class MathUtils
    {
        public static Vector2 Clamp(Vector2 value, float min, float max)
        {
            value.x = Mathf.Clamp(value.x, min, max);
            value.y = Mathf.Clamp(value.y, min, max);
            return value;
        }

        public static Vector2 ClampAbsoluteValue(Vector2 value, float min, float max)
        {
            value.x = Mathf.Sign(value.x) * Mathf.Clamp(Mathf.Abs(value.x), min, max);
            value.y = Mathf.Sign(value.y) * Mathf.Clamp(Mathf.Abs(value.y), min, max);
            return value;
        }

        public static Vector3 ClampAbsoluteValue(Vector3 value, float min, float max)
        {
            value.x = Mathf.Sign(value.x) * Mathf.Clamp(Mathf.Abs(value.x), min, max);
            value.y = Mathf.Sign(value.y) * Mathf.Clamp(Mathf.Abs(value.y), min, max);
            value.z = Mathf.Sign(value.z) * Mathf.Clamp(Mathf.Abs(value.z), min, max);
            return value;
        }
    }
}