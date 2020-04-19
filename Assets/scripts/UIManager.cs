using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject endGameUI;
    private GameManager gm;
    private void Start() {
     
        gm = GameManager.Instance;
        endGameUI.SetActive (false);
        gm.onEndGame.AddListener(EndGame);
    }

    private void EndGame () {
        endGameUI.SetActive (true);
    }
}
