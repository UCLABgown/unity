using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playBroekn : MonoBehaviour
{
    public AudioClip[] arr;
    int a = 0;
    System.Random rand ;
    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
                    gameObject.GetComponent<AudioSource>().Stop();
            int n =rand.Next(0,5);
            gameObject.GetComponent<AudioSource>().PlayOneShot(arr[n], 0.8f);
        Debug.Log("qqqqqqqqqqq");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
