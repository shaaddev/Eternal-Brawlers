using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenu;

    void Start()
    {
        gameOverMenu.SetActive(false);
    }

    public void GetMenu(){
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }

}
