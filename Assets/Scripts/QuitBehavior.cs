using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuitBehavior : MonoBehaviour {

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pressed");
            Application.Quit();
        }
    }
    private void Start()
    {
        if(Cursor.visible == false)
        {
            Cursor.visible = true;
        }
    }
}
