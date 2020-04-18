using UnityEngine;
using CustomUnityEvents;

[RequireComponent(typeof (Rigidbody2D), typeof (Fish))]
public class LaunchController : MonoBehaviour
{
#region VARIABLES
    [SerializeField] private float launchForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] private LineRenderer launchPreview;
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
    private bool isLaunching;
#endregion
    void Awake()
    {
        rb = GetComponent<Rigidbody2D> ();
        fish = GetComponent <Fish> ();
        launchPreview.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown (0)) {
            BeginLaunch ();
        } else if (Input.GetMouseButtonUp (0)) { 
            EndLaunch();
        }
        if (isLaunching) {
            launchPreview.SetPosition (0, transform.position);
            launchPreview.SetPosition (1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
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
        if (fish.Dashs > 0 || fish.puddle) {
            isLaunching = true;
            launchPreview.enabled = true;
            initialMousePos = Input.mousePosition;
            slowMo.StartSlowMo();
        }
    }
    private void EndLaunch () {
        launchDir = (Vector2) Input.mousePosition - initialMousePos;
        int _dashs = fish.Dashs;
        bool _puddle = fish.puddle;

        if (_dashs > 0 || _puddle) {
            launchPreview.enabled = false;
            slowMo.EndSlowMo();
            isLaunching = false;
            rb.velocity = Vector2.zero;
            rb.AddForce (launchDir.normalized * launchForce);
            if (!_puddle) fish.UseDash();
        }
    }
}
