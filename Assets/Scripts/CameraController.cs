using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour, InputMaster.ICameraActions
{
    public static CameraController instance = null;
    public TransformAttributeObject followTransform;
    public float scrollSpeed = 5f;

    private Transform cameraFollow;
    private InputMaster controls;
    private Vector3 movement;
    private bool follow = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        controls = new InputMaster();
        controls.Camera.SetCallbacks(this);
    }

    private void OnEnable()
    {
        controls.Enable();
        if(followTransform != null)
        {
            followTransform.ValueChanged += Follow;
        }
    }

    private void OnDisable()
    {
        controls.Disable();
        if (followTransform != null)
        {
            followTransform.ValueChanged -= Follow;
        }
    }

    private void LateUpdate()
    {
        if(follow && cameraFollow != null)
        {
            transform.position = cameraFollow.transform.position;
        }
        else
        {
            transform.position += movement;
        }
    }

    public void Follow()
    {
        cameraFollow = followTransform.Value;
    }

    public void Follow(Transform transform)
    {
        cameraFollow = transform;
    }

    public void OnMoveCamera(InputAction.CallbackContext callbackContext)
    {
        follow = false;
        Vector2 delta = callbackContext.ReadValue<Vector2>();
        movement = new Vector3(delta.x, 0f, delta.y) * Time.deltaTime * scrollSpeed;
    }

    public void OnCenter(InputAction.CallbackContext callbackContext)
    {
        follow = true;
    }
}
