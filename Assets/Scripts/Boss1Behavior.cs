using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1Behavior : MonoBehaviour {

    private float boundary;
    private bool lowerBoundReached = false;
    private bool rightBoundReached = false;
    private bool leftBoundReached = true;
    private bool _readyToAttack = true;
    private int _lives = 40;
    [SerializeField]
    private GameObject _hitEffect;
    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    private GameObject _death;
    [SerializeField]
    private GameObject[] _spread;
    
    // Use this for initialization
    void Start () {
        boundary = 2.5f;
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
        
	}

    private void Movement()
    {
        if (!lowerBoundReached)//if the lower bound is not reached, move downward
            this.transform.Translate(Vector3.down * 1.4f * Time.deltaTime);
        if (this.transform.position.y <= boundary)
        {//if berserker has reached the lower bound, start to strafe. if it is within alignment of the player, move downward
            
            lowerBoundReached = true;
            if (!rightBoundReached && leftBoundReached)
                this.transform.Translate(Vector3.right * 2.5f * Time.deltaTime);
            if (this.transform.position.x > 7.25f)
            {
                rightBoundReached = true;
                leftBoundReached = false;
            }
            if (rightBoundReached && !leftBoundReached)
            {
                this.transform.Translate(Vector3.left * 2.5f * Time.deltaTime);
            }
            if (this.transform.position.x < -7.25f)
            {
                rightBoundReached = false;
                leftBoundReached = true;
            }
            if (_readyToAttack && lowerBoundReached)
            {
                _readyToAttack = false;
                StartCoroutine("Attack");
                StartCoroutine("SecondaryAttack");
            }
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            Instantiate(_projectile, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(.04f);
        }

    }

    IEnumerator SecondaryAttack()
    {
        while (true)
        {
            foreach (GameObject p in _spread)
            {
                Instantiate(p, this.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(6);
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
            Instantiate(_death, this.transform.position + new Vector3(-.5f, .5f, 0), Quaternion.identity);
            Instantiate(_death, this.transform.position + new Vector3(.5f, -.5f, 0), Quaternion.identity);
            Instantiate(_death, this.transform.position + new Vector3(-.5f, -.5f, 0), Quaternion.identity);
            Instantiate(_death, this.transform.position + new Vector3(.5f, .5f, 0), Quaternion.identity);
            SpawnManagerBehavior sm = GameObject.Find("SpawnManager").GetComponent<SpawnManagerBehavior>();
            sm.NextLevel();
            Destroy(this.gameObject);
   
        }
    }

    private void WinLevel()
    {
        StartCoroutine("TransitionLevel");
    }

    private IEnumerator TransitionLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3);
    }
}
