using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int health{get;private set;} = GameObject.GetComponent.CharacterSheet<vitality>;
    public int food{get; private set;} = 0;
    public int medicine{get; private set;} = 0;
    public float money{get; private set;} = 0f;
    public int challengeScore{get; private set;} = 0;

    public List<string> status = new List<string>();  
    public IDictionary<string, string> statusDescription = new IDictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
