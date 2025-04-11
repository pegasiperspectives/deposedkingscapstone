using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public InventoryItemController inventoryItemController;
    //public FPSController fpscontrollerScript;
    //public ItemPickUp itempickupscript;
    //public GameObject cam;

    public GameObject ghostexample1;
    public GameObject placedexaple1;
    public bool placeIsExample1 = false;

    public GameObject ghostexample2;
    public GameObject placedexaple2;
    public bool placeIsExample2 = false;

    public bool canPlace;
    
    //public GameObject theplayer;
    
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("startplaceobj");
    }
  

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (placeIsExample1)
        {
            Debug.Log("IT IS TRUE");
            
            if (canPlace)
            {
                
                RaycastHit hit;
                
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
                {
                    ghostexample1.SetActive(true);
                    ghostexample1.transform.position = hit.point;
                    if (Input.GetMouseButtonDown(0))
                    {
                        
                        Instantiate(placedexaple1, ghostexample1.transform.position, ghostexample1.transform.rotation);
                        
                        ghostexample1.SetActive(false); 
                        placeIsExample1 = false;
                        
                        Debug.Log("HERE");
                    }
                }
                else//dont show the ghost object if cant see where itll be placed
                {
                    ghostexample1.SetActive(false);
                }

            }
            else
            {
                ghostexample1.SetActive(false);
            }
        }



        else if (placeIsExample2)
        {
           
            if (canPlace)
            {
                
                RaycastHit hit;
                
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
                {
                    ghostexample2.SetActive(true);
                    ghostexample2.transform.position = hit.point;
                    if (Input.GetMouseButtonDown(0))
                    {

                        Instantiate(placedexaple2, ghostexample2.transform.position, ghostexample2.transform.rotation);
                        
                        ghostexample2.SetActive(false);
                        placeIsExample2 = false;

                        Debug.Log("HERE");
                    }
                }
                else//dont show the ghost object if cant see where itll be placed
                {
                    ghostexample2.SetActive(false);
                }

            }
            else
            {
                ghostexample2.SetActive(false);
            }
        }





        





    }

            public void SwitchBool()
        {
            Debug.Log("done it switch");
        Debug.Log(placeIsExample1);
        
            if (placeIsExample1 == true)
            {
                placeIsExample1 = false;
            Debug.Log("ITS HERE BAD");
            }
            else
            {
                placeIsExample1 = true;
            Debug.Log("ITS HERE GOOD");
            Debug.Log(placeIsExample1);
        }
        }

    public void SwitchCanPlace()
    {
        
        ghostexample2.SetActive(false);
       
    }
}
