using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class town : MonoBehaviour
{
    private static eventHandler _event;

    public static bool friend = true;
    public static bool butcher = true;
    public static bool passA = true;

    // Text GameObjects
    // [SerializeField] TMP_Text shopButtonText;

    void Start(){
        //_event = GameObject.Find("eventUI").GetComponent<eventHandler>();
    }

    // Handled opening the shop event
    public static void openShop(int shopID){
        if (SlaveCatcher.scState != SlaveCatcherState.FINDING_PLAYER){
            World.LoadEvent(shopID); 
        }
    }

    // Closes out town and continues journey
    public static void continueJourney(){
        SceneController.LoadTrail();
    }
    public static void SetFinalDestination(int destNum) { // 0 = trail, 1 = city
        SceneController.SetDestination(destNum);
        SceneController.LoadTrail();
    }

    // Prevent infinite money gen from friend
    public static void plantationFriend(){
        if (friend == true){
            openShop(102);
            friend = false;
        }
        else{
            Debug.Log("Friend already visited.");
        }
    }

    // prevents from multiple attempts at asking for pass
    public static void passAttempt(){
        if (passA == true){
            openShop(100);
            passA = false;
        }
        else{
            Debug.Log("Already asked for pass.");
        }
    }

    // Butcher Shop
    public static void butcherShop(){
        if (SlaveCatcher.scState != SlaveCatcherState.FINDING_PLAYER){
            if (butcher == true){
                openShop(109);
                butcher = false;
            }
            else{
                openShop(1090);
            }
        }
        else{
            Debug.Log("THEY FOUND YOU");
        }
    }


}
