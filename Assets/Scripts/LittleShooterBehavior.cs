using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleShooterBehavior : MonoBehaviour {

    /*
     * This enemy will spawn out of view to the top of the screen and come 
     * down to a random boundary within to top 1/3 of the screen and stay in place
     * This enemy will shoot a fan of projectiles in a roughly 30 degree arc downward
     * every 1-2 seconds as long as it is alive.
     */
    private int _lives = 2;
    private bool _lowerBoundReached = false;
    [SerializeField]
    private GameObject[] _projectiles;
    [SerializeField]
    private GameObject _hitEffect;
    [SerializeField]
    private GameObject _death;
    private float _boundary;
    private bool _canAttack = false;
    private bool _locked = false;

	// Use this for initialization
	void Start () {
        _boundary = Random.Range(2.5f, 3.7f);
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
        if (_canAttack)
        {
            StartCoroutine("Attack");
            _canAttack = false;
        }
            
	}

    private void Movement()
    {
        if (!_lowerBoundReached)
            this.transform.Translate(Vector3.down * 3 * Time.deltaTime);
        if (!_locked)
        {
            if (this.transform.position.y <= _boundary)
            {
                _lowerBoundReached = true;
                _canAttack = true;
                _locked = true;
            }
        }
             
    }
    IEnumerator Attack()
    {
        while (true)
        {
            foreach(GameObject p in _projectiles)
            {
                Instantiate(p, this.transform.position, Quaternion.identity);
            }
            Debug.Log("pew pew");
            yield return new WaitForSeconds(Random.Range(2.0f, 2.7f));
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_hitEffect, this.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            DamageEnemy();
        }
        if (other.tag == "Player")
        {
            PlayerBehavior player = other.GetComponent<PlayerBehavior>();
            if (player != null)
            {
                player.DamagePlayer();
            }
            Instantiate(_death, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
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
            SpawnManagerBehavior spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManagerBehavior>();
            spawnManager.UpdateDestroyedEnemies();
            Instantiate(_death, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            
        }
    }
}
