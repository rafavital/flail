using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Fish fish;
    private string _puddleTag;

    private void Awake() {
        fish = GetComponentInParent <Fish> ();
        _puddleTag = fish.puddleTag;
    }
    private void OnCollisionStay2D (Collision2D other) {
       if (other.collider.tag != _puddleTag) fish.grounded = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.collider.tag != _puddleTag) fish.grounded = false;
    }
}
