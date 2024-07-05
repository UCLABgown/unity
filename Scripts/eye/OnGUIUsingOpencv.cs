using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro.Examples;
using UnityEngine;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.VideoioModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;
using System;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;


public class OnGUIUsingOpencv : MonoBehaviour
{

    public HandObserver handOb;
    public EyeGazeObserver eyeOb;
    float timer = 0.0f;
    // Start is called before the first frame update
    private Mat DrawGUI(Mat m){
        Vector2 leftHandPoint = handOb.GetLeftHandPoint();
        Vector2 rightHandPoint = handOb.GetRightHandPoint();
        Vector2 eyePoint = eyeOb.GetEyeGaze(); 
        List<UnityEngine.Rect> objPoints = eyeOb.GetObjtArr();//변환해서 보내기
        List<string> objStrs = eyeOb.GetObjNameArr();//변환해서 보내기
        
        //Mat paper = new Mat(m.size(),(int)m.type());
        int n = objStrs.Count;
        for(int i = 0; i<n; i++){
            Mat paper = new Mat(m.size(),(int)m.type(),new Scalar(0,0,0));
            Imgproc.rectangle(paper, new Point(objPoints[i].xMin, objPoints[i].yMin), new Point(objPoints[i].xMax, objPoints[i].yMax), new Scalar(0, 0, 50), -1);
            Imgproc.putText(paper, objStrs[i], new Point(objPoints[i].xMin, (objPoints[i].yMin)-10),Imgproc.FONT_HERSHEY_SIMPLEX , 0.5, new Scalar(100, 100, 100), 1, Imgproc.LINE_AA, false);
            Core.add(paper,m,m);
        }

        Mat paper2 = new Mat(m.size(),(int)m.type(),new Scalar(0,0,0));
        Imgproc.rectangle(paper2, new Point(leftHandPoint.x-5,leftHandPoint.y-5), new Point(leftHandPoint.x+5,leftHandPoint.y+5), new Scalar(100, 0, 0), -1);
        Core.add(paper2,m,m);

        Mat paper3 = new Mat(m.size(),(int)m.type(),new Scalar(0,0,0));
        Imgproc.rectangle(paper3, new Point(rightHandPoint.x-5,rightHandPoint.y-5), new Point(rightHandPoint.x+5,rightHandPoint.y+5), new Scalar(0, 100, 0), -1);
        Core.add(paper3,m,m);

        Mat paper4 = new Mat(m.size(),(int)m.type(),new Scalar(0,0,0));
        Imgproc.rectangle(paper4, new Point(eyePoint.x-2,eyePoint.y-2), new Point(eyePoint.x+2,eyePoint.y+2), new Scalar(255, 255, 255), -1);
        Core.add(paper4,m,m);
        Imgproc.putText(m, timer.ToString(), new Point(screenWidth/2.3, screenHeight-20),Imgproc.FONT_HERSHEY_SIMPLEX , 1.2, new Scalar(255, 255, 255), 1, Imgproc.LINE_AA, false);
        
        return m;
        
    }






    private Thread encoderThread;
    private bool threadIsProcessing;
    private VideoWriter writer;
    private string persistentDataPath;
    private int screenWidth;
    private int screenHeight;
    private Queue<Mat> frameQueueTexture;
    private int frameRate;
    private bool isPass;
    private Action run;
    public bool is60fps;
    public RenderTexture m_MirrorRenderTexture;
    
    Mat imgMat;
        private void run30fps(){
        if(isPass){
            timer += Time.deltaTime;
            Recodrding(toTexture2D());
        }
        isPass = !isPass;
    }
    private void run60fps(){
        timer += Time.deltaTime;
        Recodrding(toTexture2D());
    }
    void Start(){
        if(is60fps){
            frameRate = 60;
            run = run60fps;
        }
        else{
            frameRate = 30;
            run = run30fps;
        }
        frameQueueTexture = new Queue<Mat>();
        threadIsProcessing = true;
        screenWidth = GetComponent<Camera>().pixelWidth;
		screenHeight = GetComponent<Camera>().pixelHeight;
        imgMat = new Mat(screenHeight, screenWidth, CvType.CV_8UC4);
        persistentDataPath = Application.persistentDataPath + "/"+DateTime.Now.ToString("yyyy-MM-dd-HH-ss\\hmm\\m") +".avi";
        writer = new VideoWriter();
		writer.open(persistentDataPath , Videoio.CAP_OPENCV_MJPEG, VideoWriter.fourcc('M', 'J', 'P', 'G'), frameRate, new Size((int)screenWidth, (int)screenHeight));
        encoderThread = new Thread (VideoSave);
        isPass = true;
		encoderThread.Start ();
    }/*
    private Texture2D toTexture2D(RenderTexture rTex){
        Texture2D tex = new Texture2D(screenWidth, screenHeight, TextureFormat.ARGB32, true);
        Graphics.CopyTexture(rTex,tex);
        return tex;

    }*/

    Texture2D toTexture2D()
    {
        Texture2D texture = new Texture2D(m_MirrorRenderTexture.width, m_MirrorRenderTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = m_MirrorRenderTexture;
        texture.ReadPixels(new UnityEngine.Rect(0, 0, m_MirrorRenderTexture.width, m_MirrorRenderTexture.height), 0, 0);
        texture.Apply();

        return texture;
    }
    private RenderTexture toRenderTexture(Texture2D tTex){
        RenderTexture tex = new RenderTexture(screenWidth, screenHeight,24);
        RenderTexture.active = m_MirrorRenderTexture;
        Graphics.CopyTexture(tTex,tex);
        return tex;
    }
    private void Recodrding(Texture2D texture){
            Utils.fastTexture2DToMat(texture, imgMat);
            imgMat = DrawGUI(imgMat);
            frameQueueTexture.Enqueue(imgMat);
        
    }

    private void VideoSave(){
        Debug.Log("녹화시작 " + persistentDataPath );
		while (threadIsProcessing) 
		{
			if(frameQueueTexture.Count > 0)
			{
				writer.write(frameQueueTexture.Dequeue());
			}
			Thread.Sleep(10);
		}
    }
    public void VideoStop(){
        OnDisable();

    }

    // Update is called once per frame
    void LateUpdate(){
        run();

    }
    void OnDisable() 
	{
		// Reset target frame rate
        Debug.Log("녹화 종료: "+persistentDataPath );
		writer.release();
        threadIsProcessing = false;

	}
}
