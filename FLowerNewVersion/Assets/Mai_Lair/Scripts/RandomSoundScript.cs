using UnityEngine;
 using System.Collections;
 
 public class RandomSoundScript : MonoBehaviour {
 
     public AudioSource randomSound;
 
     public AudioClip[] audioSources;
 
     // Use this for initialization
     void Start () {
 
        
     }
 
 
    public void CallAudio()
     {   Debug.Log("Invoking");
         Invoke ("RandomSoundness", 0.01f);
     }
 
     void RandomSoundness()
     {
        Debug.Log("Playing");
         randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
         randomSound.Play ();
     }
 }