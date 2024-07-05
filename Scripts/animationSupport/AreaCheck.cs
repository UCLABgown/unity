using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCheck : MonoBehaviour
{
public Vector3 squareCenter; // 정사각형의 중심 좌표
    
    private double time = 0f;
    public Transform area;
    private float squareSize; // 정사각형의 한 변의 길이

    void Start(){
        squareSize = area.localScale.x;
    }
    private void Update()
    {
        time= time + Time.deltaTime;
        if(time > 1){
            time = 0;
            // 물체의 현재 위치
            Vector3 objectPosition = transform.position;

            // 정사각형 영역의 경계를 계산
            Vector3 squareMin = squareCenter - Vector3.one * squareSize / 2;
            Vector3 squareMax = squareCenter + Vector3.one * squareSize / 2;

            // 물체가 정사각형 안에 있는지 확인
            if (objectPosition.x >= squareMin.x && objectPosition.x <= squareMax.x &&
                objectPosition.y >= squareMin.y && objectPosition.y <= squareMax.y &&
                objectPosition.z >= squareMin.z && objectPosition.z <= squareMax.z)
            {
                Debug.Log("물체가 정사각형 안에 있습니다.");
            }
            else
            {
                Debug.Log("물체가 정사각형 안에 없습니다.");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 디버깅을 위해 정사각형 영역을 그려줍니다.
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(squareCenter, Vector3.one * squareSize);
    }
}
