using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineCondition : ConditionClass
{
    // Start is called before the first frame update
    public Outline[] outline;
    public float limitTime; 
    private bool prev = false;
    private bool isStart = false;

    void Start()
    {
        
    }
    public override void AniStart(){
        isStart  = true;
        gameObject.active = true;

    }
    public override void AniOver(){
        isStart  = false;
        gameObject.active = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(isStart){
            if(isStart && !prev){
                foreach(var o in outline){
                    o.OutlineWidth = 3;
                }
            }
            if((limitTime < time) || (!isStart && prev)){
                foreach(var o in outline){
                    o.OutlineWidth = 0;
                state = true;
                gameObject.active = false;
                }
            }    
            time += Time.deltaTime;
            prev = isStart;
        }
    }
}
