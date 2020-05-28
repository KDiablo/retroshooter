﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserBehavior : MonoBehaviour {
    private float _speed = 10.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //move player laser up at a speed of 10
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (this.transform.position.y > 5.5f)
            Destroy(this.gameObject);
	}
}
