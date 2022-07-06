using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject levelMenu;
    
    public void GameQuit()
    {
        Application.Quit();
    }

    public void ShowLevelMenu()
    {
        levelMenu.SetActive(true);
    }

    public void HideLevelMenu()
    {
        levelMenu.SetActive(false);
    }

    public void LoadLevel(string id)
    {
        GameObject.Find("LevelObject").GetComponent<LevelInfo>().id = id;
        SceneManager.LoadScene(1);
    }
}
