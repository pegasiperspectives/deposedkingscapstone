using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{

    [SerializeField] public GameObject self;        // Reference to the instructions UI panel itself

    // Player movement control
    private FPSController fpscontrollerScript;
    [SerializeField] private GameObject player;

    // Reference to menu manager (to check if menu/escape is active)
    public GameObject menuManager;
    private MenuManager menu;

    // Start is called before the first frame update
    void Start()
    {
        fpscontrollerScript = player.GetComponent<FPSController>(); // Get FPSController from player
        menu = menuManager.GetComponent<MenuManager>();             // Get MenuManager from menuManager object
    }

    // Update is called once per frame
    void Update()
    {
        // Only handle instructions if escape menu is NOT currently open
        if (menu.escapePressed == false)
        {
            // Toggle instructions panel when pressing Q
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (self.activeInHierarchy == true)
                {
                    // If instructions are open, close them and allow movement
                    self.SetActive(false);
                    fpscontrollerScript.canMove = true;
                }
                else if (self.activeInHierarchy == false)
                {
                    // If instructions are closed, open them
                    self.SetActive(true);


                }
            }
            // If instructions panel is open, freeze player movement
            if (self.activeInHierarchy == true)
            {
                fpscontrollerScript.canMove = false;
            }
        }
    }

    // Can be called by a UI button to open instructions
    void ButtonClick()
    {
        self.SetActive(true);
    }
}
