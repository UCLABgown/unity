using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeSightnearPosition : MonoBehaviour
{


    int time = 0;
    public GameObject obj2;
    GameObject obj1;
    bool b;
    int count = 0;
    void Start(){
        obj1 = this.gameObject;
        b = false;
    }
    public void setFalse(){
        b = false;
    }
    public void setTrue(){
        b = true;
    }
    void move(){
        Debug.Log(this.name);
        if(count == 0){
            this.transform.localScale = new Vector3(0.25f,0.25f,0.25f);
        }
        if(count == 1){
            this.transform.localScale = new Vector3(0.15f,0.15f,0.15f);
            Vector3 v1 = obj1.GetComponent<Transform>().position;
            Vector3 v2 = obj2.GetComponent<Transform>().position;
            Vector3 vDist = v1-v2;
            Vector3 vDir = vDist .normalized;
            float fDist = vDist.magnitude;
            if(fDist > 0.5f)
                obj1.GetComponent<Transform>().position -=vDir * 0.4f;
        }
        if(count == 2){
            this.transform.localScale = new Vector3(0.25f,0.25f,0.25f);
        }

        count++;
    }

    void Update(){
        if(b){
            time++;
            Debug.Log(time);
            if(time > 100){
                move();
                time = 0;
            }

        }
    }
}
