using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class textUpdate : MonoBehaviour
{

    public TMP_Text pText;
    public int fruitCount;
    // Start is called before the first frame update
    void Start()
    {   fruitCount = 0;
        pText.text = "x "+fruitCount.ToString();
    }

    

    public void updateText(){
        fruitCount++;
         pText.text = "x "+fruitCount.ToString();
    }
}
