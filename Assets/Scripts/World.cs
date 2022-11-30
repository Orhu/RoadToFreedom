using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    public static float time {get; private set;} = 0f;
    //public static string weather {get; private set;} = "clear"; cut weather
    public static string timeOfDay {get; private set;} = "night";

    public static void TickTime() {
        time += 0.1f;
        time = ((time*10f) % 240f)/10f;
        Debug.Log(time);
        UpdateTimeOfDay();

        switch (time) {
                case 0f:
                    LoadEvent(0);
                    break;
                case 5.9f:
                    if (CharacterStats.GetResource(1) >= 1) {
                        LoadEvent(1);
                    } else {
                        LoadEvent(2);
                    }
                    break;
                case 12f:
                    if (CharacterStats.GetResource(1) >= 1) {
                        LoadEvent(3);
                    } else {
                        LoadEvent(4);
                    }
                    break;
                case 18f:
                    if (CharacterStats.GetResource(1) >= 1) {
                        LoadEvent(5);
                    } else {
                        LoadEvent(6);
                    }
                    break;
                default:
                    break;
            }
    }

    public static void AdvanceTime(float timeAdvance) { // make sure you cover for mandated events in trail
        time += timeAdvance;
        time = ((time*10f) % 240f)/10f;
        UpdateTimeOfDay();
    }

    // possibly depricated
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

    /* Cut weather
    public static void ChangeWeather(string newWeather) {
        weather = newWeather;
    }*/

    public static void LoadRandomEvent() { // keep it simple for now, just pull from all possible events for the current trail
        List<int> validEvents = new List<int>(EventPools.GetEventPool(Trail.traits[0])); // trail master list

        var rng = new System.Random();
        int pickVal = rng.Next(validEvents.Count);
        SceneController.StartEventLoad(validEvents[pickVal]);
    }

    public static void LoadEvent(int eventID) {
        SceneController.StartEventLoad(eventID);
    }

    public IEnumerator QueueTimeSkip(float timeAdvance) {
        while(SceneController.gameState != GameState.ON_TRAIL) {
            yield return new WaitForSeconds(0.1f); // will create a weird delay, watch out for that
        }

        TimeSkip(timeAdvance);
    }

    private IEnumerator TimeSkip(float timeAdvance) { // make sure you only advance time in the last stage of an event and only pop up events after the last one resolves
        float oldTime = time;
        World.AdvanceTime(timeAdvance);
        float newTime = time;

        // check if any mandatory events happened during the time skip and display them after a few seconds
        if (oldTime > newTime) { // past midnight, also look out for the case where the sleep event advances time
            yield return new WaitForSeconds(0.25f);
            LoadEvent(0);
        } else if ((oldTime > 0f && oldTime < 5.9f) && newTime >= 5.9f) { // look out for sleep event here too
            yield return new WaitForSeconds(0.5f);
            if (CharacterStats.GetResource(1) >= 1) {
                LoadEvent(1);
            } else {
                LoadEvent(2);
            }
        } else if ((oldTime > 6f && oldTime < 12f) && newTime >= 12f) {
            yield return new WaitForSeconds(0.5f);
            if (CharacterStats.GetResource(1) >= 1) {
                LoadEvent(3);
            } else {
                LoadEvent(4);
            }
        } else if ((oldTime > 12f && oldTime < 18f) && newTime >= 18f) {
            yield return new WaitForSeconds(0.5f);
            if (CharacterStats.GetResource(1) >= 1) {
                LoadEvent(5);
            } else {
                LoadEvent(6);
            }
        }
    }

}