using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [Tooltip("Text to be displayed in the tooltip box.")]
    [TextArea]
    [SerializeField] string toolTipText;

    private IEnumerator activeCr;
    private MouseTooltip _mouseTT;

    public void Start() {
        _mouseTT = transform.root.GetComponent<MouseTooltip>();
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
        activeCr = ToolTipCountdown();
        StartCoroutine(activeCr);
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (activeCr != null) {
            StopCoroutine(activeCr);
            activeCr = null;
        }
        _mouseTT.ToolTipDeactivate();
        
    }

    private IEnumerator ToolTipCountdown() {
        yield return new WaitForSeconds(1f);
        _mouseTT.ToolTipActivate(toolTipText);
        activeCr = null;
    }
}
