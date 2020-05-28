using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberBulletBehavior : MonoBehaviour {

    private int _leftRight;
    private int _upDown;
    private float _vertSpeed;
    private float _horizSpeed;

	// Use this for initialization
	void Start () {
        _leftRight = Random.Range(0, 2);
        _upDown = Random.Range(0, 2);
        _vertSpeed = Random.Range(1.0f, 2.7f);
        _horizSpeed = Random.Range(1.0f, 2.7f);
    }
	
	// Update is called once per frame
	void Update () {
		if(_leftRight == 0 && _upDown == 0)
        {//left and up
            this.transform.Translate(Vector3.left * _horizSpeed * Time.deltaTime);
            this.transform.Translate(Vector3.up * _vertSpeed * Time.deltaTime);
        }
        if(_leftRight == 1 && _upDown == 0)
        {//right and up
            this.transform.Translate(Vector3.right * _horizSpeed * Time.deltaTime);
            this.transform.Translate(Vector3.up * _vertSpeed * Time.deltaTime);
        }
        if(_leftRight == 0 && _upDown == 1)
        {// left and down
            this.transform.Translate(Vector3.left * _horizSpeed * Time.deltaTime);
            this.transform.Translate(Vector3.down * _vertSpeed * Time.deltaTime);
        }
        if(_leftRight == 1 && _upDown == 1)
        {//right and down
            this.transform.Translate(Vector3.right * _horizSpeed * Time.deltaTime);
            this.transform.Translate(Vector3.down * _vertSpeed * Time.deltaTime);
        }
        if (this.transform.position.y > 10 || this.transform.position.y < -10 || this.transform.position.x > 10 || this.transform.position.x < -10)
            Destroy(this.gameObject);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerBehavior player = other.GetComponent<PlayerBehavior>();
            if (player != null)
            {
                player.DamagePlayer();
            }
            Destroy(this.gameObject);
        }
    }
}
