using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollider : MonoBehaviour
{

    bool isOnlyOneStart = true;
    /*
    private void OnCollisionEnter (Collision o){
        string n = o.gameObject.name;
        if(n.Contains("cardboardBox") && isOnlyOneStart){
            main.instance.increaseCountBlock();
            isOnlyOneStart = false;
    }
    }*/
    private void OnTriggerEnter(Collider o){
        string n = o.gameObject.name;
        if(n.Contains("colliderBox") && isOnlyOneStart){
            main.instance.increaseCountBlock();
            isOnlyOneStart = false;
    }
    }

}
