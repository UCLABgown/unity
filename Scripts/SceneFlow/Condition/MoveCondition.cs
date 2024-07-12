using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCondition : ConditionClass
{
    public Transform obj;
    public Transform target;
    public float speed = 1;
    private bool isStart = false;
    private Vector3 goal;

    // Start is called before the first frame update
    void Start()
    {
        goal = target.position;
        goal.y = obj.position.y;
        goal.x -= 1.709f;
        goal.z -= 1.04f;
    }

    Quaternion GetForward(){
        Vector3 dir = target.position - goal;
        dir.y = 0;
        Quaternion q = Quaternion.LookRotation(dir);
        //q.y = q.x;
        //q.x = 0;
        return q;
    }
    public override void AniStart(){
        isStart  = true;
        print("시작");
    }
    void Update()
    {
        if(isStart ){

            obj.position = Vector3.MoveTowards(obj.position, goal, speed*Time.deltaTime);
            //obj.rotation = GetForward();
            print(obj.position);
            if(obj.position == goal)    {state = true; isStart = false;}
        }
    }
}
