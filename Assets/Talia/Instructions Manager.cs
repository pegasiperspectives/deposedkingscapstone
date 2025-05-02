using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{

    [SerializeField] public GameObject self;
    private FPSController fpscontrollerScript;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        fpscontrollerScript = player.GetComponent<FPSController>();
    }

    // Update is called once per frame
    void Update()
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

    void ButtonClick()
    {
        self.SetActive(true);
    }
}
