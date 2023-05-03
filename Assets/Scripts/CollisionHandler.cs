using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float loadDelay = 1f;

    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem explosionParticles;

    // CACHE - stored component references
    AudioSource myAudioSource;

    // VARIABLES - private instance variables
    const string nextLevelFunctionString = "LoadNextLevel";
    const string reloadLevelFunctionString = "ReloadLevel";
    bool isTransitioning = false;
    bool hasEndSoundPlayed = false;
    bool isCollisionDisabled = false;

    void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        ForceNextLevel();
        DisableCollision();
    }

    void ForceNextLevel()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    void DisableCollision()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCollisionDisabled == true)
            {
                isCollisionDisabled = false;
            }
            else
            {
                isCollisionDisabled = true;
            }
        }
    }

    void OnCollisionEnter(Collision other) // in C#, if public isn't given, then it can be assumed that this is private
    {
        if (isTransitioning || isCollisionDisabled)
        {
            return;
        }

        // switch statements are faster and more efficient than if/else, especially if there are many cases
        // i will probably only do this is there are more than 5 cases/nested if statements
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartTransitionSequence(nextLevelFunctionString);
                break;
            default:
                StartTransitionSequence(reloadLevelFunctionString);
                break;
        }
    }

    void StartTransitionSequence(string functionToCall)
    {
        if (hasEndSoundPlayed)
        {
            return;
        }

        switch(functionToCall)
        {
            case nextLevelFunctionString:
                myAudioSource.PlayOneShot(successSound);
                successParticles.Play();
                hasEndSoundPlayed = true;
                break;
            case reloadLevelFunctionString:
                myAudioSource.PlayOneShot(explosionSound);
                explosionParticles.Play();
                hasEndSoundPlayed = true;
                break;
            default:
                myAudioSource.Stop();
                break;
        }
        GetComponent<Movement>().enabled = false;
        Invoke(functionToCall, loadDelay);
    }

    // these are what the tutorial used, may need to go back to them
    // void StartSuccessSequence()
    // {
    //     GetComponent<Movement>().enabled = false;
    //     Invoke("LoadNextLevel", loadDelay);
    // }

    // void StartCrashSequence()
    // {
    //     GetComponent<Movement>().enabled = false;
    //     Invoke("ReloadLevel", loadDelay);
    // }

    void LoadNextLevel()
    {
        isTransitioning = true;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        isTransitioning = true;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
