using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionPointSystem : MonoBehaviour
{
    // Individual point counters for each layer type
    public int points = 0;
    public int pointsTwo = 0;
    public int pointsThree = 0;
    public int pointsneg = 0;
    public int pointsnegTwo = 0;
    public int pointsnegThree = 0;
    

    public int pointstotal = 0; // Combined total


    // Layer masks to detect different types of objects
    public LayerMask m_LayerMask;
    public LayerMask m_LayerMaskTwo;
    public LayerMask m_LayerMaskThree;
    public LayerMask m_LayerMaskNeg;
    public LayerMask m_LayerMaskNegTwo;
    public LayerMask m_LayerMaskNegThree;


    public GameObject wincanvas;    // Win screen canvas

    public GameObject losecanvas;


    // Reference to player movement control
    private FPSController fpscontrollerScript;
    public GameObject player;

    //newwwwwwwwwwwwwwwwwwwwww
    public LayerMask playerMask;          //Player layer
    public Vector3 halfExtents = new Vector3(0.5f, 1f, 0.5f);
    public int requiredPoints = 9;        // threshold to win
    private bool playerInZone = false;


    [SerializeField] private Keyinv _keyInventory = null;


    public GameObject interactPrompt;  //UI NEEDED
    public BoxCollider interactBox;
    //newwwwwwwwwwwwwwwwwwwwwwwww


    void Start()
    {
        // Get FPSController from player
        fpscontrollerScript = player.GetComponent<FPSController>();
    }




    void FixedUpdate()
    {

        MyCollisions(); // Run collision checks each physics update
        pointstotal = points + pointsTwo + pointsThree - pointsneg - pointsnegTwo - pointsnegThree; // Calculate total points (positive layers minus negative layers)
        //print(pointstotal);

        


    //// Check for win condition
    //if (pointstotal >= 9)
    //{
    //    // Show win canvas and freeze player movement
    //    wincanvas.SetActive(true);
    //    Cursor.lockState = CursorLockMode.None;
    //    Cursor.visible = true;
    //    fpscontrollerScript.canMove = false;
    //}
    //else
    //{
    //    wincanvas.SetActive(false); // Hide win canvas if not enough points
    //}
}




    //neeeeewwwwwwwwwww
    void Update()
    {
        //neeeeeeewwwwwwwwwwwwwwwwwwwwwww
        if (interactBox != null)
        {
            // Use the other box's center/size/rotation
            playerInZone = Physics.CheckBox(
            interactBox.transform.TransformPoint(interactBox.center),
            Vector3.Scale(interactBox.size * 0.5f, interactBox.transform.lossyScale),
            interactBox.transform.rotation,
            playerMask
            );

            Debug.DrawLine(interactBox.transform.position, interactBox.transform.position + Vector3.up * 2f, Color.green, 0.1f);
            if (playerInZone) Debug.Log("Player is in interaction zone!");

        }
        else
        {
            playerInZone = false; // safety fallback
        }
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(playerInZone);
        }
        //nwwweeeeeeeeeeewwwww
        // Interact
        if (playerInZone && Input.GetKeyDown(KeyCode.E) && _keyInventory.hasKing)
        {
            if (pointstotal >= requiredPoints)
            {
                wincanvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                fpscontrollerScript.canMove = false;
            }
            else
            {
                losecanvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                fpscontrollerScript.canMove = false;
            }
        }

    }


        void MyCollisions()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            
            //Increase the number of Colliders in the array
            i++;
            
        }
        //print(i);
        points = i; //number of points current

        Collider[] hitCollidersTwo = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMaskTwo);
        int iq = 0;
        //Check when there is a new collider coming into contact with the box
        while (iq < hitCollidersTwo.Length)
        {

            //Increase the number of Colliders in the array
            iq++;

        }
        //print(i);
        pointsTwo = iq * 2; //number of points current


        Collider[] hitCollidersThree = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMaskThree);
        int iw = 0;
        //Check when there is a new collider coming into contact with the box
        while (iw < hitCollidersThree.Length)
        {

            //Increase the number of Colliders in the array
            iw++;

        }
        //print(i);
        pointsThree = iw * 3; //number of points current





        Collider[] hitCollidersneg = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMaskNeg);
        int ia = 0;
        //Check when there is a new collider coming into contact with the box
        while (ia < hitCollidersneg.Length)
        {
            
            //Increase the number of Colliders in the array
            ia++;

        }
        //print(i);
        pointsneg = ia; //number of points current


        Collider[] hitCollidersnegTwo = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMaskNegTwo);
        int ie = 0;
        //Check when there is a new collider coming into contact with the box
        while (ie < hitCollidersnegTwo.Length)
        {

            //Increase the number of Colliders in the array
            ie++;

        }
        //print(i);
        pointsnegTwo = ie * 2; //number of points current

        Collider[] hitCollidersnegThree = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMaskNegThree);
        int ir = 0;
        //Check when there is a new collider coming into contact with the box
        while (ir < hitCollidersnegThree.Length)
        {

            //Increase the number of Colliders in the array
            ir++;

        }
        //print(i);
        pointsnegThree = ir * 3; //number of points current
    }

    
}
