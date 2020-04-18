using UnityEngine;
using CustomUnityEvents;

[RequireComponent(typeof (Rigidbody2D))]
public class FishController : MonoBehaviour
{
    [SerializeField] private float debugSpeed;
    [SerializeField] private float launchForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] private FloatEvent onChangeVelocityRatio;

    private float _velocityRatio;
    public float VelocityRatio {
        get => _velocityRatio;
        set {
            if (_velocityRatio != value) {
                _velocityRatio = value;
                onChangeVelocityRatio.Invoke (value);
            }
        }
    }

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
            rb.velocity = Vector2.zero;
            rb.AddForce (launchDir.normalized * launchForce);
        }


    }

    private void FixedUpdate() {
        VelocityRatio = rb.velocity.magnitude / maxSpeed;
        rb.velocity = new Vector2 (
            Mathf.Clamp (rb.velocity.x, - maxSpeed, maxSpeed),
            Mathf.Clamp (rb.velocity.y, - maxSpeed, maxSpeed)
        );
        debugSpeed = rb.velocity.magnitude;
    }
}
