using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrailUI : MonoBehaviour { 
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text progressText;
    [SerializeField] RectTransform skyImageTransform;
    [SerializeField] GameObject stupidAssCharacterMenuButton;
    [SerializeField] RectTransform treesTransform;

    void Update() {
        if (SceneController.gameState == GameState.ON_TRAIL) {
            stupidAssCharacterMenuButton.SetActive(true);
            skyImageTransform.gameObject.SetActive(true);
            treesTransform.gameObject.SetActive(true);
            timeText.text = $"Time: About {(int)World.time}:00";
            progressText.text = $"Progress: {Trail.progress}/{Trail.length}";
            skyImageTransform.localRotation = Quaternion.Euler(0f,0f,Mathf.Lerp(360f, 0f, World.time/24f));
        } else if (SceneController.gameState == GameState.IN_TOWN) {
            timeText.text = $"Time: About {(int)World.time}:00";
            stupidAssCharacterMenuButton.SetActive(false);
        } else {
            skyImageTransform.gameObject.SetActive(false);
            treesTransform.gameObject.SetActive(false);
        }
    }
}
