using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerBehavior : MonoBehaviour {

    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private Sprite _playerStraight;
    [SerializeField]
    private Sprite _playerTurnLeft;
    [SerializeField]
    private Sprite _playerTurnRight;
    [SerializeField]
    private AudioClip _shootSound;
    [SerializeField]
    private AudioClip _damagedSound;
    [SerializeField]
    private AudioClip _playerDeath;
    [SerializeField]
    private GameObject _shieldPrefab;

    private float _speed = 6.0f;
    private int _lives = 3;

    UIManager uim;
    private bool _shieldEnabled = false;




    // Use this for initialization
    void Start () {
        //unload last scene and destroy all associated gameObjects
        Resources.UnloadUnusedAssets();
        Cursor.visible = false;
        uim = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
        
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
        FireWeapon();
	}

    //methdod to handle player movement
    private void Movement()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (Input.GetAxis("Horizontal") == -1)
        {//changes sprite if player moves left
            if (spriteRenderer.sprite != _playerTurnLeft)
                spriteRenderer.sprite = _playerTurnLeft;
        }
        else if (Input.GetAxis("Horizontal") == 1)
        {//changes sprite if player moves right
            if (spriteRenderer.sprite != _playerTurnRight)
                spriteRenderer.sprite = _playerTurnRight;
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {//changes sprite if player is not moving horizontally
            if (spriteRenderer.sprite != _playerStraight)
                spriteRenderer.sprite = _playerStraight;
        }

        //next two lines control up and down, left and right movement
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * Input.GetAxis("Vertical") * _speed * Time.deltaTime);

        //****************************** Keep Player In-bounds *******************************************
        if (this.transform.position.x < -8.38f)
            this.transform.position = new Vector3(-8.38f, this.transform.position.y, 0);
        if (this.transform.position.x > 8.38f)
            this.transform.position = new Vector3(8.38f, this.transform.position.y, 0);
        if (this.transform.position.y < -4.5f)
            this.transform.position = new Vector3(this.transform.position.x, -4.5f, 0);
        if (this.transform.position.y > 4.25f)
            this.transform.position = new Vector3(this.transform.position.x, 4.25f, 0);



    }
    private void FireWeapon()
    {
        //if player presses the space bar or left mouse button, a shot fires
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            //spawn a laser gameobject
            Instantiate(_laser, transform.position + new Vector3(0, 0.344f, 0), Quaternion.identity);
            AudioSource.PlayClipAtPoint(_shootSound, new Vector3(0, 0, -10));
        }
    }

    public void DamagePlayer()
    {
        if (!_shieldEnabled)
        {
            if (_lives > 0)
            {//if the player has more than 0 lives, play the damaged noise and subtract a life
                AudioSource.PlayClipAtPoint(_damagedSound, new Vector3(0, 0, -10));
                _lives--;
                uim.UpdateLives(_lives);
            }
            if (_lives == 0)
            {//if lives are 0, destroy player game object and play the player death noise
                uim.UpdateLives(_lives);
                AudioSource.PlayClipAtPoint(_playerDeath, new Vector3(0, 0, -10));
                SpawnManagerBehavior sm = GameObject.Find("SpawnManager").GetComponent<SpawnManagerBehavior>();
                sm.EndGame();
                this.gameObject.SetActive(false);
            }
        }  
    }

    public void ShieldPowerupRoutine()
    {
        _shieldEnabled = true;
        _shieldPrefab.SetActive(true);
        StartCoroutine("ShieldPowerdownRoutine");
    }

    private IEnumerator ShieldPowerdownRoutine()
    {
        yield return new WaitForSeconds(5);
        _shieldEnabled = false;
        _shieldPrefab.SetActive(false);
    }
    
    public void HealthUp()
    {
        if(_lives < 3)
        {
            _lives++;
            uim.UpdateLives(_lives);
        }
        
    }
}

