using System;
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
    public int id;
    public int stageNum;
    public Stage[] stages;
}

// Holds event stage information
[System.Serializable]
public class Stage{
    public int stageId;
    public string title;
    public string image;
    public string text;
    public int effectsNum;
    public Effect[] effects;
    public int choiceNum;
    public Choice[] choices;
}

//     CHOICE AND EFFECT VALUE INDEX
//     
//     effectType and checkType
//     // 0 - None
//     // 1 - Skill
//     // 2 - Resource
//     // 3 - Item
//     // 4 - Status
//     // 5 - Location
//     // 6 - Game
//
//     effectOperation
//     // None - None (n)
//     // Skill - Query (q), Reveal (r), or Change (c)
//     // Resource - Query (q) or Change (c)
//     // Item/Status - Query (q), Add (a), or Remove (r)
//     // Location - Change (c)
//     // Game - Win (w) or Loss  (l)
//
//     effectValA
//     // skillNumber - 
//     // resource - 0 (health), 1 (food), 2 (medicine), 3 (money)
//     // item - itemId
//     // status - statusId
//     // location - locationId

//     effectValB and checkVal
//     // int for skill / resources value changes
// }

// Holds choice information for choice outcomes
[System.Serializable]
public class Choice{
    public string buttonText;
    public bool hasCheck;
    public int checkType;
    public int checkID;
    public int checkVal;
    public int successStage;
    public int failStage;
}

// Holds effect information applied during the stage
[System.Serializable]
public class Effect{
    public int effectType;
    public char effectOperation;
    public int effectValA;
    public int effectValB;
}


public class eventHandler : MonoBehaviour
{
    [SerializeField] TextAsset eventJson;
    EventCollection allEvents;
    List<Event> journeyLeg;
    int numPathEvents;
    private Event curEvent;
    private int curEventId;
    private Stage curStage;
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

    public void BuildEvent(int id, int stageId) {
        Event e = getEvent(id);
        curEvent = e;
        curEventId = id;
        Stage s = getStage(e, stageId);
        processEffect(s); // Processes effect of stage

        if (s.choiceNum == 0){
            pressCount++;
            curStage = s;
            title.text = s.title;
            text.text = s.text;
            option1.text = "Close";
            option2.text = "";
            option3.text = "";
            option4.text = "";
        }
        else if (s.choiceNum == 1){
            pressCount++;
            curStage = s;
            title.text = s.title;
            text.text = s.text;
            option1.text = s.choices[0].buttonText;
            option2.text = "";
            option3.text = "";
            option4.text = "";
        }
        else if (s.choiceNum == 2){
            curStage = s;
            title.text = s.title;
            text.text = s.text;
            option1.text = s.choices[0].buttonText;
            option2.text = s.choices[1].buttonText;
            option3.text = "";
            option4.text = "";
        }
        else if (s.choiceNum == 3){
            curStage = s;
            title.text = s.title;
            text.text = s.text;
            option1.text = s.choices[0].buttonText;
            option2.text = s.choices[1].buttonText;
            option3.text = s.choices[2].buttonText;
            option4.text = "";
        }
        else if (s.choiceNum == 4){
            curStage = s;
            title.text = s.title;
            text.text = s.text;
            option1.text = s.choices[0].buttonText;
            option2.text = s.choices[1].buttonText;
            option3.text = s.choices[2].buttonText;
            option4.text = s.choices[3].buttonText;
        }
    }
    
    // Gets event information from event json based from eventId
    public Event getEvent(int eventId){
        Debug.Log($"searching for eventID {eventId}");
        foreach (Event e in allEvents.events){
            if (e.id == eventId){
                return e;
            }
        }
        Debug.LogError("event's id not found");
        return null;
    }

    // Gets stage from an event
    public Stage getStage(Event e, int stageId){
        Debug.Log($"searching for stageId {stageId}");
        foreach (Stage s in e.stages){
            if (s.stageId == stageId){
                return s;
            }
        }
        Debug.LogError("stageId not found");
        return null;
    }

