using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class claps : MonoBehaviour
{
    int[] soundCurrent = {0,1,2,3,4,6,7};
    int soundNo = 5;
    int n= 0;
    float time = 0.0f;
    public static claps instance = null;
        void Awake()
    {
        if (instance == null)
        {
            instance = this; //생성
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DecreasCountSound(){
        if(n >0)
            n--;

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public int GetNCount(){
        return n;
    }
    private void OnCollisionEnter(Collision collision)
    {
        time += Time.deltaTime;
        if(time > 1.0f){
            if(collision.gameObject.name.Contains("CollLeft") || collision.gameObject.name.Contains("CollRight")){
                BlockPlaySound.instance.Voice(soundCurrent[n]);
                n++;
                time = 0;
            }
        }

    }
}
