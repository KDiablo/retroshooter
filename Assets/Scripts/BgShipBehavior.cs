using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgShipBehavior : MonoBehaviour {

    //used for scenery ships, simply move down slowly

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.down * .85f * Time.deltaTime);
        if (this.transform.position.y < -15)
            Destroy(this.gameObject);
    }
}
