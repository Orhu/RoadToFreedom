using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicEventHandler : MonoBehaviour {
    public static List<int> masterPool {get; private set;}= new List<int>(); // master event pool
    public static int nextEvent {get; private set;} = -1;

    // Set Event Pools
    public static List<int> breakfastPool {get; private set;}= new List<int>(); // poolID = 0
    public static List<int> lunchPool {get; private set;}= new List<int>(); // poolID = 1
    public static List<int> dinnerPool {get; private set;}= new List<int>(); // poolID = 2
    public static List<int> sleepPool {get; private set;}= new List<int>(); // poolID = 3

    void Start() {
        //AddEventToSetPool(,0);
        //AddEventToSetPool(,1);
        //AddEventToSetPool(,2);
        //AddEventToSetPool(,3);
    }


    // Pool Management
    // Add
    public static void AddEventToPool(int toAdd) {

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

    }

    public static void SetNextEvent(int nextEventID) {
        nextEvent = nextEventID;
    }

    // Remove
    public static void RemoveEventFromPool(int toRemove) {

    }
    public static void RemoveEventFromPool(int[] toRemove) {
        foreach (var remID in toRemove) {
            RemoveEventFromPool(remID);
        }
    }

    public static void RemoveEventFromSetPool(int toRemove, int poolNum) {

    }

    // Pool Maintenance
    private static void UpdatePools() {
        // update pool based on resources, proximity to slave catchers, statuses, etc.
    }

    // Resets
    public static void ResetMasterPool() {

    }

    public static void ResetSetPool(int poolID) {

    }


    // Event Picking
    private static int PickEvent() {
        if (nextEvent != -1) {
            int temp = nextEvent;
            nextEvent = -1;
            return temp;
        }

        // randomly pick an event id from masterPool
    }

    public static void LoadEvent() {
        int eventID = PickEvent();

        switch (eventID) {

        }
    }

    public static void LoadSetEvent(int baseID) {
        switch (baseID) {

        }
    }
}
