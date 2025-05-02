using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public InventoryItemController inventoryItemController;
    

    public GameObject ghostexample1;
    public GameObject placedexaple1;
    public static bool placeIsExample1 = false;

    public GameObject ghostexample2;
    public GameObject placedexaple2;
    public static bool placeIsExample2 = false;
    
    public GameObject ghostexample3;
    public GameObject placedexaple3;
    public static bool placeIsExample3 = false;

    public GameObject fernobj;
    public static bool placeIsFern = false;

    public GameObject ghostflowers;
    public GameObject rosesobj;
    public static bool placeIsRoses = false;

    public GameObject tulipsobj;
    public static bool placeIsTulips = false;

    public GameObject orchidsobj;
    public static bool placeIsOrchids = false;

    public GameObject ghostPort;
    public GameObject ladyPortObj;
    public static bool placeIsLadyPort = false;

    public GameObject childPortObj;
    public static bool placeIsChildPort = false;
    public bool canPlace;
    

    

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
        

        //if (placeIsExample1)
        if (placeIsExample1)
        {
            
            
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




        else if (placeIsExample3)
        {

            if (canPlace)
            {

                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
                {
                    ghostexample3.SetActive(true);
                    ghostexample3.transform.position = hit.point;
                    if (Input.GetMouseButtonDown(0))
                    {

                        Instantiate(placedexaple3, ghostexample3.transform.position, ghostexample3.transform.rotation);

                        ghostexample3.SetActive(false);
                        placeIsExample3 = false;


                    }
                }
                else//dont show the ghost object if cant see where itll be placed
                {
                    ghostexample3.SetActive(false);
                }

            }
            else
            {
                ghostexample3.SetActive(false);
            }
        }

        else if (placeIsFern)
        {
            if (canPlace)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
                {
                    ghostflowers.SetActive(true);
                    ghostflowers.transform.position = hit.point;
                    if (Input.GetMouseButtonDown(0))
                    {
                        Instantiate(fernobj, ghostflowers.transform.position, ghostflowers.transform.rotation);

                        ghostflowers.SetActive(false);
                        placeIsFern = false;
                    }
                }
                else//dont show the ghost object if cant see where itll be placed
                {
                    ghostflowers.SetActive(false);
                }
            }
            else
            {
                ghostflowers.SetActive(false);
            }
        }

        else if (placeIsRoses)
        {
            if (canPlace)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
                {
                    ghostflowers.SetActive(true);
                    ghostflowers.transform.position = hit.point;
                    if (Input.GetMouseButtonDown(0))
                    {
                        Instantiate(rosesobj, ghostflowers.transform.position, ghostflowers.transform.rotation);

                        ghostflowers.SetActive(false);
                        placeIsRoses = false;
                    }
                }
                else//dont show the ghost object if cant see where itll be placed
                {
                    ghostflowers.SetActive(false);
                }
            }
            else
            {
                ghostflowers.SetActive(false);
            }
        }

        else if (placeIsTulips)
        {
            if (canPlace)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
                {
                    ghostflowers.SetActive(true);
                    ghostflowers.transform.position = hit.point;
                    if (Input.GetMouseButtonDown(0))
                    {
                        Instantiate(tulipsobj, ghostflowers.transform.position, ghostflowers.transform.rotation);

                        ghostflowers.SetActive(false);
                        placeIsTulips = false;
                    }
                }
                else//dont show the ghost object if cant see where itll be placed
                {
                    ghostflowers.SetActive(false);
                }
            }
            else
            {
                ghostflowers.SetActive(false);
            }
        }
        else if (placeIsOrchids)
        {
            if (canPlace)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
                {
                    ghostflowers.SetActive(true);
                    ghostflowers.transform.position = hit.point;
                    if (Input.GetMouseButtonDown(0))
                    {
                        Instantiate(orchidsobj, ghostflowers.transform.position, ghostflowers.transform.rotation);

                        ghostflowers.SetActive(false);
                        placeIsOrchids = false;
                    }
                }
                else//dont show the ghost object if cant see where itll be placed
                {
                    ghostflowers.SetActive(false);
                }
            }
            else
            {
                ghostflowers.SetActive(false);
            }
        }

        else if (placeIsLadyPort)
        {
            if (canPlace)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
                {
                    ghostPort.SetActive(true);
                    ghostPort.transform.position = hit.point;
                    if (Input.GetMouseButtonDown(0))
                    {
                        Instantiate(ladyPortObj, ghostPort.transform.position, ghostPort.transform.rotation);

                        ghostPort.SetActive(false);
                        placeIsLadyPort = false;
                    }
                }
                else//dont show the ghost object if cant see where itll be placed
                {
                    ghostPort.SetActive(false);
                }
            }
            else
            {
                ghostPort.SetActive(false);
            }
        }

        else if (placeIsChildPort)
        {
            if (canPlace)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
                {
                    ghostPort.SetActive(true);
                    ghostPort.transform.position = hit.point;
                    if (Input.GetMouseButtonDown(0))
                    {
                        Instantiate(childPortObj, ghostPort.transform.position, ghostPort.transform.rotation);

                        ghostPort.SetActive(false);
                        placeIsChildPort = false;
                    }
                }
                else//dont show the ghost object if cant see where itll be placed
                {
                    ghostPort.SetActive(false);
                }
            }
            else
            {
                ghostPort.SetActive(false);
            }
        }

    }

      




    








}
