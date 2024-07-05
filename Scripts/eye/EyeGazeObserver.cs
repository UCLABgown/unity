using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using FE = OVRFaceExpressions.FaceExpression;


/*
 * EyeGazeObserver�� CSVWriter�� �����͸� �����ϱ� �ռ� ������� �ü� �����͸� ��ó���ϴ� ������ �����մϴ�.
 * EyeGazeObserver processes user's gazing data prior to save data by CSVWriter.
 */
[RequireComponent(typeof(Camera))]
public class EyeGazeObserver : MonoBehaviour
{
    [SerializeField]
    [Tooltip("GameObject containing EyeRaycaster class.")]
    private EyeRaycaster eyeRaycaster;

    [SerializeField]
    private OVRFaceExpressions faceExpressions; // ���� ���Ҵ���, �������� Ȯ���ϱ� ���� ���Ǵ� Ŭ����.  Class for checking whether eyes are opened or not.

    [Tooltip("Show box boundarys in gui.")]
    public bool showReactorObjectBoundary = true;

    [Tooltip("Show eye gazing pointer in gui.")]
    public bool showEyeGazingPointer = false;

    [SerializeField]
    [Tooltip("Texture for eye gazing pointer.")]
    private Texture eyeGazingPointer; // �ü� ��ġ�� ��Ÿ�� �������� �ؽ�ó �̹���.  Texture image to indicate the position of eye gazing.

    [SerializeField]
    [Tooltip("GameObject containing RayReactor class.")]
    private List<GameObject> reactorObjects; // ����Ʈ ���� game object �鸸 �ٿ�� �ڽ��� �׸��ų� �ü� Ž���� ������.   Only game object in the list can be detected by eye gazing and drawinig bounding box.

    private Camera observerCamera;

