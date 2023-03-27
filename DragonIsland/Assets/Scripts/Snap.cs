using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    public GameObject upperJaw;
    public GameObject lowerJaw;
    public KeyCode keycode;
    public float[] UpJawPositions;
     public float[] LowJawPositions;
     public int SnapValue;
     public int SnapCost;
    public Loot loot;

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(keycode)){
        if (SnapValue <= 0){
           
            SnapValue = 1;
            loot.Find(upperJaw.GetComponent<Transform>().position);
            // Debug.Log(SnapValue);
        } else {
            SnapValue = 0;
        }
        
        JawReact(SnapValue);

             }
    
    }

    
    void JawReact(int SnapCost){

            upperJaw.GetComponent<Transform>().Rotate( 0.0f, UpJawPositions[SnapCost], 0.0f);
             lowerJaw.GetComponent<Transform>().Rotate( 0.0f, LowJawPositions[SnapCost], 0.0f);

    }
}
