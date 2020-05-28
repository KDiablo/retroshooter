using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBehavior : MonoBehaviour {

    private float opacity = 0;
	// Use this for initialization
	void Start () {
        StartCoroutine("FadeInRoutine");
        Cursor.visible = true;
	}
	
	

    private IEnumerator FadeInRoutine()
    {
        while(opacity < 1)
        {
            opacity += .05f;
            SpriteRenderer spriteColor = this.GetComponent<SpriteRenderer>();
            spriteColor.color = new Color(spriteColor.color.r, spriteColor.color.g, spriteColor.color.b, opacity);
            yield return new WaitForSeconds(.2f);
            Debug.Log("opacity updated" + opacity);
        }
        
    }
}
