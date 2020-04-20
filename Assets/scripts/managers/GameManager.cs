using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomUnityEvents;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UnityEvent onEndLevel;
    public UnityEvent onLoseGame;
    public enum GameStates {BEGINING, GAMEPLAY, END};
    public Fish currentFish;
    [SerializeField] private GameStates _gameState;
    public float timer;

    public GameStates GameState {get => _gameState; private set => _gameState = value;}
    private void Awake() {
        if (Instance != this && Instance != null) Destroy(Instance.gameObject); 
        else if (Instance == null) Instance = this;
    }
    public void StartGame () {
        GameState = GameStates.GAMEPLAY;
    }
    private void Update() {
        if (GameState != GameStates.END) timer += Time.unscaledDeltaTime;
    }
    public void EndGame () {
        GameState = GameStates.END;
        onEndLevel.Invoke();
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
