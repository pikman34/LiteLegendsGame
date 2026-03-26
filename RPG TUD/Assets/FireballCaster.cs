using UnityEngine;
using System.Collections;

public class FireballCaster : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public Camera playerCamera;
    public PlayerTurn playerTurn;

    public float distance = 10f;
    public float travelTime = 0.5f;
    public float arcHeight = 3f;
    public float spawnForwardOffset = 1f;

    public float turnTime = 0.12f;
    public float castDelay = 0.02f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(CastFireball());
        }
    }

    IEnumerator CastFireball()
    {


        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 startPoint = fireballSpawnPoint.position + forward * spawnForwardOffset;
        Vector3 endPoint = startPoint + forward * distance;

        float timer = 0f;
        while (timer < turnTime)
        {
            if (playerTurn != null)
            {
                playerTurn.TurnTowards(endPoint);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(castDelay);

        GameObject fireball = Instantiate(fireballPrefab, startPoint, Quaternion.identity);
        StartCoroutine(MoveInArc(fireball, startPoint, endPoint));
    }

    IEnumerator MoveInArc(GameObject fireball, Vector3 startPoint, Vector3 endPoint)
    {
        float timer = 0f;

        while (timer < travelTime)
        {
            if (fireball == null)
                yield break;

            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / travelTime);

            Vector3 position = Vector3.Lerp(startPoint, endPoint, t);
            position.y += arcHeight * 4f * (t - t * t);

            fireball.transform.position = position;

            float nextT = Mathf.Clamp01((timer + 0.01f) / travelTime);
            Vector3 nextPosition = Vector3.Lerp(startPoint, endPoint, nextT);
            nextPosition.y += arcHeight * 4f * (nextT - nextT * nextT);

            Vector3 moveDir = nextPosition - position;
            if (moveDir.sqrMagnitude > 0.0001f)
            {
                fireball.transform.rotation = Quaternion.LookRotation(moveDir);
            }

            yield return null;
        }

        fireball.transform.position = endPoint;
        Destroy(fireball);
    }
}