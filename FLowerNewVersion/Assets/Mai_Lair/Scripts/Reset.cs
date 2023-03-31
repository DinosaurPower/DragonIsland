using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace godzillabanana{
public class Reset : MonoBehaviour
    {

        public Scene sampleScene;
        public AudioSource Click;
        public float timer = 0.1f;
        public string switchSceneWhenSoundIsOver = "";
   

         void Update()
        {
            
            if( (switchSceneWhenSoundIsOver!="") && !Click.isPlaying) {
                SceneManager.LoadScene(switchSceneWhenSoundIsOver);
                switchSceneWhenSoundIsOver = "";
            }
        }

        public void PlayBtn(string game)
        {

            Click.time = timer;
            Click.Play();
            switchSceneWhenSoundIsOver = game;
            
           
        }


    }
}