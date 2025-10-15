using UnityEngine;
using System.Collections;

public class KeyItemController : MonoBehaviour
{
    [SerializeField] private bool oneDoor = false;
    [SerializeField] private bool oneKey = false;

    [SerializeField] private Keyinv _keyInventory = null;

    private KeyDoorController doorObject;

    private void Start()
    {
        if(oneDoor)
        {
            doorObject = GetComponent<KeyDoorController>();
        }
    }
    public void ObjectInteraction()
    {
        if (oneDoor)
        {
            doorObject.PlayAnimation();
        }
        else if (oneKey)
        {
            _keyInventory.hasKeyOne = true;
            gameObject.SetActive(false);
        }
    }
}
