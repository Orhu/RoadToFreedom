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

    [SerializeField] GameObject _gameUI;

    void Start() {
        time = "Morning";
        weather = "Coming Soon";
        _trailUI = GetComponent<TrailUI>();
    }

    void Update() {
        if (readyToShow) {
            ShowEvent();
        }
        
    }
    
    public void StartTrail() {
        _charStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        
        SetupTrail(5,1,"alpha",10); // placeholder
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
        yield return new WaitForSeconds(7f);
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
            // town time or for now end game
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
}
