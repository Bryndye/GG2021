using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level Build");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
