using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static bool characterBuilt {get; private set;} = false;

    [Tooltip("Game UI Root Object")]
    [SerializeField] GameObject gameUIObject;
    
    void Start() {
        gameUIObject.SetActive(false);
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
        
        gameUIObject.SetActive(true);
    }
}
