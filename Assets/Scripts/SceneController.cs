using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static bool characterBuilt {get; private set;} = false;
    public static GameState gameState {get; private set;} = GameState.CHARACTER_BUILDER;

    public static GameState prevState;

    private static GameObject gameUIObject;
    private static GameObject resourcesUIObject;
    
    private static eventHandler _event;
    private static Trail _trail;
    private static GameOverUI _gameOver;
    private static CharacterBuilderUI _characterBuilder;

    public static int destinationNum = -1;
    
    void Awake() {
        resourcesUIObject = GameObject.Find("ResourceCounterUI");
        gameUIObject = GameObject.Find("GameUI");
        resourcesUIObject.SetActive(false);
        gameUIObject.SetActive(false);
        SceneManager.LoadScene("CharacterBuilder", LoadSceneMode.Additive);
        StartCoroutine(AwaitCharacter());
        SceneManager.LoadScene("event", LoadSceneMode.Additive);

        _trail = GameObject.Find("Trail Manager").GetComponent<Trail>();
        _gameOver = GameObject.Find("Game Over UI").GetComponent<GameOverUI>();
        _gameOver.gameObject.SetActive(false);
    }

    void Start() {
        _event = GameObject.Find("eventUI").GetComponent<eventHandler>();
        _characterBuilder = GameObject.Find("CharacterBuilderUI").GetComponent<CharacterBuilderUI>();
    }

    public static void CharacterBuilderComplete() {
        characterBuilt = true;
        gameUIObject.SetActive(true);
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
    }

    public static void LoadTown(int townNum) {
        UpdateGameState(GameState.IN_TOWN);
        resourcesUIObject.SetActive(true);
        switch (townNum) {
            case 0:
                SceneManager.LoadScene("Plantation", LoadSceneMode.Additive);
                return;
            case 1:
                SceneManager.LoadScene("Town1", LoadSceneMode.Additive);
                return;
            case 2:
                SceneManager.LoadScene("Town2", LoadSceneMode.Additive);
                return;
            case 3:
                SceneManager.LoadScene("Town3", LoadSceneMode.Additive);
                return;
            case 4:
                GameOver(true, "You have reached Canada! Congratulations!");
                return;
            default:
                Debug.LogError($"Attempted to load invalid town: {townNum}");
                return;
        }
    }

    public static void SetDestination(int destNum) {
        destinationNum = destNum;
    }

    public static void LoadTrail() {
        GameObject townObj = GameObject.Find("TownUI");
        if (townObj != null)
            SceneManager.UnloadSceneAsync(Trail.trailNum+3);
        UpdateGameState(GameState.ON_TRAIL);
        _trail.StartTrail();

        gameUIObject.SetActive(true);
    }

    public static void UpdateGameState(GameState newGameState) {
        if (gameState == GameState.GAME_OVER) {
            return;
        }
        if ((gameState == GameState.IN_TOWN || gameState == GameState.ON_TRAIL) && (newGameState != GameState.IN_TOWN || newGameState != GameState.ON_TRAIL)) 
            prevState = gameState;
        gameState = newGameState;
        Debug.Log($"Updating Game State to {gameState}");
    }

    public static void RevertGameState() {
        if (gameState == GameState.GAME_OVER) {
            return;
        }
        if (prevState == GameState.IN_TOWN || prevState == GameState.ON_TRAIL) {
            gameState = prevState;
            Debug.Log($"Updating Game State to {gameState}");
        } else {
            Debug.LogWarning($"Couldnt revert game state due to invalid previous state. {prevState}");
        }
    }

    public static void GameOver(bool isWin, string message) {
        // TO DO
        // show game over screen
        if (prevState == GameState.IN_TOWN) {
            GameObject townObj = GameObject.Find("TownUI");
            if (townObj != null)
                SceneManager.UnloadSceneAsync(Trail.trailNum+3);
        }
        UpdateGameState(GameState.GAME_OVER);

        resourcesUIObject.SetActive(false);
        gameUIObject.SetActive(false);
        EndEvent();

        _gameOver.gameObject.SetActive(true);
        _gameOver.FillScreen(isWin, message);
        SlaveCatcher.ChangeSCState(SlaveCatcherState.INACTIVE);
        town.friend = true;
        town.butcher = true;
        town.passA = true;
        town.lumber = 3;

        Debug.Log($"GAME OVER! WIN = {isWin}, {message}");
    }

    public void RebuildCharacter() {
        characterBuilt = false;
        gameState = GameState.CHARACTER_BUILDER;
        _characterBuilder.ReopenCharacterBuilder();
        StartCoroutine(AwaitCharacter());
    }

    public static void ProcessCloseAction(int closeAction) {
        switch (closeAction) {
            case 0: // general town entry
                LoadTown(Trail.trailNum);
                UpdateGameState(GameState.IN_TOWN);
                return;
            case 1: // transition to marathon/detroit
                LoadTrail();
                return;
            case 2:
                World.LoadEvent(96);
                return;
            case 3: // juego over
                break;
        }
    }
    public static void ProcessCloseAction(int closeAction, int valA, string operation) {
        Debug.Log("process close action");
        if (valA == 1) {
            GameOver(true, operation);
        } else if (valA == 0) {
            GameOver(false, operation);
        }
    }

    public static void QueueFakeCloseAction(int closeAction) {
        _event.SetCloseAction(closeAction);
    }
    public static void QueueFakeCloseAction(int closeAction, int caV1, string caStr) {
        _event.SetCloseAction(closeAction, caV1, caStr);
    }

    public static void ForceGameSet(GameState newStet) {
        gameState = newStet;
    }
}
