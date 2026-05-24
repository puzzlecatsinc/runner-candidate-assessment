using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    private AudioSource audioSource;
    public int RunnerId;
    public SpriteRenderer towerImg;
    public GameObject projectileSpawn;
    public GameObject[] playerWeapons;
    /// <summary>
    /// Initializes the runner and starts its animation
    /// </summary>
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (GameManager.Instance.GameStarted) GetComponent<Animator>().SetFloat("Speed", 1);
    }

    /// <summary>
    /// Handle collisions with enemies
    /// </summary>
    /// <param name="collision">The collider collided with<</param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Blocker"))
        {
            transform.parent.GetComponent<PlayerController>().RemoveCharacters(RunnerId);
        }
    }

    /// <summary>
    /// Prepare to shoot
    /// </summary>
    public void PrepareShot(Sprite tower)
    {
        towerImg.sprite = tower;
        Invoke("DoShoot", UnityEngine.Random.Range(0.05f, 0.5f));
    }

    /// <summary>
    /// Shoot a projectile
    /// </summary>
    private void DoShoot()
    {
        audioSource.Play();
        Instantiate(PlayerController.Instance.ProjectilePrefab, projectileSpawn.transform.position, Quaternion.identity);
        GetComponent<Animator>().SetTrigger("Attack");
    }
}
