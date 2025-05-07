using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public InstructionsManager instructionsManager;

    public GameObject winCanvas;

    public GameObject journal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && winCanvas.activeInHierarchy == true) {
            SceneManager.LoadScene("TaliaMenu");
            SceneManager.UnloadSceneAsync("TannerNew");
            journal.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && journal.activeInHierarchy == true) {
            SceneManager.LoadScene("TaliaMenu");
            SceneManager.UnloadSceneAsync("TannerNew");
            journal.SetActive(false);
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
        SceneManager.LoadScene("TannerNew");
    }
}
