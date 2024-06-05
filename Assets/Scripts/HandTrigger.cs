using UnityEngine;

public class HandTrigger : MonoBehaviour
{
    [SerializeField]private ArmController _armController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _armController = GetComponentInParent<ArmController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scrapes"))
        {
            Debug.Log("Part grabbed: " + other.name);
            _armController.AddGrabbedPart(other.gameObject);
        }
    }
}
