using UnityEngine;

public class BlinkCar : MonoBehaviour
{
    public GameObject[] checkPoints;
    public BoxCollider[] boxPoints;

    public void enableBoxPoints() {
        for (int i = 0; i < boxPoints.Length; i++) {
            boxPoints[i].enabled = true;
        }
    }

    public int blink (Transform car, int counterCheckPoints, Rigidbody carRb, BoxCollider roadPoints) {
        if (counterCheckPoints != 0) {
            car.transform.position = checkPoints[counterCheckPoints-1].transform.position;
            car.transform.rotation = checkPoints[counterCheckPoints-1].transform.rotation;
            roadPoints.enabled = true;
            counterCheckPoints--;
        } else {
            car.transform.position = checkPoints[0].transform.position;
            car.transform.rotation = checkPoints[0].transform.rotation;
        }
        carRb.velocity = Vector3.zero;

        return counterCheckPoints;
    }

    public BoxCollider getBox (int counterCheckPoints) {
        return checkPoints[counterCheckPoints].GetComponent<BoxCollider>();
    }
}
