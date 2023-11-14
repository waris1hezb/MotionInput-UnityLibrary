using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float xSpeed = 120.0f;
    [SerializeField] private float ySpeed = 120.0f;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float accelerationRate = 10f;
    [SerializeField] private float decelerationRate = 10f;
    [SerializeField] private float targetSpeed = 5f;

    private CursorSwitchEffect _cursorSwitchEffect;
    private CameraMoveEffect _cameraMoveEffect;

    private float speedMod = 1f;
    private Vector3 moveDirection, targetVelocity;
    private Vector3 targetPosition;
    private float x, y;
    private bool isMoving;

    private bool leftMouseButtonHeld = false;
    private bool lastLeftMouseButtonHeld = false;
    public bool LeftMouseButtonHeld 
    { 
        get => leftMouseButtonHeld;
        set
        {
            leftMouseButtonHeld = value;
            if (lastLeftMouseButtonHeld != value && value == true)
            {
                EnableAllEffects();
            }
            else if (lastLeftMouseButtonHeld != value && value == false)
            {
                DisableAllEffects();
            }
            lastLeftMouseButtonHeld = value;
        }
    }

    private void Awake()
    {
        _cursorSwitchEffect = GetComponent<CursorSwitchEffect>();
        _cameraMoveEffect = GetComponent<CameraMoveEffect>();
    }

    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    private void GoForward()
    {
        enabled = true;
        StopAllCoroutines();
        StartCoroutine(GoForwardRoutine());
    }

    private void Stop()
    {
        StopAllCoroutines();
    }

    IEnumerator GoForwardRoutine()
    {
        while (true)
        {
            moveDirection = Vector3.forward;
            targetSpeed = Mathf.Clamp(targetSpeed * (1f + Input.mouseScrollDelta.y * 0.05f), 0f, maxSpeed);
            targetVelocity = Vector3.Lerp(targetVelocity, moveDirection * targetSpeed, accelerationRate * Time.deltaTime);
            targetPosition = transform.position + transform.TransformVector(targetVelocity * Time.deltaTime * speedMod);
            transform.position = targetPosition;
            yield return null;
        }
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        if (!enabled)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            moveDirection = Vector3.zero;
            targetVelocity = Vector3.zero;
        }

        LeftMouseButtonHeld = Input.GetMouseButton(1);

        if (Input.GetMouseButton(1) || isMoving)
        {
            targetPosition = transform.position;
            moveDirection = Vector3.zero;
            bool move = isMoving;
            isMoving = false;
            if (Input.GetKey(KeyCode.W))
            {
                move = true;
                moveDirection += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                move = true;
                moveDirection -= Vector3.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                move = true;
                moveDirection -= Vector3.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                move = true;
                moveDirection += Vector3.right;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                move = true;
                moveDirection -= Vector3.up;
            }
            if (Input.GetKey(KeyCode.E))
            {
                move = true;
                moveDirection += Vector3.up;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speedMod = 3f;
            }
            else
            {
                speedMod = 1f;
            }

            Vector3 mousePos = Input.mousePosition;
            mousePos.x = (mousePos.x / Screen.width) - 0.5f;
            mousePos.y = (mousePos.y / Screen.height) - 0.5f;
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            transform.rotation = rotation;
            if (move)
            {
                targetSpeed = Mathf.Clamp(targetSpeed * (1f + Input.mouseScrollDelta.y * 0.05f), 0f, maxSpeed);
                targetVelocity = Vector3.Lerp(targetVelocity, moveDirection * targetSpeed, accelerationRate * deltaTime);
            }
            else
            {
                targetVelocity = Vector3.Lerp(targetVelocity, moveDirection * targetSpeed, decelerationRate * deltaTime);
            }

            targetPosition = transform.position + transform.TransformVector(targetVelocity * deltaTime * speedMod);
            transform.position = targetPosition;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
    }

    private void EnableAllEffects()
    {
        if (_cursorSwitchEffect != null)
            _cursorSwitchEffect.EnableEffect();
        if (_cameraMoveEffect != null)
            _cameraMoveEffect.EnableEffect();
    }

    private void DisableAllEffects()
    {
        if (_cursorSwitchEffect != null)
            _cursorSwitchEffect.DisableEffect();
        if (_cameraMoveEffect != null)
            _cameraMoveEffect.DisableEffect();
    }
}
