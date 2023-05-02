using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) // in C#, if public isn't given, then it can be assumed that this is private
    {
        // switch statements are faster and more efficient than if/else, especially if there are many cases
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                Debug.Log("Finish");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            default:
                Debug.Log("Crash!");
                break;
        }
    }
}
