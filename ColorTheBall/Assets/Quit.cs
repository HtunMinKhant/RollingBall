using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public void doquit()
    {
        UnityEngine.Debug.LogError("Exit the Game");
        Application.Quit();
    }
}
