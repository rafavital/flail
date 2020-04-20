using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject prompt;
    [SerializeField] private GameObject levelSelection;
    private float _flail ;
    public float Flail {
        get => _flail;
        set {
            _flail = value;
            if (_flail == 1) StartGame ();
        } 
    }
    private GameManager gm;

    private Animator animator;

    private void Awake() {
        animator = GetComponent <Animator> ();
    }
    private void Start() {
        gm = GameManager.Instance;
        
    }
    private void Update() {
        if (gm.GameState == GameManager.GameStates.GAMEPLAY) {
            prompt.SetActive (false);
            levelSelection.SetActive (true);
        } else {
            prompt.SetActive (true);
            levelSelection.SetActive (false);
        }
    }
    private void StartGame () {
        gm.StartGame ();
        // animator.SetTrigger ("levelSelection");
    }
}
