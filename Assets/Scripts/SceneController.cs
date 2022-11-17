using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static bool characterBuilt {get; private set;} = false;
    
    void Start() {
        SceneManager.LoadScene("CharacterBuilder", LoadSceneMode.Additive);
        StartCoroutine(AwaitCharacter());
    }

    public static void CharacterBuilderComplete() {
        characterBuilt = true;
    }

    private IEnumerator AwaitCharacter() {
        while (!characterBuilt) {
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("Character Building Complete!");

        //SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
        //SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
    }
}
