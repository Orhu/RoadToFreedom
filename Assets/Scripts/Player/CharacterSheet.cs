using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour {
    public string characterName {get; private set;}
    public int job {get; private set;}
    public string dispJob {get; private set;}
    /*not yet implemented */ public string perk {get; private set;} // may change depending on how we do perks 

    // Character Stats
    // Body
    public static int statBody {get; private set;} // body
    public static int statStr {get; private set;} // strength (statNum = 0)
    public static int statSpd {get; private set;} // speed (statNum = 1)
    public static int statSrv {get; private set;} // survival (statNum = 2)
    
    // Mind
    public static int statMind {get; private set;} // mind
    public static int statKnw {get; private set;} // knowledge (statNum = 3)
    public static int statMed {get; private set;} // medicine (statNum = 4)
    public static int statSpc {get; private set;} // speech (statNum = 5)

    // Soul
    public static int statSoul {get; private set;} // soul
    public static int statVit {get; private set;} // vitality (statNum = 6)
    public static int statAct {get; private set;} // acting (statNum = 7)
    public static int statLck {get; private set;} // luck (statNum = 8)

    public void FillCharacterSheet(string[] details, int jobID, int[] statScores) {
        characterName = details[0];
        job = jobID;
        //perk = details[1];

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

    private void SetStats(int[] statVals) {
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

    public static void changeStat(int statNum, int change) {
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
}
