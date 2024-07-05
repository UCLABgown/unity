using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObserver : MonoBehaviour
{
    private List<string> colnames = new List<string> { "scene_act"}; // csv�� ������ �� �̸�. column names 
    private List<string> csvData = new List<string> { ""};
    public RunScenario2 scene;

    private void Update()
    {
        csvData[0] =scene.currentName;
    }

    public string[] GetColumnNames()
    {
        return colnames.ToArray();
    }

    public string[] GetCSVData()
    {
        return csvData.ToArray();
    }
}
