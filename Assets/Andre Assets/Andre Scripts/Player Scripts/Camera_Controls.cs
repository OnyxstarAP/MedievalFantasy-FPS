using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controls : MonoBehaviour
{
    [SerializeField]
    private float mouseSenitivity = 360f;

    public Transform playerCharacter;

    private float verRotation;

    void Start()
    {
        // Prevents the Cursor from leaving the Window Mid-Game
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //CameraRotate();
    }

    // Allows the mouse to move the camera
    void CameraRotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * GameWatcher.mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * GameWatcher.mouseSensitivity * Time.deltaTime;

        verRotation -= mouseY;
        // Prevents the Player from rotating past their Maximum position
        verRotation = Mathf.Clamp(verRotation, -90f, 90f);

        playerCharacter.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(verRotation, 0f, 0f);
    }

}
