using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour {
    [SerializeField] TMP_Text congratulationsText;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text gameOverMessage;
    [SerializeField] Button[] restartButtons;
    [SerializeField] TMP_Text winMessage;

    public void FillScreen(bool isWin, string message) {
        congratulationsText.gameObject.SetActive(isWin);
        gameOverText.gameObject.SetActive(!isWin);
        gameOverMessage.text = message;
        winMessage.gameObject.SetActive(isWin);
        restartButtons[0].gameObject.SetActive(!isWin);
        restartButtons[1].gameObject.SetActive(!isWin);
    }

    public void OnSoftRestart() { // return to first plantation
        gameObject.SetActive(false);
        SceneController.ForceGameSet(GameState.IN_TOWN);
        SlaveCatcher.Restart();
        DynamicEventHandler.ResetMasterPool();
        DynamicEventHandler.ResetCalledPool();
        Trail.Restart();
        CharacterStats.Restart(false);
    }

    public void OnHardRestart() { // rebuild character
        gameObject.SetActive(false);
        SlaveCatcher.Restart();
        DynamicEventHandler.ResetMasterPool();
        DynamicEventHandler.ResetCalledPool();
        Trail.Restart();
        CharacterStats.Restart(true);
    }
}
