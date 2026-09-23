using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    void Awake() => Instance = this;

    [SerializeField] private AudioClip clipIncrease;
    [SerializeField] private AudioClip clipDecrease;
    [SerializeField] private AudioClip clipDie;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClipIncrease()
    {
        audioSource.PlayOneShot(clipIncrease);
    }

    public void PlayClipDecrease()
    {
        audioSource.PlayOneShot(clipDecrease);
    }

    public void PlayClipDie()
    {
        audioSource.PlayOneShot(clipDie);
    }
}
