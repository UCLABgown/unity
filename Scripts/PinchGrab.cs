using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class PinchGrab : MonoBehaviour
{   
    Quaternion previousRotation; //전 프레임의 로테이션 값
    Vector3 angularVelocity; //각속도를 관리할 변수    GameObject 충돌객체;
    int 충돌감지숫자 = 0;
    bool notgrabing = true;
    bool isgrab = false;
    // Start is called before the first frame update
    GameObject 충돌객체;
    private Vector3 oldPosition;
    private Vector3 currentPosition;
    private Vector3 velocity;
    Queue<Vector3> 가속도큐 = new Queue<Vector3>();
    Queue<Vector3> 각속도큐 = new Queue<Vector3>();

    void 큐추가(ref Queue<Vector3> 큐,Vector3 값){
        큐.Enqueue(값);
        if(큐.Count > 5)
            큐.Dequeue();


    }
    void 가속도_구하기(Transform 객체){
        currentPosition = 객체.position;
        var dis = (currentPosition - oldPosition);
        velocity = dis / Time.deltaTime;
        큐추가(ref 가속도큐,velocity);
        oldPosition = currentPosition;       
    }

    void 각속도_구하기(Transform 객체){
        Quaternion deltaRotation = 객체.rotation * Quaternion.Inverse(previousRotation);

        previousRotation = 객체.rotation;

        deltaRotation.ToAngleAxis(out var angle, out var axis);

		//각도에서 라디안으로 변환
        angle *= Mathf.Deg2Rad;

        angularVelocity = (1.0f / Time.deltaTime) * angle * axis;
        큐추가(ref 각속도큐,angularVelocity);

    }
    Vector3 백터3_평균_구하기(Queue<Vector3> 큐){
        Queue<Vector3>.Enumerator e =  큐.GetEnumerator();
        Vector3 v = new Vector3();
        float 빼는값  = 1.18f;
        while(e.MoveNext()){
            if(빼는값 == 1.18f){
                빼는값  -= 1.18f;
            }
            v = v+(e.Current*빼는값);
            빼는값  -= 1.18f;
        }
        return v/5;
    }
    void Start()
    {
        
    }
    


    private FixedJoint AddFixedJoint(){
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }
    
    public void setIsGrab(bool b){
        isgrab = b;
    }
    // Update is called once per frame
    private void OnTriggerStay(Collider c){
        if(c.gameObject.tag == "grab"){
            충돌객체 = c.gameObject;
            if(충돌감지숫자 < 8)
                충돌감지숫자 += 2;
        }

    }
    void Update()
    {
        if(충돌감지숫자 > 0){
            충돌감지숫자--;
            가속도_구하기(충돌객체.transform);
            각속도_구하기(충돌객체.transform);
            if(isgrab && notgrabing ){
                var joint = AddFixedJoint();
                joint.connectedBody = 충돌객체.GetComponent<Rigidbody>();
                notgrabing = false;


        }
        else if(!isgrab && !notgrabing){
            if(gameObject.GetComponent<FixedJoint>())
            {
                notgrabing = true;
                gameObject.GetComponent<FixedJoint>().connectedBody = null;
                Destroy(gameObject.GetComponent<FixedJoint>());
                Debug.Log(velocity.ToString()+" "+angularVelocity.ToString());
                //충돌객체.GetComponent<Rigidbody>().velocity = velocity;
                충돌객체.GetComponent<Rigidbody>().AddForce(백터3_평균_구하기(가속도큐)*200);
                충돌객체.GetComponent<Rigidbody>().angularVelocity = 백터3_평균_구하기(각속도큐);
            }
        }
        }


    }
}
