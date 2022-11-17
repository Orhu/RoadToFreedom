using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseTooltip : MonoBehaviour {
    [SerializeField] TMP_Text hintText;
    [SerializeField] RectTransform canvasRect;
    [SerializeField] RectTransform backgroundRect;
    [SerializeField] RectTransform parentRect;

    private bool active = false;

    void Start() {
        ToolTipDeactivate();
    }
    
    void Update() {
        if (active) {
            Vector2 anchoredPosition = Input.mousePosition / canvasRect.localScale.x;
            if (anchoredPosition.x + backgroundRect.rect.width > canvasRect.rect.width) {
                anchoredPosition.x = canvasRect.rect.width - backgroundRect.rect.width;
            }
            if (anchoredPosition.y + backgroundRect.rect.height > canvasRect.rect.height) {
                anchoredPosition.y = canvasRect.rect.height - backgroundRect.rect.height;
            }
            parentRect.anchoredPosition = anchoredPosition;
        }
    }

    public void ToolTipActivate(string newText) {
        active = true;
        hintText.text = newText;
        parentRect.gameObject.SetActive(true);
        
    }

    public void ToolTipDeactivate() {
        active = false;
        hintText.text = "";
        parentRect.gameObject.SetActive(false);
    }
}
