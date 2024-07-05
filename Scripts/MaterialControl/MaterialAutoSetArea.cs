using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MaterialAutoSetArea : MaterialAutoSet
{
    int n = 0;
    HashSet<string> arr = new HashSet<string>();
    void OnCollisionStay (Collision c){
        if(arr.Add(c.gameObject.name)){
            Renderer render = c.gameObject.GetComponent<Renderer>();
            if(render != null){
                Material[] mat = render.materials;
                foreach(Material m in mat){
                    if(m.name.Contains("Dissolve")){
                        mats.Add(m);
                    }
                }
            }
        }
        else
            mats.Clear();
    }




        
}
