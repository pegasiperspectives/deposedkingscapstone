using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Rotatable : MonoBehaviour
{

    [SerializeField] private InputAction pressed, axis;
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

        //this.pressed.canceled += _ => { iic.rotateNow = false; };

    }

    // Update is called once per frame
    void Update()
    {
        InventoryItemController.currentObservable.SetActive(true);

        this.pressed.Enable();
        this.axis.Enable();
        this.pressed.performed += _ => { this.StartCoroutine(this.Rotate()); };
        this.axis.performed += context => { rotation = context.ReadValue<Vector2>(); };

        // Debug.Log("rotatable script is running"); this runs, so why isn't the other stuff running?
        Debug.Log("rotateNow is " + iic.rotateNow);
        Debug.Log("currently inspecting equals " + InventoryManager.currentlyInspecting.ToString());

        if (Input.GetKeyDown(KeyCode.Tab) && InventoryManager.currentlyInspecting == true)
        {
            iic.CloseInspect();
            InventoryItemController.currentObservable.SetActive(false);
            iic.rotateNow = false;
        }
    }

    private IEnumerator Rotate()
    {
        while (iic.rotateNow && InventoryManager.currentlyInspecting == true)
        {
            Debug.Log("it should be rotating rn");
            this.rotation *= objectRotationSpeed;
            this.transform.Rotate(Vector3.up, rotation.x, Space.World);
            this.transform.Rotate(Vector3.right, rotation.y, Space.World);
            yield return null;

        }
    }



}
