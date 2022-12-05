using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterBuilderUI : MonoBehaviour {
    // Fields
    private string pName = "";
    private int pJob = 0;
    private int statBody = 1;
    private int statMind = 1;
    private int statSoul = 1;
    private int[] minorStatScores = new int[9];
    private int[] scoreImbalances = new int[3];

    private int ptsRemaining = 4; // points remaining

    // Relevant Buttons
    [Header("UI Elements")]
    [SerializeField] Button[] plusButtons;
    [SerializeField] Button[] minusButtons;
    [SerializeField] Button doneButton;
    [Tooltip("Points remaining text object")]
    [SerializeField] TMP_Text ptsRemainingText;
    [Tooltip("Text objects for big stat numbers")]
    [SerializeField] TMP_Text[] majorStatTextBox;
    [Tooltip("Text objects for small stat boosts (+/- fields)")]
    [SerializeField] TMP_Text[] minorStatTextBox;
    [SerializeField] Button[] minorStatPlusButtons;
    [SerializeField] Button[] minorStatMinusButtons;
    [SerializeField] TMP_Text[] minorStatImbalanceText;

    void Start() {
        Refresh();
    }
    
    public void Refresh() { // Refresh UI elements
        bool doneButtonAvailable = false;

        switch (ptsRemaining) { // update remaining points text + available buttons
            case 0:
                ptsRemainingText.text = $"<color=#00FF00>Points Remaining: {ptsRemaining.ToString()}</color>";
                doneButtonAvailable = true;
                foreach (Button cButton in plusButtons) {
                    cButton.interactable = false;
                }
                foreach (Button cButton in minusButtons) {
                    cButton.interactable = true;
                }
                break;
            default:
                ptsRemainingText.text = $"<color=#FFFF00>Points Remaining: {ptsRemaining.ToString()}</color>";
                doneButtonAvailable = false;
                foreach (Button cButton in plusButtons) {
                    cButton.interactable = true;
                }
                foreach (Button cButton in minusButtons) {
                    cButton.interactable = true;
                }
                break;
        }

        for (int i = 0; i < 9; i++) { // update minor score buttons and text
            switch (minorStatScores[i]) {
                case -1:
                    minorStatTextBox[i].text = "<color=#FF0000>-</color>";
                    minorStatPlusButtons[i].interactable = true;
                    minorStatMinusButtons[i].interactable = false;
                    break;
                case 0:
                    minorStatTextBox[i].text = "";
                    minorStatPlusButtons[i].interactable = true;
                    minorStatMinusButtons[i].interactable = true;
                    break;
                case 1:
                    minorStatTextBox[i].text = "<color=#00FF00>+</color>";
                    minorStatPlusButtons[i].interactable = false;
                    minorStatMinusButtons[i].interactable = true;
                    break;
            }
        }

        for (int i = 0; i < 3; i++) { // update minor score imbalance text
            switch(scoreImbalances[i]) {
                case -1:
                    minorStatImbalanceText[i].text = $"Imbalance: <color=#FFFF00>{scoreImbalances[i]}</color>";
                    doneButtonAvailable = false;
                    break;
                case 0:
                    minorStatImbalanceText[i].text = $"Imbalance: <color=#00FF00>{scoreImbalances[i]}</color>";
                    break;
                case 1:
                    minorStatImbalanceText[i].text = $"Imbalance: <color=#FFFF00>{scoreImbalances[i]}</color>";
                    doneButtonAvailable = false;
                    break;
            }
        }

        // update big stats
        majorStatTextBox[0].text = statBody.ToString(); // body
        if (statBody >= 5) {
            plusButtons[0].interactable = false;
        } else if (statBody <= 1) {
            minusButtons[0].interactable = false;
        }

        majorStatTextBox[1].text = statMind.ToString(); // mind
        if (statMind >= 5) {
            plusButtons[1].interactable = false;
        } else if (statMind <= 1) {
            minusButtons[1].interactable = false;
        }

        majorStatTextBox[2].text = statSoul.ToString(); // soul
        if (statSoul >= 5) {
            plusButtons[2].interactable = false;
        } else if (statSoul <= 1) {
            minusButtons[2].interactable = false;
        }

        // update done button
        doneButton.interactable = doneButtonAvailable && pName != "";
    }

    public void OnDecrementScore(int abilityNum) { // abilityNums: 0 = body, 1 = mind, 2 = soul
        ptsRemaining++; // update number of points remaining
        switch (abilityNum) {
            case 0:
                statBody--;
                break;
            case 1:
                statMind--;
                break;
            case 2:
                statSoul--;
                break;
            default:
                Debug.LogWarning("Stat change request has invalid ability number.");
                return;
        }
        Refresh();
    }

    public void OnIncrementScore(int abilityNum) { // abilityNums: 0 = body, 1 = mind, 2 = soul
        ptsRemaining--; // update number of points remaining
        switch (abilityNum) {
            case 0:
                statBody++;
                break;
            case 1:
                statMind++;
                break;
            case 2:
                statSoul++;
                break;
            default:
                Debug.LogWarning("Stat change request has invalid ability number.");
                return;
        }
        Refresh();
    }

    public void ChangeScore(int newVal, int abilityNum) { // abilityNums: 0 = body, 1 = mind, 2 = soul
        switch (abilityNum) {
            case 0:
                ptsRemaining += statBody;
                statBody = newVal;
                ptsRemaining -= statBody;
                break;
            case 1:
                ptsRemaining += statMind;
                statMind = newVal;
                ptsRemaining -= statMind;
                break;
            case 2:
                ptsRemaining += statSoul;
                statSoul = newVal;
                ptsRemaining -= statSoul;
                break;
            default:
                Debug.LogWarning("Stat change request has invalid ability number.");
                return;
        }
        Refresh();
    }

    public void OnSetName(string newName) {
        pName = newName;
        Refresh();
    }

    public void OnSetNewJob(int newJob) {
        if (pJob != newJob) {
            pJob = newJob;
            switch (pJob) { // stat updates
                case 0: // default
                    ChangeScore(1,0);
                    ChangeScore(1,1);
                    ChangeScore(1,2);
                    minorStatScores = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
                    RefreshScoreImbalance();
                    break;
                case 1: // farmer
                    ChangeScore(4,0);
                    ChangeScore(1,1);
                    ChangeScore(2,2);
                    minorStatScores = new int[] {1, -1, 0, -1, 0, 1, 1, 0, -1};
                    RefreshScoreImbalance();
                    break;
                case 2: // butler
                    ChangeScore(1,0);
                    ChangeScore(3,1);
                    ChangeScore(3,2);
                    minorStatScores = new int[] {-1, 1, 0, 1, -1, 0, -1, 1, 0};
                    RefreshScoreImbalance();
                    break;
                case 3: // chef
                    ChangeScore(2,0);
                    ChangeScore(1,1);
                    ChangeScore(4,2);
                    minorStatScores = new int[] {0, -1, 1, 0, 1, -1, 0, -1, 1};
                    RefreshScoreImbalance();
                    break;   
                case 4: // custom
                    ChangeScore(1,0);
                    ChangeScore(1,1);
                    ChangeScore(1,2);
                    minorStatScores = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
                    RefreshScoreImbalance();
                    break;             
            }
            Refresh();
        }
    }

    public void OnIncrementMinor(int skillNum) {
        minorStatScores[skillNum] += 1;
        RefreshScoreImbalance();
        Refresh();
    }

    public void OnDecrementMinor(int skillNum) {
        minorStatScores[skillNum] -= 1;
        RefreshScoreImbalance();
        Refresh();
    }

    private void RefreshScoreImbalance() {
        scoreImbalances[0] = minorStatScores[0] + minorStatScores[1] + minorStatScores[2];
        scoreImbalances[1] = minorStatScores[3] + minorStatScores[4] + minorStatScores[5];
        scoreImbalances[2] = minorStatScores[6] + minorStatScores[7] + minorStatScores[8];
    }

    public void OnDone() {
        CharacterSheet.FillCharacterSheet(pName, pJob, new int[] {statBody, statMind, statSoul}, minorStatScores);
        Destroy(this.gameObject);
    }
}