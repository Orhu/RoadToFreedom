using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class EventCollection {
    public Event[] events;
}

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

    //Text GameObjects
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text option1;
    [SerializeField] TMP_Text option2;
    [SerializeField] TMP_Text option3;
    [SerializeField] TMP_Text option4;

    void Start(){
        allEvents = JsonUtility.FromJson<EventCollection>(eventJson.text);

        Event e = getEvent(1);
        title.text = e.eventTitle;
        text.text = e.eventDesc;
        option1.text = e.eventChoiceTexts[0];
        option2.text = e.eventChoiceTexts[1];
        option3.text = "";
        option4.text = "";
    }
    
    public Event getEvent(int id){
        foreach (Event e in allEvents.events){
            if (e.eventId == id){
                return e;
            }
        }
        Debug.LogError("eventId not found");
        return null;
    }

    // public void showEvent(){
        
    // }

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
