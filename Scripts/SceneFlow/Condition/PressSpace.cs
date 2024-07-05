using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressSpace : ConditionClass
{
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space))
            SetState(true);
        else
            SetState(false);
    }
}
