using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
 * VoiceRecorder�� ����Ʈ ���� ���� ����ũ�� ������� ��Ҹ��� �����Ͽ� wav���Ϸ� �����մϴ�.
 * VoiceRecorder records user's voice through mic in quest pro and save into wave file.
 */
public class VoiceRecord : MonoBehaviour
{
    private AudioClip recordingClip; // �������� ����� Ŭ��.  Clip recording source will be saved.
    //AudioSource aud = new AudioSource();
    const int HEADER_SIZE = 44;

    
    private string foldername;
    private string filename;
    private int _sampleWindow = 128;
    private bool IsRecording = false;
    private int count = 0;
    private int micp1;
    private int micp2;

    private bool checkSpeech = false;

    public bool GetCheckSpeech(){
        return checkSpeech;
    }
    public void SetCheckSpeech(bool b){
        checkSpeech = b;
    }
    public bool GetIsRecording(){
        return IsRecording;
    }
    void FixedUpdate()
    {
        /*
        // ���� Ȯ�ο�.  Check volume.
        if (Microphone.IsRecording(null))
        {
            int startPosition = Microphone.GetPosition(null) - 64;

            if (startPosition < 0)
                return;

            float[] waveData = new float[64];
            recordingClip.GetData(waveData, startPosition);

            float totalLoudness = 0;
            for (int i = 0; i < 64; i++)
                totalLoudness += Mathf.Abs(waveData[i]);

            if (totalLoudness < 0.1f)
                totalLoudness = 0;
            Debug.Log((totalLoudness).ToString());
        }
        */
    }

    // ���� ����. Start recording.

    void Start(){
        StartRecording();
    }
    public void StartRecording()
    {
        name =  DateTime.Now.ToString("yyyy-MM-dd-HH\\hmm\\m");
        foldername = DateTime.Now.ToString("yyyy-MM-dd-HH");
        foreach(string a in Microphone.devices)
            print(a);
        recordingClip = Microphone.Start(null, true, 999, 44100);
        this.filename = name.EndsWith(".wav") ? name : name + ".wav";
    }

    // ���� ����. Stop recording.
    public void StopRecording()
    {
        if (Microphone.IsRecording(null))
        {
            int lastTime = Microphone.GetPosition(null);

            if (lastTime == 0)
                return;
            else
            {
                Microphone.End(Microphone.devices[0]);

                float[] samples = new float[recordingClip.samples];
                recordingClip.GetData(samples, 0);

                float[] cutSamples = new float[lastTime];
                Array.Copy(samples, cutSamples, cutSamples.Length - 1);

                recordingClip = AudioClip.Create("Notice", cutSamples.Length, 1, 44100, false);
                recordingClip.SetData(cutSamples, 0);

                Save();
            }
        }
    }


    // wav ���Ϸ� ���� ����.
    // Save recording file into wave file.
    private bool Save()
    {
        name =  DateTime.Now.ToString("yyyy-MM-dd-HH\\hmm\\m");
        foldername = DateTime.Now.ToString("yyyy-MM-dd-HH");
        //string path = Application.persistentDataPath + "/"+DateTime.Now.ToString("yyyy-MM-dd-HH-ss\\hmm\\m") +name+".wav";
        string path = "./Data/"+DateTime.Now.ToString("yyyy-MM-dd-HH-ss\\hmm\\m") +name+".wav";


        // Make sure directory exists if user is saving to sub dir.
        Directory.CreateDirectory(Path.GetDirectoryName(path));

        using (var fileStream = CreateEmpty(path))
        {

            ConvertAndWrite(fileStream, recordingClip);

            WriteHeader(fileStream, recordingClip);
        }

        return true; // TODO: return false if there's a failure saving the file
    }

    // ���α׷��� ����Ǵ� ��� �ڵ����� ������ ������.
    // Save the wave file on program quit.
    private void OnApplicationQuit()
    {
        Save();
    }

    private AudioClip TrimSilence(AudioClip clip, float min)
    {
        var samples = new float[clip.samples];

        clip.GetData(samples, 0);

        return TrimSilence(new List<float>(samples), min, clip.channels, clip.frequency);
    }

    private AudioClip TrimSilence(List<float> samples, float min, int channels, int hz)
    {
        return TrimSilence(samples, min, channels, hz, false, false);
    }

