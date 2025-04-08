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
            "My grandma gave that to me. Thanks for finding it!",
            "I want that away from me right this second. I never want to see it again.",
            "I have no idea what that is.",
            "You need to get out. Right now.",
            "You're in grave danger! That object is cursed!",
            "Oh! That belongs to ____"};



    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse click
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) // Perform raycast
                {
                    Debug.Log("Clicked: " + hit.collider.gameObject.name); // Log the clicked object's name
                    SetDialogueText(allDialogue[0], textLabel);
                }
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            closeDialogue();
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
}