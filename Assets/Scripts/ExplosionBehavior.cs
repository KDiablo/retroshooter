using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour {
    //this is the behavior of the explosion when an enemy dies


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.down * 4 * Time.deltaTime);
        if (this.transform.position.y < -6)
            Destroy(this.gameObject);
	}
}
