using UnityEngine;

public class PlaceCurrentItem : MonoBehaviour
{
    public Item item;                                   // Reference to the actual item this controller represents
    public InventoryManager inventoryManager;           // Reference to the InventoryManager
    public PlaceObjects placeObjects;                   // Reference to the PlaceObjects script (handles placing objects in world)
    public GameObject placeobj;                         // Placeholder for an object that might be placed
    public GameObject ghostObject;            // Preview/ghost object for placement

    public bool placeThis = true;
    public bool canPlace;   // Global flag: can we currently place?

    // Audio
    private AudioSource audioSource;
    public AudioClip placesound;
    public AudioClip pickupsound;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // Grab AudioSource on this object

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaceItem()
    {
        if (placeThis)
        {
            if (canPlace)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Max(5)))
                {
                    ghostObject.SetActive(true);
                    ghostObject.transform.position = hit.point;
                    if (Input.GetMouseButtonDown(0))
                    {
                        Instantiate(placeobj, ghostObject.transform.position, ghostObject.transform.rotation);

                        ghostObject.SetActive(false);
                        placeThis = false;
                        audioSource.PlayOneShot(placesound);
                    }
                }
                else//dont show the ghost object if cant see where itll be placed
                {
                    ghostObject.SetActive(false);
                }
            }
            else
            {
                ghostObject.SetActive(false);
            }
        }
    }
}
