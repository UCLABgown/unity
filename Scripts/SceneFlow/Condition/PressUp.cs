using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressUp : ConditionClass
{
    void Update(){
        if(Input.GetKeyDown(KeyCode.UpArrow))
            SetState(true);
        else
            SetState(false);
    }
}
