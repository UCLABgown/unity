using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaterialDissoble : MonoBehaviour
{
    public Material[] mats;
    [HideInInspector] 
    public float value;

    void Start(){
        value = -1f;
    }
    void Update(){
        if((value>-0.99) && (value<=1))
        foreach (Material m in mats){
            Shader shader = m.shader;
            m.SetFloat("_dissolve", value);

        }
    }
        
        
    
}
