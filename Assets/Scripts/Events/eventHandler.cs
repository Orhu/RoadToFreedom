using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Holds collected json file for access
[System.Serializable]
public class EventCollection {
    public Event[] events;
}

// Hold key event information
[System.Serializable]
public class Event{
    public int eventId;
    public string eventTitle;
    public string eventImage;
    public string eventDesc;
    public int eventNumChoices;
    public string[] eventChoiceTexts;
    public Choice[] eventChoices;
}

// Holds choice information for choice outcomes
[System.Serializable]
public class Choice{
    public Attribute choicePreCond;
    public bool choiceShowResultScreen; // Should be set False by default
    public string choicePassText;
    public string choiceFailText;
    public string[] choicePassResults;
    public string[] choiceFailResults;
}

[System.Serializable]
public class Attribute{
    // Attriblute Types
    // 0 - None
    // 1 - Skill
    // 2 - Resource
    // 3 - Item
    // 4 - Status
    // 5 - Location
    // 6 - Game
    public int attrType;

    // Parameters
    // None - None (N)
    // Skill - Query (Q), Reveal (R), or Change (C)
    // Resource - Query (Q) or Change (C)
    // Item/Status - Query (Q), Add (A), or Remove (R)
    // Location - Change (C)
    // Game - Win (w) or Loss  (L)
    public char attrParam;

    // Value A
    // skillNumber - 
    // resource - 0 (health), 1 (food), 2 (medicine), 3 (money)
    // item - itemId
    // status - statusId
    // location - locationId
    public int attrValA;

    // Value B
    // skill / resources value changes
    public int attrValB;
}


public class eventHandler : MonoBehaviour
{
    [SerializeField] TextAsset eventJson;
    EventCollection allEvents;
    List<Event> journeyLeg;
    int numPathEvents;
    private Event curEvent;
    private int pressCount = 0;

    //Text GameObjects
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text option1;
    [SerializeField] TMP_Text option2;
    [SerializeField] TMP_Text option3;
    [SerializeField] TMP_Text option4;

    private CharacterStats _charStats;
    private CharacterSheet _charSheet;

    void Start() {
        allEvents = JsonUtility.FromJson<EventCollection>(eventJson.text);

        _charStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        _charSheet = GameObject.Find("Player").GetComponent<CharacterSheet>();
        
        // Used to initialize event window for testing. May be altered late     
        gameObject.SetActive(false);
    }

    public void BuildEvent(int eventID) {
        Event e = getEvent(eventID);
        if (e.eventNumChoices == 1){
            pressCount++;
            curEvent = e;
            title.text = e.eventTitle;
            text.text = e.eventDesc;
            option1.text = e.eventChoiceTexts[0];
            option2.text = "";
            option3.text = "";
            option4.text = "";
        }
        else if (e.eventNumChoices == 2){
            curEvent = e;
            title.text = e.eventTitle;
            text.text = e.eventDesc;
            option1.text = e.eventChoiceTexts[0];
            option2.text = e.eventChoiceTexts[1];
            option3.text = "";
            option4.text = "";
        }
        else if (e.eventNumChoices == 3){
            curEvent = e;
            title.text = e.eventTitle;
            text.text = e.eventDesc;
            option1.text = e.eventChoiceTexts[0];
            option2.text = e.eventChoiceTexts[1];
            option3.text = e.eventChoiceTexts[2];
            option4.text = "";
        }
        else if (e.eventNumChoices == 4){
            curEvent = e;
            title.text = e.eventTitle;
            text.text = e.eventDesc;
            option1.text = e.eventChoiceTexts[0];
            option2.text = e.eventChoiceTexts[1];
            option3.text = e.eventChoiceTexts[2];
            option4.text = e.eventChoiceTexts[3];
        }
    }
    
    // Gets event information from event json based from eventId
    public Event getEvent(int id){
        Debug.Log($"searching for eventID {id}");
        foreach (Event e in allEvents.events){
            if (e.eventId == id){
                return e;
            }
        }
        Debug.LogError("eventId not found");
        return null;
    }

    public void optionButtonPress(int option){ // Option 0 to 3
        // Makes sure option info is not out of available bounds
        if (option < 0){
            Debug.LogError("Option not in valid range");
        }
        else{
            // ADD CODE HERE LATER TO MARK IF CHANGES ARE SUCCESS CONDITION OR FAILURE

            // Sets buttons to close options and none other
            text.text = curEvent.eventChoices[option].choicePassText;
            option1.text = "Close";
            option2.text = "";
            option3.text = "";
            option4.text = "";
        
            // Presscount being greater than 0 allows for button to become exit window command
            if(pressCount > 0){
                Debug.Log("Event concluded");
                pressCount = 0;
                // Replace with code for closing scene/window to handle consequence
                SceneController.EndEvent();
                gameObject.SetActive(false);
            }
            else{
                pressCount++;
            }
        }
    }

    // public void closeEvent(bool close){
    //     if (close == true){
    //         close = false;
    //         Debug.LogError("Event concluded");
    //         // Replace with code for closing scene/window to handle consequence
    //         transform.root.gameObject.SetActive(false);
    //     }
    // }

    public void ShowEvent(){
        gameObject.SetActive(true);
    }

    // TO BE DONE ONCE WE HAVE ACTUAL EVENTS IDENTIFIED
    // public void legGeneration(int numPathEvents){
        
    // }

    // void processAttribute(string attrStr){
    //     // Ex; attrStr = L_C_O_-1
    //     // split string over '_'
    //     // switch statement to process type with nested (probably a shitty idea - Malcolm) switches for param which call
    //     // different methods in other scripts
    // }
}