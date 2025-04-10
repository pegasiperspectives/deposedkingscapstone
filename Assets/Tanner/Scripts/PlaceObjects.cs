using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public InventoryItemController inventoryItemController;
    public FPSController fpscontrollerScript;
    public ItemPickUp itempickupscript;
    

    public GameObject ghostexample1;
    public GameObject placedexaple1;
    public bool placeIsExample1 = false;

    public GameObject ghostexample2;
    public GameObject placedexaple2;

    public bool canPlace = false;
    public int totalPlace = 1;
    //public GameObject theplayer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
  

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(placeIsExample1);
        if (placeIsExample1 == true)
        {
            Debug.Log("placeIsExample1 is TRUE in PlaceObjects!");
            if (canPlace)
            {
                ghostexample1.SetActive(true);
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
                {
                    ghostexample1.transform.position = hit.point;
                    if (Input.GetMouseButtonDown(0))
                    {
                        totalPlace -= 1;
                        Instantiate(placedexaple1, ghostexample1.transform.position, ghostexample1.transform.rotation);
                        //theplayer = itempickupscript.player;

                    }
                }

            }
            else
            {
                ghostexample1.SetActive(false);
            }
        }
        //else
        //{

        //}


        
            
            
        

    }
}
