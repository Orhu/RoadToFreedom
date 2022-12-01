using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SheetsHandler : MonoBehaviour
{

    [SerializeField] TMP_Text Cname;
    [SerializeField] TMP_Text Cstr;
    [SerializeField] TMP_Text Cspd;
    [SerializeField] TMP_Text Csrv;

    [SerializeField] TMP_Text Cknw;
    [SerializeField] TMP_Text Cmed;
    [SerializeField] TMP_Text Cspc;

    [SerializeField] TMP_Text Cvit;
    [SerializeField] TMP_Text Cact;
    [SerializeField] TMP_Text Clck;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Cname.text = $"Name: {CharacterSheet.characterName}";
        Cstr.text = $"Strength: {CharacterSheet.GetStat(0)}";
        Cspd.text = $"Speed: {CharacterSheet.GetStat(1)}";
        Csrv.text = $"Survival: {CharacterSheet.GetStat(2)}";
        Cknw.text = $"Knowledge: {CharacterSheet.GetStat(3)}";
        Cmed.text = $"Medicine: {CharacterSheet.GetStat(4)}";
        Cspc.text = $"Speech: {CharacterSheet.GetStat(5)}";
        Cvit.text = $"Vitality: {CharacterSheet.GetStat(6)}";
        Cact.text = $"Acting: {CharacterSheet.GetStat(7)}";
        Clck.text = $"Luck: {CharacterSheet.GetStat(8)}";
    }
}