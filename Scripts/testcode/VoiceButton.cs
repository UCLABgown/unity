using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VoiceCheck))] 
public class VoiceButton : Editor
{

    public override void OnInspectorGUI()

    {
        base.OnInspectorGUI();
        var inventoryManager = target as VoiceCheck;

        if (GUILayout.Button("재생"))
        {
            inventoryManager.Run();
            Debug.Log("버튼");
        
        }
    }
}
