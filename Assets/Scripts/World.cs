using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    public static float time {get; private set;} = 0f;
    public static string weather {get; private set;} = "clear";
    public static string timeOfDay {get; private set;} = "night";

    public static void TickTime() {
        time += 0.1f;
        time = ((time*10f) % 240f)/10f;
        UpdateTimeOfDay();
    }

    public static void AdvanceTime(float timeAdvance) { // make sure you cover for mandated events in trail
        time += timeAdvance;
        time = ((time*10f) % 240f)/10f;
        UpdateTimeOfDay();
    }

    private static void UpdateTimeOfDay() {
        if (time < 4.5f) {
            timeOfDay = "night";
        } else if (time < 6f) {
            timeOfDay = "dawn";
        } else if (time < 12f) {
            timeOfDay = "morning";
        } else if (time < 17f) {
            timeOfDay = "afternoon";
        } else if (time < 20f) {
            timeOfDay = "evening";
        } else {
            timeOfDay = "night";
        }
    }

    public static void ChangeWeather(string newWeather) {
        weather = newWeather;
    }
}
