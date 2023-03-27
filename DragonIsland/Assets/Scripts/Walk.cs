using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
   
public Animator anim;


    void Update(){
        if (Input.GetAxis("Horizontal") > 0.01 || Input.GetAxis("Horizontal") < -0.01 || Input.GetAxis("Vertical")  > 0.01 || Input.GetAxis("Vertical")  < -0.01 ){
            //Debug.Log("Walking");
            anim.SetBool("IsWalking", true);
           

        } else {
 anim.SetBool("IsWalking", false);
        }
       

       
        }
    }

