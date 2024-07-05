using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEyeTracking : MonoBehaviour
{
    public Transform[] mirrorObject;
    public int trackingNumber = 0;

    void Update(){

        this.transform.position = mirrorObject[trackingNumber].position;
    }
}
