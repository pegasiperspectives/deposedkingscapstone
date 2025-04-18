using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionPointSystem : MonoBehaviour
{
    public int points = 0;
    public int pointsneg = 0;
    public int pointstotal = 0;


    //bool m_Started;
    public LayerMask m_LayerMask;
    public LayerMask m_LayerMaskNeg;
    public GameObject wincanvas;
    void Start()
    {
        //Use this to ensure that the Gizmos are being drawn when in Play Mode.
        //m_Started = true;
    }

    void FixedUpdate()
    {
        MyCollisions();
        pointstotal = points - pointsneg;
        //print(pointstotal);
        if(pointstotal == 3)
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
            //Output all of the collider names
            //Debug.Log("Hit : " + hitColliders[i].name + i);
            //Increase the number of Colliders in the array
            i++;
            
        }
        //print(i);
        points = i; //number of points current

        Collider[] hitCollidersneg = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMaskNeg);
        int ia = 0;
        //Check when there is a new collider coming into contact with the box
        while (ia < hitCollidersneg.Length)
        {
            //Output all of the collider names
            //Debug.Log("Hit : " + hitColliders[i].name + i);
            //Increase the number of Colliders in the array
            ia++;

        }
        //print(i);
        pointsneg = ia; //number of points current
    }

    
}
