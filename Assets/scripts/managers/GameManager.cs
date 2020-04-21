using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomUnityEvents;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    
    public enum GameStates {BEGINING, GAMEPLAY, END};
    public Fish currentFish;
    [SerializeField] private GameStates _gameState;
    public float timer {get; private set;}
    public UnityEvent onEndLevel;
    public UnityEvent onLoseGame;

    public GameStates GameState {get => _gameState; private set => _gameState = value;}
    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) {
            Destroy(Instance.gameObject); 
            Instance = this;
        }

        GetFish ();
    }
    private void Start() {
        timer = 0;
    }
    public void StartGame () {
        GameState = GameStates.GAMEPLAY;
    }
    private void Update() {
        if (GameState != GameStates.END) timer += Time.deltaTime;
    }
    public void EndGame () {
        GameState = GameStates.END;
        onEndLevel.Invoke();
    }
    public void RestartGame () {
        timer = 0;
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }

    public void GetFish () {
        GameObject[] fishes = GameObject.FindGameObjectsWithTag ("Player");
        for (int i = 0; i < fishes.Length; i++)
        {
            var fish = fishes[i];
            if (fish.activeSelf) currentFish = fish.GetComponent <Fish> ();
        }
    }

    public void ResetTimer () => timer = 0;
}
