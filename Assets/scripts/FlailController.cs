using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D), typeof (Fish))]
public class FlailController : MonoBehaviour
{
    [SerializeField] private float flailForce = 600;
    [SerializeField] private float flailTorque;
    [SerializeField] private string puddleTag;
    [SerializeField] private Vector2 minMaxX;
    [SerializeField] private Vector2 minMaxY;
    
    private GameManager gm;
    private Fish fish;
    private Vector2 closestPuddle;
    private Rigidbody2D rb;
    private Rigidbody2D[] bodyParts;
    private Vector2 flailDir;
    private GameObject[] puddles;
    private bool flail, grounded;

    void Awake()
    {
        gm = GameManager.Instance;
        fish = GetComponent<Fish> ();
        rb = GetComponent<Rigidbody2D> ();
        bodyParts = GetComponentsInChildren<Rigidbody2D> ();
        puddles = GameObject.FindGameObjectsWithTag(fish.puddleTag);
    }
    void Update()
    {
        if (gm.GameState != GameManager.GameStates.GAMEPLAY) return; 

        if (Input.GetKeyDown (KeyCode.Space) && fish.grounded) {
            closestPuddle = GetClosestPuddle ();
            flailDir = new Vector2 (
                Mathf.Sign (closestPuddle.x - transform.position.x) * Random.Range (minMaxX.x,minMaxX.y),
                Random.Range (minMaxY.x, minMaxY.y)
            );
            
            flail = true;
        } else {
            flail = false;
        }
    }

    private void FixedUpdate() {
        if (gm.GameState != GameManager.GameStates.GAMEPLAY) return;

        if (flail) {
            rb.AddForce (flailDir * flailForce);
            for (int i = 0; i < bodyParts.Length; i++)
            {
                bodyParts[i].AddTorque (Random.Range (-flailTorque, flailTorque));
            }
        }
    }

    private Vector2 GetClosestPuddle () {
        Vector2 closestPuddle = Vector2.zero;
        for (int i = 0; i < puddles.Length; i++)
        {   
            var currentPuddle = puddles[i];
            if (i == 0) {
                closestPuddle = currentPuddle.transform.position;
            } else {
                float currentDistance = Vector2.Distance (transform.position, closestPuddle);
                float newDistance = Vector2.Distance (transform.position, currentPuddle.transform.position);
                if (newDistance < currentDistance) closestPuddle = currentPuddle.transform.position;
            }
        }
        return closestPuddle;
    }
}
