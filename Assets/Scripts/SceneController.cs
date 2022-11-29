using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static bool characterBuilt {get; private set;} = false;
    public static GameState gameState {get; private set;} = GameState.CHARACTER_BUILDER;

    [Tooltip("Game UI Root Object")]
    [SerializeField] GameObject gameUIObject;
    
    private static eventHandler _event;
    private static Trail _trail;
    
    void Awake() {
        gameUIObject.SetActive(false);
        SceneManager.LoadScene("CharacterBuilder", LoadSceneMode.Additive);
        StartCoroutine(AwaitCharacter());
        SceneManager.LoadScene("event", LoadSceneMode.Additive);

        _trail = GameObject.Find("Trail Manager").GetComponent<Trail>();
    }

    void Start() {
        _event = GameObject.Find("eventUI").GetComponent<eventHandler>();
    }

    public static void CharacterBuilderComplete() {
        characterBuilt = true;
        //_trail.StartTrail();
    }

    public static void StartEventLoad(int eventID) {
        _event.BuildEvent(eventID, 0);
    }

    public static void DisplayEvent() {
        _event.ShowEvent();
    }
    
    public static void EndEvent() {
        //_trail.EndEvent();
    }

    private IEnumerator AwaitCharacter() {
        while (!characterBuilt) {
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("Character Building Complete!");
        UpdateGameState(GameState.ON_TRAIL);
        
        gameUIObject.SetActive(true);
    }

    public static void UpdateGameState(GameState newGameState) {
        gameState = newGameState;
    }
}
