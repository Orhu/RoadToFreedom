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

    // Text GameObjects
    // [SerializeField] TMP_Text shopButtonText;

    void Start(){
        //_event = GameObject.Find("eventUI").GetComponent<eventHandler>();
    }

    // Handled opening the shop event
    public static void openShop(int shopID){
        World.LoadEvent(shopID); 
    }

    // Closes out town and continues journey
    public static void continueJourney(){
        SceneController.LoadTrail();
    }

    // Prevent infinite money gen from friend
    public static void plantationFriend(){
        if (friend == true){
            openShop(102);
            friend = false;
        }
    }


}
