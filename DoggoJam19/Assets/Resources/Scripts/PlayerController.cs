using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Assertions;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    CharacterController m_CharacterController = null;
    public GameObject m_Camera = null;

    Vector3 m_MovementInputVector;
    Vector3 m_CameraInputVector;

    Vector3 m_Velocity;

    bool bWantsToJump = false;

    // In meters
    public float m_Acceleration = 10.0f;
    public float m_Deacceleration = 20.0f;
    public float m_JumpSpeed = 10.0f;
    public float m_MaxSpeed = 4.0f;
    public float m_MoveDeadzone = 0.15f;
    // In degrees
    public float m_CameraSpeed = 1000.0f;

    public Vector3 GetVelocity()
    {
        return m_Velocity;
    }

    private void Awake()
    {
        Assert.IsNotNull(m_Camera);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_CharacterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void GatherInput()
    {
        m_MovementInputVector = Vector3.zero;
        m_MovementInputVector.x = Input.GetAxis("Horizontal");
        m_MovementInputVector.z = Input.GetAxis("Vertical");

        m_CameraInputVector = Vector3.zero;
        m_CameraInputVector.x = Input.GetAxis("Mouse X");
        m_CameraInputVector.y = Input.GetAxis("Mouse Y");

        if (Input.GetButtonDown("Jump"))
        {
            bWantsToJump = true;
        }
    }

    void ConsumeInput()
    {
        if (Mathf.Abs(m_MovementInputVector.x) < m_MoveDeadzone)
            m_Velocity.x = Mathf.MoveTowards(m_Velocity.x, 0, m_Deacceleration * Time.deltaTime);

        if (Mathf.Abs(m_MovementInputVector.z) < m_MoveDeadzone)
            m_Velocity.z = Mathf.MoveTowards(m_Velocity.z, 0, m_Deacceleration * Time.deltaTime);

        m_Velocity += m_MovementInputVector * (Vector3.Dot(m_Velocity, m_MovementInputVector) > 0 ? m_Acceleration : m_Deacceleration) * Time.deltaTime;
        Vector3 horizontalVel = m_Velocity;
        horizontalVel.y = 0.0f;
        horizontalVel = Vector3.ClampMagnitude(horizontalVel, m_MaxSpeed);
        m_Velocity.x = horizontalVel.x;
        m_Velocity.z = horizontalVel.z;

        transform.Rotate(transform.up, m_CameraInputVector.x * m_CameraSpeed);
        Quaternion targetRotation = m_Camera.transform.localRotation * Quaternion.AngleAxis(-m_CameraInputVector.y * m_CameraSpeed, Vector3.right);
        if (Quaternion.Angle(Quaternion.identity, targetRotation) < 90)
            m_Camera.transform.localRotation = targetRotation;
    }

    Vector3 cachedVelocity;
    void ApplyVelocity()
    {
        Vector3 move = Vector3.zero;

        if (m_CharacterController.isGrounded)
        {
            if (bWantsToJump && m_CharacterController.isGrounded)
            {
                m_Velocity.y = m_JumpSpeed;
                bWantsToJump = false;
            }
            else
            {
                m_Velocity.y = Physics.gravity.y * Time.deltaTime;
            }
            move = transform.TransformVector(m_Velocity);
            cachedVelocity = move;
        }
        else
        {
            bWantsToJump = false;
            cachedVelocity.y += Physics.gravity.y * Time.deltaTime;
            move = cachedVelocity;
        }


        CollisionFlags flags = m_CharacterController.Move(move * Time.deltaTime);
        m_Velocity = transform.InverseTransformVector(m_CharacterController.velocity);

    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
        ConsumeInput();
        ApplyVelocity();
    }
}
