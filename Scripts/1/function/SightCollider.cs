using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightCollider : MonoBehaviour
{
    private float tTime = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update(){
        tTime += Time.deltaTime;
        if(tTime > 3){
            main.instance.setIsSight(false);
        }
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider o){
        string n = o.gameObject.name;
        if(n.Contains("_sight") ){
            main.instance.setIsSight(true);
            tTime = 0;
        }

    }
}
