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
    public GameObject itemButtonPrefab;             // Prefab for an inventory item button in dialogue
    private FPSController fpscontrollerScript;      // Reference to player controller
    private Characters characters;                  // Reference to Characters script on the lady NPC
    public GameObject player;

    public GameObject lady;                         // Lady NPC
    public PlaceObjects placeObjects;               // Controls placement state
    [SerializeField] private GameObject inventory;  // Inventory UI (to check if open)

    //added


    [SerializeField] public TMP_Text textLabel;         // Label to show dialogue text
    [SerializeField] private float typeSpeed = 50;      // Characters per second when typing
    [SerializeField] public GameObject self;            // Dialogue UI panel itself

    [SerializeField] public GameObject dialogueOption1; // (Not used here but can be used for options)

    // All possible dialogue lines that can be shown based on items
    public string[] allDialogue = {
            "What is that you're holding?!",
            "How decadent, I'm sure Charles would love it.",
            "Simple, Charles wouldn't be caught dead in that, unless you'd like him to be.",
            "Fittingly drab for such a worthless king.",
            "How nice, although I doubt Charles would appreciate their simplicity.",
            "Red at a funeral? I love it. ",
            "The same color as the Filigree flag, how royal.",
            "Orchids for a funeral? Very original.",
            "I'm surprised you even found this in the first place.",
            "My dear Arthur, he meant everything to Charles and I."};

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

        //Tanner Addition
        // Cache references
        fpscontrollerScript = player.GetComponent<FPSController>();
        characters = lady.GetComponent<Characters>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug log when pressing I away from lady
        if (Input.GetKeyDown(KeyCode.I) && characters.isAtLady != true) {
            Debug.Log("you're clicking I but it's not registering you're at the lady");
        }

        // Open dialogue automatically when player is at lady and both dialogue & inventory are closed
        if (self.activeInHierarchy == false && inventory.activeInHierarchy == false && characters.isAtLady == true) //added self chech so multiple objects arent made
        {
            self.SetActive(true);                               // Show dialogue UI
            SetDialogueText(allDialogue[0], textLabel);         // Show first line
            Debug.Log("triggered dialogue box");

            Cursor.lockState = CursorLockMode.None;             // Unlock mouse cursor for UI interaction
            Cursor.visible = true;

            //tanner addition
            ShowInventoryItemButtons();                         // Show item buttons based on inventory

            // Stop player movement and placement
            placeObjects.canPlace = false;
            fpscontrollerScript.canMove = false;
            

        }

        // Close dialogue when pressing X
        else if (Input.GetKeyDown(KeyCode.X) && self.activeInHierarchy)
        {
            closeDialogue();
            Debug.Log("exited dialogue box");

            // Re-lock mouse cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            characters.isAtLady = false;

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
        // Call the dialogue options here for the name of the object
        if (item.itemName.Contains("Solid Gold Coffin"))
            SetDialogueText(allDialogue[1], textLabel);

        else if (item.itemName.Contains("Modern Coffin"))
            SetDialogueText(allDialogue[2], textLabel);

        else if (item.itemName.Contains("Recycled Coffin"))
            SetDialogueText(allDialogue[3], textLabel);

        else if (item.itemName.Contains("Fern"))
            SetDialogueText(allDialogue[4], textLabel);

        else if (item.itemName.Contains("Roses"))
            SetDialogueText(allDialogue[5], textLabel);

        else if (item.itemName.Contains("Tulips"))
            SetDialogueText(allDialogue[6], textLabel);

        else if (item.itemName.Contains("Orchids"))
            SetDialogueText(allDialogue[7], textLabel);

        else if (item.itemName.Contains("Lady Portrait"))
            SetDialogueText(allDialogue[8], textLabel);

        else if (item.itemName.Contains("Child Portrait"))
            SetDialogueText(allDialogue[9], textLabel);
    }


    // Destroy all buttons when dialogue closes so they don’t stack up next time
    private void ClearItemButtons()// dont want repeated objects in inventory so they are deleted when closed
    {
        foreach (Transform child in itemButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }
    //added


}







