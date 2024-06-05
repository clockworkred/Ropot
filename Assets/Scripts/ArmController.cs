using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float extendSpeed = 5f;
    public int maxScrapsToGrab = 1;

    public Transform upperLimit;
    public Transform lowerLimit;
    public Transform extendLimit;
    public GameObject hand;

    [SerializeField]private List<GameObject> _grabbedScraps = new List<GameObject>();
    private bool _isExtended = false;
    private bool _isUp = true;
    private bool _isGrabbing = false;
    private Vector3 _initialPosition;

    private ScrapsManager _scrapsManager;

    void Start()
    {
        _scrapsManager = GetComponent<ScrapsManager>();
        _initialPosition = transform.position;
    }

    void Update()
    {
        // Move arm up and down
        MoveArmUpAndDown();
    }

    private void MoveArmUpAndDown()
    {
        float move = moveSpeed * Time.deltaTime;

        if (_isUp && !_isExtended)
        {
            transform.Translate(Vector3.up * move);
            if (transform.position.y >= upperLimit.position.y)
            {
                _isUp = false;
            }
        }
        else if (!_isUp && !_isExtended)
        {
            transform.Translate(Vector3.down * move);
            if (transform.position.y <= lowerLimit.position.y)
            {
                _isUp = true;
            }
        }
    }

    public void ExtendArm()
    {
        if (!_isExtended)
        {
            _initialPosition = transform.position;
            _isExtended = true;
            _isGrabbing = true;
            StopAllCoroutines(); // Stop any running coroutines to avoid conflicts
            StartCoroutine(ExtendAndGrab());
        }
    }

    public void RetractArm()
    {
        if (_isExtended)
        {
            _isExtended = false;
            transform.position = _initialPosition;
            foreach (GameObject part in _grabbedScraps)
            {
                part.transform.SetParent(null);
            }
            _grabbedScraps.Clear();
        }
    }

    private IEnumerator ExtendAndGrab()
    {
        Vector3 targetPosition = new Vector3(extendLimit.position.x, transform.position.y, transform.position.z);
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, extendSpeed * Time.deltaTime);
            yield return null;
        }

        // Return to initial position
        yield return ReturnToInitialPosition();

        _isExtended = false;
        _isGrabbing = false;
    }

    private IEnumerator ReturnToInitialPosition()
    {
        while (Vector3.Distance(transform.position, _initialPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _initialPosition, extendSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void AddGrabbedPart(GameObject part)
    {
        part.transform.SetParent(hand.transform); // Set the grabbed part as a child of the palm
        _grabbedScraps.Add(part);
        _scrapsManager.GrabPart(part); // Inform ScrapsManager about the grabbed part

        if (_grabbedScraps.Count >= maxScrapsToGrab)
        {
            // If the maximum number of parts is reached, return immediately
            StopAllCoroutines();
            StartCoroutine(ReturnToInitialPosition());
            _isExtended = false;
            _isGrabbing = false;
        }
    }

    public void SelectPreviousPart()
    {
        _scrapsManager.SelectPreviousPart();
    }

    public void SelectNextPart()
    {
        _scrapsManager.SelectNextPart();
    }
}
