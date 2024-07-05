using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollider : MonoBehaviour
{
 
    public bool 충돌감지 =false;
    public GameObject 충돌객체;

    int 충돌감지숫자 = 0;
    void Update(){
        if(충돌감지숫자 > 0)
            충돌감지숫자--;
        else{
            Debug.Log(충돌감지);
            충돌감지 = false;
        }
    }
    private void OnCollisionStay(Collision c){
        if(c.gameObject.tag == "grab"){
            충돌객체 = c.gameObject;
            if(충돌감지숫자 < 10)
                충돌감지숫자 += 2;
            if(충돌감지숫자>=10){
                충돌감지 = true;
            }
        }

    }

}
