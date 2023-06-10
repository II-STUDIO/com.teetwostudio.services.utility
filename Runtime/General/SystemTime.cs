using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public readonly struct SystemTime
    {
        private static float deltaTime;

        public static float DeltaTime
        {
            get => deltaTime;
        }

        //
        // Summary:
        //     Update deltatime.
        public static void UpdateDeltaTime()
        {
            deltaTime = Time.deltaTime;
        }
    }
}