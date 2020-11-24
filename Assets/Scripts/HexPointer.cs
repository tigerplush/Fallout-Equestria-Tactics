using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;

public class HexPointer : MonoBehaviour
{
    public static HexPointer instance = null;

    public RenderObjects pointerFeature;
    public LayerMask pointableLayers;

    private bool pointerEnabled = false;

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
    }

    private void Start()
    {
    }

    private void Update()
    {
        if(pointerEnabled)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, pointableLayers))
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
