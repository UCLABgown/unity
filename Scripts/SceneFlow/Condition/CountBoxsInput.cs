using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountBoxsInput : ConditionClass
{
    public BoxInput[] boxs;
    public int maxCounts;

    void Start(){
        foreach(BoxInput b in boxs){
            b.SetCount(0);
        }
    }


    // Update is called once per frame
    void Update()
    {
        int n = 0;
        foreach(BoxInput b in boxs){
            n = n +b.GetCount();
            if(n >= maxCounts)
                SetState(true);
            print(n);
        }
    }
}
