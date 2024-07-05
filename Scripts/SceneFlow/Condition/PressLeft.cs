using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressLeft : ConditionClass
{
    void Update(){
        if(Input.GetKeyDown(KeyCode.LeftArrow))
            SetState(true);
        else
            SetState(false);
    }
}
