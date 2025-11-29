using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Rotatable : MonoBehaviour
{

    [SerializeField] private InputAction pressed, axis;
    public bool rotateNow = false;
    public Vector2 rotation;
    public float objectRotationSpeed = .5f;              // Rotation speed for inspection
    public InventoryManager inventoryManager;
    public GameObject journalOverlay;
    public InventoryItemController iic;


    void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = InventoryManager.Instance;                                               // Cache reference to the InventoryManager
        Debug.Log("it should be setting up rotation stuff rn");
        pressed.Enable();
        axis.Enable();
        pressed.performed += _ => { StartCoroutine(Rotate()); };
        pressed.canceled += _ => { rotateNow = false; };
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("rotatable script is running"); this runs, so why isn't the other stuff running?
        Debug.Log("rotateNow is " + rotateNow);
        Debug.Log("currently inspecting equals " + InventoryManager.currentlyInspecting.ToString());

        if (Input.GetKeyDown(KeyCode.Tab) && InventoryManager.currentlyInspecting == true)
        {
            iic.CloseInspect();
        }
    }

    private IEnumerator Rotate()
    {
        rotateNow = true;
        while (rotateNow && InventoryManager.currentlyInspecting == true)
        {
            Debug.Log("it should be rotating the gold coffin rn");
            rotation *= objectRotationSpeed;
            transform.Rotate(Vector3.up, rotation.x, Space.World);
            transform.Rotate(Vector3.right, rotation.y, Space.World);
            yield return null;

        }
    }


}
