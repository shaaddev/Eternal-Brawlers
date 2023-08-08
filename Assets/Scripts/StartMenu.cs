using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private bool keyPressed = false;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.anyKey && !keyPressed)
        {
            keyPressed = true;
            SceneManager.LoadScene(1);
        }
 
    }
}
