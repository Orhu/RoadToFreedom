using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicEventHandler : MonoBehaviour {
    public static List<int> masterPool {get; private set;}= new List<int>(); // master event pool
    public static int nextEvent {get; private set;} = -1;

    public static List<int> calledEvents {get; private set;}= new List<int>(); // list of all called events (not set)

    // Set Event Pools
    public static List<int> breakfastPool {get; private set;}= new List<int>(); // poolNum = 0
    public static List<int> lunchPool {get; private set;}= new List<int>(); // poolNum = 1
    public static List<int> dinnerPool {get; private set;}= new List<int>(); // poolNum = 2
    public static List<int> sleepPool {get; private set;}= new List<int>(); // poolNum = 3

    void Start() {
        //AddEventToSetPool(,0);
        //AddEventToSetPool(,1);
        //AddEventToSetPool(,2);
        //AddEventToSetPool(,3);
    }


    // Pool Management
    // Add
    public static void AddEventToPool(int toAdd) {
        masterPool.Add(toAdd);
    }
    public static void AddEventToPool(int toAdd, int numInstances) {
        for (int i = 0; i < numInstances; i++) {
            AddEventToPool(toAdd);
        }
    }
    public static void AddEventToPool(int[] toAdd) {
        foreach (var addID in toAdd) {
            AddEventToPool(addID);
        }
    }

    public static void AddEventToSetPool(int toAdd, int poolNum) {
        switch (poolNum) {
            case 0:
                breakfastPool.Add(toAdd);
                return;
            case 1:
                lunchPool.Add(toAdd);
                return;
            case 2:
                dinnerPool.Add(toAdd);
                return;
            case 3:
                sleepPool.Add(toAdd);
                return;
            default:
                Debug.LogWarning($"Attempted to add to non-existant set pool: {poolNum}");
                return;
        }
    }

    public static void SetNextEvent(int nextEventID) {
        nextEvent = nextEventID;
    }

    // Remove
    public static void RemoveEventFromPool(int toRemove) {
        masterPool.RemoveAll(id => id == toRemove);
    }
    public static void RemoveEventFromPool(int[] toRemove) {
        foreach (var remID in toRemove) {
            RemoveEventFromPool(remID);
        }
    }

    public static void RemoveEventFromSetPool(int toRemove, int poolNum) {
        switch (poolNum) {
            case 0: 
                breakfastPool.RemoveAll(id => id == toRemove);
                return;
            case 1:
                lunchPool.RemoveAll(id => id == toRemove);
                return;
            case 2:
                dinnerPool.RemoveAll(id => id == toRemove);
                return;
            case 3:
                sleepPool.RemoveAll(id => id == toRemove);
                return;
            default:
                Debug.LogWarning($"Attempted to remove from non-existant set pool: {poolNum}");
                return;
        }
    }

    // Pool Maintenance
    private static void UpdatePools() {
        // update pool based on resources, proximity to slave catchers, statuses, etc.
    }

    // Resets
    public static void ResetMasterPool() {
        masterPool.Clear();
    }

    public static void ResetSetPool(int poolNum) {
        switch (poolNum) {
            case 0:
                breakfastPool.Clear();
                return;
            case 1:
                lunchPool.Clear();
                return;
            case 2:
                dinnerPool.Clear();
                return;
            case 3:
                sleepPool.Clear();
                return;
            default:
                Debug.LogWarning($"Attempted to clear a non-existant set pool: {poolNum}");
                return;
        }
    }


    // Event Picking
    private static int PickEvent() {
        if (nextEvent != -1) {
            int temp = nextEvent;
            nextEvent = -1;
            return temp;
        }

        if (masterPool.Count == 0) {
            return 99;
        }

        // randomly pick an event id from masterPool
        var rng = new System.Random();
        int pickVal = rng.Next(masterPool.Count);
        RemoveEventFromPool(pickVal);
        return pickVal;
    }

    public static void LoadEvent() {
        int eventID = PickEvent();
        calledEvents.Add(eventID);

        switch (eventID) {
            default:
                Debug.LogWarning($"Attempted to dynamically load invalid eventID: {eventID}");
                return;
        }
    }

    public static void LoadEvent(int eventID) {
        switch (eventID) {
            default:
                Debug.LogWarning($"Attempted to dynamically load invalid set eventID: {eventID}");
                return;
        }
    }
}
