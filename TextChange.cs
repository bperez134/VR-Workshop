using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour
{
    public Text cText;
    public static int score = 0;
    //public GameObject instance;

    void Start()
    {
        Text();
    }
    void Update()
    {
        Text();
    }
    public void Text()
    {   
        //string test = instance.transform.position.ToString();
        cText.text = score.ToString();
    }
}
