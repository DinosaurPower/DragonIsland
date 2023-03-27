using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
   
    public float radius;
    public void Find(Vector3 center)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        
        Debug.Log(center);
        foreach (var hitCollider in hitColliders)
        {
           if (hitCollider.CompareTag("Dragonfruit")){
            Destroy(hitCollider.gameObject);
            Debug.Log("Yay");
           }
        }


     void OnDrawGizmosSelected() {
     Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     Gizmos.DrawWireSphere (center, radius);
 }


    }
}
