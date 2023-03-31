using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace godzillabanana{
public class FlowerManagement : MonoBehaviour
{
    public GameObject[] petals;
    public TMP_Text Divination;
    public string[] divinationOptions;
    private int FlowerFaceValue;
    public Sprite[] calmFaces;
    public Sprite agFace;
    public Image flowerFace;
    public GameObject petalAnim;
    public GameObject animCenter;
   public GameObject reset;
   public RandomSoundScript randSound;
   public Effects greyscale;
   public Animator anim;
   void Start(){
    FlowerFaceValue = 0;
   }
       
    

    public void Pluck(int c){
                anim.SetTrigger("Flip");
                randSound.CallAudio();
                petals[c].SetActive(false);
                Divination.text = divinationOptions[Random.Range(0, divinationOptions.Length)];
                 FlowerFaceValue++;
                StartCoroutine(faceExpress(FlowerFaceValue));
                 animCenter.GetComponent<Transform>().position = petals[c].GetComponent<Transform>().position;
                animCenter.GetComponent<Transform>().rotation = petals[c].GetComponent<Transform>().rotation;
                 GameObject petal = Instantiate(petalAnim, new Vector3(petals[c].GetComponent<Transform>().position.x, petals[c].GetComponent<Transform>().position.y, petals[c].GetComponent<Transform>().position.z), Quaternion.Euler(0, 0, petals[c].GetComponent<Transform>().rotation.z)) as GameObject;
                 petal.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
                 greyscale.styleStrength+= 0.16f;
                 if (FlowerFaceValue >= 5)
                 {
                    reset.SetActive(true);
                 }
             }
           

           
    


    IEnumerator faceExpress(int i){       

        flowerFace.sprite = agFace;
        yield return new WaitForSeconds(.1f);
       
        flowerFace.sprite = calmFaces[FlowerFaceValue];

        }

 

}
}
