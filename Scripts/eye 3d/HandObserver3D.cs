using Oculus.Interaction.HandGrab;
using Oculus.Interaction.Input;
using System.Collections.Generic;
using UnityEngine;

/*
 * HandObserver�� ����� ��ġ�� � ��ü�� ����ִ���, ����ִٸ� � ����ó�� ��� �ִ����� ���� ������ �����մϴ�.
 * HandObserver saves position of both hands and which object user is holding and if user is holding something, which gesture is being used.
 */
public class HandObserver3D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Left hand GameObject")]
    private GameObject leftHand;

    [SerializeField]
    [Tooltip("Right hand GameObject")]
    private GameObject rightHand;

    [SerializeField]
    private HandGrabInteractor leftHandInteractor;
    private IHand lHand; // VR��⿡�� �޼��� Ž���Ǿ������� Ȯ���� �� Ŭ����.  Hand class used for detecting whether user's left hand is dected in vr device.

    [SerializeField]
    private HandGrabInteractor rightHandInteractor;
    private IHand rHand; // VR��⿡�� �������� Ž���Ǿ������� Ȯ���� �� Ŭ����.   Hand class used for detecting whether user's right hand is dected in vr device.

    [Tooltip("Show hands pointer in gui.")]
    public bool showHandsPointer = false;

    [SerializeField]
    [Tooltip("Texture for hands pointer.")]
    private Texture pointerTexture;

    private Camera observerCamera; // ȭ�� �� ���� ��ġ�� ��Ÿ���� ���� �ʿ��� ī�޶�.  Camera object for determining hands location in the screen.

    private List<string> colnames = new List<string> { "l_hand_x", "l_hand_y", "r_hand_x", "r_hand_y", "l_hand_hld", "l_hand_gest", "r_hand_hld", "r_hand_gest" }; // csv�� ������ �� �̸�. column names 
    private List<string> csvData = new List<string> { "0.0", "0.0", "0.0", "0.0", "None", "None", "None", "None" };

    private List<string> colnames3D = new List<string> { "l_hand_x", "l_hand_y","l_hand_z", "r_hand_x", "r_hand_y", "r_hand_z", "l_hand_hld", "l_hand_gest", "r_hand_hld", "r_hand_gest" }; // csv�� ������ �� �̸�. column names 
    private List<string> csvData3D = new List<string> { "0.0", "0.0","0.0", "0.0", "0.0","0.0", "None", "None", "None", "None" };

    // �ü� ��ġ�� �ٿ�� �ڽ��� ��ġ�� 0 ~ 1 ũ��� ����ȭ �ϱ� ���� ���� ȭ�� ũ��.
    // Screen size to regularizing gazing position and bounding box position to 0 ~ 1.
    private int screenWidth;
    private int screentHeight;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize
        observerCamera = GetComponent<Camera>();

        lHand = leftHandInteractor.Hand;
        rHand = rightHandInteractor.Hand;

        screenWidth = observerCamera.pixelWidth;
        screentHeight = observerCamera.pixelHeight;
    }

    public Vector2 GetLeftHandPoint(){
        return WorldPointToScreenPoint(leftHand.transform.position);
    }

    public Vector2 GetRightHandPoint(){
        return WorldPointToScreenPoint(rightHand.transform.position);
    }

    public Vector3 GetLeftHand3DPoint(){
        return leftHand.transform.position;
    }

    public Vector3 GetRightHand3DPoint(){
        return rightHand.transform.position;
    }


    private void OnGUI()
    {
        // ����� ��ġ�� ȭ��� ��ǥ�� ��ȯ�Ͽ� ����.
        // Get screen point of left and right hand.
        Vector3 screenLeftHand3DPoint = leftHand.transform.position;
        Vector3 screenRightHand3DPoint = rightHand.transform.position;
        Vector2 screenLeftHandPoint = WorldPointToScreenPoint(screenLeftHand3DPoint);
        Vector2 screenRightHandPoint = WorldPointToScreenPoint(screenRightHand3DPoint);

        /*if (showHandsPointer)
        {
            if(lHand.IsConnected)
                GUI.DrawTexture(new Rect(screenLeftHandPoint.x - 15f, screenLeftHandPoint.y - 15f, 30f, 30f), pointerTexture);
            if (rHand.IsConnected)
                GUI.DrawTexture(new Rect(screenRightHandPoint.x - 15f, screenRightHandPoint.y - 15f, 30f, 30f), pointerTexture);
        }*/

        // CSV ������ ����.
        // Save csv data.
        csvData[0] = lHand.IsConnected ? (screenLeftHandPoint.x / screenWidth).ToString() : "0.0";
        csvData[1] = lHand.IsConnected ? (screenLeftHandPoint.y / screentHeight).ToString() : "0.0";
        csvData[2] = rHand.IsConnected ? (screenRightHandPoint.x / screenWidth).ToString() : "0.0";
        csvData[3] = rHand.IsConnected ? (screenRightHandPoint.y / screentHeight).ToString() : "0.0";
        csvData[4] = lHand.IsConnected && leftHandInteractor.IsGrabbing ? leftHandInteractor.SelectedInteractable.GetComponent<RayReactor>().objectName: "None"; // ���ʼ��� ��� �ִ� ������Ʈ �̸�.  Object name which is holding by user's left hand.
        csvData[5] = lHand.IsConnected && leftHandInteractor.IsGrabbing ? leftHandInteractor.HandGrabTarget.Anchor.ToString() : "None"; // ���ʼ��� � ������Ʈ�� ������� ��, �ش�Ǵ� ����ó Ÿ��.  Gesture type if user's left hand is holding some object.
        csvData[6] = rHand.IsConnected && rightHandInteractor.IsGrabbing ? rightHandInteractor.SelectedInteractable.GetComponent<RayReactor>().objectName : "None"; // �����ʼ��� ��� �ִ� ������Ʈ �̸�.  Object name which is holding by user's right hand.
        csvData[7] = rHand.IsConnected && rightHandInteractor.IsGrabbing ? rightHandInteractor.HandGrabTarget.Anchor.ToString() : "None"; // �����ʼ��� � ������Ʈ�� ������� ��, �ش�Ǵ� ����ó Ÿ��.  Gesture type if user's right hand is holding some object.
    
    
        csvData3D[0] = lHand.IsConnected ? screenLeftHand3DPoint.x.ToString() : "0.0";
        csvData3D[1] = lHand.IsConnected ? screenLeftHand3DPoint.y.ToString() : "0.0";
        csvData3D[2] = lHand.IsConnected ? screenLeftHand3DPoint.z.ToString() : "0.0";

        csvData3D[3] = rHand.IsConnected ? screenRightHand3DPoint.x.ToString() : "0.0";
        csvData3D[4] = rHand.IsConnected ? screenRightHand3DPoint.y.ToString() : "0.0";
        csvData3D[5] = rHand.IsConnected ? screenRightHand3DPoint.z.ToString() : "0.0";

        csvData3D[6] = lHand.IsConnected && leftHandInteractor.IsGrabbing ? leftHandInteractor.SelectedInteractable.GetComponent<RayReactor>().objectName : "None"; // ���ʼ��� ��� �ִ� ������Ʈ �̸�.  Object name which is holding by user's left hand.
        csvData3D[7] = lHand.IsConnected && leftHandInteractor.IsGrabbing ? leftHandInteractor.HandGrabTarget.Anchor.ToString() : "None"; // ���ʼ��� � ������Ʈ�� ������� ��, �ش�Ǵ� ����ó Ÿ��.  Gesture type if user's left hand is holding some object.
        csvData3D[8] = rHand.IsConnected && rightHandInteractor.IsGrabbing ? rightHandInteractor.SelectedInteractable.GetComponent<RayReactor>().objectName : "None"; // �����ʼ��� ��� �ִ� ������Ʈ �̸�.  Object name which is holding by user's right hand.
        csvData3D[9] = rHand.IsConnected && rightHandInteractor.IsGrabbing ? rightHandInteractor.HandGrabTarget.Anchor.ToString() : "None"; // �����ʼ��� � ������Ʈ�� ������� ��, �ش�Ǵ� ����ó Ÿ��.  Gesture type if user's right hand is holding some object.
    
    }

    // 3���� ��ǥ�� ȭ����� 2���� ��ǥ�� ��ȯ.
    // Convert 3-d coordinate value to screen 2-d.
    private Vector3 WorldPointToScreenPoint(Vector3 worldPoint)
    {
        Vector3 screenPoint = observerCamera.WorldToScreenPoint(worldPoint);
        screenPoint.y = screentHeight - screenPoint.y;
        return screenPoint;
    }

    public string[] GetColumnNames()
    {
        return colnames.ToArray();
    }

    public string[] GetCSVData()
    {
        return csvData.ToArray();
    }

    public string[] GetColumn3DNames()
    {
        return colnames3D.ToArray();
    }

    public string[] GetCSVData3D()
    {
        return csvData3D.ToArray();
    }
}
