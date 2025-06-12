using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class GL_BallInputController : MonoBehaviour
{
    [SerializeField]
    public float maxForce = 20f;
    public LineRenderer aimLine;

    public Rigidbody rb;
    public Vector2 aimStart;
    public Vector2 aimCurrent;
    private bool isAiming = false;

    public InputActionReference aimAction;
    public InputActionReference shootAction;

    private GL_PlayerInfo _playerInfo;

    public void Awake()
    {
        shootAction.action.started += ctx => StartAiming();
        shootAction.action.canceled += ctx => ShootBall();
        aimAction.action.performed += ctx => aimCurrent = ctx.ReadValue<Vector2>();
    }


    void Start()
    {
        rb.GetComponent<Rigidbody>();
        aimCurrent = Vector2.zero;

        _playerInfo = GetComponent<GL_PlayerInfo>();
    }

    private void OnEnable()
    {
        aimAction.action.Enable();
        shootAction.action.Enable();
    }

    private void OnDisable()
    {
        aimAction.action.Disable();
        aimAction.action.Disable();
    }

    void Update()
    {
        if (isAiming && rb.velocity.magnitude < 0.1f) 
        {
            Vector3 dir = (aimCurrent - aimStart);
            Vector3 worlDir = new Vector3(-dir.x, 0, -dir.y).normalized;
            
            aimLine.SetPosition(0, transform.position);
            aimLine.SetPosition(1, transform.position + worlDir * Math.Clamp(dir.magnitude / 10f, 0, maxForce));
        }
    }

    private void ShootBall()
    {
        if (!isAiming) return;
        isAiming = false;

        Vector3 dir = (aimCurrent - aimStart);
        Vector3 force = new Vector3(-dir.x, 0, -dir.y).normalized * Math.Clamp(dir.magnitude / 10f, 0, maxForce);
        rb.AddForce(force, ForceMode.Impulse);

        aimLine.SetPosition(0, Vector3.zero);
        aimLine.SetPosition(1, Vector3.zero);

        _playerInfo.Shoot();
    }

    private void StartAiming()
    {
        if(rb.velocity.magnitude < 0.1f)
        {
            isAiming = true;
            aimStart = Mouse.current.position.ReadValue();
        }
    }
}
