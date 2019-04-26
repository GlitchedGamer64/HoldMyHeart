using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

	public void OnNewGameClicked() {
        SceneManager.LoadScene("test");
    }

    public void OnLoadGameClicked() { }

    public void OnExitGameClicked() {
        Application.Quit();

        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
