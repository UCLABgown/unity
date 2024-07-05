using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPosition : MonoBehaviour
{
    public GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = this.transform.position;
        p.y = hand.transform.position.y + 0.15f;
        p.z = hand.transform.position.z;
        this.transform.position = p;
    }
}
