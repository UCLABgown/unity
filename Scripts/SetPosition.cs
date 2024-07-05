using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    public float child;
    public float adult;

    public void Child(){
        Vector3 a = transform.position;
        a.y = child;
        transform.position = a;
    }
    public void Adult(){
        Vector3 a = transform.position;
        a.y = adult;
        transform.position = a;
    }
    public void up(){
        Vector3 a = transform.position;
        a.y = a.y+0.03f;
        transform.position = a;
    }
    public void down(){
                Vector3 a = transform.position;
        a.y = a.y-0.03f;
        transform.position = a;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
