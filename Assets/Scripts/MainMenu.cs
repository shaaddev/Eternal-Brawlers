using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private bool keyPressed = false;

    public GameObject switchT;
    public Button sButton;
    // bool isSwitched = true;

    void Start(){
        ScoreSystem.scoreValue = 0;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !keyPressed)
        {
            keyPressed = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void gameMode(){
        bool isGame = switchT.GetComponent<Toggle>().isOn;
        if (isGame){
            sButton.interactable = true;
            SceneManager.LoadScene("EndlessLevel");
        } else {
            sButton.interactable = false;
        }
        sButton.interactable = true;
    }


}
