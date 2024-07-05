using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initBLock : MonoBehaviour
{

    public void SetInitPostions(){
        for(int i = 0; i< this.transform.childCount; i++){
            this.transform.GetChild(i).GetComponent<BlockState>().SetInitPosition();
        }
    }

    public void InitBlocks(){
        for(int i = 0; i< this.transform.childCount; i++){
            this.transform.GetChild(i).GetComponent<BlockState>().InitBlock();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
