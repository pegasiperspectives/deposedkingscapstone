using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{


    //Tanner Addition
    // References and fields for dynamic UI
    public Transform itemButtonContainer;           // Where dynamically created buttons will go

    public Transform playerView;

    [SerializeField]
    public Transform centerOnQueen;

    public Transform centerOnGardener;
    public GameObject itemButtonPrefab;             // Prefab for an inventory item button in dialogue
    private FPSController fpscontrollerScript;      // Reference to player controller
    private Characters characters1;                  // Reference to Characters script on the lady NPC
     private Characters characters2; 
    public GameObject player;

    public Int32 objNum = 0;
    public Int32 onNext = 0;

    public GameObject lady;                         // Lady NPC

    public GameObject gardener;
    public PlaceObjects placeObjects;               // Controls placement state
    [SerializeField] private GameObject inventory;  // Inventory UI (to check if open)

    //added


    [SerializeField] public TMP_Text textLabel;         // Label to show dialogue text
    [SerializeField] private float typeSpeed = 50;      // Characters per second when typing
    [SerializeField] public GameObject self;            // Dialogue UI panel itself

    [SerializeField] public GameObject dialogueOption1; // (Not used here but can be used for options)

    // All possible dialogue lines that can be shown based on items

    [SerializeField]
    public string[] allDialogue = {};

    // (Not used in current logic but example placeholders)
    public string[] showObjects = {
        "Show object 1 in inventory",
        "Show Object 2 in inventory",
        "Show object 3 in inventory"
    };


    // Start is called before the first frame update
    void Start()
    {
        // Ensure dialogue box starts off
        self.SetActive(false);
        Debug.Log("dialogue box is not active yet");

        playerView = player.transform;

        //Tanner Addition
        // Cache references
        fpscontrollerScript = player.GetComponent<FPSController>();
        characters1 = lady.GetComponent<Characters>();
        characters2 = gardener.GetComponent<Characters>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug log when pressing E away from lady
        if (Input.GetKeyDown(KeyCode.E) && characters1.isAtLady != true && characters2.isAtGardener != true)
        {
            Debug.Log("you're clicking E but it's not registering you're at the any character");
        }

        // Open dialogue automatically when player is at lady and both dialogue & inventory are closed
        if (self.activeInHierarchy == false && inventory.activeInHierarchy == false && Input.GetKeyDown(KeyCode.E) && characters1.isAtLady == true || characters2.isAtGardener == true) //added self check so multiple objects arent made
        {
            self.SetActive(true);                               // Show dialogue UI
            SetDialogueText(allDialogue[0], textLabel);         // Show first line
            Debug.Log("triggered dialogue box");

            Cursor.lockState = CursorLockMode.None;             // Unlock mouse cursor for UI interaction
            Cursor.visible = true;


            if (characters1.isAtLady == true)
            {
                Camera.main.transform.SetPositionAndRotation(centerOnQueen.transform.position, centerOnQueen.transform.rotation);
            }

            if (characters2.isAtGardener == true)
            {
                Camera.main.transform.SetPositionAndRotation(centerOnGardener.transform.position, centerOnGardener.transform.rotation);
            }
            
            //tanner addition
            ShowInventoryItemButtons();                         // Show item buttons based on inventory

            // Stop player movement and placement
            placeObjects.canPlace = false;
            fpscontrollerScript.canMove = false;


        }

        // toggle and close dialogue when pressing E --> right now this works, but if you stay at the queen it'll make you walk away again before interacting again
        else if (Input.GetKeyDown(KeyCode.E) && self.activeInHierarchy)
        {
            closeDialogue();
            Debug.Log("exited dialogue box");

            // Re-lock mouse cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            characters1.isAtLady = false;
            characters2.isAtLady = false;

            Camera.main.transform.SetPositionAndRotation(playerView.transform.position, playerView.transform.rotation);

            //Tanner Addition
            // Allow player movement again
            fpscontrollerScript.canMove = true;

        }

    }

    // Starts typing out text slowly into the TMP text
    public void SetDialogueText(string textToType, TMP_Text textLabel)
    {
        StartCoroutine(routine: TypeText(textToType, textLabel));
    }

    // Coroutine that simulates typing effect
    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        float t = 0;
        int charIndex = 0;

        // Loop through characters and reveal them over time
        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);

            yield return null;  // Wait until next frame
        }

        // Ensure final text is fully shown
        textLabel.text = textToType;
    }


    // Close dialogue box and clear item buttons
    private void closeDialogue()
    {
        self.SetActive(false);

        //tanner addition
        ClearItemButtons();
        //added
    }

    //dont need this anymore
    //public void Option1()
    //{
    //    SetDialogueText(allDialogue[1], textLabel);
    //}

    //public void Option2()
    //{
    //    SetDialogueText(allDialogue[2], textLabel);
    //}

    //public void Option3()
    //{
    //    SetDialogueText(allDialogue[3], textLabel);
    //}





    //Tanner addition
    // Dynamically create buttons for each item in inventory
    public void ShowInventoryItemButtons()
    {

        foreach (var item in InventoryManager.Instance.Items) // make each button
        {
            GameObject button = Instantiate(itemButtonPrefab, itemButtonContainer); // Instantiate button prefab

            // Set button text and icon
            var label = button.GetComponentInChildren<TMP_Text>();
            var icon = button.transform.Find("Icon").GetComponent<Image>();
            label.text = "Show " + item.itemName;
            icon.sprite = item.icon;

            // Add click listener to show specific dialogue
            button.GetComponent<Button>().onClick.AddListener(() => OnItemShown(item));
        }
    }


    // Called when a button is clicked to show dialogue related to that item
    private void OnItemShown(Item item)
    {
        onNext = 0;

        if (characters2.isAtGardener == true)
        {
            // Call the dialogue options here for the name of the object
            if (item.itemName.Contains("Solid Gold Coffin"))
            {
                SetDialogueText(allDialogue[1], textLabel);
                objNum = 1;
            }
            else if (item.itemName.Contains("Modern Coffin"))
            {
                SetDialogueText(allDialogue[2], textLabel);
                objNum = 2;
            }
            else if (item.itemName.Contains("Recycled Coffin"))
            {
                SetDialogueText(allDialogue[3], textLabel);
                objNum = 3;
            }
            else if (item.itemName.Contains("Fern"))
            {
                SetDialogueText(allDialogue[4], textLabel);
                objNum = 4;
            }
            else if (item.itemName.Contains("Roses"))
            {
                SetDialogueText(allDialogue[5], textLabel);
                objNum = 5;
            }
            else if (item.itemName.Contains("Tulips"))
            {
                SetDialogueText(allDialogue[6], textLabel);
                objNum = 6;
            }
            else if (item.itemName.Contains("Orchids"))
            {
                SetDialogueText(allDialogue[7], textLabel);
                objNum = 7;
            }
            else if (item.itemName.Contains("Lady Portrait"))
            {
                SetDialogueText(allDialogue[8], textLabel);
                objNum = 8;
            }
            else if (item.itemName.Contains("Child Portrait"))
            {
                SetDialogueText(allDialogue[9], textLabel);
                objNum = 9;
            }
        }

        if (characters1.isAtLady == true)
        {
            // Call the dialogue options here for the name of the object
            if (item.itemName.Contains("Solid Gold Coffin"))
            {
                SetDialogueText(allDialogue[30], textLabel);
                objNum = 1;
            }
        }
    }


    // Destroy all buttons when dialogue closes so they don�t stack up next time
    private void ClearItemButtons()// dont want repeated objects in inventory so they are deleted when closed
    {
        foreach (Transform child in itemButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }
    //added

    public void Next()
    {
        if (characters2.isAtLady != true)
        {
            if (objNum == 1)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[2], textLabel);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    SetDialogueText(allDialogue[3], textLabel);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    SetDialogueText(allDialogue[4], textLabel);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    onNext = 0;
                }
            }

            if (objNum == 2)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[5], textLabel);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    onNext = 0;
                }
            }
            else if (objNum == 3)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[14], textLabel);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    SetDialogueText(allDialogue[17], textLabel);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    SetDialogueText(allDialogue[18], textLabel);
                    onNext++;
                }
                else if (onNext == 3)
                {
                    onNext = 0;
                }
            }
            else if (objNum == 4)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[19], textLabel);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    SetDialogueText(allDialogue[20], textLabel);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    SetDialogueText(allDialogue[21], textLabel);
                    onNext++;
                }
                else if (onNext == 3)
                {
                    onNext = 0;
                }
            }
            else if (objNum == 5)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[22], textLabel);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    SetDialogueText(allDialogue[23], textLabel);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    SetDialogueText(allDialogue[24], textLabel);
                    onNext++;
                }
                else if (onNext == 3)
                {
                    onNext = 0;
                }
            }
            else if (objNum == 6)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[25], textLabel);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    SetDialogueText(allDialogue[26], textLabel);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    SetDialogueText(allDialogue[27], textLabel);
                    onNext++;
                }
                else if (onNext == 3)
                {
                    onNext = 0;
                }
            }
            else if (objNum == 7)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[28], textLabel);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    SetDialogueText(allDialogue[29], textLabel);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    SetDialogueText(allDialogue[30], textLabel);
                    onNext++;
                }
                else if (onNext == 3)
                {
                    onNext = 0;
                }
            }
            else if (objNum == 8)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[31], textLabel);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    SetDialogueText(allDialogue[32], textLabel);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    SetDialogueText(allDialogue[33], textLabel);
                    onNext++;
                }
                else if (onNext == 3)
                {
                    onNext = 0;
                }
            }
            else if (objNum == 9)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[34], textLabel);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    SetDialogueText(allDialogue[35], textLabel);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    SetDialogueText(allDialogue[36], textLabel);
                    onNext++;
                }
                else if (onNext == 3)
                {
                    onNext = 0;
                }
            }
        } else if (characters2.isAtGardener != true)
        {
            if (objNum == 2)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[30], textLabel);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    onNext = 0;
                }
            }
        }
    }
}







