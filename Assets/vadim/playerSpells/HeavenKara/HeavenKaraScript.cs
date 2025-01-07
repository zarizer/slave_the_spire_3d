using System.Collections.Generic;
using UnityEngine;

public class HeavenKaraScript : MonoBehaviour
{
    public GameObject SkyPartPrefab;
    public GameObject Kara;
    public float radius;
    public float height;
    public float particlesSpeed;
    public float smoothnessDarkGlobalLight;

    private float startTime;
    private List<GameObject> skyParts = new List<GameObject>();
    private GameObject globalLight;
    private bool isKaraInstatiated = false;
    Vector3 InsDir;
    Vector3 EndPos;
    Vector3 StartHeavenKaraPos;
    Quaternion prevGlobalLightRotation;


    void Awake()
    {
        startTime = Time.time;

        globalLight = GameObject.Find("Global Light");
        prevGlobalLightRotation = globalLight.transform.rotation;

        StartHeavenKaraPos = transform.position;

        EndPos = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        InsDir = Vector3.right * radius;

        for (int i = 0; i < 12; i++)
        {
            GameObject particle = Instantiate(SkyPartPrefab, transform.position + InsDir, transform.rotation, transform);
            skyParts.Add(particle);

            InsDir = Quaternion.AngleAxis(30, Vector3.up) * InsDir;
        }
        
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, EndPos, 0.5f * Time.deltaTime);

        foreach(GameObject part in skyParts)
        {
            part.transform.RotateAround(transform.position, transform.up, particlesSpeed * Time.deltaTime);
        }

        globalLight.transform.rotation = Quaternion.Slerp(globalLight.transform.rotation, Quaternion.LookRotation(Vector3.up), smoothnessDarkGlobalLight * Time.deltaTime);

        if (Time.time - startTime > 5 && !isKaraInstatiated)
        {
            isKaraInstatiated = true;
            GameObject kara = Instantiate(Kara, transform.position, Kara.transform.rotation, transform);

        }
            

        if (Time.time - startTime > 8)
        {
            globalLight.transform.rotation = prevGlobalLightRotation;
            Destroy(transform.gameObject);
        }
    }
}
