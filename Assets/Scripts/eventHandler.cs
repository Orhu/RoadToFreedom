using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event{
    int eventId;
    string eventTitle;
    string eventImage;
    string eventDesc;
    int eventNumChoices;
    string[] eventChoicesText;
    Choice[] eventChoices;
}

[System.Serializable]
public class Choice{
    Attribute choicePreCond;
    bool choiceShowResultScreen; // Should be set False by default
    string choicePassText;
    string choiceFailText;
    string[] choicePassResults;
    string[] choiceFailResults;
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
    int attrType;

    // Parameters
    // None - None (N)
    // Skill - Query (Q), Reveal (R), or Change (C)
    // Resource - Query (Q) or Change (C)
    // Item/Status - Query (Q), Add (A), or Remove (R)
    // Location - Change (C)
    // Game - Win (w) or Loss  (L)
    char attrParam;

    // Value A
    // skillNumber - 
    // resource - 0 (health), 1 (food), 2 (medicine), 3 (money)
    // item - itemId
    // status - statusId
    // location - locationId
    int attrValA;

    // Value B
    // skill / resources value changes
    int attrValB;
}


public class eventHandler : MonoBehaviour
{
    Event[] eventsForLeg;
    string eventPoolType;
    
    void processAttribute(string attrStr){
        // Ex; attrStr = L_C_O_-1
        // split string over '_'
        // switch statement to process type with nested (probably a shitty idea - Malcolm) switches for param which call
        // different methods in other scripts
    }

    
}
