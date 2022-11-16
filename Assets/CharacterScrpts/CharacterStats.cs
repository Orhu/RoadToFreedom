using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int health{get;private set;}
    public int food{get; private set;}
    public int medicine{get; private set;}
    public float money{get; private set;}
    public int challengeScore{get; private set;}

    public List<string> status = new List<string>();  
    public IDictionary<string, string> statusDescription = new IDictionary<string, string>();

    void Start()
    {
        food = 0;
        medicine = 0;
        money = 0f;
        challengeScore = 0;
    }

    public void IntialHealth(int vit) //Sets Initial health to the player based on their vitality
    {
        health = vit * 2;
        if(health < 1){ //If vitality is 0, we assign them the minimum amount of health
            health = 1;
        }
    }

    public void InitialStatuses(List<string> st, IDictionary<string, string> stdesc)
    {
        status = st;
        statusDescription = stdesc;
    }

    public void ChangeHealth(int boost){ //Changes health based on boost
        health += boost
    }

    public void ChangeFood(int ration ){ //Changes food stored based on ration
        food += ration; 
    }

    public void ChangeMedicine(int meds){ //Changes medicine based on meds
        medicine += meds;
    }

    public void ChangeMoney(float cash){ //Changes money based on cash 
        money += cash;
    }

    public void ChangeCS(int difficult){ //Changes challengeScore based on difficult
        challengeScore += difficult;
    }

    public void ChangeStatuses(string st, string stdesc){ //Adds a status and it's description
        status.Add(st);
        statusDescription.Add(st, stdesc);
    }

    public void ChangeStatuses(string st) { //Overloaded function which only takes the status name and removes it from the list and description
        status.Remove(st);
        statusDescription.Remove(st)
    }
}
