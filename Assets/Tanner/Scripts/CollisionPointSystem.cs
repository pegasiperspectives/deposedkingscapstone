using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionPointSystem : MonoBehaviour
{
    public int points = 0;
    public int pointsTwo = 0;
    public int pointsThree = 0;
    public int pointsneg = 0;
    public int pointsnegTwo = 0;
    public int pointsnegThree = 0;
    public int pointstotal = 0;


    //bool m_Started;
    public LayerMask m_LayerMask;
    public LayerMask m_LayerMaskTwo;
    public LayerMask m_LayerMaskThree;
    public LayerMask m_LayerMaskNeg;
    public LayerMask m_LayerMaskNegTwo;
    public LayerMask m_LayerMaskNegThree;
    public GameObject wincanvas;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        MyCollisions();
        pointstotal = points + pointsTwo - pointsneg - pointsnegTwo;
        //print(pointstotal);
        if(pointstotal == 9)
        {
            wincanvas.SetActive(true);
        }
        else
        {
            wincanvas.SetActive(false);
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
        while (iq < hitColliders.Length)
        {

            //Increase the number of Colliders in the array
            iq++;

        }
        //print(i);
        pointsTwo = iq * 2; //number of points current


        Collider[] hitCollidersThree = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMaskThree);
        int iw = 0;
        //Check when there is a new collider coming into contact with the box
        while (iw < hitColliders.Length)
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
        while (ie < hitCollidersneg.Length)
        {

            //Increase the number of Colliders in the array
            ie++;

        }
        //print(i);
        pointsnegTwo = ie * 2; //number of points current

        Collider[] hitCollidersnegThree = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMaskNegThree);
        int ir = 0;
        //Check when there is a new collider coming into contact with the box
        while (ir < hitCollidersneg.Length)
        {

            //Increase the number of Colliders in the array
            ir++;

        }
        //print(i);
        pointsnegThree = ir * 3; //number of points current
    }

    
}
