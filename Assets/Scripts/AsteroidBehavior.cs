using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {

    //have asteroids travel down at randomized speed
    //if collide with player, damage player
    //have it blow up after 5 hits
    private int _lives = 5;
    private float _speed;
    [SerializeField]
    private GameObject _hitEffect;
    [SerializeField]
    private GameObject _death;
	// Use this for initialization
	void Start () {
        _speed = Random.Range(1.5f, 2.1f);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (this.transform.position.y < -15)
            Destroy(this.gameObject);
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Debug.Log("Asteroid has been hit!");
            Instantiate(_hitEffect, this.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            DamageEnemy();
        }
        if (other.tag == "Player")
        {
            Debug.Log("Asteroid has been hit by the player");
            PlayerBehavior player = other.GetComponent<PlayerBehavior>();
            if (player != null)
            {
                player.DamagePlayer();
            }
            this._lives -= 5;
        }
    }

    private void DamageEnemy()
    {
        if (_lives > 0)
        {
            _lives--;
        }
        if (_lives <= 0)
        {
            Instantiate(_death, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
