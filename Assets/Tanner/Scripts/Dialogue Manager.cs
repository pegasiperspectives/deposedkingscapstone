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
    public string[] allDialogue = { };

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
        if (self.activeInHierarchy == false && inventory.activeInHierarchy == false && Input.GetKeyDown(KeyCode.E) && (characters1.isAtLady == true || characters2.isAtGardener == true)) //added self check so multiple objects arent made
        {
            self.SetActive(true);                               // Show dialogue UI
            SetDialogueText(allDialogue[0], textLabel, 0);         // Show first line
            //Debug.Log("triggered dialogue box");

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
    public void SetDialogueText(string textToType, TMP_Text textLabel, int index)
    {
        StartCoroutine(routine: TypeText(textToType, textLabel, index));
    }

    // Coroutine that simulates typing effect
    private IEnumerator TypeText(string textToType, TMP_Text textLabel, int index)
    {
        float t = 0;
        int charIndex = 0;

        // Loop through characters and reveal them over time
        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            if (index == 9 || index == 11 || index == 25 || index == 32 || index == 34 || index == 49)
            {
                textLabel.color = Color.purple;
            }
            else
            {
                textLabel.color = Color.white;
            }

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
                SetDialogueText(allDialogue[2], textLabel, 2);
                objNum = 1;
            }
            else if (item.itemName.Contains("Modern Coffin"))
            {
                SetDialogueText(allDialogue[5], textLabel, 5);
                objNum = 2;
            }
            else if (item.itemName.Contains("Recycled Coffin"))
            {
                SetDialogueText(allDialogue[6], textLabel, 6);
                objNum = 3;
            }
            else if (item.itemName.Contains("Fern"))
            {
                SetDialogueText(allDialogue[8], textLabel, 8);
                objNum = 4;
            }
            else if (item.itemName.Contains("Roses"))
            {
                SetDialogueText(allDialogue[8], textLabel, 8);
                objNum = 5;
            }
            else if (item.itemName.Contains("Tulips"))
            {
                SetDialogueText(allDialogue[8], textLabel, 8);
                objNum = 6;
            }
            else if (item.itemName.Contains("Orchids"))
            {
                SetDialogueText(allDialogue[8], textLabel, 8);
                objNum = 7;
            }
            else if (item.itemName.Contains("Lady Portrait"))
            {
                SetDialogueText(allDialogue[28], textLabel, 28);
                objNum = 8;
            }
            else if (item.itemName.Contains("Child Portrait"))
            {
                SetDialogueText(allDialogue[26], textLabel, 26);
                objNum = 9;
            }
        }

        if (characters1.isAtLady == true)
        {
            // Call the dialogue options here for the name of the object
            if (item.itemName.Contains("Solid Gold Coffin"))
            {
                SetDialogueText(allDialogue[30], textLabel, 30);
                objNum = 1;
            }
            else if (item.itemName.Contains("Modern Coffin"))
            {
                SetDialogueText(allDialogue[8], textLabel, 8);
                objNum = 2;
            }
            else if (item.itemName.Contains("Recycled Coffin"))
            {
                SetDialogueText(allDialogue[37], textLabel, 37);
                objNum = 3;
            }
            else if (item.itemName.Contains("Fern"))
            {
                SetDialogueText(allDialogue[38], textLabel, 38);
                objNum = 4;
            }
            else if (item.itemName.Contains("Roses"))
            {
                SetDialogueText(allDialogue[41], textLabel, 41);
                objNum = 5;
            }
            else if (item.itemName.Contains("Tulips"))
            {
                SetDialogueText(allDialogue[44], textLabel, 44);
                objNum = 6;
            }
            else if (item.itemName.Contains("Orchids"))
            {
                SetDialogueText(allDialogue[47], textLabel, 47);
                objNum = 7;
            }
            else if (item.itemName.Contains("Lady Portrait"))
            {
                SetDialogueText(allDialogue[59], textLabel, 59);
                objNum = 8;
            }
            else if (item.itemName.Contains("Child Portrait"))
            {
                SetDialogueText(allDialogue[48], textLabel, 48);
                objNum = 9;
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
        //gardener dialogue
        if (characters2.isAtLady != true)
        {
            //gold coffin
            if (objNum == 1)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[2], textLabel, 2);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    SetDialogueText(allDialogue[3], textLabel, 3);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    SetDialogueText(allDialogue[4], textLabel, 4);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    onNext = 0;
                }
            }

            //modern coffin
            if (objNum == 2)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[5], textLabel, 5);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    onNext = 0;
                }
            }

            //recycled coffin
            else if (objNum == 3)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[7], textLabel, 7);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    SetDialogueText(allDialogue[8], textLabel, 8);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    onNext = 0;
                }
            }

            //fern
            else if (objNum == 4)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[9], textLabel, 9);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    SetDialogueText(allDialogue[10], textLabel, 10);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    SetDialogueText(allDialogue[11], textLabel, 11);
                    onNext++;
                }
                else if (onNext == 3)
                {
                    SetDialogueText(allDialogue[12], textLabel, 12);
                    onNext++;
                }
                else if (onNext == 4)
                {
                    SetDialogueText(allDialogue[13], textLabel, 13);
                    onNext++;
                }
                else if (onNext == 5)
                {
                    SetDialogueText(allDialogue[14], textLabel, 14);
                    onNext++;
                }
                else if (onNext == 6)
                {
                    SetDialogueText(allDialogue[15], textLabel, 15);
                    onNext++;
                }
                else if (onNext == 7)
                {
                    SetDialogueText(allDialogue[16], textLabel, 16);
                    onNext++;
                }
                else if (onNext == 8)
                {
                    onNext = 0;
                }
            }

            //roses
            else if (objNum == 5)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[9], textLabel, 9);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    SetDialogueText(allDialogue[10], textLabel, 10);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    SetDialogueText(allDialogue[11], textLabel, 11);
                    onNext++;
                }
                else if (onNext == 3)
                {
                    SetDialogueText(allDialogue[12], textLabel, 12);
                    onNext++;
                }
                else if (onNext == 4)
                {
                    SetDialogueText(allDialogue[13], textLabel, 13);
                    onNext++;
                }
                else if (onNext == 5)
                {
                    SetDialogueText(allDialogue[17], textLabel, 17);
                    onNext++;
                }
                else if (onNext == 6)
                {
                    SetDialogueText(allDialogue[18], textLabel, 18);
                    onNext++;
                }
                else if (onNext == 7)
                {
                    onNext = 0;
                }
            }

            //tulip
            else if (objNum == 6)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[9], textLabel, 9);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    SetDialogueText(allDialogue[10], textLabel, 10);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    SetDialogueText(allDialogue[11], textLabel, 11);
                    onNext++;
                }
                else if (onNext == 3)
                {
                    SetDialogueText(allDialogue[12], textLabel, 12);
                    onNext++;
                }
                else if (onNext == 4)
                {
                    SetDialogueText(allDialogue[13], textLabel, 13);
                    onNext++;
                }
                else if (onNext == 5)
                {
                    SetDialogueText(allDialogue[19], textLabel, 19);
                    onNext++;
                }
                else if (onNext == 6)
                {
                    SetDialogueText(allDialogue[20], textLabel, 20);
                    onNext++;
                }
                else if (onNext == 7)
                {
                    SetDialogueText(allDialogue[21], textLabel, 21);
                    onNext++;
                }
                else if (onNext == 8)
                {
                    onNext = 0;
                }
            }

            //orchid
            else if (objNum == 7)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[9], textLabel, 9);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    SetDialogueText(allDialogue[10], textLabel, 10);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    SetDialogueText(allDialogue[11], textLabel, 11);
                    onNext++;
                }
                else if (onNext == 3)
                {
                    SetDialogueText(allDialogue[12], textLabel, 12);
                    onNext++;
                }
                else if (onNext == 4)
                {
                    SetDialogueText(allDialogue[22], textLabel, 22);
                    onNext++;
                }
                else if (onNext == 5)
                {
                    SetDialogueText(allDialogue[23], textLabel, 23);
                    onNext++;
                }
                else if (onNext == 6)
                {
                    SetDialogueText(allDialogue[24], textLabel, 24);
                    onNext++;
                }
                else if (onNext == 7)
                {
                    SetDialogueText(allDialogue[25], textLabel, 25);
                    onNext++;
                }
                else if (onNext == 8)
                {
                    onNext = 0;
                }
            }

            //queen portrait
            else if (objNum == 8)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[29], textLabel, 29);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    onNext = 0;
                }
            }

            //child portrait
            else if (objNum == 9)
            {
                if (onNext == 0)
                {
                    SetDialogueText(allDialogue[27], textLabel, 27);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    onNext = 0;
                }
            }
        }

        //queen's dialogue
        else if (characters2.isAtGardener != true)
        {
            if (characters2.isAtLady != true)
            {
                //gold coffin
                if (objNum == 1)
                {
                    if (onNext == 0)
                    {
                        SetDialogueText(allDialogue[31], textLabel, 31);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        onNext = 0;
                    }
                }

                //modern coffin
                if (objNum == 2)
                {
                    if (onNext == 0)
                    {
                        SetDialogueText(allDialogue[32], textLabel, 32);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        SetDialogueText(allDialogue[33], textLabel, 33);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        SetDialogueText(allDialogue[34], textLabel, 34);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        SetDialogueText(allDialogue[35], textLabel, 35);
                        onNext++;
                    }
                    else if (onNext == 4)
                    {
                        SetDialogueText(allDialogue[36], textLabel, 36);
                        onNext++;
                    }
                    else if (onNext == 5)
                    {
                        onNext = 0;
                    }
                }

                //recycled coffin
                else if (objNum == 3)
                {
                    if (onNext == 0)
                    {
                        SetDialogueText(allDialogue[37], textLabel, 37);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        onNext = 0;
                    }
                }

                //fern
                else if (objNum == 4)
                {
                    if (onNext == 0)
                    {
                        SetDialogueText(allDialogue[39], textLabel, 39);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        SetDialogueText(allDialogue[40], textLabel, 40);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        onNext = 0;
                    }
                }

                //roses
                else if (objNum == 5)
                {
                    if (onNext == 0)
                    {
                        SetDialogueText(allDialogue[42], textLabel, 42);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        SetDialogueText(allDialogue[43], textLabel, 43);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        onNext = 0;
                    }
                }

                //tulip
                else if (objNum == 6)
                {
                    if (onNext == 0)
                    {
                        SetDialogueText(allDialogue[45], textLabel, 45);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        SetDialogueText(allDialogue[46], textLabel, 46);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        onNext = 0;
                    }
                }

                //orchid
                else if (objNum == 7)
                {
                    if (onNext == 0)
                    {
                        SetDialogueText(allDialogue[47], textLabel, 47);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        onNext = 0;
                    }
                }

                //queen portrait
                else if (objNum == 8)
                {
                    if (onNext == 0)
                    {
                        SetDialogueText(allDialogue[59], textLabel, 59);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        onNext = 0;
                    }
                }

                //child portrait
                else if (objNum == 9)
                {
                    if (onNext == 0)
                    {
                        SetDialogueText(allDialogue[49], textLabel, 49);
                        onNext++;
                    }
                    if (onNext == 1)
                    {
                        SetDialogueText(allDialogue[50], textLabel, 50);
                        onNext++;
                    }
                    if (onNext == 2)
                    {
                        SetDialogueText(allDialogue[51], textLabel, 51);
                        onNext++;
                    }
                    if (onNext == 3)
                    {
                        SetDialogueText(allDialogue[52], textLabel, 52);
                        onNext++;
                    }
                    if (onNext == 4)
                    {
                        SetDialogueText(allDialogue[53], textLabel, 53);
                        onNext++;
                    }
                    if (onNext == 5)
                    {
                        SetDialogueText(allDialogue[54], textLabel, 54);
                        onNext++;
                    }
                    if (onNext == 6)
                    {
                        SetDialogueText(allDialogue[55], textLabel, 55);
                        onNext++;
                    }
                    if (onNext == 7)
                    {
                        SetDialogueText(allDialogue[56], textLabel, 56);
                        onNext++;
                    }
                    if (onNext == 8)
                    {
                        SetDialogueText(allDialogue[57], textLabel, 57);
                        onNext++;
                    }
                    if (onNext == 9)
                    {
                        SetDialogueText(allDialogue[58], textLabel, 58);
                        onNext++;
                    }
                    else if (onNext == 10)
                    {
                        onNext = 0;
                    }
                }
            }
        }
    }
}







