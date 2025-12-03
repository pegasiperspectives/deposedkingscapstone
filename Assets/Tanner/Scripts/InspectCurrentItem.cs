using UnityEngine;

public class InspectCurrentItem : MonoBehaviour
{
    public GameObject currentObservable;
    public InspectManager iM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InspectItem()
    {
        iM.SetCurrentObservable(currentObservable);
        iM.InspectItem();
    }
}
