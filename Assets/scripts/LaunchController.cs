using UnityEngine;
using CustomUnityEvents;

[RequireComponent(typeof (Rigidbody2D), typeof (Fish))]
public class LaunchController : MonoBehaviour
{
#region VARIABLES
    [SerializeField] private float launchForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] private SlowMotionController slowMo;
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

    private Fish fish;
    private Rigidbody2D rb;
    private Vector2 launchDir;
    private Vector2 initialMousePos;
#endregion
    void Awake()
    {
        rb = GetComponent<Rigidbody2D> ();
        fish = GetComponent <Fish> ();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown (0)) {
            BeginLaunch ();
        } else if (Input.GetMouseButtonUp (0)) { 
            
            EndLaunch();
        }
    }

    private void FixedUpdate() {
        VelocityRatio = rb.velocity.magnitude / maxSpeed;
        rb.velocity = new Vector2 (
            Mathf.Clamp (rb.velocity.x, - maxSpeed, maxSpeed),
            Mathf.Clamp (rb.velocity.y, - maxSpeed, maxSpeed)
        );
    }

    private void BeginLaunch () {
        int _dashs = fish.Dashs;

        if (_dashs > 0 ) {
            initialMousePos = Input.mousePosition;
            slowMo.StartSlowMo();
        }
    }
    private void EndLaunch () {
        launchDir = (Vector2) Input.mousePosition - initialMousePos;
        int _dashs = fish.Dashs;
        bool _puddle = fish.puddle;

        slowMo.EndSlowMo();
        if (_dashs > 0 || _puddle) {
            rb.velocity = Vector2.zero;
            rb.AddForce (launchDir.normalized * launchForce);
            if (!_puddle) fish.UseDash();
        }
    }
}
