using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class test_scrip : MonoBehaviour
{

    public string _name;
    public string _ext_description;
    public string _state;
    public GameObject _name_text;
    public GameObject _ext_description_text;
    public GameObject _desc_slot1;
    public GameObject _desc_slot2;
    public GameObject _desc_slot3;
    public GameObject _desc_slot4;
    public GameObject _desc_slot5;
    public GameObject _desc_slot6;
    List<GameObject> _desc_slots;
    public GameObject _card;
    Vector2 _card_destination_position;

    public GameObject test_object;
    void Start()
    {
        _desc_slots.Add(_desc_slot1); _desc_slots.Add(_desc_slot2); _desc_slots.Add(_desc_slot3); _desc_slots.Add(_desc_slot2); _desc_slots.Add(_desc_slot1);
        _card_destination_position = new Vector2(_card.transform.position.x, _card.transform.position.y);
    }
    void Update()
    {
        
    }
}
