using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);

        InputManager._INPUT_MANAGER.AddListennerToPressScape(MenuPause);
    }

    public void MenuPause()
    {
        if (pauseMenu != null)
        {
            Debug.Log("wuw");
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            GameManager.Instance.pauseGame(true);
            InputManager._INPUT_MANAGER.RemoveListennerToPressScape(MenuPause);
        }
    }

    public void ToGameAgain()
    {
        InputManager._INPUT_MANAGER.AddListennerToPressScape(MenuPause);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameManager.Instance.pauseGame(false);
    }

    public void ToMenu()
    {
        SceneManager.Instance.LoadScene(0);
    }

}