using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeform : MonoBehaviour
{
    public Mesh deformingMesh;
    Vector3[] originalVertices, displacedVertices;
    Vector3[] vertexVelocities;

    public float force = 10f;
    public float forceOffest = 0.1f;
    public GameObject item;
    //public Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        //deformingMesh = GetComponent<MeshFilter>().mesh;
        originalVertices = deformingMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        
        Debug.Log(originalVertices.Length);
        for(int i = 0; i < originalVertices.Length; i++)
        {
            displacedVertices[i] = originalVertices[i];
            //Debug.Log(originalVertices[i]);
        }

        vertexVelocities = new Vector3[originalVertices.Length];
        //deformingMesh.RecalculateBounds();
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.contacts.Length);
        Debug.Log("Hit Detected");
        //Displacement(other);
    }
    */
    void OnCollisionEnter(Collision collision) 
    {
        //ContactPoint contact = collision.contacts[0];  
        //Debug.Log("Hit detected");  
        Displacement(collision);
    }

    public Vector3 testPoint;
    void Displacement(Collision other)
    {
        //MeshDeform deformer = other.gameObject.GetComponent<MeshDeform>();
        //Debug.Log(deformer);
        //ContactPoint contact = other.contacts[0];
        //contact.point;
        if(true)
        {
            //Vector3 point = gameObject.GetComponent<Collider>().ClosestPointOnBounds(other.transform.position);
            //Vector3 point = contact.point;
            Vector3 point;
            //Vector3 testPoint = new Vector3(-.4f, .4f, -.9f);
            //Debug.Log(transform.position);
            //Debug.Log(other.contacts[0]);
            //point *= -1f;
            //point += point * forceOffest;
            //point = (transform.position - point) * .5f;
            point = other.transform.position;
            //Vector3 pos = new Vector3(po);
            //GameObject e = Instantiate(item, point, transform.rotation) as GameObject;
            //Debug.Log("Hands position: " + point);
            //Debug.Log("Transform position: "  + transform.position);
            //Debug.Log("Deforms position: " + (point - transform.position));
            //Destroy(e.gameObject, 1f);
            AddDeformingForce(point, force);
        }
        //deformingMesh.RecalculateBounds();
         
    }

    void AddDeformingForce(Vector3 point, float force)
    {
        point = transform.InverseTransformPoint(point);
        for(int i = 0; i < displacedVertices.Length; i++)
        {
            AdddForceToVertex(i, point, force);
        }
    }

    void AdddForceToVertex(int i, Vector3 point, float force)
    {
        Vector3 pointToVertex = displacedVertices[i] - point;
        float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
        float velocity = attenuatedForce * Time.deltaTime;
		vertexVelocities[i] += pointToVertex.normalized * velocity;
    }
    // Update is called once per frame
    float uniformScale = 1f;
    void Update()
    {
        uniformScale = transform.localScale.x;
        for (int i = 0; i < displacedVertices.Length; i++) {
			UpdateVertex(i);
		}
		deformingMesh.vertices = displacedVertices;
		deformingMesh.RecalculateNormals();
    }

    public float springForce = 20f;
    public float damping = 5f;

    void UpdateVertex (int i) 
    {
		Vector3 velocity = vertexVelocities[i];
        Vector3 displacement = displacedVertices[i] - originalVertices[i];
        displacement *= uniformScale;

        velocity -= displacement * springForce * Time.deltaTime;
        velocity *= 1f - damping * Time.deltaTime;
        vertexVelocities[i] = velocity;
		displacedVertices[i] += velocity * (Time.deltaTime / uniformScale);
	}
}
