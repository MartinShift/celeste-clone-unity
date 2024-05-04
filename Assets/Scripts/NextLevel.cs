using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            PlayGame.CurrentLevel = nextLevel;
            if(nextLevel > PlayGame.MaxLevel)
            {
                PlayGame.MaxLevel = nextLevel;
            }
            PlayGame.LastCheckpoint = null;
            PlayerControls.LastCheckpoint = null;
            PlayGame.Save();
            SceneManager.LoadScene(nextLevel);
        }
    }
}
