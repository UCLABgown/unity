using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindCondition : ConditionClass
{
    public Renderer blind;
    public float fadeTime =1;
    private float frameColor = 0;
    private bool isStart = false;
    private int now = 0;
    private int num = 0;
    public Transform fromMove;
    public Transform  toMove;
    public float rotate;
    
    public override void AniStart(){
        gameObject.active = true;
        isStart  = true;
    }
    public override void AniOver(){
        isStart  = false;
        gameObject.active = false;
        print("isover");
    }
    // Start is called before the first frame update
    void Start()
    {
        frameColor = 1/(fadeTime*60);
        gameObject.active = false;
        blind.material.color = new Color(255,0,0,0);
    }
    void fade(float n){
        blind.material.color = new Color(0,0,0,n);
    }
    // Update is called once per frame
    void Update()
    {
        if(isStart ){
            switch(now){
                case 0 :
                    gameObject.active = true;
                    now++;
                    break;
                case 1:
                    if(num == (int)(fadeTime*60)) now++;
                    fade(frameColor*num++);
                    break;
                case 2:
                    Vector3 save= toMove.position;
                    Quaternion q = Quaternion.Euler(0,rotate,0);
                    save.y = fromMove.position.y;
                    fromMove.position = save;
                    fromMove.rotation = q;
                    now++;
                    break;
                case 3:
                    if(num == 0) now++;
                    fade(frameColor*num--);
                    break;
                case 4:
                    state = true;
                    gameObject.active= false;
                    break;
            }
        }
    }
}
