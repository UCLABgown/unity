using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JointBreak : ConditionClass
{
    public GameObject jointObject;
    public AnimationGrabVer2 grab;
    private Joint j;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(grab.Getstate())
             SetState(true);
        
        else
             SetState(false);
        
    }
}
