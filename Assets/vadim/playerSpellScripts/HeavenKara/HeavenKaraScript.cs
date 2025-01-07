using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeavenKaraScript : MonoBehaviour
{
    public GameObject SkyPartPrefab;
    private List<GameObject> skyParts = new List<GameObject>();
    private float startTime;
    Vector3 EndSkyPartPos;
    GameObject empty;



    void Awake()
    {
        startTime = Time.time;
        EndSkyPartPos = new Vector3(transform.position.x, transform.position.y + 30f, transform.position.z);

        empty = Instantiate(new GameObject("EmptyHeaven"), transform.position, transform.rotation, transform);

        Vector3 InsSkyPartDir = Vector3.right * 30f;
        for (int i = 0; i < 8; i++)
        {
            GameObject capsule = Instantiate(SkyPartPrefab, transform.position + InsSkyPartDir, Quaternion.Euler(90, 0, 0), empty.transform);
            skyParts.Add(capsule);

            InsSkyPartDir = Quaternion.AngleAxis(45, Vector3.up) * InsSkyPartDir;
        }
    }

    void Update()
    {
        empty.transform.Rotate(0, 45 * Time.deltaTime, 0);
        empty.transform.position = Vector3.Lerp(empty.transform.position, EndSkyPartPos, 0.5f * Time.deltaTime);

        if (Time.time - startTime > 10)
        {
            Destroy(transform.gameObject); 
        }
    }
}
