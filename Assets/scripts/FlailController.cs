using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class FlailController : MonoBehaviour
{
    [SerializeField] private float flailForce = 600;
    [SerializeField] private float flailTorque;
    [SerializeField] private string groundTag;

    private Rigidbody2D rb;
    private Rigidbody2D[] bodyParts;
    private Vector2 flailDir;
    private bool flail, grounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D> ();
        bodyParts = GetComponentsInChildren<Rigidbody2D> ();
    }

    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space) && grounded) {
            flailDir = Random.insideUnitCircle;
            flailDir.y = 1;
            flail = true;
        } else {
            flail = false;
        }
    }

    private void FixedUpdate() {
        if (flail) {
            rb.AddForce (flailDir * flailForce);
            for (int i = 0; i < bodyParts.Length; i++)
            {
                bodyParts[i].AddTorque (Random.Range (-flailTorque, flailTorque));
            }
        }
    }

    private void OnCollisionEnter2D (Collision2D other) {
       if (other.collider.tag == groundTag) grounded = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.collider.tag == groundTag) grounded = false;
    }
}
