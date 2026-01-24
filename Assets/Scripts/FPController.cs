using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPController : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float maxSpeed = 3.5f;
    public float acceleration = 15f;

    public Vector3 CurrentVelocity {  get; private set; }
    public float  CurrentSpeed { get; private set; }

    [Header("Looking Parameters")]
    public Vector2 lookSensitivity = new Vector2(0.1f, 0.1f);
    public float pitchLimit = 85f;
    [SerializeField] float currentPitch = 0f;
    public bool canLook = true;
    public bool canMove = true;

    public float CurrentPitch
    {
        get => currentPitch;

        set
        {
            currentPitch = Mathf.Clamp(value, -pitchLimit, pitchLimit);
        }
    }

    [Header("Input")]
    public Vector2 moveInput;
    public Vector2 lookInput;

    [Header("Components")]
    [SerializeField] public CharacterController characterController;
    [SerializeField] public Camera fpCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();

        LookUpdate();
    }

    private void MoveUpdate()
    {
        if (canMove == false) return;

        Vector3 motion = transform.forward * moveInput.y + transform.right * moveInput.x;
        motion.y = 0;
        motion.Normalize();

        if (motion.sqrMagnitude >= 0.01f)
        {
            CurrentVelocity = Vector3.MoveTowards(CurrentVelocity, motion * maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            CurrentVelocity = Vector3.MoveTowards(CurrentVelocity, Vector3.zero, acceleration * Time.deltaTime);
        }
        
        float verticalVelocity = Physics.gravity.y * 20f * Time.deltaTime;

        Vector3 fullVelocity = new Vector3(CurrentVelocity.x, verticalVelocity, CurrentVelocity.z);

        characterController.Move(fullVelocity * Time.deltaTime);

        //updating speed
        CurrentSpeed = CurrentVelocity.magnitude;
    }

    private void LookUpdate()
    {
        if (canLook == false) return;
        
        //looking up and down
        Vector2 input = new Vector2(lookInput.x * lookSensitivity.x, lookInput.y * lookSensitivity.y);

        CurrentPitch -= input.y;

        fpCamera.transform.localRotation = Quaternion.Euler(CurrentPitch, 0f, 0f);

        //looking left and right
        transform.Rotate(Vector3.up * input.x);
    }

    private void OnValidate()
    {
        if (characterController == null) characterController = GetComponent<CharacterController>();
    }

}
