using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuRotate : MonoBehaviour
{

    public Transform focusObject;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private float positionSwapTimer;
    [SerializeField]
    private float leastWaitTime;

    void Start()
    {
        positionSwapTimer = leastWaitTime;
        StartCoroutine(positionSwap());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(focusObject);
        transform.Translate(Vector3.right * rotateSpeed * Time.deltaTime);
    }

    IEnumerator positionSwap()
    {
        yield return new WaitForSeconds(positionSwapTimer);
        ValueRandomizer();
        StartCoroutine(positionSwap());
    }

    private void ValueRandomizer()
    {
        positionSwapTimer = Mathf.RoundToInt(Random.Range(leastWaitTime, leastWaitTime * 2));
        Debug.Log("Value randomized. New Value: " + positionSwapTimer);
        
    }

}
