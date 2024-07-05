using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpeech : ConditionClass
{
    public VoiceRecord record;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(record.GetCheckSpeech()){
            record.SetCheckSpeech(false);
            SetState(true);
            print("SPECHhhhhhhhhhhhhhhhhhhhhhhhh");
        }
    }
}
