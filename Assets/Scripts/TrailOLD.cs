using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PROBABLY GOING TO NEED TO COMPLETELY REWORK THIS WHOLE SCRIPT

public class TrailOLD : MonoBehaviour {
    /*
    // trail specific info
    public float length {get; private set;} // measured in miles
    public int danger {get; private set;} // (reworking)
    public int trailID {get; private set;} // to determine the trail pool
    public string biome {get; private set;}
    public int targetCS {get; private set;} // (reworking)

    // world info
    public int time {get; private set;}
    public string weather {get; private set;}
    
    public float progress = 0f; // used to determine progress on path

    private int[] order;

    private bool readyToShow = false;

    private CharacterStats _charStats;
    private TrailUI _trailUI;

    [SerializeField] GameObject _gameUI;

    [SerializeField] GameObject endText; // temp for alpha

    void Start() {
        time = "Morning";
        weather = "Coming Soon";
        _trailUI = GetComponent<TrailUI>();
        endText.SetActive(false);
    }

    void Update() {
        if (readyToShow) {
            ShowEvent();
        }
        
    }
    
    public void StartTrail() {
        _charStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        
        //SetupTrail(5,1,"alpha",10); // placeholder
        StartCoroutine(LoadEvent());
    }

    public void SetupTrail(int tLength, int tDanger, string tBiome, int tTargetCS) {
        length = tLength;
        danger = tDanger;
        biome = tBiome;
        targetCS = tTargetCS;
        progress = 0;

        order = GenerateEventOrder();
        _trailUI.Refresh();
    }

    private int[] GenerateEventOrder() {
        int[] temp = new int[length+danger];
        for (int i = 0; i < length; i++) {
            temp[i] = 0;
        }
        for (int i = length; i < length + danger; i++) {
            temp[i] = 1;
        }

        Utilities.Shuffle(temp);

        if (temp[0] == 1) {
            for (int i = temp.Length-1; i >= 0; i--) {
                if (temp[i] == 0) {
                    temp[i] = 1;
                    break;
                }
            }
            temp[0] = 0;
        }

        return temp;
    }

    private IEnumerator LoadEvent() {
        int eventID = GetRandomEvent();
        SceneController.StartEventLoad(eventID);
        yield return new WaitForSeconds(1.5f);
        readyToShow = true;
    }

    private int GetRandomEvent() {
        List<int> validEvents = new List<int>(EventPools.GetEventPool($"biome:{biome}")); // biome
        List<int> newEvents = new List<int>(EventPools.GetEventPool($"time:{time}")); // time
        List<int> toRemove = new List<int>();
        foreach (int eventID in validEvents) {
            if (!newEvents.Contains(eventID)) {
                toRemove.Add(eventID);
            }
        }

        foreach (int eventID in toRemove) {
            validEvents.Remove(eventID);
        }

        toRemove.Clear();

        // weather (implement later)

        if (order[progress] == 1) {
            newEvents = new List<int>(EventPools.GetEventPool("calamity")); // calamity
        } else {
            if (_charStats.challengeScore <= targetCS) {
                newEvents = new List<int>(EventPools.GetEventPool("easyEvents")); // easy
            } else {
                newEvents = new List<int>(EventPools.GetEventPool("hardEvents")); // hard
            }
        }
        // rework needed

        foreach (int eventID in validEvents) {
            if (!newEvents.Contains(eventID)) {
                toRemove.Add(eventID);
            }
        }

        foreach (int eventID in toRemove) {
            validEvents.Remove(eventID);
        }

        toRemove.Clear();

        // select random event
        var rng = new System.Random();
        int pickVal = rng.Next(validEvents.Count);
        return validEvents[pickVal];
    }

    public void ShowEvent() {
        SceneController.DisplayEvent();
        _gameUI.SetActive(false);
    }
    
    public void EndEvent() {
        ProgressTime();
        progress++;
        _trailUI.Refresh();

        if (progress == order.Length) {
            EndAlpha();
            return;
        } else {
        _gameUI.SetActive(true);
        readyToShow = false;
        StartCoroutine(LoadEvent());
        }
    }

    public int GetPlayerCS() {
        return _charStats.challengeScore;
    }

    private void ProgressTime() {
        switch (time) {
            case "Morning":
                time = "Midday";
                break;
            case "Midday":
                time = "Afternoon";
                break;
            case "Afternoon":
                time = "Evening";
                break;
            case "Evening":
                time = "Night";
                break;
            case "Night":
                time = "Morning";
                break;
        }
    }

    private void EndAlpha() {
        readyToShow = false;
        endText.SetActive(true);
    }*/
}
