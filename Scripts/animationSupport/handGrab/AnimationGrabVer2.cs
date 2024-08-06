using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Input;
using Unity.VisualScripting;
using UnityEngine;
using UnluckSoftware;

public class AnimationGrabVer2 : MonoBehaviour
{

    public Rigidbody holdHand;
    public bool ishold;
    private bool holdstop = false;
    private bool savehold;
    private int dragInt;
    public bool isTouch=true;
    private bool isFreez = false;
    public bool isAutoFreez = false;
    public Vector3 addPosition;
    public Quaternion addRotate;
    private bool isPrevFreez = false;
    public string sceneControllName = "Pose_2_pointing_re";
    private bool state = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter(Collider other) {
        if(other.name.Contains("ColliderDetect") && isTouch){

            Joint j = this.GetComponent<FixedJoint>();
            if(j != null){
                j.connectedBody = null;
                Destroy(j);
                this.GetComponent<Rigidbody>().drag =90;
                dragInt = 35;
                holdstop= true;
                state = true;

            }
        }
    }
    public bool Getstate(){
        return state;;
    }
    void FreezAni(){
        GameObject g = GameObject.Find(sceneControllName);
        g.GetComponent<ChoiceScenario>().FreezAni();
    }
        void HeatAni(){
        GameObject g = GameObject.Find(sceneControllName);
        g.GetComponent<ChoiceScenario>().HeatAni();
    }
    // Update is called once per frame
    void Update()
    {

        if(savehold != ishold){
                if(ishold && !holdstop){
                    if(isAutoFreez) isFreez = true;
                    transform.position = holdHand.transform.position + addPosition;
                    transform.rotation = holdHand.transform.rotation * addRotate;                    
                    Joint j = this.AddComponent<FixedJoint>();
                    j.connectedBody = holdHand;
                    j.breakForce = Mathf.Infinity;

            }
        else{
            Joint j = this.GetComponent<FixedJoint>();
            if(j != null){
                j.connectedBody = null;
                Destroy(j);
            }
        }
        }
        if(dragInt>1){
            dragInt = dragInt-1;
            this.GetComponent<Rigidbody>().drag = 1000f;
            if (dragInt == 2){
                this.GetComponent<Rigidbody>().drag = 0.5f;
                dragInt = 0;
            }
        }
        if(isFreez){
            transform.position = holdHand.transform.position + addPosition;
            transform.rotation = holdHand.transform.rotation * addRotate;
            
        }
        
        savehold = ishold;
        if(isFreez && !isPrevFreez){
            FreezAni();

        }
        if(!isFreez && isPrevFreez){
            HeatAni();
        }
        isPrevFreez= isFreez;
        
    }

    public bool Ishold {set {ishold = value;} get {return ishold;}}
}
