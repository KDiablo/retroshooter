using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.down * 1.5f * Time.deltaTime);
	}
}
