using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonClickEvent : MonoBehaviour
{
    // Start is called before the first frame update

    public void SetText(TMP_Text text){
        if(text.text.Contains("Start")){
            text.text = "Record Stop (Exit)";
        }
        else{
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
