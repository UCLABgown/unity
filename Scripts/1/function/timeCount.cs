using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeCount : MonoBehaviour
{
    private float tTime = 0;
     private float time_start;
    private float time_current;
    private float time_Max = 5f;
    private bool isEnded;
    public static timeCount instance = null;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this; //생성
        }
    }

    void Start()
    {
        Reset_Timer();
    }

    private IEnumerator timeFlow(){
        while(true){
            Check_Timer();
            yield return new WaitForSeconds(0.1f);
        }

    }

    void Update(){
        tTime += Time.deltaTime;
        if(tTime > 0.1){
            tTime = 0;
            Check_Timer();
        }
    }

      private void Check_Timer()
    {
        time_current = (float)System.DateTime.Now.TimeOfDay.TotalSeconds - time_start; 
        main.instance.UpdateTime(time_current);
    }


    public void Reset_Timer()
    {
        time_start = (float)System.DateTime.Now.TimeOfDay.TotalSeconds;
        time_current = 0;
        isEnded = false;
        Debug.Log("Start");
    }

    
}
