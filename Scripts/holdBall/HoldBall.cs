using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HoldBall : MonoBehaviour
{
    float time;
    GameObject ball;
    public Transform goal;
    bool isGrab = false;
    bool isOut = false;
    bool isGrabBall = false;
    int ballCount = 0;
    int isIncludeViewArea = 100;
    void Start()
    {
        
    }
    public void GrabTrue(){
        isGrab = true;
    }
    public void GrabFalse(){
        isGrab = false;
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other) {
        String tagName = other.tag;
        if(tagName.Contains("ViewArea")){
            if(isIncludeViewArea < 10)
                isIncludeViewArea +=2;
        }
        else if(tagName.Contains("ball")){
            ballCount =10;
            ball = other.gameObject;
            isGrabBall = true;
        }
        else{
            if(ballCount > 0)
                ballCount--;
            else{
                isGrabBall = false;
            }
        }
    }
    Vector3 ForwardObject(Vector3 v1,Vector3 v2,int force){
        Vector3 vDist = v1-v2;
        Vector3 vDir = vDist .normalized;
        return vDir*force;
    }
    void Update(){
        if(isIncludeViewArea > 0 ){
            isIncludeViewArea--;
            if(isOut && !isGrab){
                ball.transform.position = this.transform.position;
                Vector3 forward = ForwardObject(goal.position,transform.position,1000);
                ball.GetComponent<Rigidbody>().AddForce(forward);
                isOut = false;
                ball = null;
            }

        }
        else{
            if(isGrab && isGrabBall){
                Debug.Log("벗어남");
                isOut = true;
            }
            if(isOut){
                ball.transform.position = this.transform.position;
            }
        }
    }
    
}
