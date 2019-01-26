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
    bool bWantsToSprint = false;

    // In meters
    public float m_Acceleration = 10.0f;
    public float m_Deacceleration = 20.0f;
    public float m_JumpSpeed = 10.0f;
    public float m_MaxSpeedWalk = 2.0f;
    public float m_MaxSpeedSprint = 4.0f;
    public float m_MoveDeadzone = 0.15f;
    // In degrees
    public float m_CameraSpeed = 1000.0f;
    public float m_RollMax = 30.0f;

    Camera GetCamera()
    {
        return m_Camera.GetComponent<Camera>();
    }

    public Vector3 GetHorizontalVelocity()
    {
        Vector3 vel = m_Velocity;
        vel.y = 0.0f;
        return vel;
    }
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

        if (Input.GetButtonDown("Sprint"))
        {
            bWantsToSprint = true;
        }

        if (Input.GetButtonUp("Sprint"))
        {
            bWantsToSprint = false;
        }
    }

    float Pitch = 0.0f;
    float Roll = 0.0f;
    void ConsumeInput()
    {
        if (Mathf.Abs(m_MovementInputVector.x) < m_MoveDeadzone)
            m_Velocity.x = Mathf.MoveTowards(m_Velocity.x, 0, m_Deacceleration * Time.deltaTime);

        if (Mathf.Abs(m_MovementInputVector.z) < m_MoveDeadzone)
            m_Velocity.z = Mathf.MoveTowards(m_Velocity.z, 0, m_Deacceleration * Time.deltaTime);

        m_Velocity += m_MovementInputVector * (Vector3.Dot(m_Velocity, m_MovementInputVector) > 0 ? m_Acceleration : m_Deacceleration) * Time.deltaTime;
        Vector3 horizontalVel = m_Velocity;
        horizontalVel.y = 0.0f;
        horizontalVel = Vector3.ClampMagnitude(horizontalVel, bWantsToSprint ? m_MaxSpeedSprint : m_MaxSpeedWalk);
        m_Velocity.x = horizontalVel.x;
        m_Velocity.z = horizontalVel.z;

        Roll += m_CameraInputVector.x * Time.deltaTime;
        Roll = Mathf.Clamp(Roll, -1.0f, 1.0f);

        transform.Rotate(transform.up, m_CameraInputVector.x * m_CameraSpeed);

        Pitch -= m_CameraInputVector.y * m_CameraSpeed;
        Pitch = Mathf.Clamp(Pitch, -90.0f, 90.0f);
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

    void UpdateLook()
    {
        m_Camera.transform.localRotation = Quaternion.Euler(Pitch, 0.0f, Mathf.Lerp(-m_RollMax, m_RollMax, (Roll + 1.0f) / 2.0f));
        Roll = Mathf.MoveTowards(Roll, 0.0f, 6.0f * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
        ConsumeInput();
        UpdateLook();
        ApplyVelocity();
    }

    public float m_PushForce = 10.0f;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.AddForce(pushDir * m_PushForce);

        JammedDoor door = hit.gameObject.GetComponent<JammedDoor>();
        if (door)
        {
            door.TakeDamage(pushDir.magnitude * m_PushForce);
        }
    }
}
