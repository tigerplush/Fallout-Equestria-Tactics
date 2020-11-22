using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance = null;

    private Transform cameraFollow;

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

    private void LateUpdate()
    {
        if(cameraFollow != null)
        {
            transform.position = cameraFollow.transform.position;
        }
    }

    public void Follow(Transform transform)
    {
        cameraFollow = transform;
    }
}
