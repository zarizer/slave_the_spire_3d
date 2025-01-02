using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody player_control;
    private float isJumpAxis;
    private bool isShiftHolding;

    public GameObject player;
    public float jump_f;
    public float speed_f;
    public float run_speed_multiplier;
    Vector3 viewDirection;
    Vector3 moveDirection;


    public GameObject view;
    public float radiusFromPlayer;
    public float sensitivity;


    private void Start()
    {
        player_control = player.GetComponent<Rigidbody>();

        viewDirection = (view.transform.position - player.transform.position).normalized;

    }

    void Update()
    {

        isJumpAxis = Input.GetAxis("Jump");
        isShiftHolding = Input.GetKey(KeyCode.LeftShift);

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
        Debug.Log(moveDirection);
        Vector3 movement = moveDirection * moveVertical + -Vector3.Cross(moveDirection, new Vector3(0.0f, 1.0f, 0.0f)) * moveHorizontal;
        movement = Vector3.ClampMagnitude(movement, speed_f);

        if (isShiftHolding)
        {
            player_control.MovePosition(player.transform.position + movement * speed_f * run_speed_multiplier * Time.fixedDeltaTime);
        }
        else
        {
            player_control.MovePosition(player.transform.position + movement * speed_f * Time.fixedDeltaTime);
        }

    }

    private void JumpLogic()
    {
        if (isJumpAxis > 0)
        {
            if (player_control.linearVelocity.y == 0)
            {
                player_control.AddForce(Vector3.up * jump_f, ForceMode.Impulse);
            }
        }
    }

    private void RotateView()
    {
        
        view.transform.position = player.transform.position + viewDirection * radiusFromPlayer;
        

        view.transform.RotateAround(player.transform.position, view.transform.right, -Input.GetAxis("Mouse Y") * sensitivity);
        view.transform.RotateAround(player.transform.position, view.transform.up, Input.GetAxis("Mouse X") * sensitivity);


        viewDirection = (view.transform.position - player.transform.position).normalized;

        view.transform.LookAt(player.transform.position);
    }

}