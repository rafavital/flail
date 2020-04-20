using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUnityEvents;


public class CameraController : MonoBehaviour
{

    [SerializeField] private Vector3 offset;
    [SerializeField] IntEvent onChangePPU;
    [SerializeField] private bool zoom;
    [Range(0,1), SerializeField] private float movementSmooth = 0.1f;
    [Range(0,1), SerializeField] private float zoomSmooth = 0.75f;
    
    
    private Transform target;
    private int _ppu;
    public int PPU {
        get => _ppu; 
        set {
            if (_ppu != value) {
                _ppu = value; 
                if (zoom) onChangePPU.Invoke (value);
            }
        }
    }
    private int basePPU = 100;
    private float _velocityRatio;
    public float VelocityRatio { get => _velocityRatio; set => _velocityRatio = value; }
    private Vector3 vel;
    private float refSpeed;
    private GameManager gm;
    void Start()
    {
        gm = GameManager.Instance;
        Invoke("GetTarget", 2f);

        // offset = target.position - transform.position;
    }

    private void Update() {
        int newPPU = (int) (basePPU - (VelocityRatio * basePPU));
        PPU = (int) Mathf.Lerp (PPU, newPPU, zoomSmooth);
    }
    void FixedUpdate()
    {
        if (target == null) {
            gm.GetFish ();
            target = gm.currentFish.transform;
        } 
        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.SmoothDamp (transform.position, desiredPos, ref vel, movementSmooth);
    }

    public void SetVelocityRatio (float ratio) {
        VelocityRatio = ratio;
    }

    private void GetTarget () {
        target = gm.currentFish.transform;

    }

   
}
