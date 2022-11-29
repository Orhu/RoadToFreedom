using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {
    public float length {get; private set;} // trail length
    public float progress {get; private set;} // player progress on trail

    public string[] traits {get; private set;} // trail traits

    private CharacterStats _charStats;
    private float moveSpeed;

    private int timeToNextEvent = 10;

    public void StartTrail() {
        _charStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        moveSpeed = 2.5f + (0.5f * GetBonus(CharacterSheet.statSpd));
        StartCoroutine(TrailUpdate());
    }

    private int GetBonus(int statScore) {
        return ((statScore + 1)/2) - 1 + (statScore/6);
    }

    private IEnumerator TrailUpdate() {
        yield return new WaitForSeconds(1f);

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

        // go again
        StartCoroutine(TrailUpdate());
    }
}
