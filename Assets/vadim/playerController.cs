using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody player_control;
    private float isJumpAxis;
    private bool isShiftHolding;

    public float jump_f;
    RaycastHit hit;
    public float speed_f;
    public float run_speed_multiplier;
    Vector3 viewDirection;
    Vector3 moveDirection;


    private float isMouseScrollingAxis;

    public GameObject view;
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
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), playerRotationSmoothing*Time.fixedDeltaTime);
        }
    }

    private void JumpLogic()
    {
        if (isJumpAxis > 0)
        {
            Debug.Log(hit.distance);
            Debug.Log(hit.point.y);
            if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            {
                if (hit.distance < 1.07f)
                    player_control.AddForce(Vector3.up * jump_f, ForceMode.Impulse);
            }
        }
    }

    private void RotateView()
    {
        radiusFromPlayer += isMouseScrollingAxis * mouseScrollingSpeed;
        if (radiusFromPlayer < 1.5f)
            radiusFromPlayer = 1.5f;

        if (radiusFromPlayer > 50f)
            radiusFromPlayer = 50f;


        transform.TransformDirection(viewDirection);
        view.transform.position = transform.position + viewDirection * radiusFromPlayer;


        view.transform.RotateAround(transform.position, view.transform.right, -Input.GetAxis("Mouse Y") * sensitivity);
        view.transform.RotateAround(transform.position, view.transform.up, Input.GetAxis("Mouse X") * sensitivity);


        viewDirection = (view.transform.position - transform.position).normalized;

        view.transform.LookAt(transform.position);
    }

}