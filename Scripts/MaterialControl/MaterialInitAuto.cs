using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaterialInitAuto : MonoBehaviour
{
    public Transform[] transActivity;
    public Transform[] transDeactivity;
    List<Transform> tArr = new List<Transform>();
    List<Material> mats = new List<Material>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in transActivity){
            SearchMaterial(t);
        }
        foreach (Material m in mats){
            Shader shader = m.shader;
            m.SetFloat("_dissolve", -0.99f);
        }
        mats.Clear();
        tArr.Clear();
        
        foreach(Transform t in transDeactivity){
            SearchMaterial(t);
        }
        foreach (Material m in mats){
            Shader shader = m.shader;
            m.SetFloat("_dissolve", 1f);
        }
    }

    void Getchild(Transform t){
        int n = t.transform.childCount;
        for(int i = 0; i<n; i++){
            Transform transform = t.GetChild(i);
            Getchild(transform);
            tArr.Add(transform);
        }
    }
    void SearchMaterial(Transform scene){
        Getchild(scene.transform);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
