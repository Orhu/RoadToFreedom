using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {
    private static int trailNum = 0; // trail number

    public static float length {get; private set;} // trail length
    public static float progress {get; private set;} // player progress on trail

    public static string[] traits {get; private set;} // trail traits

    private int timeToNextEvent = 2;

    public void StartTrail() {
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
            progress += 0.1f * CharacterStats.moveSpeed;
            Debug.Log(progress);
            
            // end trail check
            if (progress >= length) {
                trailNum++;
                //EndTrail();
                //return;
            }
            
            // next event countdown
            timeToNextEvent--;
            if (timeToNextEvent <= 0) {
                World.LoadRandomEvent();
            }
        }
        // go again
        StartCoroutine(TrailUpdate());
    }
}
