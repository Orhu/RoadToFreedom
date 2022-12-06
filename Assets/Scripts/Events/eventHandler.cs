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
    public int timeBeforeNextEvent;
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
    public string effectOperation;
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

    private World _world; 

    void Start() {
        allEvents = JsonUtility.FromJson<EventCollection>(eventJson.text);
        _world = GameObject.Find("Scene Controller").GetComponent<World>();
        
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
        Debug.LogWarning($"eventId {eventId} not found");
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
        Debug.LogWarning($"stageId {stageId} not found");
        return null;
    }

    // Processes effects from stage  -  MAY NEED TO ADD MORE LATER
    public void processEffect(Stage s){
        if (s.effectsNum > 0){
            for (int i = 0; i < s.effectsNum; i++){
                Debug.Log($"Processing Effect {i}, type {s.effects[i].effectType}");
                switch(s.effects[i].effectType){
                    case 0: // None
                        break;
                    case 1: // Skill
                        if(s.effects[i].effectOperation == "c"){ // Change
                            CharacterSheet.ChangeStat(s.effects[i].effectValA, s.effects[i].effectValB);
                        }
                        break;
                    case 2: // Resource
                        Debug.Log($"effect operation {i} {s.effects[i].effectOperation}");
                        if(s.effects[i].effectOperation == "c"){ // Change
                            switch(s.effects[i].effectValA){
                                case 0: // health
                                    Debug.Log($"Changing Health by {s.effects[i].effectValA}");
                                    CharacterStats.ChangeHealth(s.effects[i].effectValB);
                                    break;
                                case 1: // food
                                    Debug.Log($"Changing Food by {s.effects[i].effectValA}");
                                    CharacterStats.ChangeFood(s.effects[i].effectValB);
                                    break;
                                case 2: // medicine
                                    Debug.Log($"Changing Meds by {s.effects[i].effectValA}");
                                    CharacterStats.ChangeMedicine(s.effects[i].effectValB);
                                    break;
                                case 3: // money
                                    Debug.Log($"Changing Money by {s.effects[i].effectValA}");
                                    CharacterStats.ChangeMoney(s.effects[i].effectValB);
                                    break;
                            }
                        }
                        break;
                    case 3: // Item
                        // WILL WE BE USING ITEMS???? WILL ADD IF SO
                        // No, I don't think so - Malcolm
                        break;
                    case 4: // Status
                        if(s.effects[i].effectOperation == "a"){ // add skill here
                            CharacterStats.AddStatus(s.effects[i].effectValA);
                        }
                        else if(s.effects[i].effectOperation == "r"){ // remove skill
                            CharacterStats.RemoveStatus(s.effects[i].effectValA);
                        }
                        break;
                    case 5: // Location
                        // LOCATION HANDLING EFFECTS ADDED HERE
                        break;
                    case 6: // Game
                        // WIN LOSS EFFECTS ADDED HERE
                        break;
                    case 7: // time
                        float timeToSkip = s.effects[i].effectValA + s.effects[i].effectValB/10f;
                        World.TimeSkip(timeToSkip);
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
            if (curStage.choiceNum == 0) {
                Debug.Log("Event concluded");
                pressCount = 0;
                // Replace with code for closing scene/window to handle consequence
                SceneController.EndEvent();
                Trail.UpdateTimeToNextEvent(curStage.timeBeforeNextEvent);
                gameObject.SetActive(false);
                return;
            }

            Choice c = curStage.choices[option];
            if (c.hasCheck == true){
                bool pass = false;

            // HANDLE CHECKS HERE BY CHECKING RESOURCES AND
                switch(c.checkType){
                    case 1: // Skill
                        pass = CharacterSheet.GetStat(c.checkID) >= c.checkVal;
                        break;
                    case 2: // Resource
                        pass = CharacterStats.GetResource(c.checkID) >= c.checkVal;
                        break;
                    case 4: // Status
                        pass = CharacterStats.statusDictionary.ContainsKey(c.checkID);
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
        }
    }

    // SHOWS THE EVENT ON SCREEN
    public void ShowEvent(){
        gameObject.SetActive(true);
    }

    public void HideEvent() {
        gameObject.SetActive(false);
    }
}