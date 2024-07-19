using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.AvatarIntegration;
using UnityEngine;

public class DisableTracking : MonoBehaviour
{
    public HandObserver3D ob;
    public SampleInputManager originalAvatarSdk;
    public HandTrackingInputManager origianlHandTracking;
    private string rightHold, leftHold;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rightHold = ob.getRightHandHoldingName(); leftHold = ob.getLeftHandHoldingName();

        if (rightHold == leftHold){
            GetComponent<SampleAvatarEntity>().SetBodyTracking(originalAvatarSdk);
        }
        else if ((rightHold == "None") | (leftHold == "None")){
            GetComponent<SampleAvatarEntity>().SetBodyTracking(origianlHandTracking);
        }
    }
}
