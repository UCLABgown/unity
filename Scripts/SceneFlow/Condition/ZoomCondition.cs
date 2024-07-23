using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCondition : ConditionClass
{



    public Transform obj2;
    public Transform obj1;
    private bool isStart = false;
    bool b;
    int num = 0;
    public float speed = 0;
    public float timeLimit = 0;
    Vector3 savePostion;
    void Start(){

        b = false;
    }

    void move(Vector3 target){
        Vector3 v1 = obj1.position;

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
                    time += Time.deltaTime;
                    if(timeLimit < time)
                        num++;
                    break;
                case 3:
                    move(savePostion);
                    break;
                case 4:
                    state = true;
                    this.gameObject.active = false;
                    break;
            }
        }

    }
}
