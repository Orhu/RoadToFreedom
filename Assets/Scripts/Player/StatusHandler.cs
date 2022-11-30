using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusCollection {
    public Status[] statuses;
}

[System.Serializable]
public class Status {
    public int id;
    public string name;
    public string description;
    public int[] effects; // 0-8 = skills, 9 = max health
    public int level;

    public Status(int idVal, string nameVal, string descriptionVal, int[] effectsVal) {
        id = idVal;
        name = nameVal;
        description = descriptionVal;
        effects = effectsVal;
        level = 1;
    }

    public void ChangeLevel(int amnt) {
        level += amnt;
    }
}

public class StatusHandler : MonoBehaviour {
    private static StatusCollection allStatuses;
    void Start() {
        var statusJson = Resources.Load<TextAsset>("statuses");
        allStatuses = JsonUtility.FromJson<StatusCollection>(statusJson.text);
    }
    
    public static Status GetStatusFromID(int searchID) {
        foreach (Status s in allStatuses.statuses){
            if (s.id == searchID){
                return s;
            }
        }
        Debug.LogWarning($"status id {searchID} not found");
        return null;
    }
}