using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Change(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void Exit()
    {
        Application.Quit();
    }   
}

