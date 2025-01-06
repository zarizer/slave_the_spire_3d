using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class playerController : MonoBehaviour
{
    private Rigidbody player_control;
    private float isJumpAxis;
    private bool isShiftHolding;

    public float jump_f;
    public float speed_f;
    public float run_speed_multiplier;
    public bool isGrounded;
    Vector3 moveDirection;


    private float isMouseScrollingAxis;

    public GameObject view;
    Vector3 viewDirection;
    RaycastHit viewHit;
    public float radiusFromPlayer;
    public float sensitivity;
    public float playerRotationSmoothing;
    public float mouseScrollingSpeed;


    private void Start()
    {
        player_control = GetComponent<Rigidbody>();

        viewDirection = (view.transform.position - transform.position).normalized;

    }

    void Update()
    {

        isJumpAxis = Input.GetAxis("Jump");
        isShiftHolding = Input.GetKey(KeyCode.LeftShift);
        isMouseScrollingAxis = Input.GetAxis("Mouse ScrollWheel");

        RotateView();

    }

    void FixedUpdate()
    {
        MovementLogic();
        JumpLogic();

    }

    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(-viewDirection.x, 0.0f, -viewDirection.z).normalized;
        Vector3 movement = moveDirection * moveVertical + -Vector3.Cross(moveDirection, new Vector3(0.0f, 1.0f, 0.0f)) * moveHorizontal;
        movement = Vector3.ClampMagnitude(movement * speed_f, speed_f);

        if (isShiftHolding)
        {
            player_control.MovePosition(transform.position + movement * run_speed_multiplier * Time.fixedDeltaTime);
        }
        else
        {
            player_control.MovePosition(transform.position + movement * Time.fixedDeltaTime);
        }

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), playerRotationSmoothing * Time.fixedDeltaTime);
        }
    }

    private void JumpLogic()
    {
        if (isJumpAxis > 0)
        {
            if (isGrounded)
            {
                player_control.AddForce(Vector3.up * jump_f, ForceMode.Impulse);
            }
        }

        if (!isGrounded)
        {
            if (player_control.linearVelocity.y < 0f)
            {
                player_control.AddForce(-Vector3.up * 0.2f, ForceMode.VelocityChange);
            }
        }
    }

    private void RotateView()
    {
        radiusFromPlayer += isMouseScrollingAxis * mouseScrollingSpeed;
        if (radiusFromPlayer < 1.5f)
            radiusFromPlayer = 1.5f;

        if (radiusFromPlayer > 30f)
            radiusFromPlayer = 30f;

        if (Physics.Raycast(transform.position, viewDirection, out viewHit, radiusFromPlayer))
        {
            Vector3 newPoint = viewHit.point - viewDirection;
            view.transform.position = newPoint.normalized == viewDirection ? newPoint : viewHit.point;
        }
        else
        {
            view.transform.position = transform.position + viewDirection * radiusFromPlayer;
        }


        ViewRotateAround(transform.position, view.transform.right, -Input.GetAxis("Mouse Y") * sensitivity, "y");
        ViewRotateAround(transform.position, view.transform.up, Input.GetAxis("Mouse X") * sensitivity, "x");

        viewDirection = (view.transform.position - transform.position).normalized;

        view.transform.LookAt(transform.position);
    }

    void ViewRotateAround(Vector3 point, Vector3 axis, float angle, string mouseAxis)
    {
        Vector3 vector = view.transform.position;
        Quaternion quaternion = Quaternion.AngleAxis(angle, axis);
        Vector3 vector2 = vector - point;
        float dist = vector2.magnitude;
        Vector3 vector3 = quaternion * vector2;

        vector3 = vector2 + Vector3.ClampMagnitude(vector3 - vector2, sensitivity);
        vector3 = vector3.normalized * dist;

        if (Vector3.Angle(vector3, Vector3.up) > 15 && Vector3.Angle(vector3, Vector3.up) < 175)
        {
            vector = point + vector3;
            view.transform.position = vector;
        }
        
    }

}