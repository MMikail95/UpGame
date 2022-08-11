using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float offSet;

    void Update()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.y = playerTransform.position.y + offSet;
        transform.position = cameraPos;
    }
}
