using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;

public class RunScenario : MonoBehaviour
{
    public int scenarioCount = 0;
    [Serializable]
    public struct Scenario{
        [Header ("애니메이션, 사운드 이름")]
        public string name;
        public int Number;
        public ConditionClass[] condition;

        [Header ("부울조건")]
        public bool conditionBoolActivity;
        public bool boolCondition;
        [Header ("정수조건")]
        public bool conditionIntActivity;
        [Tooltip ("0:초과,1:미만,2:같음")]
        public int OverUnderSame ;
        public int numberCondition;
        [Header ("시간 조건 (초)")]
        public bool conditionTimeActivity;
        public float time;
        public bool materialSetActivity;
        public MaterialSet materialSet;
        public bool materialSetIsIn;
        public float materialSetTime;
        public GameObject[] additionalDisable;

        public FlowHeader[] boxInputDisable;
        public FlowHeader[] boxInputAble; 


    }

    public bool ConditionCheck(Scenario s,ConditionClass c){
        bool resultB = true;
        bool resultI = true;
        bool resultT = true;
        bool r = false;
        bool b = c.GetState();
        int n = c.GetCount();
        float t = c.GetTime();
            
        if(s.conditionBoolActivity){
            resultB = (b == s.boolCondition);
            r = r || resultB;
        }
        if(s.conditionIntActivity){
            int OVS = s.OverUnderSame;
            int num = s.numberCondition;
            if(OVS == 0 )//초ss과
                resultI = (num>n);
            else if(OVS ==1)//미만
                resultI = (num<n);
            else if(OVS ==2)//같음
                resultI = (num==n);
            else
                resultI = false;
            r = r || resultI;
        }
        if(s.conditionTimeActivity){
            if(t<s.time){
                resultT = false;
            }
            r = r || resultT;
        }
        return r;
    }
    void SetBoxInput(Scenario s){
        print(s.boxInputAble.Length);
        if(s.boxInputAble.Length >0)
        foreach(FlowHeader f in s.boxInputAble){
            f.SetActivity(true);
        }
        if(s.boxInputDisable.Length>0)
        foreach(FlowHeader f in s.boxInputDisable){
            f.SetActivity(false);
        }

    }



    public Scenario[] scenarioFlowArr;
    public RunAnimationSound runAnimationSound;

    public IsAniMationOver aniOver;
    bool prev;

    void Start(){
        foreach(Scenario s in scenarioFlowArr){
            if(s.Number <scenarioCount){
                runAnimationSound.RunAni(s.name);
            }
        }
    }
    void Update(){
        bool isNext = false;
        bool current = aniOver.GetIsOver();
        
        foreach(Scenario s in scenarioFlowArr){
            if((s.condition.Length == 0)&& (scenarioCount == s.Number) && (!current && prev)){
                //runAnimationSound.AllStopAni();
                runAnimationSound.RunAni(s.name);
                runAnimationSound.RunAudio(s.name);
                scenarioCount++;
                isNext = true;
                SetBoxInput(s);
                if(s.materialSetActivity) {
                    s.materialSet.gameObject.SetActive(true);
                    s.materialSet.Run(s.materialSetIsIn,s.materialSetTime);
                }
                foreach(GameObject g in s.additionalDisable)
                    g.SetActive(false);
                break;
            }
            foreach(ConditionClass c in s.condition){
                if(ConditionCheck(s,c) && (s.Number == -1) && (scenarioCount >0) && !isNext){
                    //runAnimationSound.AllStopAni();
                    SetBoxInput(s);
                    c.SetState(false);
                    runAnimationSound.RunAni(s.name);
                    runAnimationSound.RunAudio(s.name);
                    scenarioCount = 0;
                    c.SetTime(0);
                    if(s.materialSetActivity) {
                    s.materialSet.gameObject.SetActive(true);
                    s.materialSet.Run(s.materialSetIsIn,s.materialSetTime);
                    }
                    foreach(GameObject g in s.additionalDisable)
                        g.SetActive(false);
                }


                if(ConditionCheck(s,c) && (scenarioCount == s.Number)){
                    //runAnimationSound.AllStopAni();
                    SetBoxInput(s);
                    c.SetState(false);
                    runAnimationSound.RunAni(s.name);
                    runAnimationSound.RunAudio(s.name);
                    scenarioCount++;
                    c.SetTime(0);
                    if(s.materialSetActivity) {
                    s.materialSet.gameObject.SetActive(true);
                    s.materialSet.Run(s.materialSetIsIn,s.materialSetTime);

                    }
                    foreach(GameObject g in s.additionalDisable)
                        g.SetActive(false);
                    isNext = true;

                }
            }

        }
        prev= current;
    }

}
