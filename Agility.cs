using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Agility : MonoBehaviour
{
    public XRNode inputSourceR;
    public XRNode inputSourceL;
    private bool inputActiveL;
    private bool inputActiveL2;
    private bool inputActiveR;
    private Vector2 inputAxisR;
    private Vector2 inputAxisL;
    private Rigidbody body;
    private Vector3 direction;

    public GameObject rightHand;
    public GameObject leftHand;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice deviceL = InputDevices.GetDeviceAtXRNode(inputSourceL);
        deviceL.TryGetFeatureValue(CommonUsages.primaryButton, out inputActiveL);
        
        InputDevice deviceR = InputDevices.GetDeviceAtXRNode(inputSourceR);
        deviceR.TryGetFeatureValue(CommonUsages.primaryButton, out inputActiveR);

        InputDevice deviceR2 = InputDevices.GetDeviceAtXRNode(inputSourceR);
        deviceR2.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxisR);

        InputDevice deviceL2 = InputDevices.GetDeviceAtXRNode(inputSourceL);
        deviceL2.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out inputActiveL2);

        InputDevice deviceL3 = InputDevices.GetDeviceAtXRNode(inputSourceL);
        deviceL3.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxisL);

        if(!(inputAxisR == Vector2.zero))
        {
            Vector3 directionRotation = new Vector3(0, inputAxisR.x, 0);

            transform.eulerAngles += new Vector3(0, 2 * (inputAxisR.x), 0);
            /*
            if(inputAxisL.x < 0)
                transform.eulerAngles = directionRotation;
            else
                transform.Rotate(directionRotation, inputAxisL.x * 15f);
            */
        }

        if(!(inputAxisL == Vector2.zero))
            body.velocity = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(inputAxisL.x, 0, inputAxisL.y) * 5f;

        body.velocity = body.velocity;

        if(inputActiveL & inputActiveR)
        {
            body.velocity = (-leftHand.transform.right + rightHand.transform.right) * 10f;
            //body.velocity = rightHand.transform.right * 5f;
        }
        else if(inputActiveL)
        {
            body.velocity = -leftHand.transform.right * 5f;
        }
        else if(inputActiveR)
        {
            body.velocity = rightHand.transform.right * 5f;
        }

        if(inputActiveL2)
        {
            body.velocity = Vector3.zero;
        }

    }
}
