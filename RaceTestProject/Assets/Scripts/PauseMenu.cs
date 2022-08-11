using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPause;
    public GameObject pauseMenu;
    public GameObject car;
    private MoveCar carScript;
    public Rigidbody rb;

    private void Start () {
        isPause = false;
        rb = car.GetComponent<Rigidbody>();
        carScript = car.GetComponent<MoveCar>();
    }

    public void restartLevel () {
        Time.timeScale = 1f;
        isPause = false;
        pauseMenu.SetActive(false);
        carScript.restartLevel();
        // SceneManager.LoadScene("MainScene");
    }

    public void toMenu () {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void proceed () {
        if (isPause) {
            isPause = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void pause () {
        if (!isPause) {
            isPause = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
