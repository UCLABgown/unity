using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class animatorControll : MonoBehaviour
{
    int n = 0;
    bool saveB=true;
    float tTime;    public Animator[] anim;
    // Start is called before the first frame update

    public static animatorControll instance = null;
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
    private void isOver(float t){
        if((t >= 0.99f)){
            if(!anim[0].GetCurrentAnimatorStateInfo(0).IsName("mainFocusOut") && (true == !saveB)){
                n++;
            }
            
            main.instance.setIsNaiOver(true,n);
            saveB = true;
        }
        else{
            main.instance.setIsNaiOver(false,n);
            saveB = false;
        }
    }

    public void DecreasNCount(){
        if(n >0)
            n--;
    }

    public void startStopAnimation(){
            anim[0].Play("mainFocusOut",-1,0f);
            anim[1].Play("handFocusOut",-1,0f);
    }

    public void okayAnimation(){
        anim[0].Play("mainNext",-1,0f);
        anim[1].Play("handNext",-1,0f);
    }

    public void rplyAnimation(){
        anim[0].Play("mainFocusOut",-1,0f);
    }

    public void StartNextAnimation(){
        int n = claps.instance.GetNCount()+1;
        anim[0].Play("main"+n.ToString(),-1,0f);
        anim[1].Play("hand"+n.ToString(),-1,0f);
    }
 public void StartMiddleAnimation(float num){
        anim[0].Play("main1",-1,num);
        anim[1].Play("hand1",-1,num);
    }
    public void isCurrentNameFocus(){
        bool isNoAni = anim[0].GetCurrentAnimatorStateInfo(0).IsName("mainFocusOut");
        main.instance.setisNoAni(isNoAni);
    }
    public void nextAnimation(float t){
        if((t >0.99f) && (!anim[0].GetCurrentAnimatorStateInfo(0).IsName("mainFocusOut"))){
            int numer = claps.instance.GetNCount();
            anim[0].SetBool(numer.ToString(), true);
            anim[1].SetBool(numer.ToString(), true);
            Debug.Log(numer);
        }
    }

    // Update is called once per frame
    void Update(){
        tTime += Time.deltaTime;
        if(tTime > 0.1){
            float animTime = anim[0].GetCurrentAnimatorStateInfo(0).normalizedTime;
            isOver(animTime);
            isCurrentNameFocus();
            nextAnimation(animTime);
            tTime = 0;
            
        }

    }
}
