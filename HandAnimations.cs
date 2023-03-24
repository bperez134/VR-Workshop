using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandAnimations : MonoBehaviour
{

    private Animator handAnimator;
    private InputDevice borrowedDevice;
    //private float triggerValue;
    //private float gripValue;
    public GameObject rigObject;
    public XRNode control;
    HandController handController;
    // Start is called before the first frame update
    void Start()
    {
        handAnimator = this.gameObject.GetComponent<Animator>();
        handController = this.gameObject.GetComponent<HandController>();
        //borrowedDevice = handController.deviceIn;
        borrowedDevice = InputDevices.GetDeviceAtXRNode(control);
    }

    void UpdateHandAnimator()
    {
        //handAnimator.SetFloat("Still", 0);
        if(borrowedDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) &&
            (borrowedDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerBool) && 
                borrowedDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripBool)))
        {
            handAnimator.SetFloat("Thumbs", triggerValue);
            //Debug.Log("Trigger is activated: " + triggerValue + " This is the trigger value.");
            //Debug.Log(borrowedDevice.TryGetFeatureValue(CommonUsages.trigger, out float num));
        }
        else
        {
            handAnimator.SetFloat("Thumbs", 0);
        }

        if(borrowedDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue) &&
            (borrowedDevice.TryGetFeatureValue(CommonUsages.gripButton, out gripBool) &&
                borrowedDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerBool)))
        {
            handAnimator.SetFloat("Point", gripValue);
            //Debug.Log("Grip is activated: " + gripValue + " This is the grip value.");
        }
        else
        {
            handAnimator.SetFloat("Point", 0);
        }

        if(borrowedDevice.TryGetFeatureValue(CommonUsages.gripButton, out gripBool) && 
            borrowedDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerBool) )
        {
            handAnimator.SetFloat("CloseHand", (gripValue * triggerValue));
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        UpdateHandAnimator();
    }

}
