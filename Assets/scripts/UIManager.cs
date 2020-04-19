using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject endGameUI;
    private GameManager gm;
    private void Start() {
        gm = GameManager.Instance;
        if (endGameUI != null) endGameUI.SetActive (false);

        gm.onEndGame.AddListener(EndGame);
    }

    private void EndGame () {
        if (endGameUI != null)
            endGameUI.SetActive (true);
    }
}
