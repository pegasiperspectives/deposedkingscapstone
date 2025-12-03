using System;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{


    public GameObject currentPlacingObject = null;
    public GameObject currentGhostObject = null;
    public bool currentTypeIs2d = false;
    Transform cam;

    public AudioSource audioSource;
    public AudioClip placesound;
    public AudioClip pickupsound;
    public static bool placeThis = false;
    public bool canPlace = false;   // Global flag: can we currently place?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canPlace == true && placeThis == true)
        {
            if (currentPlacingObject != null)
            {
                RaycastHit hit;

                Camera camObj = Camera.main;
                if (camObj == null)
                {
                    Debug.LogWarning("No MainCamera found!");
                    return;
                }

                cam = camObj.transform;
                currentGhostObject.SetActive(true);

                if (Physics.Raycast(cam.position, cam.forward, out hit, 5f))
                {
                    Debug.Log("place raycast is running and ghostObject should be seen");


                    currentGhostObject.transform.position = hit.point;

                    if (Input.GetMouseButtonDown(0))
                    {
                        Instantiate(currentPlacingObject, currentGhostObject.transform.position, currentGhostObject.transform.rotation);
                        Debug.Log("should've placed an object");

                        currentGhostObject.SetActive(false);
                        placeThis = false;
                        canPlace = false;

                        audioSource.PlayOneShot(placesound);
                        Debug.Log("removed item: " + currentPlacingObject.name);

                    }
                    //InventoryManager.Instance.Remove(item);     // Remove from manager


                    else//dont show the ghost object if cant see where itll be placed
                    {
                        //ghostObject.SetActive(false);
                        Debug.Log("place raycast never runs");
                    }
                }

                else
                {
                    currentGhostObject.SetActive(false);
                }
            }
        }
    }



    public void SetObjectComponents(GameObject placer, GameObject ghost, bool is2d)
    {
        currentPlacingObject = placer;
        currentGhostObject = ghost;
        currentTypeIs2d = is2d;
    }

}
