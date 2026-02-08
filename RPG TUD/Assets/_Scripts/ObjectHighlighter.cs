using UnityEngine;



public class ObjectHighlighter : MonoBehaviour
{
    [Header("Highlight Settings")]
    public Material highlightMaterial;

    private Renderer[] renderers;
    private Material[][] originalMaterials;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();

        originalMaterials = new Material[renderers.Length][];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].materials;
        }
    }



    public void EnableHighlight()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            Material[] newMats = new Material[renderers[i].materials.Length];
            renderers[i].materials.CopyTo(newMats, 0);
            newMats[^1] = highlightMaterial;
            renderers[i].materials = newMats;
        }
    }

    public void DisableHighlight()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].materials = originalMaterials[i];
        }
    }
}

