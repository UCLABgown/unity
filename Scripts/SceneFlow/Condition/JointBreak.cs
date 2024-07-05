using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JointBreak : ConditionClass
{
    public GameObject jointObject;
    private Joint j;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        j = jointObject.GetComponent<FixedJoint>();
        if(j == null){
             SetState(true);
        }
        else{
             SetState(false);
        }
    }
}
