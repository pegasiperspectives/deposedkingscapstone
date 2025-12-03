using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public InventoryItemController inventoryItemController; // Reference back to the inventory item controller (not actively used here)

    // ==== Coffins ====
    public GameObject ghostexample1;            // Preview/ghost object for placement
    public GameObject placedexaple1;            // Final prefab to place
    public static bool placeIsExample1 = false; // Flag: are we placing this?   // This is a static bool, static means it belongs to the class itself, not a specific instance(not tied to an object in the scene) and there is only 1 shared value
                                                // If it was a regular bool each copy of the script would have its own copy of this bool, we only want one shared value thats why it needs to be static. 

    public GameObject ghostexample2;
    public GameObject placedexaple2;
    public static bool placeIsExample2 = false;

    public GameObject ghostexample3;
    public GameObject placedexaple3;
    public static bool placeIsExample3 = false;


    // ==== Flowers (share same ghost) ====
    public GameObject fernobj;
    public static bool placeIsFern = false;

    public GameObject ghostflowers;
    public GameObject rosesobj;
    public static bool placeIsRoses = false;

    public GameObject tulipsobj;
    public static bool placeIsTulips = false;

    public GameObject orchidsobj;
    public static bool placeIsOrchids = false;

    // ==== Portraits ====
    public GameObject ghostPort;
    public GameObject ladyPortObj;
    public static bool placeIsLadyPort = false;

    public GameObject childPortObj;
    public static bool placeIsChildPort = false;
     public GameObject kingPortraitObj;



    public bool canPlace;   // Global flag: can we currently place?

    // Audio
    private AudioSource audioSource;
    public AudioClip placesound;
    public AudioClip pickupsound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // Grab AudioSource on this object

    }


    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Each block checks one "placeIs" flag, shows a ghost preview if we can place,
        // raycasts forward, and on click instantiates the final object.

        //if (placeIsExample1)

        PlaceGoldCoffin();
        PlaceModernCoffin();
        PlaceRecycledCoffin();













        if (placeIsLadyPort)
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
                        audioSource.PlayOneShot(placesound);
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
                        audioSource.PlayOneShot(placesound);
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


    // Play pickup sound (can be called externally)
    public void playpickupsound()
    {
        audioSource.PlayOneShot(pickupsound);
    }


    public void PlaceGoldCoffin()
    {
        if (placeIsExample1) //wait, why are they all called in update? how does that work?. so this checks if it's collected. but it will always equal true. what about when multiple equal true? hm, but after placed it's supposed to set itself to false. so then you can't place it. but if you do have the modern coffin, you can place that instead? because it's true?
        {


            if (canPlace)
            {

                RaycastHit hit;

                // Raycast forward from this object
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
                {
                    ghostexample1.SetActive(true);                  // Show ghost
                    ghostexample1.transform.position = hit.point;   // Position it where ray hit


                    if (Input.GetMouseButtonDown(0))
                    {
                        Instantiate(placedexaple1, ghostexample1.transform.position, ghostexample1.transform.rotation); // Place the actual object

                        ghostexample1.SetActive(false);
                        placeIsExample1 = false;    // Done placing
                        Debug.Log("IS THIS EVEN RUNNING can't place gold coffin");

                        audioSource.PlayOneShot(placesound);    // Play place sound

                    }
                }
                else
                {
                    // Hide ghost when not aiming at valid surface
                    ghostexample1.SetActive(false);
                }

            }
            else
            {
                ghostexample1.SetActive(false);
            }
        }

    }

    public void PlaceModernCoffin()
    {
        if (placeIsExample2)
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
                        audioSource.PlayOneShot(placesound);

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


    public void PlaceRecycledCoffin()
    {
        if (placeIsExample3)
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
                        audioSource.PlayOneShot(placesound);

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
    }

    public void PlaceFern()
    {
        if (placeIsFern)
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
                        audioSource.PlayOneShot(placesound);
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
    }

    public void PlaceRoses()
    {
        if (placeIsRoses)
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
                        audioSource.PlayOneShot(placesound);
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

    }

    public void PlaceOrchids()
    {
        if (placeIsOrchids)
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
                        audioSource.PlayOneShot(placesound);
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
    }

    public void PlaceTulips()
    {
        if (placeIsTulips)
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
                        audioSource.PlayOneShot(placesound);
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
    }



}
