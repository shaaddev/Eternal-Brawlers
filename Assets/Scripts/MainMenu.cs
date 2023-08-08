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

    public void OnevOne()
    {
        SceneManager.LoadScene("1v1");
    }

    public void FFAthree()
    {
        SceneManager.LoadScene("FFA-3");
    }

    public void FFAfour()
    {
        SceneManager.LoadScene("FFA-4");
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
