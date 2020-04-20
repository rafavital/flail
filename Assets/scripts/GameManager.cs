using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomUnityEvents;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UnityEvent onEndGame;
    public enum GameStates {BEGINING, GAMEPLAY, END};
    public Fish currentFish;
    [SerializeField] private GameStates _gameState;
    public GameStates GameState {get => _gameState; private set => _gameState = value;}
    private void Awake() {
        if (Instance != this && Instance != null) Destroy(Instance.gameObject); 
        else if (Instance == null) Instance = this;

        DontDestroyOnLoad (gameObject);
    }
    private void Update() {
        //TODO REMOVE THIS
        // if (GameState == GameStates.BEGINING && Input.GetMouseButtonDown(0)) StartGame ();
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

    public void GetFish () {
        Fish[] fishes = GameObject.FindObjectsOfType<Fish> ();
        for (int i = 0; i < fishes.Length; i++)
        {
            var fish = fishes[i];
            if (fish.gameObject.activeSelf) currentFish = fish;
        }
    }
}
