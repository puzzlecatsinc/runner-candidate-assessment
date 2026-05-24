using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

/// <summary>
/// Represents a blocker obstacle
/// </summary>
public class Blocker : MonoBehaviour
{
    private AudioSource audioSource;
    public int Health;
    [SerializeField] private TextMeshPro labelHealth;
    public SpriteRenderer towerIcon;
    public int shootSpeed;
    public GameObject projectile;
    public Sprite towerSprite;
    /// <summary>
    /// Increment the number of enemies at spawn and set the health
    /// </summary>
    void Start()
    {
        GameManager.Instance.NumEnemies++;
        //Health = UnityEngine.Random.Range(3, 11);
        //labelHealth.text = Health.ToString();
        audioSource = GetComponent<AudioSource>();
    }

    public void OverrideHealth(int overrideHealth)
    {
        Health = overrideHealth;
        labelHealth.text = Health.ToString();
    }
    /// <summary>
    /// Handles the taking of damage
    /// </summary>
    /// <param name="amount">The amount of damage done</param>
    public void TakeDamage(int amount)
    {
        if (Health <= 0) return;
        Health -= amount;
        labelHealth.text = Health.ToString();

        if (Health <= 0)
        {
            labelHealth.gameObject.SetActive(false);
            StartCoroutine(Shrink(0.2f));
            audioSource.Play();
            Invoke("Die", 0.2f);
        }
    }

    /// <summary>
    /// Handles the blocker dying
    /// </summary>
    private void Die()
    {
        GameManager.Instance.NumEnemies--;
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(transform.gameObject);
        towerIcon.gameObject.SetActive(false);
        PlayerController.Instance.UpdateShooting(shootSpeed, projectile, towerSprite);
    }

    /// <summary>
    /// Shrinks the blocker
    /// </summary>
    /// <param name="duration">Duration of shrink</param>
    IEnumerator Shrink(float duration)
    {
        float elapsedTime = 0f;
        Vector3 startSize = transform.localScale;
        Vector3 targetSize = Vector3.zero;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startSize, targetSize, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
