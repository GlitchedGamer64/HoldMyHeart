using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour {

    public Canvas Pause;
    public Canvas GameEnd;
    //public Canvas HUD;
    

    public void ResumeGame() {
        Time.timeScale = 1;
        Pause.enabled = false;
        GameObject.Find("bgAudio").GetComponent<AudioSource>().UnPause();
    }

    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void RestartGame() {
        SceneManager.LoadScene("test");
        Time.timeScale = 1;
        Pause.enabled = false;
    }

    public void Options() { }

    public void ReturnToMenu() {
        SceneManager.LoadScene("MainMenu");
    }

}
