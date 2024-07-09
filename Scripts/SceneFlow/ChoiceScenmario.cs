using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;

public class ChoiceScenario : MonoBehaviour
{
    public int scenarioCount = 0;
    [Serializable]
    public struct Scenario{
        public int num;
        public Scene nowScene;
   }




    public Scenario[] scenarioFlowArr;
    public string centerName = "cross";
    public bool aniOver;
    private bool prevAniOver = false;
    public Animator anim;
    public Animator animMouse;
    public int startCount = 0;
    public int nextCount = 0;
    public AudioSource audio;
    private bool isRun = false;
    private string prevName="";

    float time = 0;
    
    public void RunAudio(AudioClip ac){
        StopAudio();
        audio.PlayOneShot(ac);
    }

    public void StopAudio(){
        audio.Stop();
    }
    public void FreezAni(){
        anim.speed = 0;
        animMouse.speed = 0;
        audio.Stop();
    }
    public void HeatAni(){
        anim.speed = 1;
        animMouse.speed = 1;
        audio.Play();
    }
    public void RunAni(string name, int num){
        anim.SetInteger(name,num);
        animMouse.SetInteger(name,num);
    }

    void Start(){
    }   

     void ConditionInit(ConditionClass[] cs){
        foreach(ConditionClass c in cs){
            c.Init();
    }

    }

    public bool ConditionCheck(ConditionClass c){
        c.AniOver();

        return c.GetState();
    }
    void SetBoxInput(Scene.Scenario s){
        if(s.boxInputAble.Length >0)
        foreach(FlowHeader f in s.boxInputAble){
            f.SetActivity(true);
        }
        if(s.boxInputDisable.Length>0)
        foreach(FlowHeader f in s.boxInputDisable){
            f.SetActivity(false);
        }

    }
    bool ConditionChekcs(){
        bool chk = false;
        int count = scenarioFlowArr[startCount-1].nowScene.Count;
        if(nextCount <=0 || (count < nextCount))
            return true;
        Scene.Scenario s = scenarioFlowArr[startCount-1].nowScene.FlowArr[nextCount-1];
        foreach(ConditionClass c in s.condition)
            chk = chk || ConditionCheck(c);
        if(s.condition.Length == 0 )
         chk = true;
        return chk;

    }
    void RunScene(){
        if(scenarioFlowArr[startCount-1].nowScene.Count < nextCount)
            return;
        Scene.Scenario s = scenarioFlowArr[startCount-1].nowScene.FlowArr[nextCount-1];
        ConditionInit(s.condition);
        SetBoxInput(s);
        RunAudio(s.audio);

        if(s.materialSetActivity) {
            s.materialSet.gameObject.SetActive(true);
            s.materialSet.Run(s.materialSetIsIn,0.8f);//,s.materialSetTime);
            print("activity");
        }
        foreach(GameObject g in s.additionalDisable)
            g.SetActive(false);

    }
    void Update(){
        if(aniOver && !prevAniOver){
            if(ConditionChekcs()){
                RunAni("next",++nextCount);
                RunAni("start",0);
                RunScene();
                isRun = false;
            }

        }
        if(time >0.5){
            string name = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            if(name != prevName){
                if(name.Contains(centerName)){
                    RunAni("start",++startCount);
                    RunAni("next",0);
                    nextCount = 0;
                }
            }
            time = 0;
        }
        time += Time.deltaTime;
    
        prevName = name;
        prevAniOver = aniOver;
    }

}
