using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scene : MonoBehaviour
{
    [Serializable]
    public struct Scenario{
        [Header ("애니메이션, 사운드 이름")]
        public string name;
        public int Number;
        public ConditionClass[] condition;
                [Header ("소리")]

        public AudioClip audio;

        [Header ("부울조건")]
        public bool conditionBoolActivity;
        public bool boolCondition;

        public float time;
        public bool materialSetActivity;
        public MaterialAutoSet materialSet;
        public bool materialSetIsIn;
        public float materialSetTime;
        public GameObject[] additionalDisable;

        public FlowHeader[] boxInputDisable;
        public FlowHeader[] boxInputAble; 

        public ConditionClass[] InsertInitCondition;


    }
    
    public Scenario[] FlowArr;
    public int Count {get {return FlowArr.Length;}}
}
