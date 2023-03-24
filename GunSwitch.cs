using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class GunSwitch : MonoBehaviour
{
    public float speed = 20;
    public GameObject projectile;
    public Transform eject;
    public bool offOn;
    public Image buttonColor;

    private InputDevice handInputs;
    public XRNode control;

    //bool activate;

    void Start()
    {
        handInputs = InputDevices.GetDeviceAtXRNode(control);
        StartCoroutine(fireT());
    }

    void Update()
    {
        //Shoot();
    }

    void Shoot(bool active)
    {   

        if(true)
        {
            //handInputs.TryGetFeatureValue(CommonUsages.triggerButton, out activate);
            if(active && offOn)
            {
                Debug.Log("Shoot");
                GameObject spawn = Instantiate(projectile, eject.position, eject.rotation);
                spawn.GetComponent<Rigidbody>().velocity = speed * eject.forward;
                
                Destroy(spawn, 2f);  
            }

        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        offOn = !offOn;
        if(offOn == true)
        {
            //RGB
            //True
            buttonColor.color = new Color(255, 255, 255);
        }
        else
        {
            buttonColor.color = new Color(255, 0, 0);
        }
        //Debug.Log("The GunSwitch is:  " + offOn);
    }

    IEnumerator fireT()
    {
        while(true)
        {
            yield return new WaitForSeconds(.1f);
            handInputs.TryGetFeatureValue(CommonUsages.triggerButton, out bool activate);
            Shoot(activate);
        }
    }
}
