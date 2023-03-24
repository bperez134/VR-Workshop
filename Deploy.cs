using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deploy : MonoBehaviour
{
    public bool randomYSpawn;
    public GameObject obj;
    public GameObject obj2;
    public double xAxis;
    public double yAxis;
    public double zAxis;
    public double spawnTime = .5; 
    //public int num = Random.Range(0, 10);

    void Start()
    {
        StartCoroutine(spawnT());
    }
    private void spawnTargets()
    {   
        if(randomYSpawn == true)
        {
            if(Random.Range(0, 10) < 5)
            {
                GameObject left = Instantiate(obj) as GameObject;
                left.transform.position = new Vector3(transform.position.x + (float)(xAxis), (transform.position.y + (float)(yAxis)) - Random.Range(0, (float).75), transform.position.z + (float)(zAxis));
            }
            else
            {
                GameObject right = Instantiate(obj2) as GameObject;
                right.transform.position = new Vector3(transform.position.x - (float)(xAxis), (transform.position.y + (float)(yAxis)) - Random.Range(0, (float).75), transform.position.z + (float)(zAxis));
            }
        }
        else
        {
            if(Random.Range(0, 10) < 5)
            {
                GameObject left = Instantiate(obj) as GameObject;
                left.transform.position = new Vector3(transform.position.x + (float)(xAxis), (transform.position.y + (float)(yAxis)), transform.position.z + (float)(zAxis));
            }
            else
            {
                GameObject right = Instantiate(obj2) as GameObject;
                right.transform.position = new Vector3(transform.position.x - (float)(xAxis), (transform.position.y + (float)(yAxis)), transform.position.z + (float)(zAxis));
            }
        }

        
    }
    IEnumerator spawnT()
    {
        while(true)
        {   
            yield return new WaitForSeconds((float)spawnTime);
            spawnTargets();
        }
    }

    void Update()
    {
        //spawnT();
    } 

}
