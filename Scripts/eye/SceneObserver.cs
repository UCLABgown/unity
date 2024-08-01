using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObserver : MonoBehaviour
{
    private List<string> colnames = new List<string> { "scene_act"}; // csv�� ������ �� �̸�. column names 
    private List<string> csvData = new List<string> { ""};
    public RunScenario2 scene;
    public ChoiceScenario scene2;
    

    private void Update()
    {
        if(scene is not null)
            csvData[0] =scene.currentName;
        else if(scene2 is not null)
            csvData[0] =scene2.currentName;
        else
            csvData[0] ="null";
    }

    public string[] GetColumnNames()
    {
        return colnames.ToArray();
    }

    public string[] GetCSVData()
    {
        return csvData.ToArray();
    }
    public string GetCurrentName(){
        if(scene is not null){
            return scene.currentName;
        }
        else if(scene2 is not null){
            return scene2.currentName;
        }
        else
            return "null";
    }
}
