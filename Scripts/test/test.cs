using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScriptableObjectExample", menuName = "ScriptableObject/ScriptableObjectExample")]
public class test : MonoBehaviour //ScriptableObject를 상속
{

        [Header ("애니메이션, 사운드 이름")]
        public string name;
        public int Number;
        public ConditionClass[] condition;

        [Header ("부울조건")]
        public bool conditionBoolActivity;
        public bool boolCondition;
        [Header ("정수조건")]
        public bool conditionIntActivity;
        [Tooltip ("0:초과,1:미만,2:같음")]
        public int OverUnderSame ;
        public int numberCondition;
        [Header ("시간 조건 (초)")]
        public bool conditionTimeActivity;
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

