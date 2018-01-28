using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public GameObject startButton;
    public GameObject helpButton;
    public GameObject quitButton;

	// Use this for initialization
	void Start () {
	    startButton.GetComponent<Button>().onClick.AddListener(StartGame);
	    helpButton.GetComponent<Button>().onClick.AddListener(ShowHelp);
	    quitButton.GetComponent<Button>().onClick.AddListener(Quit);
	}

    public void StartGame() {
        SceneManager.LoadScene("mainscene", LoadSceneMode.Single);
    }

    public void ShowHelp() {
        SceneManager.LoadScene("helpscene", LoadSceneMode.Single);
    }

    public void Quit() {
        Application.Quit();
    }
}
