using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPools : MonoBehaviour {
    private static int[] poolAlpha = new int[]{1,2}; 
    
    public static int[] GetEventPool(string poolID) {
        switch (poolID) {
            case "biome:alpha":
                return poolAlpha;
            default:
                return poolAlpha;
        }
    }
}
