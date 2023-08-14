using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private bool keyPressed = false;
    
    void Update()
    {
        if(Input.anyKey && !keyPressed)
        {
            keyPressed = true;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
