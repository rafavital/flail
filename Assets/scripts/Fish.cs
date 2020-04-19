using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public bool puddle, grounded; 
    public string puddleTag = "Puddle";
    [SerializeField] private int initialDashCount = 1;
    [SerializeField] private int initialBreath = 100;
    [SerializeField] private float breathDecreaseRate = 0.1f;
    
    private float _breath;
    public float Breath {
        get => _breath; 
        set {
            _breath = value;
            _breath = Mathf.Clamp (_breath, 0, initialBreath);
        }
    }
    private int _dashs;
    private bool dying;

    public int Dashs {get => _dashs; set => _dashs = value;}

    private void Start() {
        Dashs = initialDashCount;
        Breath = initialBreath;
    }

    private void Update() {
        if (!puddle) {
            Breath -= breathDecreaseRate;
        }

        if (Breath <= 0) Debug.Log("I'M DEAD, STOP THE GAME!");
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
