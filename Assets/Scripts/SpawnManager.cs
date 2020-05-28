using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    //array that holds planet gameobjects
    [SerializeField]
    private GameObject[] _planets;
    //array that holds scenery ships
    [SerializeField]
    private GameObject[] _sceneryShips;
    [SerializeField]
    private GameObject[] _asteroids;
    [SerializeField]
    private GameObject _berserker;
    [SerializeField]
    private GameObject _berserkerLeft;
    [SerializeField]
    private GameObject _littleShooter;
    [SerializeField]
    private GameObject _bomber;
    [SerializeField]
    private GameObject _boss1;
    //bool holding value if player is killed
    [SerializeField]
    private bool _isPlayerDead = false;
    [SerializeField]
    private int _enemiesDestroyed = 0;
    [SerializeField]
    public bool _bossSpawned = false;
    int planetIndex;
    int sceneryShipIndex;
    int asteroidIndex;

    // Use this for initialization
    void Start () {
        StartCoroutine("SpawnBackgroundPlanets");
        StartCoroutine("SpawnBackgroundShips");
        StartCoroutine("SpawnAsteroids");
        StartCoroutine("SpawnBerserkerWave");
        StartCoroutine("SpawnShooterWave");
        StartCoroutine("SpawnBomberWave");
    }
	
	// Update is called once per frame
	void Update () {
		if(_enemiesDestroyed >= 20 && _bossSpawned == false)
        {
            _bossSpawned = true;
            Debug.Log("Boss active.");
            Instantiate(_boss1, new Vector3(0, 14, 0), Quaternion.identity);
            
        }
	}

    IEnumerator SpawnBackgroundPlanets()
    {
        while (!_isPlayerDead && _enemiesDestroyed < 20)
        {
            planetIndex = Random.Range(0, _planets.Length);
            Instantiate(_planets[planetIndex], new Vector3(Random.Range(-9.5f, 9.5f), 15, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10.0f, 13.0f));
        }
    }

    IEnumerator SpawnBackgroundShips()
    {
        while (!_isPlayerDead && _enemiesDestroyed < 20)
        {
            sceneryShipIndex = Random.Range(0, _sceneryShips.Length);
            Instantiate(_sceneryShips[sceneryShipIndex], new Vector3(Random.Range(-9.5f, 9.5f), 15, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        }
    }

    IEnumerator SpawnAsteroids()
    {
        while (!_isPlayerDead && _enemiesDestroyed < 20)
        {
            asteroidIndex = Random.Range(0, _asteroids.Length);
            Instantiate(_asteroids[asteroidIndex], new Vector3(Random.Range(-9.5f, 9.5f), 15, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        }
    }

    IEnumerator SpawnBerserkerWave()
    {
        while (!_isPlayerDead && _enemiesDestroyed < 20)
        {
            InstantiateBerserkerWave();
            yield return new WaitForSeconds(Random.Range(14.0f, 17.0f));
            
        }
    }
    IEnumerator SpawnShooterWave()
    {
        while (!_isPlayerDead && _enemiesDestroyed < 20)
        {
            yield return new WaitForSeconds(Random.Range(13.0f, 17.0f));
            InstantiateShooterWave();
        }
    }

    IEnumerator SpawnBomberWave()
    {
        while (!_isPlayerDead && _enemiesDestroyed < 20)
        {
            Instantiate(_bomber, new Vector3(-10.66f, -.68f, 0), Quaternion.identity);
            yield return new WaitForSeconds(20);
            
        }
    }


    private void InstantiateBerserkerWave()
    {
        int offset = 0;
        for (int i = 0; i <= 2; i++)
        {
            Instantiate(_berserker, new Vector3(2 + offset, 13, 0), Quaternion.identity);
            Instantiate(_berserkerLeft, new Vector3(-2 - offset, 13, 0), Quaternion.identity);
            offset += 2;
        }
    }

    private void InstantiateShooterWave()
    {
        int offset = 0;
        for (int i = 0; i < 2; i++)
        {
            Instantiate(_littleShooter, new Vector3(2 + offset, 13, 0), Quaternion.identity);
            Instantiate(_littleShooter, new Vector3(-2 - offset, 13, 0), Quaternion.identity);
            offset += 2;
        }
    }

    public void UpdateDestroyedEnemies()
    {
        _enemiesDestroyed++;
    }
    
}
