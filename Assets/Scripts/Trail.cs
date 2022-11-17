using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {
    public int length {get; private set;} // number of fully random events to town
    public int danger {get; private set;} // number of guaranteed calamities
    public string biome {get; private set;}
    public int targetCS {get; private set;}
    
    private int progress = 0; // used to determine progress on path


    void Start() {
        
    }


    void Update() {
        
    }
}
