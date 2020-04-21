using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private void Awake() {
        if (Instance != this) Destroy (Instance);
        if (Instance == null) Instance = this;
    }
    public void LoadLevel (int levelId) {
        GameManager.Instance.ResetTimer ();
        SceneManager.LoadScene (levelId);
    }
    public void LoadLevel (string levelName) {
        GameManager.Instance.ResetTimer ();
        SceneManager.LoadScene (levelName);
    }
    public int GetLoadedLevel () {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
