using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [Range(0,1), SerializeField] private float smooth;
    
    private Vector3 offset;
    private Vector3 vel;

    void Start()
    {
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPos = target.position - offset;
        transform.position = Vector3.SmoothDamp (transform.position, desiredPos, ref vel, smooth);
        // transform.position = new Vector3 (transform.position.x, transform.position.y, -10);
    }
}
