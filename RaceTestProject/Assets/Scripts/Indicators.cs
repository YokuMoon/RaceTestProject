using UnityEngine;
using TMPro;

public class Indicators : MonoBehaviour
{
    public TextMeshProUGUI tensionForceText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI progress;
    public GameObject finishObject;
    private int tensionForce;
    private float seconds;
    private int progressRace;
    private float positionFinishObject;

    private void FixedUpdate() {
        seconds += Time.deltaTime;
        timeText.text = "Время: " + string.Format("{0:F4}", seconds);
        positionFinishObject = finishObject.transform.position.x + finishObject.transform.position.z;
    }

    public void measureTensionForce (Vector2 direction, float sizeScreen) {
        if (direction.y <= sizeScreen && direction.y > 0.0f) {
            tensionForce =  (int)(direction.y/sizeScreen * 100);
            tensionForceText.text = "Сила натяжения: " + tensionForce + "%";
        } else if (direction.y > 0.0f) {
            tensionForceText.text = "Сила натяжения: 100%";
        }
    }

    public void resetTensionForce () {
        tensionForceText.text = "Сила натяжения: 0%";
    }

    public void resetProgress () {
        progressRace = 0;
    }

    public void measureProgress (Vector3 positionCar) {
        progressRace = (int)((positionCar.x + positionCar.z) / positionFinishObject * 100);
        progress.text = "Прогресс: " + progressRace + "%";
    }

    public float getTime () {
        return seconds;
    }

    public void setZeroTime () {
        seconds = 0.0f;
    }
}
