using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuHelper : MonoBehaviour
{
    public TextMeshProUGUI recordText;

    private void Start () {
        recordText.text = "Лучшее время: " + string.Format("{0:F4}", PlayerPrefs.GetFloat("recordScore"));
    }

    public void PlayGame() {
        SceneManager.LoadScene("MainScene");
    }
}
