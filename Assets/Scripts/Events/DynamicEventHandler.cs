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
        int curFood = CharacterStats.GetResource(1);
        int curMed = CharacterStats.GetResource(2);
        int curMoney = CharacterStats.GetResource(3);
        int curTrailNum = Trail.trailNum;
        int curLuck = CharacterSheet.GetStat(8);
        float curTime = World.time;

        if (curFood <= 15) {
            if (!masterPool.Contains(31)) {
                AddEventToPool(31);
            }
            if (!masterPool.Contains(32)) {
                AddEventToPool(32);
            }
            if (!masterPool.Contains(40)) {
                AddEventToPool(40);
            }
        } else {
            RemoveEventFromPool(31);
            RemoveEventFromPool(32);
            RemoveEventFromPool(40);
        }

        if (calledEvents.Count >= 15) {
            if (!masterPool.Contains(25) && !calledEvents.Contains(25)) {
                AddEventToPool(25);
            }   
        } else {
            RemoveEventFromPool(25);
        }

        if (curTime >= 7f && curTime <= 16f) {
            if (!masterPool.Contains(18) && !calledEvents.Contains(18)) {
                AddEventToPool(18);
            }
            if (!masterPool.Contains(37) && !calledEvents.Contains(37)) {
                AddEventToPool(37);
            }
        } else {
            RemoveEventFromPool(18);
            RemoveEventFromPool(37);
        }

        if (curFood <= 1) {
            if (!calledEvents.Contains(12))
                AddEventToPool(12);
        } else {
            RemoveEventFromPool(12);
        }

        if (curMoney >= 25) {
            if (!calledEvents.Contains(11))
                AddEventToPool(11);
        } else {
            RemoveEventFromPool(11);
        }

        if (curMoney >= 300 && curTrailNum >= 2 && curLuck <= 3) {
            if (!calledEvents.Contains(28))
                AddEventToPool(28);
        } else {
            RemoveEventFromPool(28);
        }

        if (curMoney >= 500 && curMed <= 0) {
            if (!calledEvents.Contains(9))
                AddEventToPool(9);
        } else {
            RemoveEventFromPool(9);
        }   
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
    public static void LoadEvent() {
        int eventID = PickEvent();

        switch (eventID) {
            case 7:
                calledEvents.Add(eventID);
                AddEventToPool(10);
                break;
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
                calledEvents.Add(eventID);
                break;
            case 16:
                break;
            case 17:
                calledEvents.Add(eventID);
                AddEventToPool(15);
                AddEventToPool(38);
                break;
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
            case 23:
            case 24:
            case 25:
            case 26:
            case 27:
                calledEvents.Add(eventID);
                break;
            case 28:
                break;
            case 29:
            case 30:
                calledEvents.Add(eventID);
                break;
            case 31:
            case 32:
                break;
            case 33:
            case 34:
            case 35:
            case 36:
            case 37:
            case 38:
            case 39:
                calledEvents.Add(eventID);
                break;
            case 40:
                break;
            case 99:
                // RestoreEvent();
                break;
            default:
                Debug.LogWarning($"Attempted to dynamically load invalid eventID: {eventID}");
                return;
        }

        UpdatePools();
    }
    public static void LoadEvent(int eventID) {
        switch (eventID) {
            case 0: // sleep\
                World.LoadEvent(0);
                break;
            case 1: // Morning meal
            case 2:
                if (CharacterStats.GetResource(1) >= 1) {
                    World.LoadEvent(1);
                } else {
                    World.LoadEvent(2);
                }
                break;
            case 3:
            case 4:
                if (CharacterStats.GetResource(1) >= 1) {
                    World.LoadEvent(3);
                } else {
                    World.LoadEvent(4);
                }
                break;
            case 5:
            case 6:
                if (CharacterStats.GetResource(1) >= 1) {
                    World.LoadEvent(5);
                } else {
                    World.LoadEvent(6);
                }
                break;
            default:
                Debug.LogWarning($"Attempted to dynamically load invalid set eventID: {eventID}");
                break;
        }
        UpdatePools();
    }

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
        int pickVal = rng.Next(0,masterPool.Count);
        RemoveEventFromPool(masterPool[pickVal]);
        return masterPool[pickVal];
    }

}
