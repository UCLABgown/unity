using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStart : MonoBehaviour
{
   public void ReStartgame(){
      SceneManager.LoadScene(0);
   }
}
