using UnityEngine;

public class BoardItem : MonoBehaviour
{
    public float moveSpeed = 15f; // How quickly to move this GameObject
    public Vector3 position; // Desired position to move to
    public Vector3 offset; // additional (optional) offset from position

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = position + offset;
        float distance = (targetPosition - transform.position).magnitude;
        float moveDistance = moveSpeed * distance * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveDistance);
    }
}
