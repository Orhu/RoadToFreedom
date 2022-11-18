using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {
    // trail specific info
    public int length {get; private set;} // number of fully random events to town
    public int danger {get; private set;} // number of guaranteed calamities
    public string biome {get; private set;}
    public int targetCS {get; private set;}

    // world info
    public string time {get; private set;}
    public string weather {get; private set;}
    
    public int progress = 0; // used to determine progress on path

    private int[] order;

    private bool readyToShow = false;

    private CharacterStats _charStats;
    private TrailUI _trailUI;

    void Start() {
        time = "Morning";
        weather = "Coming Soon";
        _charStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        _trailUI = GetComponent<TrailUI>();

        SetupTrail(5,1,"alpha",10); // placeholder

        order = GenerateEventOrder();
    }

    void Update() {
        if (readyToShow) {
            ShowEvent();
        }
        
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

    public void DoEvent() {
        StartCoroutine(LoadEvent());
    }

    private IEnumerator LoadEvent() {
        int eventID = GetRandomEvent();
        
        yield return new WaitForSeconds(7f);
        readyToShow = true;
    }

    private int GetRandomEvent() {
        List<int> validEvents = new List<int>(EventPools.GetEventPool($"biome:{biome}")); // biome
        List<int> newEvents = new List<int>(EventPools.GetEventPool($"time:{time}")); // time
        foreach (int eventID in validEvents) {
            if (!newEvents.Contains(eventID)) {
                validEvents.Remove(eventID);
            }
        }

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

        foreach (int eventID in validEvents) {
            if (!newEvents.Contains(eventID)) {
                validEvents.Remove(eventID);
            }
        }

        // select random event
        var rng = new System.Random();
        int pickVal = rng.Next(validEvents.Count + 1);
        return pickVal;
    }
    
    public void ShowEvent() {
        //todo
        ProgressTime();
        progress++;
        _trailUI.Refresh();
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
}
