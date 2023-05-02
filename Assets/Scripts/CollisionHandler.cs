using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;

    void OnCollisionEnter(Collision other) // in C#, if public isn't given, then it can be assumed that this is private
    {
        // switch statements are faster and more efficient than if/else, especially if there are many cases
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartTransitionSequence("LoadNextLevel");
                Debug.Log("Finish");
                break;
            default:
                StartTransitionSequence("ReloadLevel");
                Debug.Log("Crash!");
                break;
        }
    }

    void StartTransitionSequence(string functionToCall)
    {
        GetComponent<AudioSource>().enabled = false; // may change this to change the sound playing
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
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
