using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


public enum AreaMinMax : int{
    MIN_X = 0,MIN_Y = 1,MIN_Z = 2,MAX_X = 3,MAX_Y = 4,MAX_Z = 5
}

public class StackHeader{
    Vector3 position;
    bool isSet;
    int floor;

    public StackHeader(){
        position = new Vector3(0,0,0);

        isSet = false;
        floor = 0;

    }
    public StackHeader(Vector3 v,int f){
        position = v;
        floor = f;
        if(floor == 0 )
            isSet = true;
        else
            isSet = false;

    }
    public int GetFloor(){
        return floor;
    }

    public bool GetIsSet(){
        return isSet;
    }

    public void SetIsSet(bool s){
        isSet = s;
        Debug.Log("ok");
    }

    public Vector3 GetPosition(){
        return position;
    }

    public void SetPosition(Vector3 v){
        position = v;
    }

}

