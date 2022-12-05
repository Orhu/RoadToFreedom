using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public GameObject Panel;
    public bool PanelActive;
    //public Text Dname;

    public void Open() {
        if(Panel != null)
        {
            PanelActive = Panel.activeSelf;
            Panel.SetActive(!PanelActive);

            if (PanelActive) {
                SceneController.UpdateGameState(GameState.IN_MENU);
            } else {
                SceneController.RevertGameState();
            }
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if(PanelActive)
    //     {
    //         Dname.Text = CharacterSheet.characterName; 
    //     }
    // }
}