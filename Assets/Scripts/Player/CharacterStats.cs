using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public static int maxHealth{get; private set;}
    public static int health{get; private set;}
    public static int food{get; private set;}
    public static int medicine{get; private set;}
    public static int money{get; private set;}
    //public int challengeScore{get; private set;}

    public static float moveSpeed{get; private set;}

    public static int healthAug{get; private set;} = 0;

    public static Dictionary<int, Status> statusDictionary= new Dictionary<int, Status>();  

    private static GameUI _gameUI;

    void Start() {
        food = 0;
        medicine = 0;
        money = 0;
        //challengeScore = 0;

        _gameUI = GetComponent<GameUI>();
    }

    public static void Setup() { //Sets Initial health to the player based on their vitality and set resources to 0
        maxHealth = 2 + CharacterSheet.GetStat(6) - healthAug;
        health = maxHealth;
        moveSpeed = 2.5f + (0.5f * Utilities.GetBonus(CharacterSheet.GetStat(1)));
        if(health < 1){ //If vitality is 0, we assign them the minimum amount of health
            health = 1;
        }
        food = 0;
        medicine = 0;
        money = 0;
        _gameUI.RefreshCounters(food, medicine, money, health);
    }

    public static int GetResource(int resourceNum) { // 0 = health
        switch (resourceNum) {
            case 0:
                return health;
            case 1:
                return food;
            case 2:
                return medicine;
            case 3:
                return money;
            default:
                Debug.LogWarning($"Attempting to get unknown resource: {resourceNum}");
                return 0;
        }
    }

    public static void ChangeHealth(int boost) { //Changes health based on boost
        Debug.Log("changing health");
        health += boost;
        if (health > maxHealth) {
            health = maxHealth;
        }
        if (health <= 0) {
            SceneController.GameOver(false, "You have become too weak to continue on the road to freedom. You will are found by slave catchers and returned to your plantation.");
        }
        _gameUI.RefreshCounters(food, medicine, money, health);
    }

    public static void ChangeFood(int ration) { //Changes food stored based on ration
        Debug.Log("changing food");
        food += ration;
        if(food < 0) {
            food = 0;
        }
        _gameUI.RefreshCounters(food, medicine, money, health);
    }

    public static void ChangeMedicine(int meds) { //Changes medicine based on meds
        Debug.Log("changing meds");
        medicine += meds;
        if(medicine < 0) {
            medicine = 0;
        }
        _gameUI.RefreshCounters(food, medicine, money, health);
    }

    public static void ChangeMoney(int cash) { //Changes money based on cash 
        Debug.Log("changing munny");
        money += cash;
        if(money < 0) {
            money = 0;
        }
        _gameUI.RefreshCounters(food, medicine, money, health);
    }

    /* cut CS
    public void ChangeCS(int difficult) { //Changes challengeScore based on difficult
        challengeScore += difficult;
    }
    */

    public static void AddStatus(int statusID) { //Adds a status and it's description
        Debug.Log($"Adding status with id {statusID}");
        if (statusDictionary.ContainsKey(statusID)) {
            statusDictionary[statusID].ChangeLevel(1);
            CharacterSheet.SheetStatusRefresh();
            StatStatusRefresh();
        } else if (StatusHandler.GetStatusFromID(statusID) != null) {
            statusDictionary.Add(statusID, StatusHandler.GetStatusFromID(statusID));
            CharacterSheet.SheetStatusRefresh();
            StatStatusRefresh();
        }
    }

    public static void RemoveStatus(int statusID) { //Overloaded function which only takes the status name and removes it from the list and description
        Debug.Log($"Removing status with id {statusID}");
        if (statusDictionary.ContainsKey(statusID)) {
            if (statusDictionary[statusID].level <= 1) {
                statusDictionary.Remove(statusID);
            } else {
                statusDictionary[statusID].ChangeLevel(-1);
            }
            CharacterSheet.SheetStatusRefresh();
            StatStatusRefresh();
        }
    }

    public static void StatStatusRefresh() {
        // for each status in statusList, process updates to health
        maxHealth = 2 + CharacterSheet.GetStat(6) - healthAug;
        if (maxHealth < health) {
            health = maxHealth;
        }
        moveSpeed = 2.5f + (0.5f * Utilities.GetBonus(CharacterSheet.GetStat(1)));
        _gameUI.RefreshCounters(food, medicine, money, health);
    }

    public static void Restart(bool isHardReset) {
        // clear statuses
        statusDictionary.Clear();
        StatStatusRefresh();

        if (isHardReset) {
            GameObject.Find("Scene Controller").GetComponent<SceneController>().RebuildCharacter();
        } else {
            Setup();
            SceneController.LoadTown(Trail.trailNum);
        }
    }
}
