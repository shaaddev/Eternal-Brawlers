using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private bool keyPressed = false;

    public GameObject switchT;
    // bool isSwitched = true;

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
            SceneManager.LoadScene("EndlessLevel");
            // Debug.Log("Endless Mode");
        } else {
            SceneManager.LoadScene("NormalLevel");
            // Debug.Log("Normal Mode");
        }
    }


}
