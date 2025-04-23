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
    public GameObject player;
    public PlaceObjects placeObjects;
    [SerializeField] private GameObject inventory;
    //added


    [SerializeField] public TMP_Text textLabel;
    [SerializeField] private float typeSpeed = 50;
    [SerializeField] public GameObject self;

    [SerializeField] public GameObject dialogueOption1;


    public string[] allDialogue = {
            "Who's there?!",
            "My grandma gave that to me. Thanks for finding it!",
            "I want that away from me right this second. I never want to see it again.",
            "I have no idea what that is.",
            "You need to get out. Right now.",
            "You're in grave danger! That object is cursed!",
            "Oh! That belongs to ____"};

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && self.activeInHierarchy == false && inventory.activeInHierarchy == false) //added self chech so multiple objects arent made
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
        else if (Input.GetKeyDown(KeyCode.X))
        {
            closeDialogue();
            Debug.Log("exited dialogue box");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

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
            label.text = "Show " + item.itemName;

            button.GetComponent<Button>().onClick.AddListener(() => OnItemShown(item));
        }
    }

    private void OnItemShown(Item item)
    {
        // Call the dialogue options here for the name of the object
        if (item.itemName.Contains("Example1"))
            SetDialogueText(allDialogue[1], textLabel);

        else if (item.itemName.Contains("Example2"))
            SetDialogueText(allDialogue[5], textLabel);

        else
            SetDialogueText(allDialogue[3], textLabel);
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







