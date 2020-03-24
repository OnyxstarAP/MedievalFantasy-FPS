using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{

    [SerializeField]
    private float rotateSpeed = 0.1f;
    private void Update()
    {
        rotate();
    }

    private void rotate()
    {
        transform.Rotate(new Vector3(0, rotateSpeed, 0)); ;
    }
}
