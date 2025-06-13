using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using Cinemachine;

public class GL_BallInputController : MonoBehaviour
{
    [SerializeField]
    public float maxForce = 20f;
    public LineRenderer aimLine;
    public CinemachineInputProvider CinemachineInputProvider;

    public Rigidbody rb = null;
    public Vector2 aimStart;
    public Vector2 aimCurrent;
    private bool isAiming = false;


    public Transform target;

    public float sensitivity = 5f;
    public float maxAngle = 80f;
    public float minAngle = -30f;

    public Vector2 lookInput;

    private float xRotation = 0f;
    private float yRotation = 0f;

    public InputActionReference aimAction;
    public InputActionReference shootAction;

    private GL_PlayerInfo _playerInfo;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject.transform.parent.gameObject);

        shootAction.action.started += ctx => StartAiming();
        shootAction.action.canceled += ctx => ShootBall();
        aimAction.action.performed += ctx => aimCurrent = ctx.ReadValue<Vector2>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _playerInfo = GetComponent<GL_PlayerInfo>();
        aimCurrent = target.transform.forward;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        aimAction.action.Enable();
        shootAction.action.Enable();
    }

    private void OnDisable()
    {
        aimAction.action.Disable();
        shootAction.action.Disable();

    }

    void Update()
    {
        if(rb.velocity.magnitude > 0f && rb.velocity.magnitude < 1f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (isAiming && rb.velocity.magnitude < 1f) 
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
        Cursor.lockState = CursorLockMode.Locked;

        Vector3 dir = (aimCurrent - aimStart);
        Vector3 force = new Vector3(-dir.x, 0, -dir.y).normalized * Math.Clamp(dir.magnitude / 10f, 0, maxForce);
        rb.AddForce(force, ForceMode.Impulse);

        aimLine.SetPosition(0, Vector3.zero);
        aimLine.SetPosition(1, Vector3.zero);

        _playerInfo.Shoot();
        if (CinemachineInputProvider != null)
        {
            CinemachineInputProvider.enabled = true;
        }
    }

    private void StartAiming()
    {
        if (rb != null)
        {
            if (rb.velocity.magnitude < 0.1f)
            {
                isAiming = true;

                Cursor.lockState = CursorLockMode.None;
                aimStart = Mouse.current.position.ReadValue();


                if (CinemachineInputProvider != null)
                {
                    CinemachineInputProvider.enabled = false;
                }


            }
        }
        else
        {
            Debug.Log("Aucun RigidBody detecte");
            rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("Rigidbody set!");
            }
            else
            {
                Debug.Log("Aucun RigidBody detecte");
            }
        }
        
    }
}
