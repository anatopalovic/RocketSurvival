using UnityEngine;
using UnityEngine.SceneManagement;

public class CoalisionHandler : MonoBehaviour
{


    [SerializeField] float delayValue = 1f;
    [SerializeField] AudioClip audioClipSucces;
    [SerializeField] AudioClip audioClipCrash;

    [SerializeField] ParticleSystem particalsSucces;
    [SerializeField] ParticleSystem particalsCrash;

    AudioSource audioSource;

    bool isTransitioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isTransitioning && enabled)
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Bumped into Friendly");
                    break;
                case "Finish":
                    StartNextLevelSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }

    }

    public void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        particalsSucces.Play();
        audioSource.PlayOneShot(audioClipCrash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayValue);
    }

     public void StartNextLevelSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(audioClipSucces);
        particalsCrash.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayValue);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        isTransitioning = false;
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene((currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings);
        isTransitioning = false;
    }
}
