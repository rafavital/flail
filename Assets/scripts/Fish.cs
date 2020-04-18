using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public bool puddle; 
    [SerializeField] private int initialDashCount = 1;
    [SerializeField] private string puddleTag;
    private int _dashs;
    public int Dashs {get => _dashs; set => _dashs = value;}

    private void Start() {
        Dashs = initialDashCount;
    }

    public void UseDash () {
        if (Dashs > 0) Dashs --;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag (puddleTag)) {
            Dashs++;
            puddle = true;
            Debug.Log("TÔ NA POÇA VIU MOÇO!?");
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag (puddleTag)) puddle = false;
    }
}
