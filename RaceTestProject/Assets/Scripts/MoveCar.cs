using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCar : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 direction;
    private Rigidbody rb;
    public GameObject arrow;
    public GameObject panel;
    public GameObject blinkArea;
    private RotateArrow rotateArrowScript;
    private Indicators indicatorsScript;
    private BlinkCar blinkScript;
    private float angle;
    private float sizeScreenY;
    private float sizeScreenX;
    private int counterCheckPoints;
    private BoxCollider boxcolliderCheckPoint;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        rotateArrowScript = arrow.GetComponent<RotateArrow>();
        indicatorsScript = panel.GetComponent<Indicators>();
        blinkScript = blinkArea.GetComponent<BlinkCar>();
        sizeScreenY = Screen.height / 2;
        sizeScreenX = Screen.width / 2;
    }

    private void FixedUpdate() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            switch(touch.phase) {
                case TouchPhase.Began:
                    startPos = touch.position;
                break;
                case TouchPhase.Moved:
                    direction = startPos - touch.position;
                    rotateArrowScript.rotateArrow(angle, direction);
                    rotateArrowScript.increaseSizeArrow(direction, sizeScreenY);
                    indicatorsScript.measureTensionForce(direction, sizeScreenY);
                break;
                case TouchPhase.Ended:
                    if (direction.y > 0f) {
                        if (direction.y > sizeScreenY) {
                            direction.y = sizeScreenY;
                        }
                        angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                        transform.Rotate(Vector3.up, angle);
                        rb.AddRelativeForce((direction.x / sizeScreenY) * 100 * 2, 0f, (direction.y / sizeScreenX) * 100 * 2);
                    }
                    direction = Vector2.zero;
                    rotateArrowScript.resetRotateArrow(angle);
                    indicatorsScript.resetTensionForce();
                break;
            }
        }

        if (rb.velocity == Vector3.zero) {
            indicatorsScript.measureProgress(transform.position);
        }
    }

    public void restartLevel () {
        counterCheckPoints = 0;
        blinkScript.enableBoxPoints();
        indicatorsScript.resetTensionForce();
        indicatorsScript.resetProgress();
        indicatorsScript.setZeroTime();
        blinkScript.blink(transform, counterCheckPoints, rb, boxcolliderCheckPoint);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Blink") {
            counterCheckPoints = blinkScript.blink(transform, counterCheckPoints, rb, boxcolliderCheckPoint);
            if (counterCheckPoints != 0) {
                boxcolliderCheckPoint = blinkScript.getBox(counterCheckPoints);
            }
        } else if (other.tag == "FinishPoint") {
            if (PlayerPrefs.GetFloat("recordScore") > indicatorsScript.getTime()) {
                PlayerPrefs.SetFloat("recordScore", indicatorsScript.getTime());
            }
            SceneManager.LoadScene("Menu");
        } else if (other.tag == "RoadTrigger") {
            boxcolliderCheckPoint = other.GetComponent<BoxCollider>();
            boxcolliderCheckPoint.enabled = false;
            counterCheckPoints++;
        }
    }
}
