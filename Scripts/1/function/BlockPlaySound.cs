using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlaySound : MonoBehaviour
{
    int saveNum = 0;
    public AudioClip[] arr;
    public static BlockPlaySound instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; //생성
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int nowAniNum(){
        return saveNum;
    }

    public void Voice(int num,float time = 0.8f){
        saveNum = num;
        this.GetComponent<AudioSource>().Stop();
        this.GetComponent<AudioSource>().PlayOneShot(arr[num], 0.8f);
    }
}
