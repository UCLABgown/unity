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

public class URPScreenRecorder : MonoBehaviour
{
    private Thread encoderThread;
    private bool threadIsProcessing;
    private VideoWriter writer;
    private string persistentDataPath;
    public Camera _cam;
    private int screenWidth;
    private int screenHeight;
    private Queue<Mat> frameQueueTexture;
    public int frameRate;
    private float delay;
    private float time;
    public RenderTexture m_MirrorRenderTexture;
    
    void Start(){
        frameQueueTexture = new Queue<Mat>();
        threadIsProcessing = true;
        delay = 1/frameRate;
        time = 0.0f;
        screenWidth = GetComponent<Camera>().pixelWidth;
		screenHeight = GetComponent<Camera>().pixelHeight;
        persistentDataPath = Application.persistentDataPath + "/"+DateTime.Now.ToString("yyyy-MM-dd-HH-ss\\hmm\\m") +".avi";
        writer = new VideoWriter();
		writer.open(persistentDataPath , Videoio.CAP_OPENCV_MJPEG, VideoWriter.fourcc('M', 'J', 'P', 'G'), frameRate, new Size((int)screenWidth, (int)screenHeight));
        Debug.Log(screenWidth);
        Debug.Log(screenHeight);
        encoderThread = new Thread (VideoSave);

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
        Graphics.CopyTexture(tTex,tex);
        return tex;
    }
    private void Recodrding(Texture2D t){
        Mat imgMat = new Mat(screenHeight, screenWidth, CvType.CV_8UC4);
		Utils.texture2DToMat(t, imgMat);
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

    private RenderTexture ReadTexture(){
        RenderTexture rt = new RenderTexture(screenWidth, screenHeight, 24);
        _cam.targetTexture = rt;
        _cam.Render();
        RenderTexture.active = rt;
        return rt;
    }
    // Update is called once per frame
    void Update(){
        time += Time.deltaTime;
        if(time >= delay)
            Recodrding(toTexture2D());

    }
    void OnDisable() 
	{
		// Reset target frame rate
        Debug.Log("녹화 종료: "+persistentDataPath );
		writer.release();
        threadIsProcessing = false;

	}

    
}
