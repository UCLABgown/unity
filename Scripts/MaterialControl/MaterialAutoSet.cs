using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MaterialAutoSet : MonoBehaviour
{
    bool isIncrease;
    [HideInInspector]
    public List<Material> mats = new List<Material>();
    float disTime;
    float delay;
    float time = 0;
    float value = -1;
    bool run = false;
    int first = 0;

    List<Transform> tArr = new List<Transform>();
    public Transform[] scene;



    void Start(){
            SearchMaterial();

    }

    void Getchild(Transform t){
        int n = t.childCount;
            for(int i = 0; i<n; i++){
                Transform transform = t.GetChild(i);
                Getchild(transform);
                tArr.Add(transform);
                
            }
    }

    void SearchMaterial(){
        foreach(Transform t in scene){
            
            Getchild(t);
        }
        foreach(Transform t in tArr){
            Renderer render = t.GetComponent<Renderer>();
            if(render != null){
                Material[] mat = render.materials;
                foreach(Material m in mat){
                    if(m.name.Contains("Dissolve")){
                        mats.Add(m);
                    }
                }
            }
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
        delay = 0.001f;//disTime/200;
        isIncrease = isIncrease_;
    }
    void DisIn(){
        if(isIncrease) InDecrease(-0.99f);
        else InDecrease(0.99f);
        /*
        time = time + Time.deltaTime;
        if(time > delay){
            if(isIncrease){
                if(value >-0.99f){
                    value = value-0.02f;
                    InDecrease(value);
                    
                }
            }
            else{
                if(value <1f){
                    value = value+0.02f;
                    InDecrease(value);
                    
                }
                else{
                    this.gameObject.SetActive(false);
                }
            }
           
            time = 0;
        }
        */
    }
    void Update(){
        if(run)
            DisIn();

    }
        
}
