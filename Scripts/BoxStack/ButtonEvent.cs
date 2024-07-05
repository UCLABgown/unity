using System.Collections;
using System.Collections.Generic;
using Oculus.Platform;
//using UnityEditor.SearchService;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    int count = -1;
    int prevCount = -1;
    public GameObject[] blocks;
    // Start is called before the first frame update
    void Start()
    {
        AllRender(false);
        count = 0;
    }
    void EnableRender(GameObject obj){
        for (int i = 0; i < obj.transform.childCount; i++){
            obj.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
        }
    }
    void AllRender(bool b){
        foreach(GameObject obj in blocks){
            for (int i = 0; i < obj.transform.childCount; i++)
            obj.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = b;
        }
    }


    public void IncreasCount(){
        count++;
    }
    public void SetCount(int n ){
        count = n;
    }

    // Update is called once per frame
    void Update()
    {
        if(count != prevCount){
            AllRender(false);
            EnableRender(blocks[count]);
        }
        prevCount = count;
    }
}
