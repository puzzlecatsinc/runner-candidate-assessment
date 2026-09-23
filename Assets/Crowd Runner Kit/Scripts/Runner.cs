using UnityEngine;

public class Runner : MonoBehaviour
{
    private AudioSource audioSource;
    public int RunnerId;
    public SpriteRenderer towerImg;
    public GameObject projectileSpawn;
    public GameObject[] playerWeapons;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (GameManager.Instance.GameStarted) GetComponent<Animator>().SetFloat("Speed", 1);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Blocker"))
        {
            transform.parent.GetComponent<PlayerController>().RemoveCharacters(RunnerId);
        }
    }

    public void PrepareShot(Sprite tower)
    {
        towerImg.sprite = tower;
        Invoke("DoShoot", UnityEngine.Random.Range(0.05f, 0.5f));
    }

    private void DoShoot()
    {
        audioSource.Play();
        Instantiate(PlayerController.Instance.ProjectilePrefab, projectileSpawn.transform.position, Quaternion.identity);
        GetComponent<Animator>().SetTrigger("Attack");
    }
}
