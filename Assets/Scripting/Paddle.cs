using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerType
{
    Player1,
    Player2
}

public class Paddle : MonoBehaviour
{
    public PlayerType CurrentPlayer;
    public float Speed = 10f;
    public float Boundary = 4f;

    [SerializeField] private InputAction movementInput;
    private float movementDirection;

    void Awake()
    {
        movementInput.performed += OnMovePerformed;
        movementInput.canceled += OnMoveCanceled;
    }

    void OnEnable()
    {
        movementInput.Enable();
    }
    
    void OnDisable()
    {
        movementInput.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<float> ();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        movementDirection = 0;
    }

    void Update()
    {
        float movement = movementDirection * Speed * Time.deltaTime;
        transform.Translate(0f, movement, 0f);

        float clampedY = Mathf.Clamp(transform.position.y, -Boundary, Boundary);
        transform.position = new Vector3(transform.position.x, clampedY, 0f);
    }
}