    // Processes effects from stage  -  MAY NEED TO ADD MORE LATER
    public void processEffect(Stage s){
        if (s.effectsNum > 0){
            for (int i = 0; i < s.effectsNum; i++){
                switch(s.effects[i].effectType){
                    case 0: // None
                        break;
                    case 1: // Skill
                        if(s.effects[i].effectOperation == 'c'){ // Change
<<<<<<< HEAD
                            // switch(s.effects[i].effectValA){
                            //     // ADD SKILL NUMBERS HERE
                            // }
=======
                            //switch(s.effects[i].effectValA){
                                // ADD SKILL NUMBERS HERE
                                
                            //}
>>>>>>> 7cc43c07d555214acdb102cd67394499874648b3
                        }
                        break;
                    case 2: // Resource
                        if(s.effects[i].effectOperation == 'c'){ // Change
                            switch(s.effects[i].effectValA){
                                case 0: // health
                                    int changeH = s.effects[i].effectValB;
                                    // ADD HEALTH VALUE CHANGE HERE
                                    break;
                                case 1: // food
                                    int changeF = s.effects[i].effectValB;
                                    // ADD FOOD VALUE CHANGE HERE
                                    break;
                                case 2: // medicine
                                    int changeMe = s.effects[i].effectValB;
                                    // ADD MEDICINE VALUE CHANGE HERE
                                    break;
                                case 3: // money
                                    int changeMo = s.effects[i].effectValB;
                                    // ADD MONEY VALUE CHANGE HERE
                                    
                                    break;
                            }
                        }
                        break;
                    case 3: // Item
                        // WILL WE BE USING ITEMS???? WILL ADD IF SO
                        break;
                    case 4: // Status
                        if(s.effects[i].effectOperation == 'a'){ // add skill here
                            // ADD ADDING SKILLS
                        }
                        else if(s.effects[i].effectOperation == 'r'){ // remove skill
                            // ADD REMOVING SKILLS
                        }
                        break;
                    case 5: // Location
                        // LOCATION HANDLING EFFECTS ADDED HERE
                        break;
                    case 6: // Game
                        // WIN LOSS EFFECTS ADDED HERE
                        break;
                }
            }
        }
    }

    public void optionButtonPress(int option){ // Option 0 to 3
        // Makes sure option info is not out of available bounds

        if (option < 0){
            Debug.LogError("Option not in valid range");
        }
        else{
            //processAttribute(option);
            // ADD CODE HERE LATER TO MARK IF CHANGES ARE SUCCESS CONDITION OR FAILURE

            // Sets buttons to close options and none other
            // text.text = curEvent.eventChoices[option].choicePassText;
            // option1.text = "Close";
            // option2.text = "";
            // option3.text = "";
            // option4.text = "";

            Choice c = curStage.choices[option];
            if (c.hasCheck == true){
                bool pass = false;

            // HANDLE CHECKS HERE BY CHECKING RESOURCES AND
                switch(c.checkType){
                    case 1: // Skill
                        switch(c.checkID){
                            case 0: // strength
                                if (CharacterSheet.statStr >= c.checkVal){pass = true;}
                                break;
                            case 1: // speed
                                if (CharacterSheet.statSpd >= c.checkVal){pass = true;}
                                break;
                            case 2: // survival
                                if (CharacterSheet.statSrv >= c.checkVal){pass = true;}
                                break;
                            case 3: // knowledge
                                if (CharacterSheet.statKnw >= c.checkVal){pass = true;}
                                break;
                            case 4: // medicine
                                if (CharacterSheet.statMed >= c.checkVal){pass = true;}
                                break;
                            case 5: // speech
                                if (CharacterSheet.statSpc >= c.checkVal){pass = true;}
                                break;
                            case 6: // vitality
                                if (CharacterSheet.statVit >= c.checkVal){pass = true;}
                                break;
                            case 7: // acting
                                if (CharacterSheet.statAct >= c.checkVal){pass = true;}
                                break;
                            case 8: // luck
                                if (CharacterSheet.statLck >= c.checkVal){pass = true;}
                                break;
                        }
                        break;
                    case 2: // Resource
                        switch(c.checkID){
                            case 0: // health
                                if (CharacterStats.health >= c.checkVal){pass = true;}
                                break;
                            case 1: // food
                                if (CharacterStats.food >= c.checkVal){pass = true;}
                                break;
                            case 2: // medicine
                                if (CharacterStats.medicine >= c.checkVal){pass = true;}
                                break;
                            case 3: // money
                                if (CharacterStats.money >= c.checkVal){pass = true;}
                                break;
                        }
                        break;
                    case 4: // Status
                        // HANDLE STATUS EFFECTS HERE
                        break;
                }

                if (pass == true){
                    BuildEvent(curEventId, c.successStage);
                }
                else{
                    BuildEvent(curEventId, c.failStage);
                }
            }
            else{
                BuildEvent(curEventId, c.successStage);
            }
        
            // Presscount being greater than 0 allows for button to become exit window command
            if(pressCount > 0){
                Debug.Log("Event concluded");
                pressCount = 0;
                // Replace with code for closing scene/window to handle consequence
                SceneController.EndEvent();
                gameObject.SetActive(false);
            }
            // else{
            //     pressCount++;
            // }
        }
    }

