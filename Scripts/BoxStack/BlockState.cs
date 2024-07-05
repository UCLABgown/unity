using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//using UnityEditorInternal;
using UnityEngine;

public class BlockState : MonoBehaviour
{
    Vector3 key = new Vector3(346,876,-134);
    public Vector3 initPosition;
    public void SetInitPosition(){
        initPosition = this.transform.position;
    }
    public void InitBlock(){
        this.transform.position = initPosition;
    }
    public Vector3 GetPostion(){
        return key;
    }
    public void SetPosition(Vector3 v){
        key = v;
    }
    public Vector3 GetUpStairPosition(){
        Vector3 v = key;
        v.y = v.y + this.transform.localScale.y;
        return v;
    }
    public void SetIsKinematic(bool b,float distance){

        this.GetComponent<Rigidbody>().isKinematic = b;
    }
    public void SetIsKinematic(bool b){
        
        this.GetComponent<Rigidbody>().isKinematic = b;
    }
    public void SetDragIfInDistance(bool b){
        if(b)
            this.GetComponent<Rigidbody>().drag = 100;
        else
            this.GetComponent<Rigidbody>().drag = 5;
    }

    internal void SetIsSet(bool v)
    {
        throw new NotImplementedException();
    }

    public static implicit operator BlockState(StackHeader v)
    {
        throw new NotImplementedException();
    }
}
