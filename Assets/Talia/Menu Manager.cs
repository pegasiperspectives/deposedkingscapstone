using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public InstructionsManager instructionsManager;

    public GameObject winCanvas;

    public GameObject journal;

    public bool escapePressed;

    public GameObject self;

    // Start is called before the first frame update
    void Start()
    {
        escapePressed = false;
        if (self.activeInHierarchy) {
            journal.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && winCanvas.activeInHierarchy == true) {
            escapePressed = true;
            journal.SetActive(false);
            SceneManager.LoadScene("TaliaMenu");
            SceneManager.UnloadSceneAsync("Sprint2");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && journal.activeInHierarchy == true) {
            escapePressed = true;
            journal.SetActive(false);
            SceneManager.LoadScene("TaliaMenu");
            SceneManager.UnloadSceneAsync("Sprint2");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void QuitGame() {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void BeginGame()
    {
        SceneManager.LoadScene("Sprint2");
    }
}
