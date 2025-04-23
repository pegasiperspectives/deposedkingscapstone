using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsCamera : MonoBehaviour
{

    [HideInInspector]
    public Transform model;
    public Transform rig;
    public float sensitivity = 3f;
    Quaternion modelRot;
    Quaternion rigRot;
    public float modelScaleFactor = 5.5f; // Factor to scale the model
    private Vector3 originalScale; // To store the original scale of the model

    [SerializeField] public GameObject self;

    void Update()
    {
        if (Input.GetMouseButton(0) && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y")  != 0)) {
            if (model == null) {
                return;
            }

            modelRot = model.rotation;
            rigRot = rig.rotation;
            ObjectRotation();
        } 
    }

    public void ObjectRotation() {
        float yRot = Input.GetAxis("Mouse X") * sensitivity;
        float xRot = Input.GetAxis("Mouse Y") * sensitivity;

        modelRot *= Quaternion.Euler(0f, -yRot, 0f);
        rigRot *= Quaternion.Euler(xRot, 0f, 0f);

        model.rotation = modelRot;
        rig.rotation = rigRot;
    }

     public void Close()
    {
        model.localScale = originalScale; // Reset to original scale
        Destroy(model.gameObject);
        rig.rotation = Quaternion.identity;
        gameObject.SetActive(false);
        self.SetActive(false);
    }


    public void Start()
    {
        
    }


}
