using UnityEngine;

public class isGrounded : MonoBehaviour
{
    private void OnTriggerStay()
    {
        transform.parent.gameObject.GetComponent<playerController>().isGrounded = true;
    }

    private void OnTriggerExit()
    {
        transform.parent.gameObject.GetComponent<playerController>().isGrounded = false;
    }
}
