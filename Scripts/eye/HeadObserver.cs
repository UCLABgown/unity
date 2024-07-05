using System.Collections.Generic;
using UnityEngine;

/*
 * HeadObserver�� ������� �Ӹ��� ���� ȸ������ �����մϴ�.
 * HeadObserver saves user's head lotation value.
 */
public class HeadObserver : MonoBehaviour
{
    [SerializeField]
    private GameObject centerEyeAnchor; // ������ �Ǵ� ������Ʈ.  Standard object.

    private List<string> colnames = new List<string> { "head_roll", "head_pitch", "head_yaw","head_angul_vel"}; // csv�� ������ �� �̸�. column names 
    private List<string> csvData = new List<string> { "0.0", "0.0", "0.0","0.0"};
    private Vector3 previousPosition;
    private Vector3 currentPosition;
    private float deltaTime;

    void Start()
    {
        // 초기 설정
        previousPosition = transform.position;
    }
    private void Update()
    {
        
        // 시간 차이 계산
        currentPosition = transform.position;
        
        // 시간 차이 계산
        deltaTime = Time.deltaTime;
        
        // 각속도 계산
        float speed = CalculateAngularVelocity(deltaTime);

        // 현재 회전값을 이전 회전값으로 업데이트
        previousPosition = currentPosition;

        csvData[0] = centerEyeAnchor.transform.eulerAngles.z.ToString(); // roll
        csvData[1] = centerEyeAnchor.transform.eulerAngles.x.ToString(); // pitch
        csvData[2] = centerEyeAnchor.transform.eulerAngles.y.ToString(); // yaw
        csvData[3] = speed.ToString();//angle speed
    }
    float CalculateAngularVelocity(float deltaTime)
    {

            Vector3 linearVelocity = (currentPosition - previousPosition) / deltaTime;

            // 각속도를 계산하기 위한 위치 벡터
            Vector3 radius = currentPosition;

            // 각속도 벡터 계산 (cross product)
            Vector3 angularVelocity = Vector3.Cross(radius, linearVelocity) / radius.sqrMagnitude;

            // 선형 속도 벡터 계산 (각속도와 위치 벡터의 벡터 곱)
            Vector3 linearVelocityFromAngular = Vector3.Cross(angularVelocity, radius);

            // 1차원 속도 (선형 속도의 크기)
            float linearSpeed = linearVelocityFromAngular.magnitude;

        return linearSpeed;
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
