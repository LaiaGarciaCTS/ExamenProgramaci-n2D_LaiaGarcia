using UnityEngine;
using UnityEngine.SceneManagement;


public class Star : MonoBehaviour
{
    void OnEnterTrigger2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}