using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxType : int{
    NOTHING = -1,DOG = 0,FISH = 1,BIRD = 2,CAT = 3
}
public class FlowHeader : MonoBehaviour
{
    public string  type;
    public bool activity = false;
    bool prevActivity = false;
    bool state = false;



    private double time = 0f;
    public Transform area;
    private float squareSize; // 정사각형의 한 변의 길이
    private Vector3 squareCenter;
    public Transform Tomove;
    private Vector3 movePosition;


    public string GetType(){
        return type;
    }
    public bool GetState(){
        return state;
    }
    
    public void SetState(bool b){
        state = b;
    }
    public void SetActivity(bool act){
        activity = act;
    }

    void Start(){
        squareSize = area.localScale.x;
        squareCenter = area.position;
        movePosition = Tomove.position;
    }

    void Update(){
        if(activity && !prevActivity)
            state = true;
        prevActivity = activity;

        time= time + Time.deltaTime;
        if(time > 1){
            time = 0;
            // 물체의 현재 위치
            Vector3 objectPosition = transform.position;

            // 정사각형 영역의 경계를 계산
            Vector3 squareMin = squareCenter - Vector3.one * squareSize / 2;
            Vector3 squareMax = squareCenter + Vector3.one * squareSize / 2;

            // 물체가 정사각형 안에 있는지 확인
            if (objectPosition.x >= squareMin.x && objectPosition.x <= squareMax.x &&
                objectPosition.y >= squareMin.y && objectPosition.y <= squareMax.y &&
                objectPosition.z >= squareMin.z && objectPosition.z <= squareMax.z)
            {

            }
            else
            {
                this.transform.position = movePosition;
            }
        }
    }
}
