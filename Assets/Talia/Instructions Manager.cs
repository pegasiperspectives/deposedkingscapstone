using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{

    [SerializeField] public GameObject self;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (self.activeInHierarchy == true)
            {
                self.SetActive(false);
            }
            else if (self.activeInHierarchy == false)
            {
                self.SetActive(true);
            }
        }
    }

    void ButtonClick()
    {
        self.SetActive(true);
    }
}
