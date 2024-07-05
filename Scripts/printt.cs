using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.GetComponent<Camera>().pixelWidth);
        Debug.Log(this.GetComponent<Camera>().pixelHeight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
