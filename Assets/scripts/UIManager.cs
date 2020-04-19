using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider breathSlider;
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private GameObject pauseUI;
    private GameManager gm;
    private bool paused;
    private void Start() {
        gm = GameManager.Instance;
        gm.onEndGame.AddListener(EndGame);

        if (endGameUI != null) endGameUI.SetActive (false);
        if (pauseUI != null) pauseUI.SetActive (false);
        if (breathSlider != null) gm.currentFish.onChangeBreath.AddListener (BreathValue);
    }
    private void Update() {
        if (Input.GetKeyDown (KeyCode.Escape)) Pause ();
    }

    private void EndGame () {
        if (endGameUI != null)
            endGameUI.SetActive (true);
    }
    private void BreathValue (float breath) {
        breathSlider.value = breath/100;
    }
    private void Pause () {
        if (paused) {
            Time.timeScale = 1;
            paused = false;
            if (pauseUI != null) pauseUI.SetActive (false);
        } else {
            Time.timeScale = 0;
            paused = true;
            if (pauseUI != null) pauseUI.SetActive (true);
        }

        

    }
}
