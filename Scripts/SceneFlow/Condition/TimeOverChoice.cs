using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeOverChoice : ConditionClass 
{
    bool isFlow = false;
    public float limitTime = 0;
    void StopTimer(){
        isFlow = false;
    }
    
    void StartTimer(){
        isFlow = true;
    }

    void SetTime(float t){
        time = t;
    }
    public void AddTime(float t){
        time +=t;
    }
    public void ReStart(){
        time = 0;
    }
    public override void AniOver(){
         StartTimer();
    }
    public override void AniStart(){
         StopTimer();
    }
    void Update(){
        if(isFlow){
            AddTime(Time.deltaTime);
            if(time > limitTime ){
                state = true;
                AniStart();
                time = 0;
            }
        }
    }
}
