using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class viewSCore : MonoBehaviour
{
    public GameObject[] ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitBall(){
        foreach(GameObject obj in ball ){
            obj.GetComponent<stopBall>().InitBall();
        }
        SetText("0");
    }
    void SetText(String str){
        this.GetComponent<TMP_Text>().text = str;
    }

    // Update is called once per frame
    void Update()
    {
        int score = 0;
        foreach(GameObject obj in ball ){
            int s = obj.GetComponent<stopBall>().getScore();
            if(s != 0){
                score += obj.GetComponent<stopBall>().getScore();
            }
        }
        SetText(score.ToString());
        
    }
}
