using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Gesture : MonoBehaviour
{

    public XRNode inputSourceR;
    public GameObject rHand;
    public GameObject menu;
    public GameObject view;

    private bool inputActiveR;
    private float timer;
    private float firPY;
    private float secPY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice deviceR = InputDevices.GetDeviceAtXRNode(inputSourceR);
        deviceR.TryGetFeatureValue(CommonUsages.gripButton, out inputActiveR);

        //Debug.Log(timer += Time.deltaTime);
        //rHand.transform.position.y;

        if(inputActiveR)
        {
            Debug.Log("Activated");
            timer += Time.fixedDeltaTime;
            //Debug.Log(timer);
            if(timer == 0.02f && inputActiveR)
            {
                firPY = rHand.transform.position.y;
                
            }

        }

        if(!inputActiveR && timer > 0f)
        {
            Debug.Log("Release");
            secPY = rHand.transform.position.y;
            Debug.Log(firPY);
            Debug.Log(secPY);
            timer = 0f;
            if(firPY - secPY >= .13f && firPY - secPY <= .23f)
            {
                Instantiate(menu);
                menu.transform.position = new Vector3(view.transform.position.x, view.transform.forward.y, view.transform.position.z);
            }
        }
        
    }


}
