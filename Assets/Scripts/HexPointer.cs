using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;

public class HexPointer : MonoBehaviour, InputMaster.IPointerActions
{
    public static HexPointer instance = null;

    public RenderObjects pointerFeature;
    public LayerMask pointableLayers;

    private bool pointerEnabled = false;

    private InputMaster controls;

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
        controls.Pointer.SetCallbacks(this);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void OnMousePosition(InputAction.CallbackContext callbackContext)
    {
        if(pointerEnabled)
        {
            Ray ray = Camera.main.ScreenPointToRay(callbackContext.ReadValue<Vector2>());
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, pointableLayers))
            {
                CubeCoordinates tileCoordinates = Hex.FromWorld(hitInfo.point);
                Vector3 position = Hex.ToWorld(tileCoordinates);
                Vector4 materialPosition = new Vector4(position.x, position.y, position.z);
                pointerFeature.settings.overrideMaterial.SetVector("_Center", materialPosition);
            }
        }
    }

    public void Disable()
    {
        pointerFeature.SetActive(false);
        pointerEnabled = false;
    }

    public void Enable()
    {
        pointerFeature.SetActive(true);
        pointerEnabled = true;
    }
}
