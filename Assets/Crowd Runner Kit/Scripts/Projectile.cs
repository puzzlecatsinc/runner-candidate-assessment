using UnityEngine;

public class Projectile : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, .6f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * 50 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().Die();
            GetComponent<Collider>().enabled = false;
            transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, 4);
        }
        else if (other.CompareTag("Blocker"))
        {
            other.gameObject.GetComponent<Blocker>().TakeDamage(1);
            GetComponent<Collider>().enabled = false;
            transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, 4);
        }
    }
}
