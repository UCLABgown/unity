using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimationSound : MonoBehaviour
{
    [Serializable]
    public struct AudioName{
        public string name;
        public AudioClip audio;
    };
    public Animator anim;
    [Tooltip ("애니메이션 변수와 오디오 소스 변수명은 같게해주세요")]
    public AudioName[] audioNames;
    Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();
    public AudioSource audio;
    


    void Start(){
        foreach(AudioName a in audioNames)
            audioDictionary.Add(a.name,a.audio);
    }
    public void RunAni(string name){
        anim.SetBool(name,true);
    }
    public void StopAni(string name){
        anim.SetBool(name,false);
    }
    public void AllStopAni(){
        foreach (AudioName str in audioNames)
        {
            anim.SetBool(str.name,false);
        }
    }
    public void RunAudio(string name){
        StopAudio();
        if(audioDictionary.ContainsKey(name))
            audio.PlayOneShot(audioDictionary[name]);
    }

    public void StopAudio(){
        audio.Stop();
    }
    public void FreezAni(){
        anim.speed = 0;
    }
    public void HeatAni(){
        anim.speed = 1;
    
    }
}
