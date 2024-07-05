using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class mainScene3
{
    //내부에서씀
    int soundNum = 0;
    int functionNum = 0;
    int[] soundCurrent = {0,1,2,3,4,6,7};

    //외부에서 받아봄
    int isAniOverCount = 0;
    private int countBlock = 0;
    private Double time = 0;
    private bool isAniOver = false;
    private bool isSight = false; 
    private float tTime = 0;
    private bool isNoAni = false;
    private bool isPlayedStop = false;
    private bool timerEnd = true;

    public void setVar(int isAniOverCount_,bool isAniOver_,int countBlock_,Double time_,bool isSight_,float tTime_,bool isNoAni_){
        isAniOverCount = isAniOverCount_;
        isAniOver = isAniOver_;
        countBlock = countBlock_;
        time = time_;
        isSight = isSight_;
        tTime = tTime_;
        isNoAni = isNoAni_;

    }

     int scene0(){
        if((isAniOverCount == 1) && isAniOver){
            timeCount.instance.Reset_Timer();
            functionNum++;

            Debug.Log("씬0 종료");
        }
        return 0;
    }

    int scene1(){
        if(countBlock == 1){
            animatorControll.instance.okayAnimation();
            soundNum++;
            BlockPlaySound.instance.Voice(soundCurrent[3]);
            functionNum++;
            Debug.Log("씬1 종료");
        }
        if((time >= 7) && timerEnd){
            BlockPlaySound.instance.Voice(2);
            timerEnd =false;
            timeCount.instance.Reset_Timer();
        }
        return 0;

    }
        void selectFunc(){
        switch (functionNum){
            case 0:
                scene0();
                break;
            case 1:
                scene1();
                break;
        }


    }
    public void run(){
        selectFunc();
        //sceneNo();
        //sceneReply();
    }
}
