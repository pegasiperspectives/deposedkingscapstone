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
    public Transform itemButtonContainer;  
    public GameObject itemButtonPrefab;
    private FPSController fpscontrollerScript;
    private Characters characters;
    public GameObject player;

    public GameObject lady;
    public PlaceObjects placeObjects;
    [SerializeField] private GameObject inventory;
    //added


    [SerializeField] public TMP_Text textLabel;
    [SerializeField] private float typeSpeed = 50;
    [SerializeField] public GameObject self;

    [SerializeField] public GameObject dialogueOption1;


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

    public string[] showObjects = {
        "Show object 1 in inventory",
        "Show Object 2 in inventory",
        "Show object 3 in inventory"
    };


    // Start is called before the first frame update
    void Start()
    {
        self.SetActive(false);
        Debug.Log("dialogue box is not active yet");

        //Tanner Addition
        fpscontrollerScript = player.GetComponent<FPSController>();
        //added

        characters = lady.GetComponent<Characters>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && characters.isAtLady != true) {
            Debug.Log("you're clicking I but it's not registering you're at the lady");
        }

        if (self.activeInHierarchy == false && inventory.activeInHierarchy == false && characters.isAtLady == true) //added self chech so multiple objects arent made
        {
            self.SetActive(true);
            SetDialogueText(allDialogue[0], textLabel);
            Debug.Log("triggered dialogue box");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //tanner addition
            ShowInventoryItemButtons();
            placeObjects.canPlace = false;
            fpscontrollerScript.canMove = false;
            //added

        }
        else if (Input.GetKeyDown(KeyCode.X) && self.activeInHierarchy)
        {
            closeDialogue();
            Debug.Log("exited dialogue box");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            characters.isAtLady = false;

            //Tanner Addition
            fpscontrollerScript.canMove = true;
            //added
        }
        
    }

    public void SetDialogueText(string textToType, TMP_Text textLabel)
    {
        StartCoroutine(routine: TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);

            yield return null;
        }

        textLabel.text = textToType;
    }

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
    public void ShowInventoryItemButtons()
    {
       
        foreach (var item in InventoryManager.Instance.Items) // make each button
        {
            GameObject button = Instantiate(itemButtonPrefab, itemButtonContainer);
            var label = button.GetComponentInChildren<TMP_Text>();
            var icon = button.transform.Find("Icon").GetComponent<Image>();
            label.text = "Show " + item.itemName;
            icon.sprite = item.icon;

            button.GetComponent<Button>().onClick.AddListener(() => OnItemShown(item));
        }
    }

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

    private void ClearItemButtons()// dont want repeated objects in inventory so they are deleted when closed
    {
        foreach (Transform child in itemButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }
    //added


}







