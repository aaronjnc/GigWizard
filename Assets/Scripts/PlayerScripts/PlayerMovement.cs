using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : Singleton<PlayerMovement>
{
    private Rigidbody rb;
    private PlayerControls controls;
    private CharacterAnimator movementAnimator;
    private Camera cam;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float cameraSpeed;
    [SerializeField]
    private float cameraMinDistance;
    private Vector2 moveDir;

    // Start is called before the first frame update
    protected override void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        movementAnimator = GetComponentInChildren<CharacterAnimator>();
    }

    public void SetupControls(PlayerControls newControls)
    {
        controls = newControls;
        controls.Movement.Move.performed += Move;
        controls.Movement.Move.canceled += StopMove;
        controls.Movement.Move.Enable();
    }

    void Move(CallbackContext ctx) {
        moveDir = ctx.ReadValue<Vector2>();
        if (moveDir.x > 0)
        {
            movementAnimator.Move(DirectionEnum.Right);
        }
        else if (moveDir.x < 0)
        {
             movementAnimator.Move(DirectionEnum.Left);
        }
        else if (moveDir.y > 0)
        {
            movementAnimator.Move(DirectionEnum.Up);
        }
        else
        {
            movementAnimator.Move(DirectionEnum.Down);
        }
        rb.velocity = new Vector3(moveDir.x, 0, moveDir.y) * playerSpeed;
    }

    void StopMove(CallbackContext ctx) { 
        movementAnimator.StopMove();
        moveDir = Vector2.zero;
        rb.velocity = moveDir * playerSpeed;
    }

    private void FixedUpdate()
    {
        if (cam)
        {
            Vector3 goalLoc = new Vector3(transform.position.x, cam.transform.position.y, transform.position.z);
            Vector3 camDir = goalLoc - cam.transform.position;
            if (camDir.magnitude < cameraMinDistance)
            {
                cam.transform.position = goalLoc;
            }
            else
            {
                camDir.Normalize();
                cam.transform.position += camDir * cameraSpeed * Time.deltaTime;
            }
        }
    }
}
