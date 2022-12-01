using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static bool characterBuilt {get; private set;} = false;
    public static GameState gameState {get; private set;} = GameState.CHARACTER_BUILDER;

    private static GameState prevState;

    private static GameObject gameUIObject;
    private static GameObject resourcesUIObject;
    
    private static eventHandler _event;
    private static Trail _trail;
    
    void Awake() {
        resourcesUIObject = GameObject.Find("ResourceCounterUI");
        gameUIObject = GameObject.Find("GameUI");
        resourcesUIObject.SetActive(false);
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
        DisplayEvent();
    }

    public static void DisplayEvent() {
        _event.ShowEvent();
        UpdateGameState(GameState.IN_EVENT);
    }
    
    public static void EndEvent() {
        _event.HideEvent();
        RevertGameState();
    }

    private IEnumerator AwaitCharacter() {
        while (!characterBuilt) {
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("Character Building Complete!");

        LoadTown(0);
        resourcesUIObject.SetActive(true);
    }

    public static void LoadTown(int townNum) {
        UpdateGameState(GameState.IN_TOWN);
        switch (townNum) {
            default:
                SceneManager.LoadScene("townTemplate", LoadSceneMode.Additive);
                return;
        }
    }

    public static void LoadTrail() {
        UpdateGameState(GameState.ON_TRAIL);
        _trail.StartTrail();

        gameUIObject.SetActive(true);
    }

    public static void UpdateGameState(GameState newGameState) {
        if ((gameState == GameState.IN_TOWN || gameState == GameState.ON_TRAIL) && (newGameState != GameState.IN_TOWN || newGameState != GameState.ON_TRAIL)) 
            prevState = gameState;
        gameState = newGameState;
    }

    public static void RevertGameState() {
        if (prevState == GameState.IN_TOWN || prevState == GameState.ON_TRAIL)
            gameState = prevState;
    }
}
