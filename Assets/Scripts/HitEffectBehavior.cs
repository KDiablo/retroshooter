using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectBehavior : MonoBehaviour {
    //this behavior swans a hit effect animation when the player shoots an enemy


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.up * 2 * Time.deltaTime);
        if (this.transform.position.y > 7)
            Destroy(this.gameObject);
	}
}
