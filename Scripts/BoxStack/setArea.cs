using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using JetBrains.Annotations;
using UnityEngine;

public class setArea : MonoBehaviour
{
    List<Vector3> topVector3Arr = new List<Vector3>();
    Dictionary<string,StackHeader> bulidArea = new Dictionary<string,StackHeader>();
    public GameObject silhouette;
    public GameObject table;
    GameObject colliderObject = null;
    float[] minMaxXY;
    bool isTel = false;
    Vector3 nearPosition;
    float distance ;
    const float LIMIT_DISTANCE = 0.5f;
    bool isStart = false;
    MeshRenderer silMeshRenderer;

    public int maxFloor = 3;
    /* 버택스기준 변형 하면안됌
    List<Vector3> GetTopVector(MeshFilter m){
         float max = 0;
        List<Vector3> arr = new List<Vector3>();
        Vector3[] v = m.mesh.vertices;
        foreach (Vector3 q in v){
            if(q.y > max)
                max = q.y;
        }
        foreach (Vector3 q in v){
            if(q.y == max){
                arr.Add(q);
            }
        }

        return arr;
    }   */

    float[] GetTopVector(Transform t,Vector3 silhouette){
        float maxX = t.localPosition.x + (t.localScale.x/2);
        float minX = t.localPosition.x - (t.localScale.x/2);
        float minY = t.localPosition.y + (t.localScale.y/2) + (silhouette.y/2) + silhouette.y;
        float maxY = minY +(silhouette.y*maxFloor);
        float maxZ = t.localPosition.z + (t.localScale.z/2);
        float minZ = t.localPosition.z - (t.localScale.z/2);
        float[] arr = {minX,minY,minZ,maxX,maxY,maxZ}; 
        return arr;
    }   

    public void InitDic(){
        foreach(var i in bulidArea){
            StackHeader sh =  bulidArea[i.Key];
            if(sh.GetFloor() == 0)
                sh.SetIsSet(true);
            else
                sh.SetIsSet(false);
        }
    }


    Dictionary<string,StackHeader> MakeBuildArea(Vector3 silhouette,float[] minMaxXY ){
        Dictionary<string,StackHeader> buildArea_ = new Dictionary<string,StackHeader>();
        float x,y,z;
        x = silhouette.x;
        y = silhouette.y;
        z = silhouette.z;

        for (int l = 0; l < maxFloor; l++){
            for (float i = minMaxXY[(int)AreaMinMax.MIN_X]; i <minMaxXY[(int)AreaMinMax.MAX_X]; i+=x){
                for (float j = minMaxXY[(int)AreaMinMax.MIN_Z]; j <minMaxXY[(int)AreaMinMax.MAX_Z]; j+=z){

                    float addX = (float)Math.Round(((int)(i/x)*x),2);
                    float addY = (float)Math.Round(((int)((minMaxXY[(int)AreaMinMax.MIN_Y]+(l*y))/y)*y),2);
                    float addZ = (float)Math.Round(((int)(j/z)*z),2);
                    buildArea_.Add(addX.ToString()+addY.ToString()+addZ.ToString(),new StackHeader(new Vector3(addX,addY,addZ),l));
                }
            }
        }
        return buildArea_;

    }

    Vector3 CalNearPosition(Vector3 obj, Vector3 silhouette, float[] minMaxXY){
        float x = (float)Math.Round(obj.x,2);
        float y = (float)Math.Round(obj.y,2);
        float z = (float)Math.Round(obj.z,2);
        if(minMaxXY[(int)AreaMinMax.MIN_X] > x){
            x = minMaxXY[(int)AreaMinMax.MIN_X] + silhouette.x;
        }
        else if(minMaxXY[(int)AreaMinMax.MAX_X] < x){
            x = minMaxXY[(int)AreaMinMax.MAX_X] - silhouette.x;
        }
        else{
            x = ((int)((x+0.02f)/silhouette.x)*silhouette.x);
        }

        if(minMaxXY[(int)AreaMinMax.MIN_Y] >= y){
            y = minMaxXY[(int)AreaMinMax.MIN_Y] ;
        }
        else if(minMaxXY[(int)AreaMinMax.MAX_Y] <= y){
            y = minMaxXY[(int)AreaMinMax.MAX_Y];
        }
        else{
            y = ((int)(y/silhouette.y)*silhouette.y);
        }

        if(minMaxXY[(int)AreaMinMax.MIN_Z] > z){
            z = minMaxXY[(int)AreaMinMax.MIN_Z] + silhouette.z;
        }
        else if(minMaxXY[(int)AreaMinMax.MAX_Z] < z){
            z = minMaxXY[(int)AreaMinMax.MAX_Z] - silhouette.z;
        }
        else{
            z = ((int)((z+0.02f)/silhouette.z)*silhouette.z);
        }
        return new Vector3(x,y,z);
    }

    public float CalDistance(Vector3 v,Vector3 me){
        return Vector3.Distance(me, v);
    }

    public bool IsNearObject(){
        return distance<LIMIT_DISTANCE;
    }

    void SilhouetteTel(GameObject sil,Vector3 near){
        sil.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        sil.transform.localPosition = near;

    }
    public void SetColliderObject(GameObject obj){
        colliderObject = obj;
    }
    public GameObject GetColliderObject(){
        return colliderObject;
    }

    public Vector3 GetNearPosition(){
        return nearPosition;
    }
    Vector3 CalIsTel(Vector3 v,Vector3 sil){
        string key= ((float)Math.Round(v.x,2)).ToString()+((float)Math.Round(v.y,2)).ToString()+((float)Math.Round(v.z,2)).ToString();
        int count = 0;
        if (bulidArea.ContainsKey(key)){
            StackHeader b  =  (StackHeader)bulidArea[v.x.ToString()+((float)Math.Round(v.y,2)).ToString()+v.z.ToString()];
            while(!b.GetIsSet()){
                float downY = (float)Math.Round(v.y-(sil.x*count),2);
                key = v.x.ToString()+downY.ToString()+v.z.ToString();
                if (bulidArea.ContainsKey(key)){
                    b  =  (StackHeader)bulidArea[key];
                    count++;
                }
                else  return new Vector3(999,999,999);
            }

        return b.GetPosition();
        }
        return new Vector3(999,999,999);

    }
    public bool GetIsTel(){
        return isTel;
    }

    public void SetIsSet(Vector3 v,bool b){
        string key = v.x.ToString()+v.y.ToString()+v.z.ToString();
        if (bulidArea.ContainsKey(key)){
            StackHeader p  =  (StackHeader)bulidArea[key];
            p.SetIsSet(b);
        }
    }
    public void initArea(){
        minMaxXY = GetTopVector(table.transform,silhouette.transform.localScale);
        bulidArea = MakeBuildArea(silhouette.transform.localScale,minMaxXY);
        isStart = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        silMeshRenderer = silhouette.GetComponent<MeshRenderer>();
        initArea();
    }


    // Update is called once per frame
    void Update() //충돌객체
    {
        if(!isStart)
            return;
        if(!(colliderObject is null)){
            nearPosition  = CalNearPosition(colliderObject.transform.position,silhouette.transform.localScale,minMaxXY);
            nearPosition = CalIsTel(nearPosition,silhouette.transform.localScale);
            distance = CalDistance(nearPosition,colliderObject.transform.position);
            if(distance <LIMIT_DISTANCE){
                SilhouetteTel(silhouette,nearPosition);
                isTel=true;
                silMeshRenderer.enabled = true;
            }
            else{
                isTel=false;
                silMeshRenderer.enabled = false;
            }
                
        }
        else{
            isTel=false;
        }

    }
}
