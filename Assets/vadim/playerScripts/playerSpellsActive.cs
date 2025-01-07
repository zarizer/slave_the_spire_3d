using UnityEngine;

public class playerSpellsActive : MonoBehaviour
{
    public GameObject player;
    string spellID;
    public GameObject prefabHeavenKara;

    Vector3 ViewRayStartPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    Ray ViewRay;
    RaycastHit hit;

    private GameObject view;

    void Start()
    {
        spellID = "heaven_kara";
        view = transform.Find("View").gameObject;
        ViewRay = view.GetComponent<Camera>().ScreenPointToRay(ViewRayStartPos);
    }

    
    void Update()
    {
        ViewRay = view.GetComponent<Camera>().ScreenPointToRay(ViewRayStartPos);

        CheckSpellChose();

        ItemIteraction();
    }

    void CheckSpellChose()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            UseCard(spellID);
        }
    }

    void ItemIteraction()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (Physics.Raycast(ViewRay, out hit))
            {
                if (hit.transform.gameObject.CompareTag("ItemIteraction"))
                {
                    hit.transform.gameObject.GetComponent<IteractionScript>().ItemIteraction();
                }
            }
        }
    }

    void UseCard(string cardID)
    {
        switch(cardID)
        {
            case "heaven_kara":
                if (Physics.Raycast(ViewRay, out hit))
                {
                    GameObject heaven_kara = Instantiate(prefabHeavenKara, hit.point, prefabHeavenKara.transform.rotation);
                }

                break;

        }
    }
}
