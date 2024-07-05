using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DetectVoiceStart : MonoBehaviour
{
        float MicLoudness;
 
        private string _device;
        
        public VoiceRecord vr;
        int count = 0;
        bool IsRecording = false;
        int micp1;
        int micp2;
        string foldername;
     
        //mic initialization
        void InitMic(){
            string path = Application.dataPath;
            foldername = DateTime.Now.ToString("yyyy-MM-dd-HH");
            foldername = Path.Combine(path.Substring(0, path.LastIndexOf('/')), "Recordings", foldername);
            if(_device == null) _device = Microphone.devices[0];
            _clipRecord = Microphone.Start(_device, true, 999, 44100);
        }
     
        void StopMicrophone()
        {
            Microphone.End(_device);
        }
     
 
        AudioClip _clipRecord;
        int _sampleWindow = 128;
     
        //get data from microphone into audioclip
        float  LevelMax()
        {
            float levelMax = 0;
            float[] waveData = new float[_sampleWindow];
            int micPosition = Microphone.GetPosition(_device)-(_sampleWindow+1); // null means the first microphone
            if (micPosition < 0) return 0;
            _clipRecord.GetData(waveData, micPosition);
            // Getting a peak on the last 128 samples
            for (int i = 0; i < _sampleWindow; i++) {
                float wavePeak = waveData[i] * waveData[i];
                if (levelMax < wavePeak) {
                    levelMax = wavePeak;
                }
            }
            return levelMax;
        }
     
     
     
        void Update()
        {
            // levelMax equals to the highest normalized value power 2, a small number because < 1
            // pass the value to a static var so we can access it from anywhere
            MicLoudness = LevelMax ();
            print(MicLoudness);
            if((MicLoudness>0.0001)&&!IsRecording){
                vr.StartRecording();
                IsRecording=true;
                Debug.Log("start");
                //micp1 = Microphone.GetPosition(null);
            }
            else{
                if(IsRecording){
                    if((MicLoudness>0.00001))
                        count = 0;
                    count++;
                    if(count>128){
                        //vr.StopRecord();
                        //micp2 = Microphone.GetPosition(null);
                        //float[] waveData = new float[micp2-micp1];
                        //vr.StopRecord();
                        /* _clipRecord.GetData(waveData,micp1);
                         foreach(float i in waveData)
                            print(i);
                         AudioClip _clipSave = AudioClip.Create("MySinusoid",micp2-micp1,1,44100,false);
                         _clipSave.SetData(waveData,0);
                         aud.clip = _clipSave;
                         name =  DateTime.Now.ToString("yyyy-MM-dd-HH\\hmm\\m");
                         new SavWav().Save(foldername +'/'+name,aud.clip);
                         */
                        IsRecording = false;
                        Debug.Log("stop");
                    }
                }
            }
        }
     
        bool _isInitialized;
        // start mic when scene starts
        void OnEnable()
        {
            InitMic();
            _isInitialized=true;
            print("시작");
        }
     
        //stop mic when loading a new level or quit application
        void OnDisable()
        {
            StopMicrophone();
        }
     
        void OnDestroy()
        {
            StopMicrophone();
        }
     
     
        // make sure the mic gets started & stopped when application gets focused
        void OnApplicationFocus(bool focus) {
            if (focus)
            {
                //Debug.Log("Focus");
             
                if(!_isInitialized){
                    //Debug.Log("Init Mic");
                    InitMic();
                    _isInitialized=true;
                }
            }      
            if (!focus)
            {
                //Debug.Log("Pause");
                StopMicrophone();
                //Debug.Log("Stop Mic");
                _isInitialized=false;
             
            }
        }
}
