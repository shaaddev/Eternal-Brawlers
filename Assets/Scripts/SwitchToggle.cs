using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    public GameObject gameDetails;
    public GameObject normalMode;

    Toggle toggle;

    Vector2 handlePosition;

    void Awake(){
        normalMode.SetActive(true);
        gameDetails.SetActive(false);
        handlePosition = uiHandleRectTransform.anchoredPosition;

        toggle = GetComponent<Toggle>();
        
        toggle.onValueChanged.AddListener(ButtonClick);

        if (toggle.isOn)
            ButtonClick(true);
    }

    public void ButtonClick(bool on){
        if (on){
            uiHandleRectTransform.anchoredPosition = handlePosition * -1;
            gameDetails.SetActive(true);
            normalMode.SetActive(false);
        } else {
            uiHandleRectTransform.anchoredPosition = handlePosition;
            gameDetails.SetActive(false);
            normalMode.SetActive(true);
        }
    }

}
