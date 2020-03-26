using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuRotate : MonoBehaviour
{

    public Transform focusObject;

    [SerializeField]
    private float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(focusObject);
        transform.Translate(Vector3.right * rotateSpeed * Time.deltaTime);
    }
}
