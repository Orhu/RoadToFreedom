using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour {
    public string characterName {get; private set;}
    public string job {get; private set;}
    public string perk {get; private set;} // may change depending on how we do perks 

    // Character Stats
    // Body
    public int statBody {get; private set;} // body
    public int statStr {get; private set;} // strength (statNum = 0)
    public int statSpeed {get; private set;} // speed (statNum = 1)
    public int statSurv {get; private set;} // survival (statNum = 2)
    
    // Mind
    public int statMind {get; private set;} // mind
    public int statKnow {get; private set;} // knowledge (statNum = 3)
    public int statMed {get; private set;} // medicine (statNum = 4)
    public int statSpeech {get; private set;} // speech (statNum = 5)

    // Soul
    public int statSoul {get; private set;} // soul
    public int statVit {get; private set;} // vitality (statNum = 6)
    public int statAct {get; private set;} // acting (statNum = 7)
    public int statLuck {get; private set;} // luck (statNum = 8)

    public void fillCharacterSheet(string[] details, int[] statScores) {
        characterName = details[0];
        job = details[1];
        perk = details[2];

        statBody = statScores[0];
        statMind = statScores[1];
        statSoul = statScores[2];

        // fill substats on formula
    }

    public void changeStat(int statNum, int change) {
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
                statSpeed += change;
                if (statSpeed > 5) {
                    statSpeed = 5;
                } else if (statSpeed < 1) {
                    statSpeed = 1;
                }
                break;
            case 2:
                statSurv += change;
                if (statSurv > 5) {
                    statSurv = 5;
                } else if (statSurv < 1) {
                    statSurv = 1;
                }
                break;
            case 3:
                statKnow += change;
                if (statKnow > 5) {
                    statKnow = 5;
                } else if (statKnow < 1) {
                    statKnow = 1;
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
                statSpeech += change;
                if (statSpeech > 5) {
                    statSpeech = 5;
                } else if (statSpeech < 1) {
                    statSpeech = 1;
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
                statLuck += change;
                if (statLuck > 5) {
                    statLuck = 5;
                } else if (statLuck < 1) {
                    statLuck = 1;
                }
                break;
            default:
                Debug.LogWarning($"Attempting to change an invalid stat: {statNum}");
                break;
        }
    }
}
