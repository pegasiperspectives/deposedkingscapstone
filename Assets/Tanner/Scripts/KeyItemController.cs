using UnityEngine;
using System.Collections;

public class KeyItemController : MonoBehaviour
{
    [SerializeField] private bool canAlreadyOpen = false;

    [SerializeField] private bool oneKing = false;


    [SerializeField] public bool needsRustyKey = false;
    [SerializeField] public bool needsServantKey = false;
    [SerializeField] private Keyinv _keyInventory = null;

    public InventoryManager inventoryM;

    public KeyDoorController doorObject;

    [SerializeField] private int timeToShowUI = 1;
    [SerializeField] private GameObject showDoorLockedUI = null;

    private void Start()
    {
        if (canAlreadyOpen == true)
        {
            doorObject = GetComponent<KeyDoorController>();
        }
    }
    public void ObjectInteraction()
    {
        if (canAlreadyOpen)
        {
            doorObject.PlayAnimation();
            Debug.Log("wdawdwadawdww");
        }
        else
        {
            if (inventoryM.rKeyCollected == true && needsRustyKey == true)
            {
                doorObject.PlayAnimation();
                //gameObject.SetActive(false);
            }

            else if (inventoryM.kingCollected == true)
            {
                _keyInventory.hasKing = true;
                //gameObject.SetActive(false);
            }

            else if (inventoryM.sKeyCollected == true && needsServantKey == true)
            {
                doorObject.PlayAnimation();
                //gameObject.SetActive(false);
            }
            else
            {

                StartCoroutine(ShowDoorLocked());
            }

        }
    }


    IEnumerator ShowDoorLocked()
    {
        showDoorLockedUI.SetActive(true);
        yield return new WaitForSeconds(timeToShowUI);
        showDoorLockedUI.SetActive(false);
    }
}