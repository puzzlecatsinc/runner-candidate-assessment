using UnityEngine;

/// <summary>
/// Handles audio
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region SINGLETON
    public static AudioManager Instance;
    void Awake() => Instance = this;
    #endregion

    [SerializeField] private AudioClip clipIncrease;
    [SerializeField] private AudioClip clipDecrease;
    [SerializeField] private AudioClip clipDie;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Play the increase sound
    /// </summary>
    public void PlayClipIncrease()
    {
        audioSource.PlayOneShot(clipIncrease);
    }

    /// <summary>
    /// Play the decrease sound
    /// </summary>
    public void PlayClipDecrease()
    {
        audioSource.PlayOneShot(clipDecrease);
    }

    /// <summary>
    /// Play the die sound
    /// </summary>
    public void PlayClipDie()
    {
        audioSource.PlayOneShot(clipDie);
    }
}
