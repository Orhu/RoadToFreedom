using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour {

   // array shuffler
    public static void Shuffle(int[] arr) {
        var rng = new System.Random();
        
        int n = arr.Length;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            int value = arr[k];  
            arr[k] = arr[n];  
            arr[n] = value;  
        }  
    }

    public static int GetBonus(int statScore) {
        if (statScore > 6)
            statScore = 6;

        if (statScore < 0)
            statScore = 0;

        return ((statScore + 1)/2) - 1 + (statScore/6);
    }
}
