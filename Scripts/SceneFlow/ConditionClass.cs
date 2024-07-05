using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ConditionClass : MonoBehaviour
{

    protected bool state = false;
    string type = "";
    public float time = 0;
    int count = 0;
    public void AddCount(){
        count++;
    }
    public int GetCount(){
        return count;
    }
    public bool GetState(){
        return state;
    }

    public string GetType(){
        return type;
    }

    public float GetTime(){
        return time;
    }

    public void SetState(bool b){
        state = b;
    }

    public void SetType(string c){
        type = c;
    }
    public void SetTime(float t){
        time = t;
    }
    public void SetCount(int n){
        count = n;
    }
    public virtual void AniOver(){
    }
    public virtual void AniStart(){
    }

    public void Init(){
        state = false;
        type = "";
        time = 0;
        count = 0;
        AniStart();
    }


}
