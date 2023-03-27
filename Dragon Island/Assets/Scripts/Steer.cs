using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steer : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 angularVelocityRight;
     public Vector3 angularVelocityLeft;
    public KeyCode[] SteerKeys;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKey(SteerKeys[0])){

            Quaternion deltaRotation = Quaternion.Euler(angularVelocityRight * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
        }

         if (Input.GetKey(SteerKeys[1])){

            Quaternion deltaRotation = Quaternion.Euler(angularVelocityLeft * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
        }
        
    }
}
