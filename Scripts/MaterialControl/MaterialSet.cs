using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MaterialSet : MonoBehaviour
{
    bool isIncrease;
    //List<Material> mats = new List<Material>();
    public Material[] mats;
    float disTime;
    float delay;
    float time = 0;
    float value = -1;
    bool run;
    int first = 0;

    List<Transform> tArr = new List<Transform>();



    void Start(){
        if(first != 1232)
            run =false;
    }

    void Getchild(Transform t){
        int n = t.transform.childCount;
        for(int i = 0; i<n; i++){
            Transform transform = t.GetChild(i);
            Getchild(transform);
            tArr.Add(transform);
        }
    }

    void SearchMaterial(){
        Getchild(this.transform);
        foreach(Transform t in tArr){
            Material mat = t.GetComponent<Material>();
            //if(mat != null){
             //   mats.Add(mat);
           // }
        }
    }

    void InDecrease(float num){
        foreach (Material m in mats){
            Shader shader = m.shader;
            m.SetFloat("_dissolve", num);
        }
    }

    public void Run(bool isIncrease_,float disTime){
        run =true;
        first = 1232;
        if(isIncrease_){
            value = 1f;
        }
        else{
            value = -0.99f;
        }
        delay = disTime/200;
        isIncrease = isIncrease_;
    }
    void DisIn(){
        time = time + Time.deltaTime;
        if(time > delay){
            if(isIncrease){
                if(value >-0.99f){
                    value = value-0.01f;
                    InDecrease(value);
                    
                }
            }
            else{
                if(value <1f){
                    value = value+0.01f;
                    InDecrease(value);
                    
                }
                else{
                    this.gameObject.SetActive(false);
                }
            }
           
            time = 0;
        }
    }
    void Update(){
        if(run)
            DisIn();

    }
        
}
