using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour {
    [Header("On Screen resource counters")]
    [SerializeField] TMP_Text foodText;
    [SerializeField] TMP_Text medText;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text healthText;

    public void RefreshCounters(int foodCount, int medCount, float moneyCount, int healthCount) {
        foodText.text = $"<b>Food</b>  {foodCount}";
        medText.text = $"<b>Medicine</b>  {medCount}";
        moneyText.text = $"<b>Money</b>  {moneyCount}";
        healthText.text = $"<b>Health</b>  {healthCount}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
