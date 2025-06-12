using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GL_CameraController : MonoBehaviour
{
    public Transform ball;
    public Vector3 followOffset = new Vector3(0, -6, 10);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (ball == null) return;

        Vector3 targetPos = ball.position + followOffset;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        transform.LookAt(ball);
    }

}
