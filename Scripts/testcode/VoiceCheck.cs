using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class VoiceCheck : MonoBehaviour
{
    public AudioSource 오디오소스;
    public AudioClip 오디오클립_wav;
    public AnimationClip 애니메이션클립;
    public Animator animator;
    private int count = 0;
    public AnimatorOverrideController overrideController; // 오버라이드 컨트롤러를 연결하세요.

    public void Run(){
        
        animator.SetBool("run",true);
        오디오소스.Stop();
        오디오소스.PlayOneShot(오디오클립_wav);
        count = 10;

        
    }

    void Update(){
        if(--count == 0) animator.SetBool("run",false);
    }

}
