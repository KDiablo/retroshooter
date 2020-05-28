using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Text _score;
    public Image _lifeImage;
    [SerializeField]
    private Sprite[] _livesArray;
    
    

    public void UpdateLives(int livesLeft)
    {
        _lifeImage.sprite = _livesArray[livesLeft];
    }

    public void UpdateScore(int destroyed)
    {
        _score.text = "Enemies Destroyed: " + destroyed;
    }

}
