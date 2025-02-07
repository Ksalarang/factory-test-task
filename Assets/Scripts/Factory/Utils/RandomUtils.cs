using UnityEngine;

namespace Factory.Utils
{
    public static class RandomUtils
    {
        public static bool NextBool()
        {
            return Random.value < 0.5f;
        }
    }
}