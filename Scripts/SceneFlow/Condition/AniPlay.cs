using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniPlay : ConditionClass
{
    public Animator[] anim;
    public float limitTime = 1;
    public bool checkNotOver = true;
    private bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        StopAni();
    }

    void PlayAni(){
        foreach (var i in anim){
            i.speed = 1;

        }
    }
    void StopAni(){
        foreach (var i in anim){
            i.speed = 0;
        }
    }
    
    public override void AniStart(){
        isStart  = true;
        this.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(isStart){
            if(time == 0 ) PlayAni();
            time += Time.deltaTime;
            if(time >limitTime) {
                if(!checkNotOver)
                    state = true;
                StopAni();

                this.enabled = false;
            }
        }
    }
}