    private List<string> colnames = new List<string> { "l_eye_pnt_x", "l_eye_pnt_y", "r_eye_pnt_x", "r_eye_pnt_y", "eye_pnt_x", "eye_pnt_y", "gaz_obj", "l_eye_cls", "r_eye_cls" }; // csv�� ������ �� �̸�. column names
    private List<string> csvData = new List<string> { "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "None", "0.0", "0.0" }; // �ʱ� csv ������   Initial csv data

    // �ü� ��ġ�� �ٿ�� �ڽ��� ��ġ�� 0 ~ 1 ũ��� ����ȭ �ϱ� ���� ���� ȭ�� ũ��.
    // Screen size to regularizing gazing position and bounding box position to 0 ~ 1.
    private int screenWidth;
    private int screenHeight;

    private GUIStyle timerStyle; // Timer gui style
    List<RayReactor> rayList = new List<RayReactor>();
    List<Renderer> renderList = new List<Renderer>();

    List<Rect> particleRectArr = new List<Rect>();

    private float timer = 0f;

    // CSVWriter�� �ʱ�ȭ�Ǳ� ���� reactorObjects ���� �� ������Ʈ�� �ٿ�� �ڽ��� �׸��� ���� ��ǥ �÷��� �߰��Ǿ�� �ϹǷ� Awake()���� ����.
    // The columns indicating bounding box position of each object in reactorObjects must be added into colnames before initalizing the CSVWriter.
    // Thus, add new column to colnames in Awake() not in Start().
    void Awake()
    {
        // �� ������Ʈ�� �ٿ�� �ڽ��� �׸��� ���� �� ������Ʈ ���� ���� ���, �ϴ� ���� ��ġ�� ��Ÿ���� �� ����.
        // Add columns indicating top-left and bottom-right position of bounding box of each game object in reactorObjects.
        foreach (var reactorObject in reactorObjects)
        {
            
            RayReactor r = reactorObject.GetComponent<RayReactor>();
            Renderer render = reactorObject.GetComponent<Renderer>();
            rayList.Add(r);
            renderList.Add(render);
            colnames.Add(r.objectName + "_top_left_x");
            colnames.Add(r.objectName + "_top_left_y");
            colnames.Add(r.objectName + "_bottom_right_x");
            colnames.Add(r.objectName + "_bottom_right_y");
            // csv �� ������ �ʱ�ȭ.
            // initalize bounding box csv data.
            for (int i = 0; i < 4; i++)
                csvData.Add("0.0");
        }
    }

    private void Start()
    {
        observerCamera = GetComponent<Camera>();

        screenWidth = observerCamera.pixelWidth;
        screenHeight = observerCamera.pixelHeight;
        // Ÿ�̸� GUI �ؽ�Ʈ ��Ÿ��
        // gui text style for showing timer.
        timerStyle = new GUIStyle();
        timerStyle.normal.textColor = Color.white;
        timerStyle.alignment = TextAnchor.MiddleCenter;
        timerStyle.fontSize = 60;
    }

    private void Update()
    {

        timer += Time.deltaTime;
    }


    public Vector2 GetEyeGaze(){
        Vector2 screenEyeGazingPoint =WorldPointToScreenPointEye(eyeRaycaster.GazingPoint); // ������� ���ʰ� �������� �߽� �ü� ��ġ.   Gazing position from between user's left eye and right eye.

        Rect screenEyeGazingRect = new Rect(screenEyeGazingPoint.x - 5f, screenEyeGazingPoint.y - 5f, 30f, 30f); // GUI �ü� ������ ũ��.   The size of user gazing pointer in gui.
        return   screenEyeGazingPoint;
    }

    public List<Rect> GetObjtArr(){
        List<Rect> arr = new List<Rect>();
        foreach (Renderer reactorObject in renderList){
            Vector3 screenPointOfObjectCenter = observerCamera.WorldToScreenPoint(reactorObject.bounds.center);
             if (screenPointOfObjectCenter.z > 0){
                arr.Add(WorldObjectToScreenRect(reactorObject.gameObject));

             }
        }
        return arr;
    }
    public List<string> GetObjNameArr(){
        List<string>  arr = new List<string>();
        foreach (Renderer reactorObject in renderList){
            Vector3 screenPointOfObjectCenter = observerCamera.WorldToScreenPoint(reactorObject.bounds.center);
             if (screenPointOfObjectCenter.z > 0){
                arr.Add(reactorObject.name);

             }
        }
        return arr;
    }
/*
    public void CalParticleRect(){
        particleRectArr.Clear();
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        int count = particleSystem.GetParticles(particles);
        for (int i = 0; i < count; i++)
        {
        
            
            float particlesize = particles[i].startSize;
            float particleHalfSize = particlesize/2;
            Vector3 p = WorldPointToScreenPoint(particles[i].position);
            
            if(p.z>0)
                particleRectArr .Add(new Rect(p.x - 5, p.y - 5, 10, 10));

        }
    }
*/
    public List<Rect> GetparticleRect(){
        return particleRectArr;
    }

    private void OnGUI()
    {
        Vector2 screenLeftEyeGazingPoint = WorldPointToScreenPoint(eyeRaycaster.LeftGazingPoint); // ������� ���� ���� �ü� ��ġ.   Gazing position from user's left eye.
        Vector2 screenRightEyeGazingPoint = WorldPointToScreenPoint(eyeRaycaster.RightGazingPoint); // ������� ������ ���� �ü� ��ġ.   Gazing position from user's right eye.
        Vector2 screenEyeGazingPoint = WorldPointToScreenPoint(eyeRaycaster.GazingPoint); // ������� ���ʰ� �������� �߽� �ü� ��ġ.   Gazing position from between user's left eye and right eye.

        Rect screenEyeGazingRect = new Rect(screenEyeGazingPoint.x - 2f, screenEyeGazingPoint.y - 2f, 4f, 4f); // GUI �ü� ������ ũ��.   The size of user gazing pointer in gui.
        // If showEyeGazingPointer option is on, show pointer of eye gazing point.
        //if (showEyeGazingPointer)
            //GUI.DrawTexture(screenEyeGazingRect, eyeGazingPointer);

        // �⺻���� CSV ������ ����.
        // Save base csv data.
        csvData[0] = (screenLeftEyeGazingPoint.x / screenWidth).ToString(); csvData[1] = (screenLeftEyeGazingPoint.y / screenHeight).ToString();
        csvData[2] = (screenRightEyeGazingPoint.x / screenWidth).ToString(); csvData[3] = (screenRightEyeGazingPoint.y / screenHeight).ToString();
        csvData[4] = (screenEyeGazingPoint.x / screenWidth).ToString(); csvData[5] = (screenEyeGazingPoint.y / screenHeight).ToString();
        csvData[6] = "None"; // �ٶ󺸰� �ִ� ��ü��.   Name of the game object which is gazing.
        csvData[7] = faceExpressions.ValidExpressions ? faceExpressions.GetWeight(FE.EyesClosedL).ToString() : "0.0";
        csvData[8] = faceExpressions.ValidExpressions ? faceExpressions.GetWeight(FE.EyesClosedR).ToString() : "0.0";

        int count = 0;
        bool isFirstDetect = false;
        // reactorObjects�� �߰��� ������Ʈ�� �ϳ��� ��ȸ�ϸ� �ش� ������Ʈ�� ���� �ִ��� Ȯ���ϰ� �ٿ�� �ڽ� ���� ����.
        // Check each object whether user is gazing on object and if it does, save and draw bounding box information.
        int forCount = renderList.Count;
        
        for(int i =0; i<forCount; i++){
            Vector3 screenPointOfObjectCenter = observerCamera.WorldToScreenPoint(renderList[i].bounds.center);
                        // ���� ������Ʈ�� ī�޶� �ڿ� �ִ� ��� �ٿ���� �ڽ��� �׸��� ����.
            // If object is located in behind of user, it doesn't draw the bounding box.
            if (screenPointOfObjectCenter.y > 0)
            {
                Rect screenRectForObject = WorldObjectToScreenRect(renderList[i].gameObject); // ��ü ��ġ.   Location of the object.
                // �ٿ�� �ڽ� ������ ����.  Save bounding box data.
                csvData[4 * count + 9] = (screenRectForObject.xMin / screenWidth).ToString();
                csvData[4 * count + 10] = (screenRectForObject.yMin / screenHeight).ToString();
                csvData[4 * count + 11] = (screenRectForObject.xMax / screenWidth).ToString();
                csvData[4 * count + 12] = (screenRectForObject.yMax / screenHeight).ToString();

                if (screenRectForObject.Overlaps(screenEyeGazingRect) && !isFirstDetect) // �ش� ��ü�� �ٶ󺸰� �ִ��� Ȯ��.  Check whether user is gazing an object.
                {
                    isFirstDetect = true;
                    rayList[i].isEyeReacted = true;

                    csvData[6] = rayList[i].objectName;
                    if(renderList[i].GetComponent<eyeSightnearPosition>() is not null){
                        renderList[i].GetComponent<eyeSightnearPosition>().setTrue();
                    }
                }   
                else{
                    if(renderList[i].GetComponent<eyeSightnearPosition>() is not null){
                         renderList[i].GetComponent<eyeSightnearPosition>().setFalse();
                    }
                    rayList[i].isEyeReacted = false;
                }

                //if (showReactorObjectBoundary)
                   // GUI.Box(screenRectForObject, rayList[i].objectName);
            }
            count++;
        }

        /*
        foreach (var reactorObject in reactorObjects)
        {
            Vector3 screenPointOfObjectCenter = observerCamera.WorldToScreenPoint(reactorObject.GetComponent<Renderer>().bounds.center);

            // ���� ������Ʈ�� ī�޶� �ڿ� �ִ� ��� �ٿ���� �ڽ��� �׸��� ����.
            // If object is located in behind of user, it doesn't draw the bounding box.
            if (screenPointOfObjectCenter.z > 0)
            {
                Rect screenRectForObject = WorldObjectToScreenRect(reactorObject); // ��ü ��ġ.   Location of the object.

                // �ٿ�� �ڽ� ������ ����.  Save bounding box data.
                csvData[4 * count + 9] = (screenRectForObject.xMin / screenWidth).ToString();
                csvData[4 * count + 10] = (screenRectForObject.yMin / screenHeight).ToString();
                csvData[4 * count + 11] = (screenRectForObject.xMax / screenWidth).ToString();
                csvData[4 * count + 12] = (screenRectForObject.yMax / screenHeight).ToString();

                if (screenRectForObject.Overlaps(screenEyeGazingRect) && !isFirstDetect) // �ش� ��ü�� �ٶ󺸰� �ִ��� Ȯ��.  Check whether user is gazing an object.
                {
                    isFirstDetect = true;
                    reactorObject.GetComponent<RayReactor>().isEyeReacted = true;

                    csvData[6] = reactorObject.GetComponent<RayReactor>().objectName;
                }   
                else
                    reactorObject.GetComponent<RayReactor>().isEyeReacted = false;

                if (showReactorObjectBoundary)
                    GUI.Box(screenRectForObject, reactorObject.GetComponent<RayReactor>().objectName);
            }
            count++;
        }
        */

       // GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height - 100, 300, 100), timer.ToString("0#.00"), timerStyle); // Ÿ�̸� GUI ǥ��.  Draw timer gui in screen.
    }

    // ������Ʈ�� ȭ�鿡 �������� ��Ų ��ǥ�� ��ȯ. (ȭ�� ���� ������Ʈ�� ���� ��ġ���� 2���� ���ͷ� ��ȯ��)
    // Return the coordinate value which is the result of projecting game object to screen.
    private Rect WorldObjectToScreenRect(GameObject worldObject)
    {
        Bounds bounds = worldObject.GetComponent<Collider>().bounds;

        Vector3 cen = bounds.center;
        Vector3 ext = bounds.extents;

        // There are 8 corners of a rectangle bounding box. Get the screenspace coordinate of each corner. We are using the
        // array member variable to save allocations when you create a new array each frame.
        Vector3[] screenBoundsExtents = new Vector3[8];
        screenBoundsExtents[0] = WorldPointToScreenPoint(new Vector3(cen.x - ext.x, cen.y - ext.y, cen.z - ext.z));
        screenBoundsExtents[1] = WorldPointToScreenPoint(new Vector3(cen.x + ext.x, cen.y - ext.y, cen.z - ext.z));
        screenBoundsExtents[2] = WorldPointToScreenPoint(new Vector3(cen.x - ext.x, cen.y - ext.y, cen.z + ext.z));
        screenBoundsExtents[3] = WorldPointToScreenPoint(new Vector3(cen.x + ext.x, cen.y - ext.y, cen.z + ext.z));
        screenBoundsExtents[4] = WorldPointToScreenPoint(new Vector3(cen.x - ext.x, cen.y + ext.y, cen.z - ext.z));
        screenBoundsExtents[5] = WorldPointToScreenPoint(new Vector3(cen.x + ext.x, cen.y + ext.y, cen.z - ext.z));
        screenBoundsExtents[6] = WorldPointToScreenPoint(new Vector3(cen.x - ext.x, cen.y + ext.y, cen.z + ext.z));
        screenBoundsExtents[7] = WorldPointToScreenPoint(new Vector3(cen.x + ext.x, cen.y + ext.y, cen.z + ext.z));

        // Set these variables for a safe margin distance around the unscaled canvas which we can set things to. These can be located outside of this method, but are shown here for clarity. If left here, it can handle dynamically changing the resolution.
        int margin = 0;                                // Indicates the size of the margin outside of the canvas on all sides we are willing to let the indicators stretch to.
        int minimum = -margin;                          // Beyond the left and bottom of the screen.
        int maximumWidth = screenWidth  + margin;       // Beyond the right side of the screen.
        int maximumHeight = screenHeight + margin;     // Beyond the top of the screen.

        // The following section is used instead of Vector2.Min and Max, as they cause drawcalls.

        // These variables will hold the screenspace bounds of the object. We're initializing to the margin to the left and bottom of the canvas.
        float xMin = minimum;
        float xMax = minimum;
        float yMin = minimum;
        float yMax = minimum;

        // Now that we've done that, we find the first screenpoint of one of the bounds that is in front of the camera plane, and setting that as the first to be compared against.
        for (int i = 0; i < screenBoundsExtents.Length; i++)
        {
            if (screenBoundsExtents[i].z > 0)
            {
                xMin = screenBoundsExtents[i].x;
                xMax = screenBoundsExtents[i].x;
                yMin = screenBoundsExtents[i].y;
                yMax = screenBoundsExtents[i].y;
                // Break out of the loop as we don't need to loop further.
                break;
            }
        }

        // To save repeated calculations.
        float widthMiddle = screenWidth  * 0.5f;
        float heightMiddle = screenHeight * 0.5f;

        // Now we go through each element of the array again to do various things.
        for (int i = 0; i < screenBoundsExtents.Length; i++)
        {
            // If this particular point is behind the camera.
            if (screenBoundsExtents[i].z <= 0)
            {
                // The following comparisons are the heart of this solution. Due to the way Camera.WorldToScreenPoint works,
                // any point behind the camera gets flipped to the opposite sides.
                // Therefore, if the point is to the left of the middle of the screen width, we put it on the right outside of the canvas.
                // If the height is less than the middle of the screen height, then we put it above the canvas, etc.
                // This allows us to have the indicator appear in the right places as it should when we're very close to or passing through an object.

                // Width checks of the point.
                if (screenBoundsExtents[i].x <= widthMiddle)
                    screenBoundsExtents[i].x = maximumWidth;
                else if (screenBoundsExtents[i].x > widthMiddle)
                    screenBoundsExtents[i].x = minimum;
                // Height checks of the point.
                if (screenBoundsExtents[i].y <= heightMiddle)
                    screenBoundsExtents[i].y = maximumHeight;
                else if (screenBoundsExtents[i].y > heightMiddle)
                    screenBoundsExtents[i].y = minimum;
            }

            // Every point will be checked now that they're put in the appropriate place in screen space, even if they're behind the camera.
            // Find the values which comprise the extents of the bounds in screen space by saving it to the previously declared variables.
            if (screenBoundsExtents[i].x < xMin)
                xMin = screenBoundsExtents[i].x;

            else if (screenBoundsExtents[i].x > xMax)
                xMax = screenBoundsExtents[i].x;

            if (screenBoundsExtents[i].y < yMin)
                yMin = screenBoundsExtents[i].y;

            else if (screenBoundsExtents[i].y > yMax)
                yMax = screenBoundsExtents[i].y;
        }

        // Clamp all the values now, so we don't crash the editor with large screenspace coordinates.
        // This is done here rather than an if (screenBoundsExtents[i].z > 0 block because if all points are in front of the camera,
        // there is a possibility of still crashing the editor.
        xMin = Mathf.Clamp(xMin, minimum, maximumWidth);
        xMax = Mathf.Clamp(xMax, minimum, maximumWidth);
        yMin = Mathf.Clamp(yMin, minimum, maximumHeight);
        yMax = Mathf.Clamp(yMax, minimum, maximumHeight);

        xMin = xMin >= 0 ? xMin : 0;
        yMin = yMin >= 0 ? yMin : 0;
        xMax = xMax <= screenWidth  ? xMax : screenWidth ;
        yMax = yMax <= screenHeight ? yMax : screenHeight;

        // Now we have the lowest left point and the upper right point of the rect. Calculate the
        // width and height of the rect by subtracting the max minus the min values and return it as a rect.
        return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
    }

    // 3���� ��ǥ�� ȭ����� 2���� ��ǥ�� ��ȯ.
    // Convert 3-d coordinate value to screen 2-d.
    private Vector3 WorldPointToScreenPoint(Vector3 worldPoint)
    {
        Vector3 screenPoint = observerCamera.WorldToScreenPoint(worldPoint);
        screenPoint.y = screenHeight - screenPoint.y;

        return screenPoint;
    }

    
    private Vector3 WorldPointToScreenPointEye(Vector3 worldPoint)
    {
        Vector3 screenPoint = observerCamera.WorldToScreenPoint(worldPoint);
        screenPoint.y = screenHeight - screenPoint.y;


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

    internal IEnumerable<string> GetColumn3DNames()
    {
        throw new NotImplementedException();
    }

    internal IEnumerable<string> GetCSVData3D()
    {
        throw new NotImplementedException();
    }
}
