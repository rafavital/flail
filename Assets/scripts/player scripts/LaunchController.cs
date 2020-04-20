using UnityEngine;
using CustomUnityEvents;
using System;

[RequireComponent(typeof (Rigidbody2D), typeof (Fish))]
public class LaunchController : MonoBehaviour
{
#region VARIABLES
    [Header ("Launch physics parameters")]
    [Tooltip ("the type of the force added to the rigidbody on launch")]
    [SerializeField] private ForceMode2D forceMode;
    [Tooltip ("the force added to the rigidbody if Force Mode is set to 'Force'")]
    [SerializeField] private float launchForce;
    [Tooltip ("the force added to the rigidbody if Force Mode is set to 'Impulse'")]
    [SerializeField] private float launchImpulse;
    [Tooltip ("the maximum speed of the fish")]
    [SerializeField] private float maxSpeed;
    
    [Space, Header ("Other parameters")]
    [Tooltip ("the name of the sound that plays when there is a launch")]
    [SerializeField] private string dashSoundName = "dash";
    [Tooltip ("bubbles particle system")]
    [SerializeField] private ParticleSystem bubbles;
    [Tooltip ("reference to the Line Renderer of the fish")]
    [SerializeField] private LineRenderer launchPreview;
    [Tooltip ("this actually shouldn't be here, so don't touch it!")]
    [SerializeField] private SlowMotionController slowMo; //TODO: change this to an event system
    

    [Space, Header ("Events")]
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
    private AudioManager am;
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
        am = AudioManager.Instance;

        cam = Camera.main;
        onChangeVelocityRatio.AddListener (cam.GetComponent<CameraController>().SetVelocityRatio);

        rb = GetComponent<Rigidbody2D> ();

        fish = GetComponent <Fish> ();
        
        if (launchPreview != null)
          launchPreview.enabled = false;
        
        bodyParts = fish.rbParts;
        
        gm.onEndGame.AddListener (EndGame);
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
            am.PlaySound (dashSoundName);
            bubbles.Play ();
            
            if (!_puddle) fish.UseDash();
        }
    }
    private void EndGame () {
        this.enabled = false;
    }
}
