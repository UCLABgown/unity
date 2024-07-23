using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCondition : ConditionClass
{



    public Transform obj2;
    public Transform obj1;
    private bool isStart = false;
    bool b;
    int num = 0;
    public float speed = 0;
    Vector3 savePostion;
    void Start(){

        b = false;
    }

    void move(Vector3 target){
        Vector3 v1 = obj1.position;
        v1.y = 0;
        target.y=0;
        Vector3 vDist = v1-target;
        Vector3 vDir = vDist .normalized;
        float fDist = vDist.magnitude;
        if(fDist < 0.07f){
            num ++;
        }
        obj1.position -=vDir * speed;

    }
    public override void AniStart(){
        isStart  = true;
        print("시작");
    }
    void Update(){
        if(isStart){
            switch(num ){
                case 0:
                    savePostion = obj1.position;
                    num ++;
                    break;
                case 1:
                    move(obj2.position);
                    break;
                case 2:
                    state = true;
                    this.gameObject.active = false;
                    break;
            }
        }

    }
}
