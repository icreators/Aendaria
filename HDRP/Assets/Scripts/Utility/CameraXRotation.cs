using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraXRotation : MonoBehaviour
{
    [SerializeField] float rotateSensivity = 1;

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSensivity, 0);
    }
}
