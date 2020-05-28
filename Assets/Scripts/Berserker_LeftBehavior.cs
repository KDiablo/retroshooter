using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker_LeftBehavior : MonoBehaviour
{
    /*
     * This enemy will move down from out of view, and once it hits a lower boundary set by a random number generator, 
     * it will start strafing to the left. If it does not detect that it is lined up vertically with the player by the
     * time it hits the right side of the screen, it will strafe right. If the enemy detects that it is lined up, it will move fast
     * downward toward the player.
     * 
     */
    [SerializeField]
    private float boundary;
    [SerializeField]
    private bool lowerBoundReached = false;
    private bool rightBoundReached = true;
    private bool leftBoundReached = false;
    private bool aligned;
    private int lives = 3;
    //[SerializeField]
    //private GameObject _playerObject;
    [SerializeField]
    private GameObject _death;
    [SerializeField]
    private GameObject _hitEffect;
    // Use this for initialization
    void Start()
    {
        boundary = Random.Range(3.0f, 4.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    private void Movement()
    {//controls movement
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (!lowerBoundReached)//if the lower bound is not reached, move downward
            this.transform.Translate(Vector3.down * 2 * Time.deltaTime);
        if (this.transform.position.y <= boundary)
        {//if berserker has reached the lower bound, start to strafe. if it is within alignment of the player, move downward
            lowerBoundReached = true;
            if (!rightBoundReached && leftBoundReached)
                this.transform.Translate(Vector3.right * 4 * Time.deltaTime);
            if (this.transform.position.x > 8.54f)
            {
                rightBoundReached = true;
                leftBoundReached = false;
            }
            if (rightBoundReached && !leftBoundReached)
            {
                this.transform.Translate(Vector3.left * 4 * Time.deltaTime);
            }
            if (this.transform.position.x < -8.54f)
            {
                rightBoundReached = false;
                leftBoundReached = true;
            }
            if(player != null)
            {
                if (Mathf.Abs(this.transform.position.x - player.transform.position.x) <= 0.5f)
                {
                    rightBoundReached = true;
                    leftBoundReached = true;
                    aligned = true;

                }
            }
            if(player != null)
            {
                if (Mathf.Abs(this.transform.position.x - player.transform.position.x) <= 0.5f)
                {
                    rightBoundReached = true;
                    leftBoundReached = true;
                    aligned = true;

                }
            }
            
            if (aligned)
            {
                this.transform.Translate(Vector3.down * 6 * Time.deltaTime);
            }
            if (this.transform.position.y < -6)
            {
                this.transform.position = new Vector3(Random.Range(-8.54f, 8.54f), 7.25f, 0);
                aligned = false;
                lowerBoundReached = false;
                rightBoundReached = false;
                leftBoundReached = true;

            }
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
        if (lives > 0)
        {
            lives--;
        }
        if (lives <= 0)
        {
            SpawnManagerBehavior spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManagerBehavior>();
            spawnManager.UpdateDestroyedEnemies();
            Instantiate(_death, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            
        }
    }
}
