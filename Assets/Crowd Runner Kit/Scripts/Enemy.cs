using UnityEngine;

public class Enemy : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private ParticleSystem particleHit;
    [SerializeField] public Animator enemyAnims;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        GameManager.Instance.NumEnemies++;
        if (GameManager.Instance.GameStarted) transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", 1);
    }

    public void Die()
    {
        enemyAnims.SetTrigger("Dead");
        particleHit.Play();
        GameManager.Instance.NumEnemies--;
        gameObject.GetComponent<Collider>().enabled = false;
        transform.GetChild(0).GetComponent<Animator>().SetBool("Dead", true);
        if (GetComponent<HorizontalMover>().enabled) GetComponent<HorizontalMover>().enabled = false;
        Destroy(gameObject, 1);
        audioSource.Play();
    }
}
