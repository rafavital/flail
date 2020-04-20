using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider breathSlider;
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private GameObject endLevelUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject[] dashIcons;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text endTimerText;
    private GameManager gm;
    private LevelManager lm;
    private bool paused;
    private float timer;

    private void Start() {
        gm = GameManager.Instance;
        lm = LevelManager.Instance;

        gm.onLoseGame.AddListener(LoseGame);
        gm.onEndLevel.AddListener(EndLevel);

        if (endGameUI != null) endGameUI.SetActive (false);
        if (pauseUI != null) pauseUI.SetActive (false);
        if (endLevelUI != null) endLevelUI.SetActive (false);
        Invoke("SetListeners",2f);
        timer = 0;
    }
    private void Update() {
        timerText.text = (int)gm.timer/60 + ":" + Mathf.Round (gm.timer % 60);
        if (Input.GetKeyDown (KeyCode.Escape)) Pause ();
    }

    private void LoseGame () {
        if (endGameUI != null)
            endGameUI.SetActive (true);
    }
    private void EndLevel () {
        if (endLevelUI != null)
            endLevelUI.SetActive (true);
        endTimerText.text = "your time was: " + (int)gm.timer/60 + ":" + Mathf.Round (gm.timer % 60);
    }
    private void BreathValue (float breath) {
        breathSlider.value = breath/100;
    }
    private void DashCount (int dashCount) {
        for (int i = 0; i < dashIcons.Length; i++)
        {
            GameObject currentIcon = dashIcons[i];
            if (i <= dashCount - 1) currentIcon.SetActive (true);
            else currentIcon.SetActive (false);
        }
    }
    public void Pause () {
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
    public void ReturnHome () {
        lm.LoadLevel (0);
        if (paused) Pause();
    }
    public void RestartLevel () {
        lm.LoadLevel (lm.GetLoadedLevel ());
        if (paused) Pause();
    }
    public void QuitGame () {
        Application.Quit ();
    }

    private void SetListeners () {
        if (breathSlider != null) gm.currentFish.onChangeBreath.AddListener (BreathValue);
        if (dashIcons != null) gm.currentFish.onChangeDashCount.AddListener (DashCount);
    }
}
