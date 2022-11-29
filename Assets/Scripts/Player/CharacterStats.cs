using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public static int health{get;private set;}
    public static int food{get; private set;}
    public static int medicine{get; private set;}
    public static float money{get; private set;}
    public int challengeScore{get; private set;}

    public List<string> status = new List<string>();  
    public Dictionary<string, string> statusDescription = new Dictionary<string, string>();

    private static GameUI _gameUI;

    void Start() {
        food = 0;
        medicine = 0;
        money = 0f;
        challengeScore = 0;

        _gameUI = GetComponent<GameUI>();
    }

    public static void Setup() { //Sets Initial health to the player based on their vitality
        health = CharacterSheet.statVit * 2;
        if(health < 1){ //If vitality is 0, we assign them the minimum amount of health
            health = 1;
        }
        _gameUI.RefreshCounters(food, medicine, money, health);
    }

    public void InitialStatuses(List<string> st, Dictionary<string, string> stdesc) {
        status = st;
        statusDescription = stdesc;
    }

    public static void ChangeHealth(int boost) { //Changes health based on boost
        Debug.Log("changing health");
        health += boost;
        _gameUI.RefreshCounters(food, medicine, money, health);
    }

    public static void ChangeFood(int ration) { //Changes food stored based on ration
        Debug.Log("changing food");
        food += ration;
        _gameUI.RefreshCounters(food, medicine, money, health);
    }

    public static void ChangeMedicine(int meds) { //Changes medicine based on meds
        Debug.Log("changing meds");
        medicine += meds;
        _gameUI.RefreshCounters(food, medicine, money, health);
    }

    public static void ChangeMoney(float cash) { //Changes money based on cash 
        Debug.Log("changing munny");
        money += cash;
        _gameUI.RefreshCounters(food, medicine, money, health);
    }

    public void ChangeCS(int difficult) { //Changes challengeScore based on difficult
        challengeScore += difficult;
    }

    public void AddStatus(string st, string stdesc) { //Adds a status and it's description
        status.Add(st);
        statusDescription.Add(st, stdesc);
    }

    public void RemoveStatus(string st) { //Overloaded function which only takes the status name and removes it from the list and description
        status.Remove(st);
        statusDescription.Remove(st);
    }
}