    // SHOWS THE EVENT ON SCREEN
    public void ShowEvent(){
        gameObject.SetActive(true);
    }


    // void processAttribute(int option){
    //     Debug.Log("processing attributes");
    //     // Ex; attrStr = L_C_O_-1
    //     // split string over '_'
    //     // switch statement to process type with nested (probably a shitty idea - Malcolm) switches for param which call
    //     // different methods in other scripts
    //     string attr = curEvent.eventChoices[option].choicePassResults;
    //     Debug.Log(attr);
    //     //foreach (string attr in attrAll) {   
    //         string[] attrVals = attr.Split("_");
    //         Debug.Log(attrVals[0]);
    //         string type = attrVals[0];
    //         string param = attrVals[1];
    //         string a = attrVals[2];
    //         string b = attrVals[3];

    //         if (type == "0"){ // None
    //             if (param != "n"){
    //                 Debug.LogError("How did we screw up None?");
    //             }
    //         }
    //         else if (type == "1"){ // Skill
    //             if (param == "q"){
    //                 // ADD CHECK SKILL
    //             }
    //             else if (param == "c"){
    //                 // ADD CHANGE SKILL
    //             }
    //             else if (param == "r"){
    //                 // ADD REVEAL SKILL
    //             }
    //             else{
    //                 Debug.LogError("Skill param not valid");
    //             }
    //         }
    //         else if (type == "2"){ // Resource
    //             if (param == "c"){ // change resource
    //                 if (a == "0"){ // health
    //                     int numVal = Int32.Parse(b);
    //                     _charStats.ChangeHealth(numVal);
    //                 }
    //                 else if (a == "1"){ // food
    //                     int numVal = Int32.Parse(b);
    //                     _charStats.ChangeFood(numVal);
    //                 }
    //                 else if (a == "2"){ // medicine
    //                     int numVal = Int32.Parse(b);
    //                     _charStats.ChangeMedicine(numVal);
    //                 }
    //                 else if (a == "3"){ // money
    //                     float numVal = float.Parse(b);
    //                     _charStats.ChangeMoney(numVal);
    //                 }
    //                 else{
    //                     Debug.LogError("resource key not valid");
    //                 }
    //             }
    //             else if (param == "q"){ // query resource

    //             }
    //             else{
    //                 Debug.LogError("Resource param not valid");
    //             }
    //         }
    //         else if (type == "3"){ // Item
    //             if (param == "q"){
    //                 // ADD INFO TO CHECK RESOURCE NUM
    //             }
    //             else if (param == "a"){
    //                 // ADD INFO TO ADD ITEMS
    //             }
    //             else if (param == "r"){
    //                 // ADD INFO TO REMOVE ITEMS
    //             }
    //             else{
    //                 Debug.LogError("Item param not valid");
    //             }
    //         }
    //         else if (type == "4"){ // Status
    //             if (param == "q"){
    //                 // ADD CHECK CURRENT STATUS
    //             }
    //             else if (param == "a"){
    //                 // ADD ADD STATUS
    //             }
    //             else if (param == "r"){
    //                 // ADD REMOVE STATUS
    //             }
    //             else{
    //                 Debug.LogError("Status param not valid");
    //             }
    //         }
    //         else if (type == "5"){ // Location
    //             if (param == "c"){
    //                 // ADD LOCATION CHANGE INFO HERE
    //             }
    //             else{
    //                 Debug.LogError("Location param not valid");
    //             }
    //         }
    //         else if (type == "6"){ // Game
    //             if (param == "w"){
    //                 // ADD WIN INFO HERE
    //             }
    //             else if (param == "l"){
    //                 // ADD LOSS INFO HERE
    //             }
    //             else{
    //                 Debug.LogError("Game param not valid");
    //             }
    //         }
    //         else{
    //             Debug.LogError("Attribute Type is not specified values");
    //         }
    //     //}
    // }
}