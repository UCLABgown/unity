using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction.Input;
using Unity.VisualScripting;
using UnityEngine;

public class IsHandGrab : MonoBehaviour
{
    HandGrabInteractor leftHandInteractor;
    HandGrabInteractor rightHandInteractor;
    SphereCollider lColl;
    SphereCollider rColl;
    private IHand lHand;
    private IHand rHand;

    // Start is called before the first frame update
    void Start()
    {
        lHand = leftHandInteractor.Hand;
        rHand = rightHandInteractor.Hand;
    }

    void Update(){
    }

    bool IsGrabRight(){
        return rightHandInteractor.IsGrabbing;
    }
    bool IsGrabLeft(){
        return leftHandInteractor.IsGrabbing;
    }
    bool IsActivateRightHand(){
        return rHand.IsConnected;
    }
    
    bool IsActivateLeftHand(){
        return lHand.IsConnected;
    }
}
