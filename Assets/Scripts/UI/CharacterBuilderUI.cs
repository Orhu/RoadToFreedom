using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterBuilderUI : MonoBehaviour {
    // Fields
    private string pName = "";
    private int pJob = 0;
    private string pPerk = "<i>Perk name here</i>";
    private int statBody = 1;
    private int statMind = 1;
    private int statSoul = 1;
    private string[] minorStatText = new string[] {"", "", "", "", "", "", "", "", ""}; // text for minor stat (assigned when selecting job)

    private int ptsRemaining = 4; // points remaining

    // Relevant Buttons
    [Header("UI Elements")]
    [SerializeField] Button[] plusButtons;
    [SerializeField] Button[] minusButtons;
    [SerializeField] Button doneButton;
    [Tooltip("Perk text game object")]
    [SerializeField] GameObject perkText;
    [Tooltip("Points remaining text object")]
    [SerializeField] TMP_Text ptsRemainingText;
    [Tooltip("Text objects for big stat numbers")]
    [SerializeField] TMP_Text[] majorStatTextBox;
    [Tooltip("Text objects for small stat boosts (+/- fields)")]
    [SerializeField] TMP_Text[] minorStatTextBox;

    void Start() {
        Refresh();
    }
    
    public void Refresh() { // Refresh UI elements
        bool doneButtonAvailable = false;
        perkText.GetComponent<TMP_Text>().text = pPerk; // update perk textbox

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

        // update minor scores
        for (int i = 0; i < minorStatTextBox.Length; i++) {
            minorStatTextBox[i].text = minorStatText[i];
        }

        // update done button
        doneButton.interactable = doneButtonAvailable && pName != "" && pJob != 0;
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
        string plus = "<color=#00FF00>+</color>";
        string minus = "<color=#FF0000>-</color>";
        if (pJob != newJob) {
            pJob = newJob;
            // perk update
            switch (pJob) { // stat updates
                case 0: // default
                    ChangeScore(1,0);
                    ChangeScore(1,1);
                    ChangeScore(1,2);
                    minorStatText = new string[] {"", "", "", "", "", "", "", "", ""};
                    break;
                case 1: // farmer
                    ChangeScore(4,0);
                    ChangeScore(1,1);
                    ChangeScore(2,2);
                    minorStatText = new string[] {plus, minus, "", minus, "", plus, plus, "", minus};
                    break;
                case 2: // butler
                    ChangeScore(1,0);
                    ChangeScore(3,1);
                    ChangeScore(3,2);
                    minorStatText = new string[] {minus, plus, "", plus, minus, "", minus, plus, ""};
                    break;
                case 3: // chef
                    ChangeScore(2,0);
                    ChangeScore(1,1);
                    ChangeScore(4,2);
                    minorStatText = new string[] {"", minus, plus, "", plus, minus, "", minus, plus};
                    break;                
            }
            Refresh();
        }
    }

    public void OnDone() {
        CharacterSheet.FillCharacterSheet(new string[] {pName, pPerk}, pJob, new int[] {statBody, statMind, statSoul});
        Destroy(this.gameObject);
    }
}