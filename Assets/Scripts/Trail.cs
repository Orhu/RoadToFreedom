using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {
    public static int trailNum = 0; // trail number

    public static float length {get; private set;} // trail length
    public static float progress {get; private set;} // player progress on trail

    public static string[] traits {get; private set;} // trail traits

    private static int timeToNextEvent = 10;

    public void StartTrail() {
        progress = 0f;
        InitializeTrail();
        StartCoroutine(TrailUpdate());
    }

    private void InitializeTrail() {
        switch (trailNum) {
            case 0: // trail from start to town 1
                length = 45f;
                traits = new string[]{"easy"};
                timeToNextEvent = 10;
                return;
            case 1: // trail from town 1 to town 2
                length = 60f;
                traits = new string[]{"medium"};
                timeToNextEvent = 10;
                SlaveCatcher.Activate();
                return;
            case 2: // trail from town 2 to town 3
                length = 90f;
                traits = new string[]{"medium"};
                timeToNextEvent = 10;
                return;
            case 3: // trail from town 3 to final challenge
                length = 90f;
                traits = new string[]{"hard"};
                timeToNextEvent = 10;
                return;
            case 4: // marathon final challenge
                length = 120f;
                traits = new string[]{"marathon"};
                timeToNextEvent = 10;
                return;
            case 5: // city final challenge
                length = 15f;
                traits = new string[]{"city"};
                timeToNextEvent = 10;
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
                trailNum++;
                EndTrail();
            }

            SlaveCatcher.TickTime();
            
            // next event countdown
            if (timeToNextEvent <= 0 && SceneController.gameState == GameState.ON_TRAIL) {
                Debug.Log("Loading Random Event");
                World.LoadRandomEvent();
            }
        }
        // go again
        StartCoroutine(TrailUpdate());
    }

    public static void UpdateTimeToNextEvent(int nextTime) {
        if (SceneController.gameState == GameState.ON_TRAIL && nextTime != 0) {
            timeToNextEvent = nextTime;
            Debug.Log($"Updating time to next event. Values should match: {nextTime}, {timeToNextEvent}");
        }
    }

    public static void EndTrail() {
        SceneController.LoadTown(trailNum);
    }
}
