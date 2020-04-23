using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuRotate : MonoBehaviour
{

    public Transform focusObject;

    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float angleTimer;
    [SerializeField]
    private float xPosition;
    [SerializeField]
    private float yPosition;
    [SerializeField]
    private float zPosition;

    // Start is called before the first frame update
    void Start()
    {
        reRandomizer();
        StartCoroutine(cameraPositionSwitch());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(focusObject);
        transform.Translate(Vector3.right * rotateSpeed * Time.deltaTime);
    }

    private IEnumerator cameraPositionSwitch()
    {
        yield return new WaitForSeconds(angleTimer);
        reRandomizer();
        StartCoroutine(cameraPositionSwitch());
    }

    private void reRandomizer()
    {
        angleTimer = Mathf.Round(Random.Range(4,7.5f));
        //rotateSpeed = Random.Range(0.25f, 0.75f);
        //xPosition = Random.Range(Random.Range(-1, -3), Random.Range(1, 3));
        yPosition = Random.Range(0.25f, 1);
       // zPosition = Random.Range(Random.Range(-1, -3), Random.Range(1, 3));
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }
}
