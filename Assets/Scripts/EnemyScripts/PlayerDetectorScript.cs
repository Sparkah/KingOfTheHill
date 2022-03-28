using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetectorScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerControllerScript>()!=null)
        {
            SceneManager.LoadScene(0);
        }
    }
}