    private AudioClip TrimSilence(List<float> samples, float min, int channels, int hz, bool _3D, bool stream)
    {
        int i;

        for (i = 0; i < samples.Count; i++)
        {
            if (Mathf.Abs(samples[i]) > min)
            {
                break;
            }
        }

        samples.RemoveRange(0, i);

        for (i = samples.Count - 1; i > 0; i--)
        {
            if (Mathf.Abs(samples[i]) > min)
            {
                break;
            }
        }

        samples.RemoveRange(i, samples.Count - i);

        var clip = AudioClip.Create("TempClip", samples.Count, channels, hz, _3D, stream);

        clip.SetData(samples.ToArray(), 0);

        return clip;
    }

    private FileStream CreateEmpty(string filepath)
    {
        var fileStream = new FileStream(filepath, FileMode.Create);
        byte emptyByte = new byte();

        for (int i = 0; i < HEADER_SIZE; i++) //preparing the header
        {
            fileStream.WriteByte(emptyByte);
        }

        return fileStream;
    }

    private void ConvertAndWrite(FileStream fileStream, AudioClip clip)
    {
        var samples = new float[clip.samples];

        clip.GetData(samples, 0);

        Int16[] intData = new Int16[samples.Length];
        //converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]

        Byte[] bytesData = new Byte[samples.Length * 2];
        //bytesData array is twice the size of
        //dataSource array because a float converted in Int16 is 2 bytes.

        int rescaleFactor = 32767; //to convert float to Int16

        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * rescaleFactor);
            Byte[] byteArr = new Byte[2];
            byteArr = BitConverter.GetBytes(intData[i]);
            byteArr.CopyTo(bytesData, i * 2);
        }

        fileStream.Write(bytesData, 0, bytesData.Length);
    }

    private void WriteHeader(FileStream fileStream, AudioClip clip)
    {

        var hz = clip.frequency;
        var channels = clip.channels;
        var samples = clip.samples;

        fileStream.Seek(0, SeekOrigin.Begin);

        Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        fileStream.Write(riff, 0, 4);

        Byte[] chunkSize = BitConverter.GetBytes(fileStream.Length - 8);
        fileStream.Write(chunkSize, 0, 4);

        Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        fileStream.Write(wave, 0, 4);

        Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
        fileStream.Write(fmt, 0, 4);

        Byte[] subChunk1 = BitConverter.GetBytes(16);
        fileStream.Write(subChunk1, 0, 4);

        UInt16 two = 2;
        UInt16 one = 1;

        Byte[] audioFormat = BitConverter.GetBytes(one);
        fileStream.Write(audioFormat, 0, 2);

        Byte[] numChannels = BitConverter.GetBytes(channels);
        fileStream.Write(numChannels, 0, 2);

        Byte[] sampleRate = BitConverter.GetBytes(hz);
        fileStream.Write(sampleRate, 0, 4);

        Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2
        fileStream.Write(byteRate, 0, 4);

        UInt16 blockAlign = (ushort)(channels * 2);
        fileStream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

        UInt16 bps = 16;
        Byte[] bitsPerSample = BitConverter.GetBytes(bps);
        fileStream.Write(bitsPerSample, 0, 2);

        Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
        fileStream.Write(datastring, 0, 4);

        Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
        fileStream.Write(subChunk2, 0, 4);

        //		fileStream.Close();
    }

    float  LevelMax(AudioClip clip)
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null)-(_sampleWindow+1); // null means the first microphone
        if (micPosition < 0) return 0;
        recordingClip.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++) {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak) {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

    void Update(){
        float MicLoudness  = 0;
        MicLoudness = LevelMax (recordingClip);
        if(!IsRecording){
            if((MicLoudness>0.0001)){
                Debug.Log("start");
                IsRecording  = true;
                micp1 = Microphone.GetPosition(null);
            }
        }
        else{
            if(MicLoudness > 0.00001)
                count = 0;
            count++;
            if(count>60){
                micp2 = Microphone.GetPosition(null);
                float[] waveData = new float[micp2-micp1];
                recordingClip.GetData(waveData,micp1);
                AudioClip _clipSave = AudioClip.Create("MySinusoid",(micp2-micp1),1,44100,false);
                _clipSave.SetData(waveData,0);
                name =  DateTime.Now.ToString("yyyy-MM-dd-HH\\hmm\\m");
                foldername = DateTime.Now.ToString("yyyy-MM-dd-HH");
                //string path = Application.persistentDataPath + "/"+DateTime.Now.ToString("yyyy-MM-dd-HH-ss\\hmm\\m") +name;
                string path = "./Data/"+DateTime.Now.ToString("yyyy-MM-dd-HH-ss\\hmm\\m") +name;
                new SavWav().Save(path,_clipSave);
                IsRecording = false;
                checkSpeech = true;

            }
        }
        
    }
     
}
