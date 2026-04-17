using UnityEngine;
using System.Collections;

public class PlayerTurn : MonoBehaviour
{
    public float turnSpeed = 12f;
    public float stopAngle = 2f;
    public float maxTurnAngle = 65f;

    Coroutine turnRoutine;

    public void TurnTowards(Vector3 worldPoint)
    {
        if (turnRoutine != null)
            StopCoroutine(turnRoutine);

        turnRoutine = StartCoroutine(SmoothTurn(worldPoint));
    }

    public void StopTurning()
    {
        if (turnRoutine != null)
        {
            StopCoroutine(turnRoutine);
            turnRoutine = null;
        }
    }

    IEnumerator SmoothTurn(Vector3 worldPoint)
    {
        Vector3 toTarget = worldPoint - transform.position;
        toTarget.y = 0f;

        if (toTarget.sqrMagnitude < 0.001f)
        {
            turnRoutine = null;
            yield break;
        }

        Vector3 currentForward = transform.forward;
        currentForward.y = 0f;
        currentForward.Normalize();

        Vector3 targetDirection = toTarget.normalized;
        float signedAngle = Vector3.SignedAngle(currentForward, targetDirection, Vector3.up);
        signedAngle = Mathf.Clamp(signedAngle, -maxTurnAngle, maxTurnAngle);

        Quaternion targetRotation = Quaternion.AngleAxis(signedAngle, Vector3.up) * transform.rotation;

        while (Quaternion.Angle(transform.rotation, targetRotation) > stopAngle)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.rotation = targetRotation;
        turnRoutine = null;
    }
}