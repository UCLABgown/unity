using System.Collections;
using System.Collections.Generic;
using OVR.OpenVR;
using UnityEngine;

public class stopBall : MonoBehaviour
{

    float time;
    int score = 0;
    string save = "";
    Vector3 savePostion;
    public AudioClip[] arr;
    public GameObject table;

    bool iscol = false;
    // Start is called before the first frame update
    void Start()
    {
        savePostion = this.transform.position;
    }

    void PlaySoundEffet(){
        int num = Random.Range(0, arr.Length);
        this.GetComponent<AudioSource>().Stop();
        this.GetComponent<AudioSource>().PlayOneShot(arr[num], 0.8f);
    }

    public void InitBall(){
        float ty = table.transform.position.y;
        savePostion.y = ty+0.9f;
        this.GetComponent<Rigidbody>().drag = 100;
        SetPosition(savePostion);
        SetIsKinematic(false);
        this.GetComponent<Rigidbody>().drag = 0.5f;
        iscol = false;
        score = 0;
    }

    void SetPosition(){
        this.transform.position = savePostion;
    }
    void SetPosition(Vector3 v){
        this.transform.position = v;
    }
    void SetIsKinematic(bool b){
        this.GetComponent<Rigidbody>().isKinematic = b;
    }

    // Update is called once per frame
void OnTriggerStay(Collider other) {
        //실행문
        string name = other.name;

        time = 0;
        if(name.Contains("target") && !iscol){
            this.GetComponent<Rigidbody>().isKinematic = true;
            iscol = true;
            PlaySoundEffet();
            this.transform.position = new Vector3 (other.transform.position.x,this.transform.position.y,this.transform.position.z) ;

        }
        if(iscol && other.tag.Contains("score")){
            save = name;
            setScore();
        }
}

void setScore(){
    int num = int.Parse(save);
    if(score < num)
        score= num;
}

public int getScore(){
    return score;
}
}
