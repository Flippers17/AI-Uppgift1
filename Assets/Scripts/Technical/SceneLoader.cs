using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadSceneAtIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
