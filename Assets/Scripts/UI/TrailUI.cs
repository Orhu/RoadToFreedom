using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrailUI : MonoBehaviour { 
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text progressText;

    void Update() {
        if (SceneController.gameState == GameState.ON_TRAIL) {
            timeText.text = $"Time: About {(int)World.time}:00";
            progressText.text = $"Progress: {Trail.progress}/{Trail.length}";
        }
    }
}
