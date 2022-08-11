using UnityEngine;

public class RotateArrow : MonoBehaviour
{
    private Quaternion origRotation;
    public Vector3 resetArrow;
    private Vector3 origSizeArrow;

    void Start()
    {
        resetArrow = transform.eulerAngles;
        origRotation = Quaternion.Euler(0f, 0f, 0f);
        origSizeArrow = transform.localScale;
    }

    public void rotateArrow (float angle, Vector2 direction) {
        if (direction.y > 0) {
            angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(new Vector3(90f, angle, 0.0f));
        }
    }

    public void increaseSizeArrow (Vector2 direction, float sizeScreen) {
        if (direction.y <= sizeScreen && direction.y > 0.0f) {
            float resizeArrow = origRotation.y + (direction.y / sizeScreen + 3);
            transform.localScale = new Vector3(transform.localScale.x, resizeArrow, transform.localScale.z);
        }
    }

    public void resetRotateArrow (float angle) {
        transform.localScale = origSizeArrow;
        transform.localRotation = Quaternion.Euler(new Vector3(90f, 0.0f, 0.0f));
    }
}
