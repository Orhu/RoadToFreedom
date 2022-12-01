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

    // Text GameObjects
    // [SerializeField] TMP_Text shopButtonText;

    void Start(){
        _event = GameObject.Find("eventUI").GetComponent<eventHandler>();
    }

    // Handled opening the shop event
    public static void openShop(int shopID){
        World.LoadEvent(shopID);
    }


}
