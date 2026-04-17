using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 15f;
    public float lifetime = 5f;
    public GameObject particleEffect;
    public 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 20f);
    }
}
