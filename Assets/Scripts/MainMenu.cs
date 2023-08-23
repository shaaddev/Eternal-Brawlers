using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool keyPressed = false;

    public void Practice()
    {
        SceneManager.LoadScene("Practice");
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !keyPressed)
        {
            keyPressed = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
