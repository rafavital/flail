using UnityEngine;
using CustomUnityEvents;
using System;

[RequireComponent(typeof (Rigidbody2D), typeof (Fish))]
public class LaunchController : MonoBehaviour
{
#region VARIABLES
    [SerializeField] private ForceMode2D forceMode;
    [SerializeField] private float launchForce;
    [SerializeField] private float launchImpulse;
    [SerializeField] private float maxSpeed;
    [SerializeField] private LineRenderer launchPreview;
    [SerializeField] private SlowMotionController slowMo; //TODO: change this to an event system

    [HideInInspector] public FloatEvent onChangeVelocityRatio;
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

    private GameManager gm;
    private Fish fish;
    private Rigidbody2D rb;
    private Rigidbody2D[] bodyParts;
    private Vector2 launchDir, initialPos;
    private Camera cam;
    private bool isLaunching;
#endregion
    void Start()
    {
        gm = GameManager.Instance;

        cam = Camera.main;
        onChangeVelocityRatio.AddListener (cam.GetComponent<CameraController>().SetVelocityRatio);

        rb = GetComponent<Rigidbody2D> ();

        fish = GetComponent <Fish> ();
        
        if (launchPreview != null)
          launchPreview.enabled = false;
        
        bodyParts = fish.rbParts;

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay (transform.position, launchDir);
    }
    void Update()
    {
        if (gm.GameState != GameManager.GameStates.GAMEPLAY) return;

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
        if (gm.GameState != GameManager.GameStates.GAMEPLAY) return;

        VelocityRatio = rb.velocity.magnitude / maxSpeed;
        rb.velocity = Vector2.ClampMagnitude (rb.velocity, maxSpeed);
    }

    private void BeginLaunch () {
        if (fish.Dashs > 0 || fish.puddle) {
            isLaunching = true;

            launchPreview.enabled = true;
            initialPos = Input.mousePosition;
            slowMo.StartSlowMo();
            
        }
    }
    private void EndLaunch () {
        launchDir = (Vector2) cam.ScreenToWorldPoint(Input.mousePosition) - (Vector2) transform.position;
        Debug.DrawRay (transform.position, launchDir, Color.blue);
        int _dashs = fish.Dashs;
        bool _puddle = fish.puddle;

        if (_dashs > 0 || _puddle) {
            isLaunching = false;

            launchPreview.enabled = false;
            slowMo.EndSlowMo();

            if (_puddle) {
                foreach (var body in bodyParts)
                {
                    body.velocity = Vector2.zero;
                }
            }

            rb.AddForce (
                launchDir.normalized * (forceMode == ForceMode2D.Force ? launchForce : launchImpulse), forceMode
            );
            
            if (!_puddle) fish.UseDash();
        }
    }
}
