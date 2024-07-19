using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightCOnidtion : ConditionClass
{
    public EyeGazeObserver3D eyeGazeObserver3D;
    public float limitTime;
    private float time;
    private string saveObj;
    private bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void AniStart(){
        isStart  = true;
            }
    // Update is called once per frame
    void Update()
    { 
        if(isStart){
            string obj = eyeGazeObserver3D.GetSightObJ();
            if(obj != "0"){
                if(saveObj == obj){
                    time += Time.deltaTime;
                    if(time > limitTime){
                        state = true;
                        gameObject.active = false;
                    }
                else time = 0;
                saveObj = obj;
                }
            }
        }
    }
}
