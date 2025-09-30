using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    public InstructionsManager instructionsManager;

    public InventoryManager Instance;

    public GameObject winCanvas;

    public GameObject journal;

    public GameObject sureExit;

    public bool escapePressed;

    public bool escapeClosed = false;
    public bool exit = false;

    public GameObject self;

    // Start is called before the first frame update
    void Start()
    {
        escapePressed = false;
        if (self.activeInHierarchy)
        {
            journal.SetActive(false);
            sureExit.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && winCanvas.activeInHierarchy == true) {
            TriggerExitScreenSure();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && journal.activeInHierarchy == true) {
            TriggerExitScreenSure();
        }
    }

    public void TriggerExitScreenSure()
    {
        sureExit.SetActive(true);
        journal.SetActive(false);
        Instance.CursorOn();
    }

    public void KeepPlaying()
    {
        Debug.Log("no don't exit was selected");
        sureExit.SetActive(false);
        journal.SetActive(true);
        if (Instance.inventoryOpen != true)
        {
           Instance.CursorOff();  
        }
        escapeClosed = true;
    }
    public void TriggerMenu()
    {
        Debug.Log("yes to exit was selected");
        escapePressed = true;
        SceneManager.LoadScene("TaliaMenu");
        SceneManager.UnloadSceneAsync("Sprint2");
    }

    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void BeginGame()
    {
        SceneManager.LoadScene("Sprint2");
    }
}
