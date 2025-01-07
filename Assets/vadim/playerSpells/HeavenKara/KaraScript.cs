using UnityEngine;
using UnityEngine.UIElements;

public class KaraScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Нанесено 60 урона!");
        }
    }
}
