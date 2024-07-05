using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabTel : MonoBehaviour
{
    //잡은거 실루엣 텔포
    bool isgrab = false;
    bool isGrabObject =false;
    bool isPrevGrabObject =false;
    GameObject colliderObject = null;
    int colliderCount = 0;
    setArea sa ;
    float tTime = 0.0f;
    float uTime = 0.0f;
    public GameObject Area;
    public string stackObjectName = "rabbit";
    void Start(){
        sa = Area.GetComponent<setArea>();
    }
    public void GrabFalse(){
        isgrab = false;
    }

    public void GrabTrue(){
        isgrab = true;
    }
    public GameObject GetColliderObject (){
        return colliderObject;
    }
    void OnTriggerStay(Collider other) {
        tTime += Time.deltaTime;
        if(tTime > 0.1){
            if(other.name.Contains(stackObjectName)){
                colliderObject = other.gameObject;
            }
        }
    }

    void ObjectTel(GameObject obj,Vector3 near){
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        obj.transform.position = near;

    }
    void Update(){
        BlockState bs = colliderObject.GetComponent<BlockState>();
        bs.SetDragIfInDistance(sa.IsNearObject());
        if(isgrab){
            isPrevGrabObject = isGrabObject;
            isGrabObject = true;
            bs.SetIsKinematic(false);
            sa.SetColliderObject(colliderObject);
            sa.SetIsSet(bs.GetPostion(),true);
            sa.SetIsSet(bs.GetUpStairPosition(),false);
            bs.SetPosition(new Vector3(345,-234,123));
        }
        else{
            sa.SetColliderObject(colliderObject);
            isPrevGrabObject = isGrabObject;
            isGrabObject= false;
        }
        if(isPrevGrabObject && !isGrabObject){
            if(sa.GetIsTel()){
                Vector3 nearPosition = sa.GetNearPosition();
                sa.SetIsSet(nearPosition,false);
                ObjectTel(colliderObject,nearPosition);
                bs.SetPosition(nearPosition);       
                sa.SetIsSet(bs.GetUpStairPosition(),true);     
                }
            colliderObject = null;
            sa.SetColliderObject(colliderObject);
        }
        

    }

}
