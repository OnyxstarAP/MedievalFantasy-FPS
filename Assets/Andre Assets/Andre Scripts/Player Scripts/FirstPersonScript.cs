using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonScript : MonoBehaviour
{
    public float mouseSensitivity;

    public Transform playerCharacter;

    private float verRotation;

    public Slider sensitivitySlider;

    void Start()
    {
        // Prevents the Cursor from leaving the Window Mid-Game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CameraRotate();
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
