using UnityEngine;

// �ش� ��ũ��Ʈ�� 'LeftEye'�� 'rightEye' �ڽ� ������Ʈ�� ���� �θ� ������Ʈ�� ����Ǿ�� �մϴ�.
// This class must be connected to 'Eyes' Component, which is containing left and right eye.

/*
 * EyeRaycaster�� LeftEye�� RightEye�� ������ �ִ� �θ� ������Ʈ�� ����Ǵ� ������Ʈ�Դϴ�.
 * �� Ŭ������ ����� �� ��ġ���� ���� �������� ���̸� �߻��Ͽ� �߻�� ���̰� Border ������Ʈ �Ǵ� �ٸ� ������Ʈ�� �΋H�� �� �΋H�� ��ġ�� ����ڰ� �ٶ󺸰� �ִ� ��ġ�� �Ǵ��Ͽ� �����մϴ�.
 * EyeRaycaster is parent object which is containing LeftEye and RightEye objects.
 * In this class, a ray is fired from user's eye location to foward direction and if ray is hit object like Border or something, it determines that user is gazing this position and save that position.
 */
public class EyeRaycaster : MonoBehaviour
{
    [Tooltip("Reference frame for eye. " +
             "Reference frame should be set in the forward direction of the eye. It is there to calculate the initial offset of the eye GameObject. " +
             "If it's null, then world reference frame will be used.")]
    public Transform ReferenceFrame;
    [SerializeField]
    [Tooltip("Layers to be detected.")]
    private LayerMask layersToInclude; // ���� �߻� �� �΋H�� ������Ʈ�� ���� ����.  Hit object range.
    

    private Vector3 gazingPoint; // ������ ���ʴ��� �ٶ󺸰� �ִ� ��ġ�� �߽���.  The center point of users' left and right gazing position.
    private Vector3 leftGazingPoint; // ������ ���ʴ��� �ٶ󺸰� �ִ� ��ġ.  Point of user's left gazing point.
    private Vector3 rightGazingPoint; // ������ �����ʴ��� �ٶ󺸰� �ִ� ��ġ.  Point of user's right gazing point.

    private Transform leftEye; // User's left eye object.
    private Transform rightEye; // User's right eye object.
    private string hitObject;
    

    private void Start()
    {
        // �ڽ� ��� �� LeftEye�� RightEye�� ������.
        // Get left eye and right eye from child objects.
        for (int i = 0; i < 2; i++)
        {
            if (transform.GetChild(i).GetComponent<OVREyeGaze>().Eye == OVREyeGaze.EyeId.Left) // Left eye
            {
                leftEye = transform.GetChild(i);
                leftEye.GetComponent<OVREyeGaze>().ReferenceFrame = ReferenceFrame;
            }
            else // Right eye
            {
                rightEye = transform.GetChild(i);
                rightEye.GetComponent<OVREyeGaze>().ReferenceFrame = ReferenceFrame;
            }
        }
        OVRPlugin.EyeGazeState eyeGaze ;
        
        }

    void FixedUpdate()
    {
        RaycastHit hit; // ���̰� �΋H�� ������Ʈ�� hit�� �����.  A hit object will be saved in hit.
        // �� ���� ������ ������.  Get direction of each eye.
        Vector3 leftEyeGazingDirection = leftEye.transform.TransformDirection(Vector3.forward);
        Vector3 rightEyeGazingDirection = rightEye.transform.TransformDirection(Vector3.forward);
        // ������� �� ���� ��ġ���� �������� ���̸� �߻��Ͽ� �΋H�� ��ǥ�� gazingPoint�� ����.
        // Fire a ray to forward direction from user's eye location and save hit position into gazingPoint.
        if (Physics.Raycast(leftEye.position, leftEyeGazingDirection, out hit, Mathf.Infinity, layersToInclude)) // left eye
        {
            leftGazingPoint = hit.point;
            RayReactor r = hit.collider.GetComponent<RayReactor>();
            if(r is null)
                hitObject = "None";
            else
                hitObject = r.objectName;
        }
        if (Physics.Raycast(rightEye.position, rightEyeGazingDirection, out hit, Mathf.Infinity, layersToInclude)) // right eye
            rightGazingPoint = hit.point;
        gazingPoint = GetMiddlePoint(leftGazingPoint, rightGazingPoint); // �� ���� gazingPoint�� �߽��� ���.   Calculate middle point between left and right gazing point.
    }

    // �� ���� ���� �߽��� ���.
    // Calculate middle point from two points.
    Vector3 GetMiddlePoint(Vector3 vec1, Vector3 vec2)
    {
        return new Vector3((vec1.x + vec2.x) / 2, (vec1.y + vec2.y) / 2, (vec1.z + vec2.z) / 2);
    }


    public Vector3 GazingPoint { get { return gazingPoint; } }
    public Vector3 LeftGazingPoint { get { return leftGazingPoint; } }
    public Vector3 RightGazingPoint { get { return rightGazingPoint; } }
    public Quaternion LeftRotation{get {return leftEye.transform.localRotation;}}
    public Quaternion RightRotation{get {return rightEye.transform.localRotation;}}


    public string HitObject {get {return hitObject;}}
}
