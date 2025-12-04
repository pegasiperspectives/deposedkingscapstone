using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{

    //public GameObject defaultCrosshair;
    //public GameObject speechCrosshair;
    public Texture2D cursorTexture;
    //Tanner Addition
    // References and fields for dynamic UI
    public Transform itemButtonContainer;           // Where dynamically created buttons will go

    public Transform playerView;

    [SerializeField]
    public Transform centerOnQueen;

    public Transform centerOnGardener;
    public GameObject itemButtonPrefab;             // Prefab for an inventory item button in dialogue
    private FPSController fpscontrollerScript;      // Reference to player controller
    private Characters character1;                  // Reference to Characters script on the lady NPC
    private Characters character2;
    public GameObject player;

    public Int32 objNum = -1;
    public Int32 onNext = 0;

    public GameObject lady;                         // Lady NPC

    public GameObject gardener;

    public PlaceObjects placeObjects;               // Controls placement state
    [SerializeField] private GameObject inventory;  // Inventory UI (to check if open)
    public InventoryManager inventoryManager;

    //added
    private CharacterController controller;

    [SerializeField] public TMP_Text textLabel;         // Label to show dialogue text
    [SerializeField] private float typeSpeed = 50;      // Characters per second when typing
    [SerializeField] public GameObject self;            // Dialogue UI panel itself

    [SerializeField] public GameObject dialogueOption1; // (Not used here but can be used for options)

    [SerializeField] private float focusDuration = 0.5f;

    public bool CurrentlyInDialogue = false;

    public GameObject speechBubbles;

    #region Dialogue Arrays - Topher Code
    // All possible dialogue lines that can be shown based on items
    [Header("Dialogue Trackers")]
    public bool metG = false; //Have you met the groundskeeper?
    public bool metL = false; //Have you met the Lady?
    public int textColorIndex = 0; // 0 is white, 1 is green, 2 is blue - used when text is printed in the dialogue box

    [Header("Dialogue Arrays")] // header to split the dialogue in the inspector
    // Separate item dialogue arrays - organized in the same order as the Item Sheet google sheet / Inventory page
    [SerializeField] string[] memoDialogueG = { };                   //Memo
    [SerializeField] string[] memoDialogueL = { };
    [SerializeField] string[] solidGoldCasketDialogueG = { };        //Solid Gold Casket
    [SerializeField] string[] solidGoldCasketDialogueL = { };
    [SerializeField] string[] modernCasketDialogueG = { };           //Modern Casket
    [SerializeField] string[] modernCasketDialogueL = { };
    [SerializeField] string[] recycledCoffinDialogueG = { };         //Recycled Coffin
    [SerializeField] string[] recycledCoffinDialogueL = { };
    [SerializeField] string[] fernBoquetDialogueG = { };             //Fern Boquet
    [SerializeField] string[] fernBoquetDialogueL = { };
    [SerializeField] string[] roseBoquetDialogueG = { };             //Rose Boquet
    [SerializeField] string[] roseBoquetDialogueL = { };
    [SerializeField] string[] orchidBoquetDialogueG = { };           //Orchid Boquet
    [SerializeField] string[] orchidBoquetDialogueL = { };
    [SerializeField] string[] tulipBoquetDialogueG = { };             //Tulip Boquet
    [SerializeField] string[] tulipBoquetDialogueL = { };
    [SerializeField] string[] brokenFiligreeCrestDialogueG = { };    //Broken Filigree Crest
    [SerializeField] string[] brokenFiligreeCrestDialogueL = { };
    [SerializeField] string[] boxofBugsDialogueG = { };              //Box of Bugs
    [SerializeField] string[] boxofBugsDialogueL = { };
    [SerializeField] string[] wovenShawlDialogueG = { };             //Woven Shawl
    public bool wovenShawlShownG = false;                       // have you shown the shawl to the groundskeeper?
    [SerializeField] string[] wovenShawlDialogueL = { };
    [SerializeField] string[] halfKnitQuiltDialogueG = { };          //Half-Knit Quilt
    [SerializeField] string[] halfKnitQuiltDialogueL = { };
    [SerializeField] string[] filigreeKeepLedgerDialogueG = { };     //Filigree Keep Ledger
    [SerializeField] string[] filigreeKeepLedgerDialogueL = { };
    [SerializeField] string[] strippedGobletDialogueG = { };         //Stripped Goblet
    [SerializeField] string[] strippedGobletDialogueL = { };
    [SerializeField] string[] portraitofLadyDialogueG = { };         //Portrait of Lady
    [SerializeField] string[] portraitofLadyDialogueL = { };
    [SerializeField] string[] portraitofKingDialogueG = { };         //Portrait of King
    [SerializeField] string[] portraitofKingDialogueL = { };
    [SerializeField] string[] portraitofChildDialogueG = { };        //Portrait of Child
    [SerializeField] string[] portraitofChildDialogueL = { };
    [SerializeField] string[] rustyKeyDialogueG = { };               //Rusty Key
    [SerializeField] string[] rustyKeyDialogueL = { };
    [SerializeField] string[] quartersKeyDialogueG = { };            //Quarter's Key
    [SerializeField] string[] quartersKeyDialogueL = { };
    [SerializeField] string[] woodenPlankDialogueG = { };            //Suspiciously Long Wooden Plank
    [SerializeField] string[] woodenPlankDialogueL = { };
    [SerializeField] string[] hisMajestyDialogueG = { };             //His Majesty
    [SerializeField] string[] hisMajestyDialogueL = { };

    [SerializeField] string[] introDialoguePart1G = { };            // Character Introduction Dialogue Script
    [SerializeField] string[] introDialoguePart2G = { };
    [SerializeField] string[] introDialoguePart1L = { };
    [SerializeField] string[] introDialoguePart2L = { };


    [SerializeField] public string[] allDialogue = { };

    #endregion

    // (Not used in current logic but example placeholders)
    public string[] showObjects = {
        "Show object 1 in inventory",
        "Show Object 2 in inventory",
        "Show object 3 in inventory"
    };

    public bool wantstoexit = false;


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
        character1 = lady.GetComponent<Characters>();
        character2 = gardener.GetComponent<Characters>();
        controller = player.GetComponent<CharacterController>();

        speechBubbles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug log when pressing E away from lady
        if (Input.GetMouseButtonDown(0) && Characters.isAtLady != true && Characters.isAtGardener != true && InventoryManager.currentlyInspecting != true)
        {
            Debug.Log("you're clicking on a character but it's not registering you're at the any character");
        }

        if ((Characters.isAtLady == true || Characters.isAtGardener == true) && inventory.activeInHierarchy == false && self.activeInHierarchy == false)
        {
            speechBubbles.SetActive(true);

            for (int i = 0; i < 6; i++)
            {
                speechBubbles.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        // Open dialogue on click when player is at lady and both dialogue & inventory are closed
        if (self.activeInHierarchy == false && inventory.activeInHierarchy == false && Input.GetMouseButtonDown(0) && (Characters.isAtLady == true || Characters.isAtGardener == true)) //added self check so multiple objects arent made
        {
            inventoryManager.CursorOn();
            self.SetActive(true);

            if (Characters.isAtLady == true && Characters.isAtGardener == false)
            {

                //if (controller) controller.enabled = false;

                StartCoroutine(SmoothMovePlayer(centerOnQueen, focusDuration));
                //player.transform.SetPositionAndRotation(centerOnQueen.transform.position, centerOnQueen.transform.rotation);

                if (controller) controller.enabled = true;
            }

            if (Characters.isAtGardener == true && Characters.isAtLady == false)
            {

                //if (controller) controller.enabled = false;

                StartCoroutine(SmoothMovePlayer(centerOnGardener, focusDuration));
                //player.transform.SetPositionAndRotation(centerOnGardener.transform.position, centerOnGardener.transform.rotation);


                if (controller) controller.enabled = true;
            }

            //tanner addition
            //ShowInventoryItemButtons();                         // Show item buttons based on inventory

            // Stop player movement and placement
            placeObjects.canPlace = false;
            fpscontrollerScript.canMove = false;


        }

        if (Input.GetMouseButtonDown(0) && self.activeInHierarchy)   // this activates when clicking on a new item, PROBLEM: displaying the Next() dialogue of the previous item first.
        {
            Next();
            Debug.Log("Registering mouse click; should change line now");
        }

        else if (wantstoexit == true && self.activeInHierarchy)
        {
            closeDialogue();
            speechBubbles.SetActive(false);
            //speechCrosshair.SetActive(false);
            //defaultCrosshair.SetActive(true);
            Debug.Log("exited dialogue box");

            // Re-lock mouse cursor
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Characters.isAtLady = false;
            Characters.isAtGardener = false;

            fpscontrollerScript.ForceCameraLevel();

            //Tanner Addition
            // Allow player movement again
            fpscontrollerScript.canMove = true;


        }

    }

    // Starts typing out text slowly into the TMP text
    public void SetDialogueText(string textToType, TMP_Text textLabel, int index)
    {
        StartCoroutine(routine: TypeText(textToType, textLabel, index));
        //speechCrosshair.SetActive(false);
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

            if(textColorIndex == 1)
            {
                textLabel.color = Color.green;
            }
            else if(textColorIndex == 2)
            {
                textLabel.color = Color.blue;
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
        wantstoexit = false;
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


    private IEnumerator SmoothMovePlayer(Transform target, float duration)
    {
        //speechCrosshair.SetActive(false);
        if (!target) yield break;
        Vector3 startPos = player.transform.position;
        Quaternion startRot = player.transform.rotation;
        Vector3 endPos = target.transform.position;
        Quaternion endRot = target.transform.rotation;

        if (controller) controller.enabled = false;

        float t = 0f;
        float Ease(float x) => x * x * (3f - 2f * x);
        Transform cam = Camera.main.transform;
        Quaternion camStartRot = cam.localRotation;


        Quaternion camEndRot = Quaternion.identity;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float u = Ease(Mathf.Clamp01(t / duration));
            player.transform.position = Vector3.Lerp(startPos, endPos, u);
            player.transform.rotation = Quaternion.Slerp(startRot, endRot, u);


            Vector3 currentCamEuler = cam.localEulerAngles;

            cam.localRotation = Quaternion.Slerp(camStartRot, camEndRot, u);


            yield return null;
        }
        player.transform.SetPositionAndRotation(endPos, endRot);

        Camera.main.transform.localRotation = Quaternion.identity;
        //if (controller) controller.enabled = true;

    }
    // Called when a button is clicked to show dialogue related to that item
    public void OnItemShown(Item item)
    {
        CurrentlyInDialogue = true;
        inventory.SetActive(false);
        //speechCrosshair.SetActive(false);                             // Show dialogue UI
        SetDialogueText(allDialogue[60], textLabel, 60);         // Show first line
        objNum = -1;
        // Resets objNum so no dialogue is activated from Next() 


        //Debug.Log("triggered dialogue box");

        //Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        //speechCrosshair.SetActive(false);
        //defaultCrosshair.SetActive(false);
        Cursor.lockState = CursorLockMode.None;             // Unlock mouse cursor for UI interaction
        Cursor.visible = true;
        onNext = 1; // ensures that activating Next() will play the 2nd line of dialogue

        if (Characters.isAtGardener == true)
        {
            textColorIndex = 1; // Starts all Hayem dialogue as green

            // Call the dialogue options here for the name of the object
            if (item.itemName.Contains("Memo"))                              
            {
                character2.SpriteChanger(2);
                SetDialogueText(memoDialogueG[0], textLabel, 0);
                objNum = 0;
            }
            else if (item.itemName.Contains("Solid Gold Coffin"))
            {
                character2.SpriteChanger(2);
                SetDialogueText(solidGoldCasketDialogueG[0], textLabel, 0);
                objNum = 1;
            }
            else if (item.itemName.Contains("Modern Coffin"))
            {
                character2.SpriteChanger(3);
                SetDialogueText(modernCasketDialogueG[0], textLabel, 0);
                objNum = 2;
            }
            else if (item.itemName.Contains("Recycled Coffin"))
            {
                character2.SpriteChanger(0);
                SetDialogueText(recycledCoffinDialogueG[0], textLabel, 0);
                objNum = 3;
            }
            else if (item.itemName.Contains("Fern"))
            {
                character2.SpriteChanger(1);
                SetDialogueText(fernBoquetDialogueG[0], textLabel, 0);
                objNum = 4;
            }
            else if (item.itemName.Contains("Roses"))
            {
                character2.SpriteChanger(4);
                SetDialogueText(roseBoquetDialogueG[0], textLabel, 0);
                objNum = 5;
            }
            else if (item.itemName.Contains("Orchids"))
            {
                character2.SpriteChanger(5);
                SetDialogueText(orchidBoquetDialogueG[0], textLabel, 0);
                objNum = 6;
            }
            else if (item.itemName.Contains("Tulips"))
            {
                character2.SpriteChanger(0);
                SetDialogueText(tulipBoquetDialogueG[0], textLabel, 0);
                objNum = 7;
            }
            else if (item.itemName.Contains("Broken Filigree Crest"))
            {
                character2.SpriteChanger(3);
                SetDialogueText(brokenFiligreeCrestDialogueG[0], textLabel, 0);
                objNum = 8;
            }
            else if (item.itemName.Contains("Box of Bugs"))
            {
                character2.SpriteChanger(1);
                SetDialogueText(boxofBugsDialogueG[0], textLabel, 0);
                objNum = 9;
            }
            else if (item.itemName.Contains("Woven Shawl"))
            {
                character2.SpriteChanger(0);
                wovenShawlShownG = true;

                SetDialogueText(wovenShawlDialogueG[0], textLabel, 0);
                objNum = 10;
            }
            else if (item.itemName.Contains("Half-Knit Quilt"))
            {
                character2.SpriteChanger(0);
                SetDialogueText(halfKnitQuiltDialogueG[0], textLabel, 0);
                objNum = 11;
            }
            else if (item.itemName.Contains("Filigree Keep Ledger"))
            {
                character2.SpriteChanger(5);
                SetDialogueText(filigreeKeepLedgerDialogueG[0], textLabel, 0);
                objNum = 12;
            }
            else if (item.itemName.Contains("Stripped Goblet"))
            {
                character2.SpriteChanger(1);
                SetDialogueText(strippedGobletDialogueG[0], textLabel, 0);
                objNum = 13;
            }
            else if (item.itemName.Contains("Lady Portrait"))
            {
                character2.SpriteChanger(4);
                SetDialogueText(portraitofLadyDialogueG[0], textLabel, 0);
                objNum = 14;
            }
            else if (item.itemName.Contains("King Portrait"))
            {
                character2.SpriteChanger(2);
                SetDialogueText(portraitofKingDialogueG[0], textLabel, 0);
                objNum = 15;
            }
            else if (item.itemName.Contains("Child Portrait"))
            {
                character2.SpriteChanger(3);
                SetDialogueText(portraitofChildDialogueG[0], textLabel, 0);
                objNum = 16;
            }
            else if (item.itemName.Contains("Rusty Key"))
            {
                character2.SpriteChanger(2);
                SetDialogueText(rustyKeyDialogueG[0], textLabel, 0);
                objNum = 17;
            }
            else if (item.itemName.Contains("Quarter's Key"))
            {
                character2.SpriteChanger(4);
                SetDialogueText(quartersKeyDialogueG[0], textLabel, 0);
                objNum = 18;
            }
            else if (item.itemName.Contains("Wooden Plank"))
            {
                character2.SpriteChanger(3);
                SetDialogueText(woodenPlankDialogueG[0], textLabel, 0);
                objNum = 19;
            }
            else if (item.itemName.Contains("His Majesty"))
            {
                character2.SpriteChanger(4);
                SetDialogueText(hisMajestyDialogueG[0], textLabel, 0);
                objNum = 20;
            }
        }

        else if (Characters.isAtLady == true)
        {
            textColorIndex = 2; // Starts all Lady Dialogue as blue

            // Call the dialogue options here for the name of the object
            if (item.itemName.Contains("Memo"))                              //ITEMS 0, 8-13, 15, and 17-20 NEED IMPLEMENTED
            {
                character1.SpriteChanger(0);
                SetDialogueText(memoDialogueL[0], textLabel, 0);
                objNum = 0;
            }
            else if (item.itemName.Contains("Solid Gold Coffin"))
            {
                character1.SpriteChanger(3);
                SetDialogueText(solidGoldCasketDialogueL[0], textLabel, 0);
                objNum = 1;
            }
            else if (item.itemName.Contains("Modern Coffin"))
            {
                character1.SpriteChanger(3);
                SetDialogueText(modernCasketDialogueL[0], textLabel, 0);
                objNum = 2;
            }
            else if (item.itemName.Contains("Recycled Coffin"))
            {
                character1.SpriteChanger(1);
                SetDialogueText(recycledCoffinDialogueL[0], textLabel, 0);
                objNum = 3;
            }
            else if (item.itemName.Contains("Fern"))
            {
                character1.SpriteChanger(3);
                SetDialogueText(fernBoquetDialogueL[0], textLabel, 0);
                objNum = 4;
            }
            else if (item.itemName.Contains("Roses"))
            {
                character1.SpriteChanger(1);
                SetDialogueText(roseBoquetDialogueL[0], textLabel, 0);
                objNum = 5;
            }
            else if (item.itemName.Contains("Orchids"))
            {
                character1.SpriteChanger(0);
                SetDialogueText(orchidBoquetDialogueL[0], textLabel, 0);
                objNum = 6;
            }
            else if (item.itemName.Contains("Tulips"))
            {
                character1.SpriteChanger(0);
                SetDialogueText(tulipBoquetDialogueL[0], textLabel, 0);
                objNum = 7;
            }
            else if (item.itemName.Contains("Broken Filigree Crest"))
            {
                character1.SpriteChanger(0);
                SetDialogueText(brokenFiligreeCrestDialogueL[0], textLabel, 0);
                objNum = 8;
            }
            else if (item.itemName.Contains("Box of Bugs"))
            {
                character1.SpriteChanger(3);
                SetDialogueText(boxofBugsDialogueL[0], textLabel, 0);
                objNum = 9;
            }
            else if (item.itemName.Contains("Woven Shawl"))
            {
                character1.SpriteChanger(0);
                SetDialogueText(wovenShawlDialogueL[0], textLabel, 0);
                objNum = 10;
            }
            else if (item.itemName.Contains("Half-Knit Quilt"))
            {
                character1.SpriteChanger(0);
                SetDialogueText(halfKnitQuiltDialogueL[0], textLabel, 0);
                objNum = 11;
            }
            else if (item.itemName.Contains("Filigree Keep Ledger"))
            {
                character1.SpriteChanger(0);
                SetDialogueText(filigreeKeepLedgerDialogueL[0], textLabel, 0);
                objNum = 12;
            }
            else if (item.itemName.Contains("Stripped Goblet"))
            {
                character1.SpriteChanger(0);
                SetDialogueText(strippedGobletDialogueL[0], textLabel, 0);
                objNum = 13;
            }
            else if (item.itemName.Contains("Lady Portrait"))
            {
                character1.SpriteChanger(5);
                SetDialogueText(portraitofLadyDialogueL[0], textLabel, 0);
                objNum = 14;
            }
            else if (item.itemName.Contains("King Portrait"))
            {
                character1.SpriteChanger(1);
                SetDialogueText(portraitofKingDialogueL[0], textLabel, 0);
                objNum = 15;
            }
            else if (item.itemName.Contains("Child Portrait"))
            {
                character1.SpriteChanger(4);
                SetDialogueText(portraitofChildDialogueL[0], textLabel, 0);
                objNum = 16;
            }
            else if (item.itemName.Contains("Rusty Key"))
            {
                character1.SpriteChanger(0);
                SetDialogueText(rustyKeyDialogueL[0], textLabel, 0);
                objNum = 17;
            }
            else if (item.itemName.Contains("Quarter's Key"))
            {
                character1.SpriteChanger(0);
                SetDialogueText(quartersKeyDialogueL[0], textLabel, 0);
                objNum = 18;
            }
            else if (item.itemName.Contains("Wooden Plank"))
            {
                character1.SpriteChanger(2);
                SetDialogueText(woodenPlankDialogueL[0], textLabel, 0);
                objNum = 19;
            }
            else if (item.itemName.Contains("His Majesty"))
            {
                character1.SpriteChanger(5);
                SetDialogueText(hisMajestyDialogueL[0], textLabel, 0);
                objNum = 20;
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
        if (Characters.isAtLady != true)
        {
            Debug.Log("Next while on gardener was clicked");

            // 2nd Part of Hayem's introductory dialogue
            if (metG == false) 
            {
                if (onNext == 0)
                {
                    textColorIndex = 1;
                    character2.SpriteChanger(2);
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 3)
                {
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 4)
                {
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 5)
                {
                    textColorIndex = 1;
                    character2.SpriteChanger(4);
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 6)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 7)
                {
                    textColorIndex = 1;
                    character2.SpriteChanger(2);
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 8)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 9)
                {
                    textColorIndex = 1;
                    character2.SpriteChanger(0);
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 10)
                {
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 11)
                {
                    character2.SpriteChanger(4);
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 12)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 13)
                {
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 14)
                {
                    textColorIndex = 1;
                    character2.SpriteChanger(0);
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 15)
                {
                    SetDialogueText(introDialoguePart2G[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext >= 16)
                {
                    metG = true;
                    DialogueComplete();
                }
            }
            else
            {
                #region Groundskeeper Dialogue Next
                // memo
                if (objNum == 0)
                {
                    if (onNext == 0)
                    {
                        textColorIndex = 1;
                        character2.SpriteChanger(0);
                        SetDialogueText(memoDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character2.SpriteChanger(3);
                        SetDialogueText(memoDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        textColorIndex = 0;
                        SetDialogueText(memoDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        SetDialogueText(memoDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 4)
                    {
                        textColorIndex = 1;
                        character2.SpriteChanger(0);
                        SetDialogueText(memoDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 5)
                    {
                        SetDialogueText(memoDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 6)
                    {
                        textColorIndex = 0;
                        SetDialogueText(memoDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 7)
                    {
                        SetDialogueText(memoDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 8)
                    {
                        textColorIndex = 1;
                        SetDialogueText(memoDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 9)
                    {
                        textColorIndex = 0;
                        SetDialogueText(memoDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 10)
                    {
                        DialogueComplete();
                    }

                }

                // gold coffin
                if (objNum == 1)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(2);
                        SetDialogueText(solidGoldCasketDialogueG[onNext], textLabel, onNext); // plays line number of dialogue equal to onNext
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character2.SpriteChanger(1);
                        SetDialogueText(solidGoldCasketDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        SetDialogueText(solidGoldCasketDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 3)
                    {
                        DialogueComplete();
                    }
                }

                //modern coffin
                if (objNum == 2)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(3);
                        SetDialogueText(modernCasketDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 1)
                    {
                        DialogueComplete();
                    }
                }

                //recycled coffin
                if (objNum == 3)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(0);
                        SetDialogueText(recycledCoffinDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        SetDialogueText(recycledCoffinDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        character2.SpriteChanger(3);
                        SetDialogueText(recycledCoffinDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 3)
                    {
                        DialogueComplete();
                    }
                }

                //fern boquet
                if (objNum == 4)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(1);
                        SetDialogueText(fernBoquetDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        SetDialogueText(fernBoquetDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        character2.SpriteChanger(5);
                        SetDialogueText(fernBoquetDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 3)
                    {
                        DialogueComplete();
                    }
                }

                //rose boquet
                if (objNum == 5)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(4);
                        SetDialogueText(roseBoquetDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character2.SpriteChanger(3);
                        SetDialogueText(roseBoquetDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 2)
                    {
                        DialogueComplete();
                    }
                }

                //orchid boquet
                if (objNum == 6)
                {
                    if (onNext == 0)
                    {
                        textColorIndex = 1;
                        character2.SpriteChanger(5);
                        SetDialogueText(orchidBoquetDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character2.SpriteChanger(0);
                        SetDialogueText(orchidBoquetDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        textColorIndex = 0;
                        SetDialogueText(orchidBoquetDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        textColorIndex = 1;
                        character2.SpriteChanger(1);
                        SetDialogueText(orchidBoquetDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 4)
                    {
                        DialogueComplete();
                    }
                }

                //tulip boquet
                if (objNum == 7)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(0);
                        SetDialogueText(tulipBoquetDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character2.SpriteChanger(1);
                        SetDialogueText(tulipBoquetDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        SetDialogueText(tulipBoquetDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        character2.SpriteChanger(0);
                        SetDialogueText(tulipBoquetDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 4)
                    {
                        DialogueComplete();
                    }
                }

                // broken filigree crest
                if (objNum == 8)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(3);
                        SetDialogueText(brokenFiligreeCrestDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character2.SpriteChanger(0);
                        SetDialogueText(brokenFiligreeCrestDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        SetDialogueText(brokenFiligreeCrestDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        character2.SpriteChanger(4);
                        SetDialogueText(brokenFiligreeCrestDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 4)
                    {
                        DialogueComplete();
                    }
                }

                // box of bugs
                if (objNum == 9)
                {
                    if (onNext == 0)
                    {
                        textColorIndex = 1;
                        character2.SpriteChanger(1);
                        SetDialogueText(boxofBugsDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        textColorIndex = 0;
                        SetDialogueText(boxofBugsDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        textColorIndex = 1;
                        character2.SpriteChanger(0);
                        SetDialogueText(boxofBugsDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        SetDialogueText(boxofBugsDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 4)
                    {
                        character2.SpriteChanger(3);
                        SetDialogueText(boxofBugsDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 5)
                    {
                        character2.SpriteChanger(4);
                        SetDialogueText(boxofBugsDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 6)
                    {
                        DialogueComplete();
                    }
                }

                // woven shawl
                if (objNum == 10)
                {


                    if (onNext == 0)
                    {
                        character2.SpriteChanger(0);
                        SetDialogueText(wovenShawlDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character2.SpriteChanger(4);
                        SetDialogueText(wovenShawlDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 2)
                    {
                        DialogueComplete();
                    }
                }

                // half-knit quilt
                if (objNum == 11)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(0);
                        SetDialogueText(halfKnitQuiltDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 1)
                    {
                        DialogueComplete();
                    }
                }

                // filigree keep ledger
                if (objNum == 12)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(5);
                        SetDialogueText(filigreeKeepLedgerDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 1)
                    {
                        DialogueComplete();
                    }
                }

                // stripped goblet
                if (objNum == 13)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(1);
                        SetDialogueText(strippedGobletDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character2.SpriteChanger(0);
                        SetDialogueText(strippedGobletDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        character2.SpriteChanger(1);
                        SetDialogueText(strippedGobletDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 3)
                    {
                        DialogueComplete();
                    }
                }

                //queen portrait
                if (objNum == 14)
                {
                    if (onNext == 0)
                    {
                        textColorIndex = 1;
                        character2.SpriteChanger(4);
                        SetDialogueText(portraitofLadyDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        SetDialogueText(portraitofLadyDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {

                        SetDialogueText(portraitofLadyDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        textColorIndex = 0;
                        SetDialogueText(portraitofLadyDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 4)
                    {
                        textColorIndex = 1;
                        character2.SpriteChanger(5);
                        SetDialogueText(portraitofLadyDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 5)
                    {
                        character2.SpriteChanger(3);
                        SetDialogueText(portraitofLadyDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 6)
                    {
                        DialogueComplete();
                    }
                }

                // portrait of king
                if (objNum == 15)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(2);
                        SetDialogueText(portraitofKingDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character2.SpriteChanger(4);
                        SetDialogueText(portraitofKingDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        SetDialogueText(portraitofKingDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 3)
                    {
                        DialogueComplete();
                    }
                }

                //child portrait
                if (objNum == 16)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(3);
                        SetDialogueText(portraitofChildDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        SetDialogueText(portraitofChildDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        SetDialogueText(portraitofChildDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        SetDialogueText(portraitofChildDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 4)
                    {
                        SetDialogueText(portraitofChildDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 5)
                    {
                        DialogueComplete();
                    }
                }

                // rusty key
                if (objNum == 17)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(2);
                        SetDialogueText(rustyKeyDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 1)
                    {
                        DialogueComplete();
                    }
                }

                // quarter's key
                if (objNum == 18)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(4);
                        SetDialogueText(quartersKeyDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 1)
                    {
                        DialogueComplete();
                    }
                }

                // suspiciously long wooden plank
                if (objNum == 19)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(3);
                        SetDialogueText(woodenPlankDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 1)
                    {
                        DialogueComplete();
                    }
                }

                // His Majesty
                if (objNum == 20)
                {
                    if (onNext == 0)
                    {
                        character2.SpriteChanger(0);
                        SetDialogueText(hisMajestyDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        SetDialogueText(hisMajestyDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 2)
                    {
                        DialogueComplete();
                    }
                }
                #endregion
            }
        }


        //queen's dialogue
        else if (Characters.isAtGardener != true && Characters.isAtLady == true)
        {
            Debug.Log("Next while on queen was clicked");

            // 2nd Part of Lady F's Introductory Dialogue
            if (metL == false) 
            {
                if (onNext == 0)
                {
                    textColorIndex = 2;
                    character1.SpriteChanger(3);
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 1)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 2)
                {
                    textColorIndex = 2;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 3)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 4)
                {
                    textColorIndex = 2;
                    character1.SpriteChanger(4);
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 5)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 6)
                {
                    textColorIndex = 2;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 7)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 8)
                {
                    textColorIndex = 2;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 9)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 10)
                {
                    textColorIndex = 2;
                    character1.SpriteChanger(0);
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 11)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 12)
                {
                    textColorIndex = 2;
                    character1.SpriteChanger(3);
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 13)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 14)
                {
                    textColorIndex = 2;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 15)
                {
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 16)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 17)
                {
                    textColorIndex = 2;
                    character1.SpriteChanger(0);
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 18)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 19)
                {
                    textColorIndex = 2;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 20)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 21)
                {
                    textColorIndex = 2;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 22)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 23)
                {
                    textColorIndex = 2;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 24)
                {
                    textColorIndex = 0;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 25)
                {
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 26)
                {
                    character1.SpriteChanger(2);
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 27)
                {
                    textColorIndex = 2;
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext == 28)
                {
                    character1.SpriteChanger(0);
                    SetDialogueText(introDialoguePart2L[onNext], textLabel, onNext);
                    onNext++;
                }
                else if (onNext >= 29)
                {
                    metL = true;
                    DialogueComplete();
                }
            }
            else
            {
                #region Lady Dialogue Next
                // memo
                if (objNum == 0)
                {
                    if (onNext == 0)
                    {
                        textColorIndex = 0;
                        character1.SpriteChanger(0);
                        SetDialogueText(memoDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        textColorIndex = 2;
                        character1.SpriteChanger(3);
                        SetDialogueText(memoDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        textColorIndex = 0;
                        SetDialogueText(memoDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        textColorIndex = 2;
                        SetDialogueText(memoDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 4)
                    {
                        character1.SpriteChanger(2);
                        SetDialogueText(memoDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 5)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(memoDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 6)
                    {
                        textColorIndex = 0;
                        SetDialogueText(memoDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 7)
                    {
                        DialogueComplete();
                    }

                }

                // gold coffin
                if (objNum == 1)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(3);
                        SetDialogueText(solidGoldCasketDialogueL[onNext], textLabel, onNext); // plays line number of dialogue equal to onNext
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character1.SpriteChanger(1);
                        SetDialogueText(solidGoldCasketDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 2)
                    {
                        DialogueComplete();
                    }
                }

                //modern coffin
                if (objNum == 2)
                {
                    if (onNext == 0)
                    {
                        textColorIndex = 2;
                        character1.SpriteChanger(3);
                        SetDialogueText(modernCasketDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        textColorIndex = 0;
                        SetDialogueText(modernCasketDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        textColorIndex = 2;
                        SetDialogueText(modernCasketDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        textColorIndex = 0;
                        SetDialogueText(modernCasketDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 4)
                    {
                        textColorIndex = 2;
                        SetDialogueText(modernCasketDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 5)
                    {
                        character1.SpriteChanger(2);
                        SetDialogueText(modernCasketDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 6)
                    {
                        DialogueComplete();
                    }
                }

                //recycled coffin
                if (objNum == 3)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(1);
                        SetDialogueText(recycledCoffinDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 1)
                    {
                        DialogueComplete();
                    }
                }

                //fern boquet
                if (objNum == 4)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(3);
                        SetDialogueText(fernBoquetDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(fernBoquetDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        character1.SpriteChanger(1);
                        SetDialogueText(fernBoquetDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 3)
                    {
                        DialogueComplete();
                    }
                }

                //rose boquet
                if (objNum == 5)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(1);
                        SetDialogueText(roseBoquetDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character1.SpriteChanger(2);
                        SetDialogueText(roseBoquetDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(roseBoquetDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 3)
                    {
                        DialogueComplete();
                    }
                }

                //orchid boquet
                if (objNum == 6)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(orchidBoquetDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 1)
                    {
                        DialogueComplete();
                    }
                }

                //tulip boquet
                if (objNum == 7)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(tulipBoquetDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character1.SpriteChanger(4);
                        SetDialogueText(tulipBoquetDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(tulipBoquetDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 3)
                    {
                        DialogueComplete();
                    }
                }

                // broken filigree crest
                if (objNum == 8)
                {
                    if (onNext == 0)
                    {
                        textColorIndex = 0;
                        character1.SpriteChanger(0);
                        SetDialogueText(brokenFiligreeCrestDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        textColorIndex = 2;
                        SetDialogueText(brokenFiligreeCrestDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        SetDialogueText(brokenFiligreeCrestDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        SetDialogueText(brokenFiligreeCrestDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 5)
                    {
                        character1.SpriteChanger(4);
                        SetDialogueText(brokenFiligreeCrestDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 6)
                    {
                        DialogueComplete();
                    }
                }

                // box of bugs
                if (objNum == 9)
                {
                    if (onNext == 0)
                    {
                        textColorIndex = 0;
                        character1.SpriteChanger(3);
                        SetDialogueText(boxofBugsDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        textColorIndex = 2;
                        SetDialogueText(boxofBugsDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        textColorIndex = 0;
                        SetDialogueText(boxofBugsDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        textColorIndex = 2;
                        character1.SpriteChanger(4);
                        SetDialogueText(boxofBugsDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 4)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(boxofBugsDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 5)
                    {
                        SetDialogueText(boxofBugsDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 6)
                    {
                        DialogueComplete();
                    }
                }

                // woven shawl
                if (objNum == 10)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(wovenShawlDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 1)
                    {
                        DialogueComplete();
                    }
                }

                // half-knit quilt
                if (objNum == 11)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(halfKnitQuiltDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        SetDialogueText(halfKnitQuiltDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        SetDialogueText(halfKnitQuiltDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 3)
                    {
                        DialogueComplete();
                    }
                }

                // filigree keep ledger
                if (objNum == 12)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(filigreeKeepLedgerDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character1.SpriteChanger(4);
                        SetDialogueText(filigreeKeepLedgerDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        character1.SpriteChanger(3);
                        SetDialogueText(filigreeKeepLedgerDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 3)
                    {
                        DialogueComplete();
                    }
                }

                // stripped goblet
                if (objNum == 13)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(strippedGobletDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character1.SpriteChanger(1);
                        SetDialogueText(strippedGobletDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        character1.SpriteChanger(2);
                        SetDialogueText(strippedGobletDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(strippedGobletDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 4)
                    {
                        character1.SpriteChanger(3);
                        SetDialogueText(strippedGobletDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 5)
                    {
                        DialogueComplete();
                    }
                }

                //queen portrait
                if (objNum == 14)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(5);
                        SetDialogueText(portraitofLadyDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(portraitofLadyDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        SetDialogueText(portraitofLadyDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        character1.SpriteChanger(1);
                        SetDialogueText(portraitofLadyDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 4)
                    {
                        character1.SpriteChanger(4);
                        SetDialogueText(portraitofLadyDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 5)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(portraitofLadyDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 6)
                    {
                        DialogueComplete();
                    }
                }

                // portrait of king
                if (objNum == 15)
                {
                    if (onNext == 0)
                    {
                        textColorIndex = 0;
                        character1.SpriteChanger(1);
                        SetDialogueText(portraitofKingDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        textColorIndex = 2;
                        SetDialogueText(portraitofKingDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {
                        SetDialogueText(portraitofKingDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        character1.SpriteChanger(2);
                        SetDialogueText(portraitofKingDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 4)
                    {
                        DialogueComplete();
                    }
                }

                //child portrait
                if (objNum == 16)
                {
                    if (onNext == 0)
                    {
                        textColorIndex = 2;
                        character1.SpriteChanger(4);
                        SetDialogueText(portraitofChildDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        textColorIndex = 0;
                        SetDialogueText(portraitofChildDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 2)
                    {

                        character1.SpriteChanger(4);
                        SetDialogueText(portraitofChildDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 3)
                    {
                        textColorIndex = 2;
                        character1.SpriteChanger(0);
                        SetDialogueText(portraitofChildDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 4)
                    {
                        textColorIndex = 0;
                        SetDialogueText(portraitofChildDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 5)
                    {
                        textColorIndex = 2;
                        SetDialogueText(portraitofChildDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 6)
                    {
                        SetDialogueText(portraitofChildDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 7)
                    {
                        SetDialogueText(portraitofChildDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 8)
                    {
                        character1.SpriteChanger(5);
                        SetDialogueText(portraitofChildDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 9)
                    {
                        SetDialogueText(portraitofChildDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 10)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(portraitofChildDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 11)
                    {
                        SetDialogueText(portraitofChildDialogueL[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 12)
                    {
                        DialogueComplete();
                    }
                }

                // rusty key
                if (objNum == 17)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(rustyKeyDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 1)
                    {
                        DialogueComplete();
                    }
                }

                // quarter's key
                if (objNum == 18)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(quartersKeyDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 1)
                    {
                        DialogueComplete();
                    }
                }

                // suspiciously long wooden plank
                if (objNum == 19)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(2);
                        SetDialogueText(woodenPlankDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 1)
                    {
                        DialogueComplete();
                    }
                }

                // His Majesty
                if (objNum == 20)
                {
                    if (onNext == 0)
                    {
                        character1.SpriteChanger(5);
                        SetDialogueText(hisMajestyDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext == 1)
                    {
                        character1.SpriteChanger(0);
                        SetDialogueText(hisMajestyDialogueG[onNext], textLabel, onNext);
                        onNext++;
                    }
                    else if (onNext >= 2)
                    {
                        DialogueComplete();
                    }
                }
                #endregion
            }
        }

        // NEED TO MAKE THIS DIALOGUE CODE PLAY AT THE FIRST FLOOR DOOR TO THE SERVANT'S QUARTER'S
        else if (metG == false) 
        {
            if (onNext == 0)
            {
                textColorIndex = 0;
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 1)
            {
                textColorIndex = 1;
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 2)
            {
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 3)
            {
                textColorIndex = 0;
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 4)
            {
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 5)
            {
                textColorIndex = 0;
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 6)
            {
                textColorIndex = 1;
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 7)
            {
                textColorIndex = 0;
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 8)
            {
                textColorIndex = 1;
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 9)
            {
                textColorIndex = 0;
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 10)
            {
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 11)
            {
                textColorIndex = 1;
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 12)
            {
                textColorIndex = 0;
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 13)
            {
                textColorIndex = 1;
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 14)
            {
                textColorIndex = 0;
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 15)
            {
                textColorIndex = 1;
                SetDialogueText(introDialoguePart1G[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext >= 16)
            {
                metG = true;
                DialogueComplete();
            }
        }
        // NEED TO MAKE THIS DIALOGUE CODE PLAY AT THE LADY'S DUNGEON DOOR BEFORE YOU GRAB THE PLANK
        else {
            if (onNext == 0)
            {
                textColorIndex = 0;
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 1)
            {
                textColorIndex = 2;
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 2)
            {
                textColorIndex = 0;
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 3)
            {
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 4)
            {
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 5)
            {
                textColorIndex = 2;
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 6)
            {
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 7)
            {
                textColorIndex = 0;
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 8)
            {
                textColorIndex = 2;
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 9)
            {
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 10)
            {
                textColorIndex = 0;
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 11)
            {
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 12)
            {
                textColorIndex = 2;
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 13)
            {
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 14)
            {
                textColorIndex = 0;
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 15)
            {
                textColorIndex = 2;
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 16)
            {
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 17)
            {
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 18)
            {
                textColorIndex = 0;
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext == 19)
            {
                SetDialogueText(introDialoguePart1L[onNext], textLabel, onNext);
                onNext++;
            }
            else if (onNext >= 20)
            {
                DialogueComplete();
            }
        }
    }

    private void DialogueComplete() // currently just resets onNext to zero; can be used to bring the inventory menu back up once dialogue finishes.
    {
        onNext = 0;
        SetDialogueText(allDialogue[60], textLabel, 60); // sets the textbox to blank
    }

    public void CheckForExit()
    {
        wantstoexit = true;
    }
}







