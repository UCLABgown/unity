using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro.Examples;
using UnityEngine;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.VideoioModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.ImgcodecsModule;
using OpenCVForUnity.UnityUtils;
using System;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using System.IO;


public class OnGUIUsingOpencv3D : MonoBehaviour
{

    //public HandObserver3D handOb;
    public EyeGazeObserver3D eyeOb;
    float timer = 0.0f;
    // Start is called before the first frame update
    private Mat DrawGUI(Mat m){
        //Vector2 leftHandPoint = handOb.GetLeftHandPoint();
        //Vector2 rightHandPoint = handOb.GetRightHandPoint();
        //Vector2 eyePoint = eyeOb.GetEyeGaze(); 
        //List<UnityEngine.Rect> objPoints = eyeOb.GetObjtArr();//변환해서 보내기
        //List<string> objStrs = eyeOb.GetObjNameArr();//변환해서 보내기
        string time = eyeOb.GetTimer3D();
        //Mat paper = new Mat(m.size(),(int)m.type());
        //int n = objStrs.Count;
        /*
        for(int i = 0; i<n; i++){
            Mat paper = new Mat(m.size(),(int)m.type(),new Scalar(0,0,0));
            if(objPoints[i].width > 0){
                print(objStrs[i][1]);
            if(objStrs[i][1] == 'O')
                Imgproc.rectangle(paper, new Point(objPoints[i].xMin, objPoints[i].yMin), new Point(objPoints[i].xMax, objPoints[i].yMax), new Scalar(0, 0, 70), -1);
            else
                Imgproc.rectangle(paper, new Point(objPoints[i].xMin, objPoints[i].yMin), new Point(objPoints[i].xMax, objPoints[i].yMax), new Scalar(70, 0, 0), -1);
            Imgproc.putText(paper, objStrs[i], new Point(objPoints[i].xMin, (objPoints[i].yMin)-8),Imgproc.FONT_HERSHEY_SIMPLEX , 0.5, new Scalar(100, 100, 100), 1, Imgproc.LINE_AA, false);
            }
            Core.add(paper,m,m);
            paper.release();
        }

        //Imgproc.rectangle(m, new Point(leftHandPoint.x-5,leftHandPoint.y-5), new Point(leftHandPoint.x+5,leftHandPoint.y+5), new Scalar(100, 0, 0), -1);

        //Imgproc.rectangle(m, new Point(rightHandPoint.x-5,rightHandPoint.y-5), new Point(rightHandPoint.x+5,rightHandPoint.y+5), new Scalar(0, 100, 0), -1);
        Imgproc.rectangle(m, new Point(eyePoint.x-10,eyePoint.y-10), new Point(eyePoint.x+10,eyePoint.y+10), new Scalar(255, 255, 255), -1);
*/
        Imgproc.putText(m, time, new Point(screenWidth/2.3, screenHeight-20),Imgproc.FONT_HERSHEY_SIMPLEX , 1.2, new Scalar(255, 255, 255), 1, Imgproc.LINE_AA, false);
        
        return m;
        
    }






    private bool threadIsProcessing;
    private VideoWriter writer;
    private string persistentDataPath;
    private string imagePaht;
    private int screenWidth;
    private int screenHeight;
    private int count = 0;
    private int frameRate;
    private bool isPass;
    private Action run;
    public bool is60fps;
    public bool isimage;
    public RenderTexture m_MirrorRenderTexture;
    /*
    private void run30fps(){
        if(isPass){
            Recodrding(toTexture2D());
        }
        isPass = !isPass;

    }
    private void run60fps(){
        Recodrding(toTexture2D());

    }
    */

    private void runimg30fps(){
        if(isPass){
            ImageSave(toTexture2D());
        }
        isPass = !isPass;

    }
    private void runimg60fps(){
        ImageSave(toTexture2D());

    }
    void Start(){
        if(is60fps){
            frameRate = 50;
            //run = run60fps;
            if(isimage) run = runimg60fps;
        }
        else{
            frameRate = 25;
            //run = run30fps;
            if(isimage)run = runimg30fps;
        }
        screenWidth = m_MirrorRenderTexture.width;
		screenHeight = m_MirrorRenderTexture.height;
        persistentDataPath = "./Data/"+DateTime.Now.ToString("yyyy-MM-dd-HH-ss\\hmm\\m") +".avi";
        imagePaht = "./Data/"+DateTime.Now.ToString("yyyy-MM-dd-HH-ss\\hmm\\m");
        Directory.CreateDirectory(imagePaht );
        if(!isimage){
        //writer = new VideoWriter();
		//writer.open(persistentDataPath , Videoio.CAP_OPENCV_MJPEG, VideoWriter.fourcc('M', 'J', 'P', 'G'), frameRate, new Size((int)screenWidth, (int)screenHeight));
        }
        //encoderThread = new Thread (VideoSave);
        isPass = true;
		//encoderThread.Start ();
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

    /*private void Recodrding(Texture2D texture){
            Mat imgMat = new Mat(screenHeight, screenWidth, CvType.CV_8UC4);

            Utils.texture2DToMat(texture, imgMat);
            imgMat = DrawGUI(imgMat);
            writer.write(imgMat);
            imgMat.release();
    }*/
    private void ImageSave(Texture2D texture){
            Mat imgMat = new Mat(screenHeight, screenWidth, CvType.CV_8UC4);

            Utils.texture2DToMat(texture, imgMat);
            imgMat = DrawGUI(imgMat);
            //writer.write(imgMat);
            Imgcodecs.imwrite(imagePaht+"/"+count+++".jpg", imgMat);  
            imgMat.release();
            Destroy(texture);
    }

    /*private void VideoSave(){
        Debug.Log("녹화시작 " + persistentDataPath );
		while (threadIsProcessing) 
		{
			if(frameQueueTexture.Count > 0)
			{
                Mat img = frameQueueTexture.Dequeue();
				writer.write(img);
                img.empty();
                print(frameQueueTexture.Count);
			}
			Thread.Sleep(14);
		}
    }*/
    public void VideoStop(){
        OnDisable();

    }

    // Update is called once per frame
    void FixedUpdate(){
        run();

    }
    void OnDisable() 
	{
		// Reset target frame rate
        Debug.Log("녹화 종료: "+persistentDataPath );
        if(!isimage)
		writer.release();
        threadIsProcessing = false;

	}
}
