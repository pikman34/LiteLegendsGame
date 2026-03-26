using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    public float turnSpeed = 900f;
    public float maxTurnAngle = 65f;

    public void TurnTowards(Vector3 worldPoint)
    {
        Vector3 dir = worldPoint - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(dir);

        float angle = Quaternion.Angle(transform.rotation, targetRotation);
        float maxThisCall = Mathf.Min(maxTurnAngle, angle);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            Mathf.Min(turnSpeed * Time.deltaTime, maxThisCall)
        );
    }
}