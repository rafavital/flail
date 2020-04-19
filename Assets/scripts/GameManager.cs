using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
#region SINGLETON
    public GameManager Instance;
    private void Awake() {
        if (Instance != this) Destroy(Instance);
        if (Instance == null) Instance = this;
    }
#endregion

    public void EndGame () {
        
    }
}
