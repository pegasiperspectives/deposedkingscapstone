using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{

    [SerializeField] public GameObject self;
    private FPSController fpscontrollerScript;
    [SerializeField] private GameObject player;

    public GameObject menuManager;
    private MenuManager menu;

    // Start is called before the first frame update
    void Start()
    {
        fpscontrollerScript = player.GetComponent<FPSController>();
        menu = menuManager.GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (menu.escapePressed == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (self.activeInHierarchy == true)
                {
                    self.SetActive(false);
                    fpscontrollerScript.canMove = true;
                }
                else if (self.activeInHierarchy == false)
                {
                    self.SetActive(true);


                }
            }
            if (self.activeInHierarchy == true)
            {
                fpscontrollerScript.canMove = false;
            }
        }
    }

    void ButtonClick()
    {
        self.SetActive(true);
    }
}
