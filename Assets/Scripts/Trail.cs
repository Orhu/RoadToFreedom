using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {
    public float length {get; private set;} // trail length
    public float progress {get; private set;} // player progress on trail

    public string[] traits {get; private set;} // trail traits

    private float moveSpeed;

    private int timeToNextEvent = 10;

    public void StartTrail() {
        moveSpeed = 2.5f + (0.5f * GetBonus(CharacterSheet.statSpd));
        StartCoroutine(TrailUpdate());
    }

    private int GetBonus(int statScore) {
        return ((statScore + 1)/2) - 1 + (statScore/6);
    }

    private IEnumerator TrailUpdate() {
        yield return new WaitForSeconds(1f);
        if (SceneController.gameState == GameState.ON_TRAIL) {
            // update time and distance
            World.TickTime(); // +0.1 hours every second
            progress += 0.1f * moveSpeed;
            
            // end trail check
            if (progress >= length) {
                //EndTrail();
                //return;
            }

            // time sensitive events
            switch (World.time) {
                case 0f:
                    // sleep event
                    break;
                case 5.9f:
                    // show morning meal event (if skipped sleep)
                    break;
                case 12f:
                    // lunch event
                    break;
                case 18f:
                    // dinner event
                    break;
                default:
                    break;
            }

            // next event countdown
            timeToNextEvent--;
            if (timeToNextEvent <= 0) {
                // LoadEvent();
            }
        }
        // go again
        StartCoroutine(TrailUpdate());
    }

    private void TimeSkip(float timeAdvance) { // make sure you only advance time in the last stage of an event and only pop up events after the last one resolves
        float oldTime = World.time;
        World.AdvanceTime(timeAdvance);
        float newTime = World.time;

        // check if any mandatory events happened during the time skip and display them after a few seconds
        if (oldTime > newTime) { // past midnight, also look out for the case where the sleep event advances time
            // show sleep event immediately
        } else if ((oldTime > 0f && oldTime < 5.9f) && newTime >= 5.9f) { // look out for sleep event here too
            // show morning meal after a few seconds
        } else if ((oldTime > 6f && oldTime < 12f) && newTime >= 12f) {
            // show midday meal after a few seconds
        } else if ((oldTime > 12f && oldTime < 18f) && newTime >= 18f) {
            // show evening meal after a few seconds
        }
    } // potentially worth adding a queue for this so that we can have a way to wait until we're out of the last event to update time.

    public IEnumerator QueueTimeSkip(float timeAdvance) {
        while(SceneController.gameState != GameState.ON_TRAIL) {
            yield return new WaitForSeconds(0.1f); // will create a weird delay, watch out for that
        }

        TimeSkip(timeAdvance);
    }

}
