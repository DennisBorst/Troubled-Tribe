using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PausedMenu : MonoBehaviour
{
    public GameObject ui;

    void Update()
    {
        if (ui != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.P)))
            {
                Toggle();
            }
        }
    }

    public void Toggle()
    {

        if (ui != null)
        {
            ui.SetActive(!ui.activeSelf);

            if (ui.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void MainMenu(string levelName)
    {
        Toggle();
        SceneManager.LoadScene(levelName);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
