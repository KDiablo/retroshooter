using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToMainMenuBehavior : MonoBehaviour, IPointerClickHandler {

    private float opacity = 0;


    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(0);
    }

    void Start()
    {
        StartCoroutine("FadeInRoutine");
    }



    private IEnumerator FadeInRoutine()
    {
        while (opacity < 1)
        {
            opacity += .05f;
            Image spriteColor = this.GetComponent<Image>();
            spriteColor.color = new Color(spriteColor.color.r, spriteColor.color.g, spriteColor.color.b, opacity);
            yield return new WaitForSeconds(.2f);
            Debug.Log("opacity updated" + opacity);
        }

    }
}
