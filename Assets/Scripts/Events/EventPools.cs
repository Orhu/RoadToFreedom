using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPools : MonoBehaviour {
    private static int[] poolAlpha = new int[]{1,2,3}; 
    private static int[] poolCalamity = new int[]{3}; 
    
    public static int[] GetEventPool(string poolID) {
        switch (poolID) {
            case "biome:alpha":
                return poolAlpha;
            case "calamity":
                return poolCalamity;
            default:
                return poolAlpha;
        }
    }
}
