using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeObject : MonoBehaviour
{
    public GameObject center;
    public GameObject[] rigiObject;
    public GameObject[] outObject;
    public GameObject shilut;
    public initBLock initPostions;
    public float claNum;
    public Vector3 saveNum;
    public setArea sa;
    int count = 0;
    float time;
    public Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        startPosition = this.transform.position;
        SetRigidbody(false);
    }
    
    Vector3 CalPostion(){
        float calY = center.transform.position.y - claNum; //*0.85f;
        Vector3 setPosition = this.transform.position;
        setPosition.y = calY;
        return setPosition; 
    }

    void SetOutObject(Vector3 p){
        foreach(GameObject i in outObject){
            i.transform.position = i.transform.position - (startPosition - p);
        }
    }

    void SetRigidbody(bool b){
        foreach(GameObject i in rigiObject){
            for (int j = 0; j<i.transform.childCount; j++){
                i.transform.GetChild(j).GetComponent<Rigidbody>().useGravity = b;
            }
        }
    }

    void OnSetInitBlock(){
        if(initPostions !=null){
            initPostions.SetInitPostions();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        if (count <10){
            if((time>0.1)){
                count++;
                time = 0;
                saveNum += CalPostion();
            }
        }
        else{
            Vector3 v = saveNum/10;
            if(shilut != null)
                v.y = (int)(v.y/ shilut.transform.localScale.y)*shilut.transform.localScale.y;
            this.transform.position = v;
            SetOutObject(this.transform.position);
            SetRigidbody(true);
            this.GetComponent<ResizeObject>().enabled = false;
            if(!(sa is null))
                sa.initArea();
             OnSetInitBlock();        }
        
    }
}
