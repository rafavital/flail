using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionController : MonoBehaviour
{
    [SerializeField] private float slowMoTimeScale = 0.05f;
    private float standardFixed;
    private void Start() {
        standardFixed = Time.fixedDeltaTime;
    }
    public void StartSlowMo () {
        Time.timeScale = slowMoTimeScale;
        Time.fixedDeltaTime = Time.timeScale * standardFixed;
    }

    public void EndSlowMo () {
        if (Time.timeScale == 0) return; //TODO: make pause a game state and remove this
        Time.timeScale = 1;
        Time.fixedDeltaTime = standardFixed;
    }
}
