using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPools : MonoBehaviour {
    private static int[] poolEasy = new int[]{7, 8, 9, 10, 12, 13}; // trail 0
    private static int[] poolMedium = new int[]{7, 8, 9, 10, 11, 12, 13, 14, 15, 17, 18, 19, 20, 21, 25, 30}; // trail 1
    private static int[] poolHard = new int[]{7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30}; // trail 2/3
    private static int[] poolMarathon = new int[]{7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30}; // marathon
    private static int[] poolCity = new int[]{7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30}; // city
    
    public static int[] GetEventPool(string poolID) {
        switch (poolID) {
            case "easy":
                return poolEasy;
            case "medium":
                return poolMedium;
            case "hard":
                return poolHard;
            case "marathon":
                return poolMarathon;
            case "city":
                return poolCity;
            default:
                return poolEasy;
        }
    }
}
