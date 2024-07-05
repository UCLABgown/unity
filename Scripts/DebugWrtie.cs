using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugWrtie : MonoBehaviour
{
	public TMP_Text t;
	void Update(){
		t.text = Application.persistentDataPath;
	}
}
