using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChangeTimer : MonoBehaviour
{

    public TMP_Text changeTimeText;

    void Start()
    {
        
    }

    public void Add(int points){
        Image uiImage = GetComponent<Image>();
        if (uiImage != null){
            uiImage.color = new Color32(229,0,0,150);
        }

        int pointsNoDecimal = points / 10;
        int pointsYesDecimal = points % 10;
        changeTimeText.text = "+ " + pointsNoDecimal + "." + pointsYesDecimal;
        StartCoroutine(deactivateDelay());
    }

    public void Subtract(int points){
        Image uiImage = GetComponent<Image>();
        if (uiImage != null){
            uiImage.color = new Color32(0,229,13,150);
        }

        int pointsNoDecimal = points / 10;
        int pointsYesDecimal = points % 10;
        changeTimeText.text = "- " + pointsNoDecimal + "." + pointsYesDecimal;
        StartCoroutine(deactivateDelay());
    }

    IEnumerator deactivateDelay(){
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
    }
}
