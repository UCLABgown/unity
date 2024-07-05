using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaterialInit : MonoBehaviour
{
    public Material[] matsActivity;
    public Material[] matsDeactivity;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Material m in matsActivity){
            Shader shader = m.shader;
            m.SetFloat("_dissolve", -0.99f);
        }
        foreach (Material m in matsDeactivity){
            Shader shader = m.shader;
            m.SetFloat("_dissolve", 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
