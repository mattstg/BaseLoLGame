using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class GridScrollInit : MonoBehaviour {

	void Start ()
    {
        gameObject.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
    }
	
	void Update ()
    {
		
	}
}
