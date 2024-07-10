using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInput : ConditionClass
{
    // Start is called before the first frame update
    HashSet<string> flowHead = new HashSet<string>();
    void OnTriggerStay(Collider other) {
        FlowHeader f = other.GetComponent<FlowHeader>();
        if((f != null) && f.GetState()){
            string t = f.GetType();
                if(flowHead.Add(f.name)){
                    SetState(true);
                    SetType(t);
                    AddCount();
                    f.SetState(false);
                    print("들어감");
                }
                f.SetActivity(false);

            }
        }

}
