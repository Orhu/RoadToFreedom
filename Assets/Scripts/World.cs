using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    public static float time {get; private set;} = 6.0f;
    public static string timeOfDay {get; private set;} = "morning";

    public static void TickTime() {
        time += 0.1f;
        time = Mathf.Round((((time*10f) % 240f)/10f)*10f)/10f;
        Debug.Log($"Current World Time: {time}");
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
            yield return null; // will create a weird delay, watch out for that
        }

        TimeSkip(timeAdvance);
    }

    public static void TimeSkip(float timeAdvance) { // make sure you only advance time in the last stage of an event and only pop up events after the last one resolves
        float oldTime = time;
        World.AdvanceTime(timeAdvance);
        SlaveCatcher.AdvanceTime(timeAdvance);
        if (SceneController.gameState == GameState.IN_TOWN && SlaveCatcher.scState == SlaveCatcherState.INVESTIGATING_TOWN){
            if (Trail.trailNum-1 <= SlaveCatcher.scTrailNum-1){
                SlaveCatcher.CatchPlayer();
            }
        }
        float newTime = time;

        // check if any mandatory events happened during the time skip and display them after a few seconds
        if (SceneController.gameState == GameState.ON_TRAIL || SceneController.prevState == GameState.ON_TRAIL) {
            if (oldTime > newTime) { // past midnight, also look out for the case where the sleep event advances time
                DynamicEventHandler.SetNextEvent(0);
                Trail.SetTimeToNext(3);
                return;
            } else if ((oldTime > 0f && oldTime < 5.9f) && newTime >= 5.9f) { // look out for sleep event here too
                if (CharacterStats.GetResource(1) >= 1) {
                    DynamicEventHandler.SetNextEvent(1);
                    Trail.SetTimeToNext(3);
                    return;
                } else {
                    DynamicEventHandler.SetNextEvent(2);
                    Trail.SetTimeToNext(3);
                    return;
                }
            } else if ((oldTime > 6f && oldTime < 12f) && newTime >= 12f) {
                if (CharacterStats.GetResource(1) >= 1) {
                    DynamicEventHandler.SetNextEvent(3);
                    Trail.SetTimeToNext(3);
                    return;
                } else {
                    DynamicEventHandler.SetNextEvent(4);
                    Trail.SetTimeToNext(3);
                    return;
                }
            } else if ((oldTime > 12f && oldTime < 18f) && newTime >= 18f) {
                if (CharacterStats.GetResource(1) >= 1) {
                    DynamicEventHandler.SetNextEvent(5);
                    Trail.SetTimeToNext(3);
                    return;
                } else {
                    DynamicEventHandler.SetNextEvent(6);
                    Trail.SetTimeToNext(3);
                    return;
                }
            }
        }
    }

}
