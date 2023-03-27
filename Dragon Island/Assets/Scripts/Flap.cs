using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flap : MonoBehaviour
{
    public GameObject RightWing;
    public GameObject LeftWing;
    public KeyCode keycode;
    public float[] RightWingPositions;
     public float[] LeftWingPositions;

public int FlightHeight;
   

    // Update is called once per frame
    void Update()
    {
      if (Input.GetButtonDown("Jump")||Input.GetKeyDown(keycode)){
        
            RightWing.GetComponent<Transform>().Rotate( RightWingPositions[1], 0.0f, 0.0f);
             LeftWing.GetComponent<Transform>().Rotate( LeftWingPositions[1], 0.0f, 0.0f);

  
  

        }

        if (Input.GetButtonUp("Jump")||Input.GetKeyUp(keycode)){
            RightWing.GetComponent<Transform>().Rotate( RightWingPositions[0], 0.0f, 0.0f);
             LeftWing.GetComponent<Transform>().Rotate( LeftWingPositions[0], 0.0f, 0.0f);
        }
     

    }
    
    

    
   
}
