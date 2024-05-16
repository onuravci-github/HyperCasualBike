using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFunction : MonoBehaviour
{
    public void RestartGame() {
        Application.LoadLevel(0);
    }
    public void PauseGame() {
        Time.timeScale = 0;
    }
    public void ResumeGame() {
        Time.timeScale = 1;
    }
    public void GameStart() {
        Time.timeScale = 1;
    }
}
