using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {
    public static int trailNum = 0; // trail number

    public static float length {get; private set;} // trail length
    public static float progress {get; private set;} // player progress on trail

    public static string[] traits {get; private set;} // trail traits

    private static int timeToNextEvent = 10;
    private static bool mandatedResetTime = false;
    private static bool running = false;

    public void StartTrail() {
        progress = 0f;
        InitializeTrail();
        if (!running) {
            running = true;
            StartCoroutine(TrailUpdate());
        }
    }
    

    private void InitializeTrail() {
        switch (trailNum) {
            case 0: // trail from start to town 1
                length = 45f;
                traits = new string[]{"easy"};
                timeToNextEvent = 15;
                DynamicEventHandler.AddEventToPool(new int[]{7,8,13,14,17,20,35});
                return;
            case 1: // trail from town 1 to town 2
                length = 60f;
                traits = new string[]{"medium"};
                timeToNextEvent = 15;
                DynamicEventHandler.AddEventToPool(new int[]{19,21,22,23,24,26,29,33,34,39});
                SlaveCatcher.Activate();
                return;
            case 2: // trail from town 2 to town 3
                length = 90f;
                traits = new string[]{"hard"};
                timeToNextEvent = 15;
                DynamicEventHandler.AddEventToPool(new int[]{27,30,36,41});
                return;
            case 3: // marathon
                length = 1f;
                traits = new string[]{"last"};
                timeToNextEvent = 15;
                return;
            case 4: // marathon final challenge
                length = 120f;
                traits = new string[]{"marathon"};
                timeToNextEvent = 15;
                return;
            case 5: // city final challenge
                length = 30f;
                traits = new string[]{"city"};
                timeToNextEvent = 15;
                DynamicEventHandler.ResetMasterPool();
                DynamicEventHandler.AddEventToPool(new int[]{120,121,122,123,124,125,126,127});
                return;
        }
    }

    private IEnumerator TrailUpdate() {
        yield return new WaitForSeconds(0.5f); // 2x speed to make game slightly faster
        if (SceneController.gameState == GameState.ON_TRAIL) {
            // update time and distance
            World.TickTime(); // +0.1 hours every second, also handles set events since we don't do event loading in here anymore
            progress = Mathf.Round((progress + (0.1f * CharacterStats.moveSpeed))*10f)/10f;
            timeToNextEvent--;
            Debug.Log($"Player progress on trail = {progress}/{length}\nTime to next event: {timeToNextEvent}");

            // end trail check
            if (progress >= length) {
                progress = 0f;
                EndTrail();                
            }

            SlaveCatcher.TickTime();
            
            // next event countdown
            if (timeToNextEvent <= 0 && SceneController.gameState == GameState.ON_TRAIL) {
                Debug.Log("Loading Random Event");
                DynamicEventHandler.LoadEvent();
            }
        }
        // go again
        StartCoroutine(TrailUpdate());
    }

    public static void UpdateTimeToNextEvent(int nextTime) {
        if (mandatedResetTime) {
            mandatedResetTime = false;
        } else if (SceneController.gameState == GameState.ON_TRAIL && nextTime != 0) {
            timeToNextEvent = nextTime;
            Debug.Log($"Updating time to next event. Values should match: {nextTime}, {timeToNextEvent}");
        }
    }

    public static void EndTrail() {
        if (trailNum == 3) {
            trailNum = 4 + SceneController.destinationNum;
            if (trailNum == 4) {
                World.LoadEvent(132); // enter marathon
            } else if (trailNum == 5) {
                World.LoadEvent(119); // enter detroit
            }
        }
        else if (trailNum == 4) {
            World.LoadEvent(131); // trail ending
        } else if (trailNum == 5) {
            World.LoadEvent(128); // boat ending
        } else {
            switch (trailNum) {
                case 0:
                    World.LoadEvent(117);
                    break;
                case 1:
                    World.LoadEvent(116);
                    break;
                case 2:
                    World.LoadEvent(118);
                    break;
            }
            trailNum++;
        }
    }

    public static void Restart() {
        trailNum = 0;
        timeToNextEvent = 10;
        progress = 0f;
    }

    public static void SetTimeToNext(int newTime) {
        timeToNextEvent = newTime;
        mandatedResetTime = true;
    }
}
