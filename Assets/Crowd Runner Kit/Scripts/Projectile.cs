using UnityEngine;

/// <summary>
/// Represents a fired projectile
/// </summary>
public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Set the projectile to self-destruct after a time
    /// </summary>
    void Start()
    {
        Destroy(gameObject, .6f);
    }

    /// <summary>
    /// Move the projectile forward
    /// </summary>
    void Update()
    {
        transform.Translate(Vector3.forward * 50 * Time.deltaTime);
    }

    /// <summary>
    /// Handle collision with an enemy
    /// </summary>
    /// <param name="other">The collider collided with</param>
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
