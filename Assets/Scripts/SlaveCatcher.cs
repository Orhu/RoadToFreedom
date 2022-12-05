using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaveCatcher : MonoBehaviour {
    public static bool active {get; private set;} = false;

    public static int latestLocation = -1;

    public static SlaveCatcherState scState = SlaveCatcherState.INACTIVE;

    private static float currentStallTime = 0f; // time delay in town
    private static float[] townStallTimes = new float[]{48f, 48f, 48f, 48f};

    private static float scSpeed = 4f;
    private static float scProgress = 0f;
    private static float scCurrentTrailLength = 0f;
    private static float[] scTrailLengths = new float[]{45f, 60f, 90f, 90f, 120f, 15f};
    private static int scTrailNum = 0;

    //public static float getCaughtChance = 0f;

    public static void Activate() {
        Debug.Log("activating slave catchers");
        active = true;
        if (latestLocation == -1) {
            ArriveInTown();
        }
    }

    public static void Deactivate() {
        Debug.Log("deactivating slave catchers");
        active = false;
    }

    public static void TickTime() {
        // tick time forward one time
        if (active) {
            if (scState == SlaveCatcherState.ON_TRAIL) {
                if (World.time >= 6.0f && World.time < 22.0f) {
                    scProgress = Mathf.Round((scProgress + (0.1f * scSpeed))*10f)/10f;
                    Debug.Log($"slave catcher progress on trail {scTrailNum} = {scProgress}/{scCurrentTrailLength}");
                }

                if (scTrailNum == Trail.trailNum) {
                    if (scProgress >= Trail.progress)
                        CatchPlayer();
                }

                if (scCurrentTrailLength <= scProgress) {
                    scTrailNum++;
                    ArriveInTown();
                }
            }

            if (scState == SlaveCatcherState.INVESTIGATING_TOWN || scState == SlaveCatcherState.FINDING_PLAYER) {
                if (currentStallTime <= 0f) {
                    if (Trail.trailNum != scTrailNum || SceneController.gameState != GameState.IN_TOWN) { // if player is not in same town
                        EmbarkToTrail();
                    } else {
                        ChangeSCState(SlaveCatcherState.FINDING_PLAYER);
                        CatchPlayer();
                    }
                } else{
                    currentStallTime = Mathf.Round((currentStallTime - 0.1f)*10f)/10f;
                    Debug.Log($"slave catcher stall time in town {latestLocation} = {currentStallTime}");
                }
            }
        }
    }

    public static void AdvanceTime(float timeAdvance) { // maybe this works. feels like it won't!
        // for skipping time forward
        if (active) {
            if (scState == SlaveCatcherState.FINDING_PLAYER) {
                CatchPlayer();
                //SearchForPlayer();
            } else {
                if (scState == SlaveCatcherState.ON_TRAIL) {
                    // ((time*10f) % 240f)/10f;
                    float resultTime = World.time + timeAdvance;
                    if (resultTime > 22f && resultTime - 24f <= 6f) { // if time < 22f but resultant time is between 22f and 6f the next day.
                        timeAdvance = resultTime - 22f;
                    } else if (World.time < 6f) { // if time < 6f
                        timeAdvance = timeAdvance - (6f - World.time);
                    } else if ((World.time > 22f && resultTime < 30f) || (World.time < 6f && resultTime < 6f)) { // if all of the advance happens during down time
                        timeAdvance = 0f;
                    } else if ((World.time > 22f && resultTime > 30f)) {
                        timeAdvance = resultTime - 30f;
                    }
                    scProgress = Mathf.Round((scProgress + (scSpeed * timeAdvance))*10f)/10f;
                    Debug.Log($"slave catcher progress on trail {scTrailNum} = {scProgress}/{scCurrentTrailLength}");
                } else if (scState == SlaveCatcherState.INVESTIGATING_TOWN) {
                    currentStallTime -= timeAdvance;
                    Debug.Log($"slave catcher stall time in town {latestLocation} = {currentStallTime}");
                    if(currentStallTime <= 0f) {
                        scState = SlaveCatcherState.FINDING_PLAYER;
                    }
                }
            }
        }
    }

    public static void EmbarkToTrail() {
        Debug.Log($"Embarking on trail {scTrailNum}");
        // embark to trail from town
        scCurrentTrailLength = scTrailLengths[scTrailNum];
        currentStallTime = 0f;
        ChangeSCState(SlaveCatcherState.ON_TRAIL);
    }

    public static void ArriveInTown() {
        // arriving in town from trail
        latestLocation++; // arrive in next town
        Debug.Log($"Arriving in town {latestLocation}");
        currentStallTime = townStallTimes[scTrailNum];
        scProgress = 0f;
        ChangeSCState(SlaveCatcherState.INVESTIGATING_TOWN);
    }

    public static void CatchPlayer() {
        // trigger slave catcher event depending on the game state
        SceneController.GameOver(false, "You were caught by slave catchers.");
    }

    public static void ResetSlaveCatchers(int resetType) { // 0 = trail, 1 = full (for game overs)
        switch (resetType) {
            case 0:
                scProgress = 0f;
                break;
            case 1: 
                scProgress = 0f;
                latestLocation = -1;
                scTrailNum = 0;
                ArriveInTown();
                break;
        }
    }

    public static void ChangeSCState(SlaveCatcherState newSCState) {
        Debug.Log($"Changing SC State from {scState} to {newSCState}");
        scState = newSCState;
    }
}