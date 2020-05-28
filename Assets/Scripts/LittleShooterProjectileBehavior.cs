using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleShooterProjectileBehavior : MonoBehaviour {
    //Behavior for the fan of projectiles shot by the LittleShooter enemy


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        DetermineProjectile();
        Cleanup();
	}

    private void DetermineProjectile()
    {
        if (this.transform.tag.Equals("LS_Left"))
        {
            LeftMovement();
        }
        else if (this.transform.tag.Equals("LS_Center"))
        {
            CenterMovement();
        }
        else if (this.transform.tag.Equals("LS_Right"))
        {
            RightMovement();
        }
    }

    private void LeftMovement()
    {
        this.transform.Translate(Vector3.down * 2.5f * Time.deltaTime);
        this.transform.Translate(Vector3.left * .7f * Time.deltaTime);
    }
    private void CenterMovement()
    {
        this.transform.Translate(Vector3.down * 2.5f * Time.deltaTime);
        
    }
    private void RightMovement()
    {
        this.transform.Translate(Vector3.down * 2.5f * Time.deltaTime);
        this.transform.Translate(Vector3.right * .7f * Time.deltaTime);
    }

    private void Cleanup()
    {
        if (this.transform.position.y < -6)
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
