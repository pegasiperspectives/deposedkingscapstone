using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Rotatable : MonoBehaviour
{
    [SerializeField] private InputAction pressed;
    [SerializeField] private InputAction axis;

    public Vector2 rotation;
    public float objectRotationSpeed = .5f;
    public InventoryManager inventoryManager;
    public InspectManager iM;

    private Coroutine rotateRoutine;


    private void OnPressed(InputAction.CallbackContext ctx)
    {
        if (rotateRoutine == null)
        {
            rotateRoutine = StartCoroutine(Rotate());
        }
    }

    private void OnAxis(InputAction.CallbackContext ctx)
    {
        rotation = ctx.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        pressed.Enable();
        axis.Enable();

        pressed.performed += OnPressed;
        axis.performed += OnAxis;
    }

    private void OnDisable()
    {
        pressed.performed -= OnPressed;
        axis.performed -= OnAxis;

        pressed.Disable();
        axis.Disable();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && InventoryManager.currentlyInspecting)
        {
            StopRotating();
            iM.CloseInspect();
            iM.rotateNow = false;
        }
    }

    private IEnumerator Rotate()
    {
        while (iM.rotateNow && InventoryManager.currentlyInspecting)
        {
            rotation *= objectRotationSpeed;
            transform.Rotate(Vector3.up, rotation.x, Space.World);
            transform.Rotate(Vector3.right, rotation.y, Space.World);

            yield return null;
        }

        rotateRoutine = null;
    }

    private void StopRotating()
    {
        if (rotateRoutine != null)
        {
            StopCoroutine(rotateRoutine);
            rotateRoutine = null;
        }
    }
}




