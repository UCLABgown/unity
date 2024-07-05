using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class highlight : MonoBehaviour
{
    public GameObject camera;
    public GameObject space;
    public GameObject me;
    private Vector3 SavePosition;
    int 포커스확인;
    // Start is called before the first frame update
    void Start()
    {
        SavePosition = space.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        float viewNum = camera.GetComponent<Camera>().fieldOfView;
        if(포커스확인 > 0)
            포커스확인--;
        else{
            space.GetComponent<Transform>().position = SavePosition;
        }
    }

    private void OnTriggerStay(Collider other){
        if(other.gameObject.name.Equals("highlight")){
            if(포커스확인 <10){
                포커스확인 += 2;
            }
            Vector3 v1 = other.GetComponent<Transform>().position;
            Vector3 v2 = me.GetComponent<Transform>().position;
            Vector3 vDist = v1-v2;
            Vector3 vDir = vDist .normalized;
            float fDist = vDist.magnitude;
            if(fDist > 0.5f)
                space.GetComponent<Transform>().position +=vDir * 5.0f * Time.deltaTime;
           //float viewNum = camera.GetComponent<Camera>().fieldOfView;
            //Debug.Log(viewNum);
            //if(viewNum >85)
                //camera.GetComponent<Camera>().fieldOfView = viewNum-0.1f;
        }
    }
}
