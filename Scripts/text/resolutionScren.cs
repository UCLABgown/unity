using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resolutionScren : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.GetComponent<Camera>().pixelWidth);
        Debug.Log(this.GetComponent<Camera>().pixelHeight);
    }
}
