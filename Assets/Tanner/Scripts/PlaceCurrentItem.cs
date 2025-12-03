using UnityEngine;

public class PlaceCurrentItem : MonoBehaviour
{
    public Item item;                                   // Reference to the actual item this controller represents
    public InventoryManager inventoryManager;           // Reference to the InventoryManager
    public GameObject placeobj;                         // Placeholder for an object that might be placed
    public GameObject ghostObject;            // Preview/ghost object for placement

    public static bool placeThis = false;
    public bool canPlace = false;   // Global flag: can we currently place?

    // Audio
    private AudioSource audioSource;
    public AudioClip placesound;
    public AudioClip pickupsound;
    Transform cam;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // Grab AudioSource on this object
        cam = Camera.main.transform;


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("can place is " + canPlace);

        if (!placeThis) return;


        Debug.Log("placeThis is true and canPlace is " + canPlace);
        if (canPlace)
        {
            InventoryManager.Instance.CloseInventoryButton();
            InventoryManager.Instance.TurnoffInv();

            Debug.Log("canPlace is true");
            RaycastHit hit;

            if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, 5f))
            {
                Debug.Log("place raycast is running");
                ghostObject.SetActive(true);
                ghostObject.transform.position = hit.point;
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(placeobj, ghostObject.transform.position, ghostObject.transform.rotation);
                    Debug.Log("should've placed an object");

                    ghostObject.SetActive(false);
                    placeThis = false;
                    canPlace = false;

                    audioSource.PlayOneShot(placesound);
                    Debug.Log("removed item: " + item.name);

                }
                //InventoryManager.Instance.Remove(item);     // Remove from manager

            }
            else//dont show the ghost object if cant see where itll be placed
            {
                //ghostObject.SetActive(false);
                Debug.Log("place raycast never runs");
            }
        }
        else
        {
            ghostObject.SetActive(false);
        }


        /*  if (InventoryManager.Instance != null)
          {
              InventoryManager.Instance.CloseInventoryButton();
              InventoryManager.Instance.TurnoffInv(); // optional if you need that too
          } */

    }

    public void PlaceItem()
    {
        placeThis = true;
        canPlace = true;
    }
}
