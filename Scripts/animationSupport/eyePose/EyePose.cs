using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePose : MonoBehaviour
{
    // Start is called 
    public Transform trackingObj;
    [HideInInspector] 
    public Quaternion rotation;
    float MaxCameraRotX = 0.05f; //0.12 0.30 0.22 0.6 0.033
    float MinCameraRotX = -0.05f;//
    float MaxCameraRotY = 0.02f;//0.04 0.30 0.07 0.6 0.01
    float MinCameraRotY = -0.02f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setPose();
    }
    /*
    void Update()
    {
       Quaternion rotation = Quaternion.FromToRotation(object1.forward, object2.forward);

        // 회전을 오일러 각도로 변환합니다.
        Vector3 eulerAngle = rotation.eulerAngles;

        // 각 축에 대한 회전 각도를 얻습니다.
        float angleX = eulerAngle.x;
        float angleY = eulerAngle.y;
        float angleZ = eulerAngle.z;

        // 각 축에 대한 회전 각도를 출력합니다.
        Debug.Log("Angle X: " + angleX);
        Debug.Log("Angle Y: " + angleY);
        Debug.Log("Angle Z: " + angleZ);
    }*/
    void setPose(){
        Vector3 direction;
        direction = trackingObj.position - this.transform.position;
        //float r = direction.z;
        float rotX = direction.x;
        float rotY = direction.y;
        float rotZ = direction.z*20;
        //Debug.Log(direction);
        if (rotX > MaxCameraRotX*rotZ) {rotX = MaxCameraRotX*rotZ;}
        if (rotX < MinCameraRotX*rotZ) {rotX = MinCameraRotX*rotZ;}
        if (rotY > MaxCameraRotY*rotZ) {rotY = MaxCameraRotY*rotZ;}
        if (rotY < MinCameraRotY*rotZ) {rotY = MinCameraRotY*rotZ;}
        
        direction.x = rotX;
        direction.y = rotY;

        rotation = Quaternion.LookRotation(direction); 

    }

     public Quaternion Rotation {get {return rotation;}}
}
