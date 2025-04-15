using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{

    [SerializeField] public TMP_Text textLabel;
    [SerializeField] private float typeSpeed = 50;
    [SerializeField] public GameObject self;


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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            self.SetActive(true);
            SetDialogueText(allDialogue[0], textLabel);
            Debug.Log("triggered dialogue box");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            closeDialogue();
            Debug.Log("exited dialogue box");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
    }

    public void Option1() {
        SetDialogueText(allDialogue[1], textLabel);
    }

    public void Option2() {
        SetDialogueText(allDialogue[2], textLabel);
    }

    public void Option3() {
        SetDialogueText(allDialogue[3], textLabel);
    }
}