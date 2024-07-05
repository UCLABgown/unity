using System.Collections.Generic;
using UnityEngine;

/*
 * HeadObserver�� ������� �Ӹ��� ���� ȸ������ �����մϴ�.
 * HeadObserver saves user's head lotation value.
 */
public class HeadObserver3D : MonoBehaviour
{
    [SerializeField]
    private GameObject centerEyeAnchor; // ������ �Ǵ� ������Ʈ.  Standard object.

    private List<string> colnames = new List<string> { "head_roll", "head_pitch", "head_yaw"}; // csv�� ������ �� �̸�. column names 
    private List<string> csvData = new List<string> { "0.0", "0.0", "0.0" };

    private void Update()
    {
        csvData[0] = centerEyeAnchor.transform.eulerAngles.z.ToString(); // roll
        csvData[1] = centerEyeAnchor.transform.eulerAngles.x.ToString(); // pitch
        csvData[2] = centerEyeAnchor.transform.eulerAngles.y.ToString(); // yaw
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
