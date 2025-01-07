using UnityEngine;
using UnityEngine.UIElements;

public class KaraScript : MonoBehaviour
{
    private ParticleSystem ps;
    private float length;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        length = transform.parent.GetComponent<HeavenKaraScript>().height;
        main.startLifetime = length / 300f;
        main.startSize = transform.parent.GetComponent<HeavenKaraScript>().radius*2;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Нанесено 60 урона!");
        }
    }
}
