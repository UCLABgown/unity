using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class main : MonoBehaviour
{
    public int SceneNum;
    int[] soundCurrent = {0,1,2,3,4,6,7};
    private int countBlock = 0;
    private int functionNum = 0;
    private Double time = 0;
    private bool isAniOver = false;
    private int isAniOverCount = 0;
    private int soundNum = 0;
    private bool isSight = false; 
    private float tTime = 0;
    private bool isNoAni = false;
    mainScene1 scene1;
    mainScene2 scene2;
    mainScene3 scene3;
    mainScene4 scene4;
    mainSceneSeparate sceneseparate ;


    void Start(){
        scene1 = new mainScene1();
        scene2 = new mainScene2();
        scene3 = new mainScene3();
        scene4 = new mainScene4();
        sceneseparate = new mainSceneSeparate();

    }
    public void setisNoAni(bool s){
        isNoAni = s;
    }
    public void setIsSight(bool s){
        isSight = s;
    }

    public static main instance = null;
        void Awake()
    {
        if (instance == null)
        {
            instance = this; //생성
        }
    }

    public void UpdateTime(float t){
        time = t;
    }
    public void increaseCountBlock(){
        countBlock++;
    }

    public void setIsNaiOver(bool isAni,int num){
        isAniOver = isAni;
        isAniOverCount = num;
    }

    // Update is called once per frame

    void Update(){
        tTime += Time.deltaTime;
        if(tTime > 0.1){
            switch (SceneNum ){
                case 1:
                    scene1.setVar(isAniOverCount,isAniOver,countBlock,time,isSight,tTime,isNoAni);
                    scene1.run();
                    break;
                case 2:
                    scene2.setVar(isAniOverCount,isAniOver,countBlock,time,isSight,tTime,isNoAni);
                    scene2.run();
                    break;
                case 3:
                    scene3.setVar(isAniOverCount,isAniOver,countBlock,time,isSight,tTime,isNoAni);
                    scene3.run();
                    break;
                case 4:
                    scene4.setVar(isAniOverCount,isAniOver,countBlock,time,isSight,tTime,isNoAni);
                    scene4.run();
                    break;
                case 5:
                    sceneseparate.setVar(isAniOverCount,isAniOver,countBlock,time,isSight,tTime,isNoAni);
                    sceneseparate.run();
                    break;


        }
    }
}
}
