using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberBehavior : MonoBehaviour {

    [SerializeField]
    private GameObject _projectile;
    private int _lives = 25;
    [SerializeField]
    private GameObject _hitEffect;
    [SerializeField]
    private GameObject _death;


	// Use this for initialization
	void Start () {
        StartCoroutine("Attack");
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        UpdateDamageState();
	}
    private void Movement()
    {
        if(this.transform.position.x < 0)
        {
            this.transform.Translate(Vector3.right * 1.7f * Time.deltaTime);
            this.transform.Translate(Vector3.up * .3f * Time.deltaTime);
        }
        else if (this.transform.position.x >= 0)
        {
            this.transform.Translate(Vector3.right * 1.7f * Time.deltaTime);
            this.transform.Translate(Vector3.down * .3f * Time.deltaTime);
        }
        if(this.transform.position.x > 11)
        {
            Destroy(this.gameObject);
        }
    }

    private void UpdateDamageState()
    {
        if (this._lives == 10)
            this.transform.GetChild(1).gameObject.SetActive(true);
        else if(this._lives == 15)
            this.transform.GetChild(2).gameObject.SetActive(true);
        else if (this._lives == 20)
            this.transform.GetChild(0).gameObject.SetActive(true);
    }

    IEnumerator Attack()
    {
        while (true)
        {
            Instantiate(_projectile, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(.12f);
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
            SpawnManagerBehavior spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManagerBehavior>();
            spawnManager.UpdateDestroyedEnemies();
            Instantiate(_death, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            
        }
    }
}
