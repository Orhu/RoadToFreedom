using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrailUI : MonoBehaviour { 
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text progressText;
    [SerializeField] RectTransform skyImageTransform;

    void Update() {
        if (SceneController.gameState == GameState.ON_TRAIL) {
            skyImageTransform.gameObject.SetActive(true);
            timeText.text = $"Time: About {(int)World.time}:00";
            progressText.text = $"Progress: {Trail.progress}/{Trail.length}";
            skyImageTransform.localRotation = Quaternion.Euler(0f,0f,Mathf.Lerp(360f, 0f, World.time/24f));
        } else {
            skyImageTransform.gameObject.SetActive(false);
        }
    }
}
