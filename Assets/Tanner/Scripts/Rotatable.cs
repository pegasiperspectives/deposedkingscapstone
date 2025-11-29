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
    public float objectRotationSpeed = 5f;              // Rotation speed for inspection
    public InventoryManager inventoryManager;


    void Awake()
    {
        pressed.Enable();
        axis.Enable();
        pressed.performed += _ => { StartCoroutine(Rotate()); };
        pressed.canceled += _ => { rotateNow = false; };
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = InventoryManager.Instance;                                               // Cache reference to the InventoryManager

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Rotate()
    {
        while (rotateNow && InventoryManager.currentlyInspecting == true)
        {
            rotation *= objectRotationSpeed;
            transform.Rotate(Vector3.up, rotation.x, Space.World);
            transform.Rotate(Vector3.right, rotation.y, Space.World);
            yield return null;

        }
    }
}
