using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerID;

    private PlayerInput _input;

    private ArmController _armController;
    // Start is called before the first frame update
    void Awake()
    {
        _input = new PlayerInput();

        if (playerID == 1)
        {
            _input.Player1.Grab.performed += ctx => Grab();
            _input.Player1.ChooseScraps.performed += ctx => ChooseScraps(ctx.ReadValue<Vector2>());
        }
        else if (playerID == 2)
        {
            _input.Player2.Grab.performed += ctx => Grab();
            _input.Player2.ChooseScraps.performed += ctx => ChooseScraps(ctx.ReadValue<Vector2>());
        }

        _armController = GetComponent<ArmController>();
    }

    void OnEnable()
    {
        if (playerID == 1)
        {
            _input.Player1.Enable();
        }
        else if (playerID == 2)
        {
            _input.Player2.Enable();
        }
    }

    void OnDisable()
    {
        if (playerID == 1)
        {
            _input.Player1.Disable();
        }
        else if (playerID == 2)
        {
            _input.Player2.Disable();
        }
    }

    void Grab()
    {
        _armController.ExtendArm();
    }

    void ChooseScraps(Vector2 direction)
    {
        if (direction.x < 0)
        {
            _armController.SelectPreviousPart();
        }
        else if (direction.x > 0)
        {
            _armController.SelectNextPart();
        }
    }
}
