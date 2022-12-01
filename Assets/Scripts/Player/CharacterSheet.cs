using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour {
    public static string characterName {get; private set;}
    public static int job {get; private set;}
    public static string dispJob {get; private set;}

    // Character Stats
    // Body
    public static int statBody {get; private set;} // body
    private static int statStr; // strength (statNum = 0)
    private static int statSpd; // speed (statNum = 1)
    private static int statSrv; // survival (statNum = 2)
    
    // Mind
    public static int statMind {get; private set;} // mind
    private static int statKnw; // knowledge (statNum = 3)
    private static int statMed; // medicine (statNum = 4)
    private static int statSpc; // speech (statNum = 5)

    // Soul
    public static int statSoul {get; private set;} // soul
    private static int statVit; // vitality (statNum = 6)
    private static int statAct; // acting (statNum = 7)
    private static int statLck; // luck (statNum = 8)

    private static int[] statAug = new int[9]; // status based stat augmentations

    public static void FillCharacterSheet(string pName, int jobID, int[] statScores) {
        characterName = pName;
        job = jobID;

        statBody = statScores[0];
        statMind = statScores[1];
        statSoul = statScores[2];

        // fill substats on formula
        switch (jobID) {
            case 1: // farmer
                SetStats(new int[]{1,-1,0,-1,0,1,1,0,-1});
                break;
            case 2: // butler
                SetStats(new int[]{-1,1,0,1,-1,0,-1,1,0});
                break;
            case 3: // chef
                SetStats(new int[]{0,-1,1,0,1,-1,0,-1,1});
                break;
            default:
                Debug.LogError("Invalid Job Type Selected.");
                break;
        }

        CharacterStats.Setup();
        SceneController.CharacterBuilderComplete();
        
    }

    private static void SetStats(int[] statVals) {
        statStr = statBody + statVals[0];
        statSpd = statBody + statVals[1];
        statSrv = statBody + statVals[2];
        statKnw = statMind + statVals[3];
        statMed = statMind + statVals[4];
        statSpc = statMind + statVals[5];
        statVit = statSoul + statVals[6];
        statAct = statSoul + statVals[7];
        statLck = statSoul + statVals[8];
    }

    public static int GetStat(int statNum) {
        int statScore = 0;
        switch (statNum) {
            case 0:
                statScore = statStr + statAug[statNum];
                break;
            case 1:
                statScore = statSpd + statAug[statNum];
                break;
            case 2:
                statScore = statSrv + statAug[statNum];
                break;
            case 3:
                statScore = statKnw + statAug[statNum];
                break;
            case 4:
                statScore = statMed + statAug[statNum];
                break;
            case 5:
                statScore = statSpc + statAug[statNum];
                break;
            case 6:
                statScore = statVit + statAug[statNum];
                break;
            case 7:
                statScore = statAct + statAug[statNum];
                break;
            case 8:
                statScore = statLck + statAug[statNum];
                break;
            default:
                Debug.LogWarning($"Attempting to get an invalid stat: {statNum}");
                return 0;
        }

        if (statScore > 6)
            return 6;
        if (statScore < 0)
            return 0;
        return statScore;
    }

    public static void ChangeStat(int statNum, int change) {
        switch (statNum) {
            case 0:
                statStr += change;
                if (statStr > 5) {
                    statStr = 5;
                } else if (statStr < 1) {
                    statStr = 1;
                }
                break;
            case 1:
                statSpd += change;
                if (statSpd > 5) {
                    statSpd = 5;
                } else if (statSpd < 1) {
                    statSpd = 1;
                }
                break;
            case 2:
                statSrv += change;
                if (statSrv > 5) {
                    statSrv = 5;
                } else if (statSrv < 1) {
                    statSrv = 1;
                }
                break;
            case 3:
                statKnw += change;
                if (statKnw > 5) {
                    statKnw = 5;
                } else if (statKnw < 1) {
                    statKnw = 1;
                }
                break;
            case 4:
                statMed += change;
                if (statMed > 5) {
                    statMed = 5;
                } else if (statMed < 1) {
                    statMed = 1;
                }
                break;
            case 5:
                statSpc += change;
                if (statSpc > 5) {
                    statSpc = 5;
                } else if (statSpc < 1) {
                    statSpc = 1;
                }
                break;
            case 6:
                statVit += change;
                if (statVit > 5) {
                    statVit = 5;
                } else if (statVit < 1) {
                    statVit = 1;
                }
                break;
            case 7:
                statAct += change;
                if (statAct > 5) {
                    statAct = 5;
                } else if (statAct < 1) {
                    statAct = 1;
                }
                break;
            case 8:
                statLck += change;
                if (statLck > 5) {
                    statLck = 5;
                } else if (statLck < 1) {
                    statLck = 1;
                }
                break;
            default:
                Debug.LogWarning($"Attempting to change an invalid stat: {statNum}");
                break;
        }
    }

    public static void SheetStatusRefresh() {
        statAug = new int[]{0,0,0,0,0,0,0,0,0}; // zero out the array before processing statuses
        foreach(var item in CharacterStats.statusDictionary) {
            int[] cEff = item.Value.effects; // current effects array
            for (int i = 0; i < 9; i++) {
                statAug[i] += cEff[i];
            }
        }
    }
}
