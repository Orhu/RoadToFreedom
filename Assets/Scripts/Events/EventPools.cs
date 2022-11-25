using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPools : MonoBehaviour {
    private static int[] poolAlpha = new int[]{0,1,2}; 
    private static int[] poolCalamity = new int[]{2}; 
    
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
