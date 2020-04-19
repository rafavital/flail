using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider breathSlider;
    [SerializeField] private GameObject endGameUI;
    private GameManager gm;
    private void Start() {
        gm = GameManager.Instance;
        gm.onEndGame.AddListener(EndGame);

        if (endGameUI != null) endGameUI.SetActive (false);
        if (breathSlider != null) gm.currentFish.onChangeBreath.AddListener (BreathValue);
    }

    private void EndGame () {
        if (endGameUI != null)
            endGameUI.SetActive (true);
    }

    private void BreathValue (float breath) {
        breathSlider.value = breath/100;
    }
}
