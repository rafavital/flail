using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomUnityEvents;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
#region SINGLETON
    public static GameManager Instance;
    private void Awake() {
        if (Instance != this) Destroy(Instance);
        if (Instance == null) Instance = this;
    }
#endregion
    public UnityEvent onEndGame;
    public enum GameStates {BEGINING, GAMEPLAY, END};
    private GameStates _gameState;
    public GameStates GameState {get => _gameState; private set => _gameState = value;}
    private void Start() {
        GameState = GameStates.BEGINING;
    }
    private void Update() {
        //TODO REMOVE THIS
        if (GameState == GameStates.BEGINING && Input.GetKeyDown(KeyCode.Space)) StartGame ();
    }
    public void StartGame () {
        GameState = GameStates.GAMEPLAY;
    }
    public void EndGame () {
        GameState = GameStates.END;
        onEndGame.Invoke();
    }
    public void RestartGame () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }
}
