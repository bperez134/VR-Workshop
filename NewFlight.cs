using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class NewFlight : MonoBehaviour
{
    public Transform head;
    public GameObject leftHand;
    public GameObject rightHand;
    public XRNode inputSourceL;
    public XRNode inputSourceR;
    //public float gravity = -9.81f;
    //public LayerMask groundLayer;
    public float speed;
    //public float fallingSpeed = 0;

    private bool flick = false;
    private bool inputActiveL;
    private bool inputActiveR;
    private bool inputActiveL2;
    private bool flight;
    private CharacterController character;
    //private XRRig rig;
    private Grapple grapple;
    //private Gravity gForce;
    public Vector3 direction;
    private Booster boost;
    private float timerR;
    private float timerL;
    public Rigidbody body;
    
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        //rig = GetComponent<XRRig>();
        //gForce = GetComponent<Gravity>();
        //boost = GetComponent<Booster>();
        body = GetComponent<Rigidbody>();

        //gForce.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        InputDevice deviceL = InputDevices.GetDeviceAtXRNode(inputSourceL);
        deviceL.TryGetFeatureValue(CommonUsages.primaryButton, out inputActiveL);
        
        InputDevice deviceR = InputDevices.GetDeviceAtXRNode(inputSourceR);
        deviceR.TryGetFeatureValue(CommonUsages.primaryButton, out inputActiveR);

        InputDevice deviceL2 = InputDevices.GetDeviceAtXRNode(inputSourceL);
        deviceL2.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out inputActiveL2);

        body.velocity = body.velocity;
        //gForce.enabled = false;

        if(inputActiveL2)
        {   
            body.velocity = Vector3.zero;
        }
            //direction = new Vector3(0, 0, 0);


        if(inputActiveL && inputActiveR)
        {
            //gForce.enabled = false;
            Vector3 rightD = rightHand.transform.forward;
            Vector3 leftD = leftHand.transform.forward; 
            direction = leftD + rightD;
            direction *= 2f;

            body.velocity += direction;
        }
        else if(inputActiveR)
        {
            //gForce.enabled = false;
            //if(timerR < 5f)
            //    timerR += 0.5f;
            direction = rightHand.transform.forward * 5f; 
            //direction *= .05f;

            //transform.position += direction;
            //body.velocity += direction *(timerR * Time.fixedDeltaTime);
            body.velocity = rightHand.transform.forward  * (5f);
            //Debug.Log("Character movement: " + body.velocity);
        }
        else if(inputActiveL)
        {   
            //gForce.enabled = false;
            direction = leftHand.transform.forward * 5f;
            //direction *= .05f;
            
            body.velocity = leftHand.transform.forward * 5f;
            //transform.position += direction* (timerL * Time.fixedDeltaTime);
        }


    }
    /*
    public void Switch()
    {

        if(gForce.enabled)
        {
            gForce.enabled = false;
        }
        else
        {
            gForce.enabled = true;
        }
    }*/
}
