using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class FishController : MonoBehaviour
{
    [SerializeField] private float launchForce;

    private Rigidbody2D rb;
    private Vector2 launchDir;
    private Vector2 initialMousePos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown (0)) {
            initialMousePos = Input.mousePosition;
        } else if (Input.GetMouseButtonUp (0)) { 
            launchDir = (Vector2) Input.mousePosition - initialMousePos;
            
            rb.AddForce (launchDir * launchForce);
        }
    }

    private void FixedUpdate() {
        
    }
}
