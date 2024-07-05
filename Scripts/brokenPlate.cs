using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using Oculus.Interaction;
using Unity.VisualScripting;
using UnityEngine;

public class brokenPlate : MonoBehaviour
{
       public AudioClip[] arr;
    int a = 0;
    System.Random rand ;
   public GameObject brokenP;
   public GameObject dest;
   public int num = 1;
   public int isbroken = 0;
   float z = 0;
   float y = 0;
   float x = 0;
    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
    }
   void isBroken(float x,float y,float z){
      if((Math.Abs(x) > 1) && (isbroken <20))
         isbroken +=3;
      if((Math.Abs(y) > 1) && (isbroken <20))
         isbroken +=3;
      if((Math.Abs(z) > 1) && (isbroken <20))
         isbroken +=3;
      if(isbroken != 0)
         isbroken--;
         
   }
    // Update is called once per frame
    void Update()
    {
        isBroken(x,y,z);
        Vector3 save = gameObject.GetComponent<Rigidbody>().velocity;
        x = save.x;
        y = save.y;
        z = save.z;
    }
   private void OnCollisionEnter(Collision collision)
   {      
      String name  = collision.transform.name;
      if(!name.Contains("Coll") && (num==1) && (isbroken >0)){
         num++;
         Vector3 p = gameObject.transform.position + new Vector3(0,0.1f,0);
         GameObject obj = Instantiate(brokenP,gameObject.transform.position,gameObject.transform.rotation);//, gameObject.transform.position,gameObject.transform.rotation);
         //obj.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
         this.GetComponent<MeshRenderer>().enabled = false;
         this.GetComponent<MeshCollider>().enabled = false;
         Rigidbody a = GetComponent<Rigidbody>();
         a.isKinematic = true;
             this.GetComponent<AudioSource>().Stop();
            int n =rand.Next(0,5);
            this.GetComponent<AudioSource>().PlayOneShot(arr[1], 0);
         //Destroy(dest);
      }
   }

}
