using UnityEngine;

public class playerController : MonoBehaviour
{
    public GameObject tarrain;
    public float jump_f = 1300f;
    public float speed_f = 0.1f;

    private Rigidbody player_control;
    private float _isJumpAxis;


    private void Start()
    {
        player_control = GetComponent<Rigidbody>();
    }

    void Update()
    {

        MovementLogic();
        _isJumpAxis = Input.GetAxis("Jump");

    }

    void FixedUpdate()
    {

        JumpLogic();

    }

    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        player_control.Move(transform.position + movement * speed_f, transform.rotation);
        
    }

    private void JumpLogic()
    {
        if (_isJumpAxis > 0)
        {
            if (player_control.linearVelocity.y == 0)
            {
                player_control.AddForce(Vector3.up * jump_f);
            }
        }
    }

}