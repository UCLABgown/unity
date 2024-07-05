using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceObserver : MonoBehaviour
{
    [SerializeField]
    private VoiceRecord voiceRecord; // ������ �Ǵ� ������Ʈ.  Standard object.

    private List<string> colnames = new List<string> { "is_speak"}; // csv�� ������ �� �̸�. column names 
    private List<string> csvData = new List<string> { "FALSE"};

    private void Update()
    {
        csvData[0] =voiceRecord.GetIsRecording().ToString();
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
