using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
    // void Start() {
    //     // show trigger warning, hide main title screen
    // }

    private void OnContinueToTitle() {
        //hide trigger warning, show title screen
    }
    
    public void OnStartGame() {
        Debug.Log("CLICKED BUTTON");
        SceneManager.LoadScene("Base Scene");
    }

    public void OnShowCredits() {

    }
}
