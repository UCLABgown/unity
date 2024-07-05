using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBubble : MonoBehaviour
{

    public ParticleSystem particleSystem;
    public Camera camera;

    public Transform bubbleArr;
    private List<Transform> gArr = new List<Transform>();

    public void CalParticleRect(){

        
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        int count = particleSystem.GetParticles(particles);
        for (int i = 0; i < count; i++)
        {
            float size = particles[i].size;
            //Vector3 p = WorldPointToScreenPoint(particles[i].position);
            gArr[i].position = particles[i].position;
            gArr[i].localScale =  new Vector3(size,size,size);

        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        int c = bubbleArr.childCount;

        for(int n =0; n<c; n++){
            gArr.Add(bubbleArr.GetChild(n));

        }
    }

    // Update is called once per frame
    void Update()
    {
        CalParticleRect();
    }

    private Vector3 WorldPointToScreenPoint(Vector3 worldPoint)
    {
        Vector3 screenPoint = camera.WorldToScreenPoint(worldPoint);
        screenPoint.y = camera.pixelHeight - screenPoint.y;

        return screenPoint;
    }



}
