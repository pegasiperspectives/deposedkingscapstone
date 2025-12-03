using UnityEngine;

public class PlaceCurrentItem : MonoBehaviour
{
    public Item item;                                   // Reference to the actual item this controller represents
    public InventoryManager inventoryManager;           // Reference to the InventoryManager
    public GameObject placeobj;                         // Placeholder for an object that might be placed
    public GameObject ghostObject;            // Preview/ghost object for placement

    // Audio
    private AudioSource audioSource;
    public AudioClip placesound;
    public AudioClip pickupsound;
    Transform cam;

    public PlacementManager PlacementManager;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // Grab AudioSource on this object
        //cam = Camera.main.transform;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaceItem()
    {
        Debug.Log("place object is " + placeobj.ToString());
        PlacementManager.placeThis = true;
        PlacementManager.canPlace = true;
        PlacementManager.SetObjectComponents(placeobj, ghostObject);
        InventoryManager.Instance.CloseInventoryButton();
        InventoryManager.Instance.TurnoffInv();
    }
}
