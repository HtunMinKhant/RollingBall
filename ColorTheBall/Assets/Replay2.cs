using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay2 : MonoBehaviour
{
    public void HeheReplay2()
    {
        if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            SceneManager.LoadScene(6);
        }
    }
}
