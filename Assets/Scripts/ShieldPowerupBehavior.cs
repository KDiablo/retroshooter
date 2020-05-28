using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerupBehavior : MonoBehaviour {

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupId;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        Cleanup();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(_powerupId + " collided with " + other.name);
            //access player

            PlayerBehavior player = other.GetComponent<PlayerBehavior>();

            if (player != null)
            {
                if (_powerupId == 0)
                {
                    player.HealthUp();
                    //increase health
                }
                else if (_powerupId == 1)
                {
                    //enable speed boost
                    //player.SpeedPowerUpRoutine();
                }
                else if (_powerupId == 2)
                {
                    //enable shields
                    player.ShieldPowerupRoutine();
                }

            }

            Destroy(this.gameObject);

        }

    }
    private void Cleanup()
    {
        if (this.transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }
    }
}
