using UnityEngine;
using System.Collections;

public class KeyItemController : MonoBehaviour
{
    [SerializeField] private bool oneDoor = false;
    [SerializeField] private bool oneKey = false;
    [SerializeField] private bool twoKey = false;
    [SerializeField] private bool oneKing = false;

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
            Debug.Log("wdawdwadawdww");
        }
        else if (oneKey)
        {
            _keyInventory.hasKeyOne = true;
            gameObject.SetActive(false);
        }
        else if (oneKing)
        {
            _keyInventory.hasKing = true;
            gameObject.SetActive(false);
        } else if (twoKey)
        {
            _keyInventory.hasKeyTwo = true;
            gameObject.SetActive(false);
        }
    }
}
