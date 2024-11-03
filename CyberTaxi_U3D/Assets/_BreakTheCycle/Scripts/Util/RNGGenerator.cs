using System;
using UnityEngine;

namespace BreakTheCycle.Util {
    public static class RNGGenerator {

        /// <summary>
        /// Sets up the seed for the RNGGenerator only the first time this class is being called
        /// </summary>
        private static void InitializeSeed()
        {
            int seed = int.Parse(GetTimestamp());
            UnityEngine.Random.InitState(seed);
            Debug.Log($"[RNGGenerator] Seed initialized with timestamp: {seed}");
        }

        /// <summary>
        /// Rools 1 dice
        /// </summary>
        /// <param name="maxValue">Max Value of the dice</param>
        /// <returns></returns>
        public static int Roll1D(int maxValue) {            
            return UnityEngine.Random.Range(1, maxValue + 1);
        }

        /// <summary>
        /// Returns a random number between min and max.
        /// Always generates a new seed.
        /// </summary>
        /// <param name="min">min value [inclusive]</param>
        /// <param name="max">max value [inclusive]</param>
        /// <returns></returns>
        public static int RandomBetween(int min, int max) {            
            return UnityEngine.Random.Range(min, max + 1);
        }

        /// <summary>
        /// Returns a random number between min and max.
        /// Always generates a new seed.
        /// </summary>
        /// <param name="min">min value [inclusive]</param>
        /// <param name="max">max value [inclusive]</param>
        /// <returns></returns>
        public static float RandomBetween(float min, float max) {
            return UnityEngine.Random.Range(min, max + 1);
        }

        /// <summary>
        /// Returns a random enum value of type T.
        /// Always generates a new seed.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="includeNone">Should include the first value (NONE)? False by default</param>
        /// <returns></returns>
        public static T RandomEnumValue<T>(bool includeNone = false) where T : struct, IConvertible {
            var values = Enum.GetValues(typeof(T));
            int randomIndex = RandomBetween(includeNone ? 0 : 1, values.Length);
            return (T) values.GetValue(randomIndex);
        }

        public static string GetTimestamp() {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            int currentTime = (int) (DateTime.UtcNow - epochStart).TotalSeconds;
            return currentTime.ToString();
        }
    }
}