using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneLoaderPort : ScriptableObject
{
    public void LoadSceneAtIndex(int index)
    {
        SceneLoader.LoadSceneAtIndex(index);
    }


    public void QuitGame()
    {
        SceneLoader.QuitGame();
    }
}
