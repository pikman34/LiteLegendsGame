using UnityEngine;
using System.Collections;

public class SpellCaster : MonoBehaviour
{
    public GameObject spellPrefab;
    public Transform spellSpawnPoint;

    public float spellForce = 30f;

    public float fadeInTime = 0.05f;
    public float lifeTime = 0.3f;
    public float fadeOutTime = 0.08f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CastSpell();
        }
    }

    void CastSpell()
    {
        GameObject spell = Instantiate(
            spellPrefab,
            spellSpawnPoint.position,
            spellSpawnPoint.rotation
        );

        Rigidbody rb = spell.GetComponent<Rigidbody>(); 
        if (rb != null)
        {
            rb.linearVelocity = spellSpawnPoint.forward * spellForce;
            rb.angularVelocity = Vector3.zero;
        }

        StartCoroutine(FadeAndDestroy(spell));
    }

    IEnumerator FadeAndDestroy(GameObject spell) //Fade doesn't currently work :(
    {
        Renderer renderer = spell.GetComponentInChildren<Renderer>();
        if (renderer == null)
        {
            Debug.LogWarning("Spell has no Renderer.");
            yield break;
        }

        Material mat = renderer.material; 
        Color baseColor = mat.color;

        float totalTime = fadeInTime + lifeTime + fadeOutTime;
        float timer = 0f;

        while (timer < totalTime)
        {
            timer += Time.deltaTime;

            float alpha = 1f;

            if (timer < fadeInTime)
            {
                alpha = Mathf.Lerp(0f, 1f, timer / fadeInTime);
            }
            else if (timer > fadeInTime + lifeTime)
            {
                float fadeTimer = timer - (fadeInTime + lifeTime);
                alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeOutTime);
            }

            Color c = baseColor;
            c.a = alpha;
            mat.color = c;

            yield return null;
        }

        Destroy(spell);
    }
}