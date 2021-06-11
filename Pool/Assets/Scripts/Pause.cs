using TMPro;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            paused = togglePause();
    }

    public bool togglePause()
    {
        if (!GetComponent<GameManager>().victoryConditionIsMet)
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
                return (false);
            }
            else
            {
                Time.timeScale = 0f;
                return (true);
            }
        }
        return paused;
    }
}