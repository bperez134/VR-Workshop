using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMovement : MonoBehaviour
{
    public float yRotate;
    public Rigidbody m_Mon;
    public float thrust = 2f;
    public LayerMask ground; 
    public GameObject canvas;
    public Slider slider;
    public Image image;
    public Transform groundD;
    public GameObject mob;
    public float rayDistance;
    public float jump = 1.5f;
    public float timer;
    public float countdown;

    private float time;
    private bool turn;

    
    private Vector3 og_Vector;
    private Vector3 in_Tran;

    // Start is called before the first frame update
    void Start()
    {
        m_Mon = mob.GetComponent<Rigidbody>();
        in_Tran = transform.position;
        time = timer;
        turn = true;
        //transform.rotation = new Quaternion(0, yRotate, 0, 1);
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("This is dumb.");
        if(other.gameObject.layer == 7)
        {

            RotateToTarget(other.gameObject);
            RotateToTarget(canvas);

            Vector3 direction = new Vector3(other.transform.position.x - transform.position.x , 
                                            (other.transform.position.y - transform.position.y) * jump,
                                             other.transform.position.z - transform.position.z);
            //m_Mon.AddForce(direction *  thrust);
            if(CheckGround())
            {   
                m_Mon.AddForce(direction *  thrust);
                //m_Mon.useGravity= false;
            }
                
            //Debug.Log("This is being reached.");
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 12)
        {
            //slider.value
        }
    }

    void RotateToTarget(GameObject target)
    {
        og_Vector = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);

        float zAxis = target.transform.position.z - transform.position.z;
        float xAxis = target.transform.position.x - transform.position.x;
        if(zAxis < 0 && xAxis < 0)
        {
            yRotate = ((float)Math.Atan(zAxis / xAxis) * -1f) * (float)(180/ Math.PI);
            transform.eulerAngles = new Vector3(og_Vector.x, yRotate, og_Vector.z);
        }
        else if(zAxis > 0 && xAxis < 0)
        {
            yRotate = ((float)Math.Atan(zAxis / xAxis) * -1f) * (float)(180/ Math.PI);
            transform.eulerAngles = new Vector3(og_Vector.x, yRotate, og_Vector.z);
        }
        else if(zAxis > 0 && xAxis > 0)
        {
            yRotate = ((float)Math.Atan(xAxis / zAxis)) * (float)(180/ Math.PI) + 90f;
            transform.eulerAngles = new Vector3(og_Vector.x, yRotate, og_Vector.z);
        }
        else if(zAxis < 0 && xAxis > 0)
        {
            yRotate = (((float)Math.Atan(xAxis / zAxis)) * (float)(180/ Math.PI)) - 90f;
            transform.eulerAngles = new Vector3(og_Vector.x, yRotate, og_Vector.z);
            //Debug.Log(transform.forward);
            
        }
    }

    void Wander()
    {
        //float timeIn = timer;
        float countdownIn = countdown;
        timer -= Time.fixedDeltaTime;

        if(CheckGround())
        {
            if(m_Mon.velocity == Vector3.zero)
            {
                if(turn)
                    transform.eulerAngles += new Vector3(0, 25, 0) * Time.fixedDeltaTime;
                else
                    transform.eulerAngles += new Vector3(0, -25, 0) * Time.fixedDeltaTime;
            }

            if(timer < 0)
            {
                if(UnityEngine.Random.Range(0, 10) < 5f)
                {
                    turn = false;
                }
                else 
                    turn = true;

                if((transform.position.x - in_Tran.x > 0 && transform.position.x - in_Tran.x > 5f)
                 || (transform.position.y - in_Tran.y > 0 && transform.position.y - in_Tran.y > 5f)
                 || (transform.position.z - in_Tran.z > 0 && transform.position.z - in_Tran.z > 5f))
                {
                    transform.eulerAngles += (new Vector3(0, 180 ,0) * 1);
                    m_Mon.AddForce((in_Tran - transform.position) * (m_Mon.mass * 20));
                    m_Mon.AddForce((transform.forward) * (m_Mon.mass * 100f));
                    timer = time;
                }
                else if((transform.position.x - in_Tran.x < 0 && transform.position.x - in_Tran.x < -5f)
                 || (transform.position.y - in_Tran.y < 0 && transform.position.y - in_Tran.y < -5f)
                 || (transform.position.z - in_Tran.z < 0 && transform.position.z - in_Tran.z < -5f))
                {
                    transform.eulerAngles += (new Vector3(0, 180, 0) * 1);
                    m_Mon.AddForce((in_Tran - transform.position) * (m_Mon.mass * 20));
                    m_Mon.AddForce((transform.forward) * (m_Mon.mass * 100f));
                    timer = time;
                }
                else
                {
                    m_Mon.AddForce((-transform.right) * (m_Mon.mass * 150f));
                    m_Mon.AddForce((transform.forward) * (m_Mon.mass * 100f));
                    timer = time;
                }

                //transform.Rotate(new Vector3(0, UnityEngine.Random.Range(-2, 2), 0) , 20f * Time.fixedDeltaTime);

            }

            //m_Mon.velocity = Vector3.zero;
            
        }
        
    }

    bool CheckGround()
    {
        Ray ray = new Ray(transform.position, -groundD.up);
        RaycastHit hit;
        return Physics.Raycast(ray, out hit, rayDistance, ground);
    }
    // Update is called once per frame
    void Update()
    {
        var rayColor = CheckGround() ? Color.green : Color.red;
        Debug.DrawRay(transform.position, -groundD.up * rayDistance, rayColor);
        Wander();
        //transform.rotation = new Quaternion(transform.rotation.x, yRotate, transform.rotation.z, transform.rotation.w);
        //Debug.Log(transform.rotation.y);
        //Debug.Log(transform.rotation);
        //transform.eulerAngles = new Vector3(0, yRotate, 0);
    }
}
