using System;
using System.Collections.Generic;
using UnityEngine;

public class card_script : MonoBehaviour
{

    public string _name;
    public string _ext_description;
    public string _state;
    public GameObject _name_text;
    public GameObject _ext_description_text;
    
    //public List<_desc_slots_struct> _desc_slots_ser;
    public List<GameObject> _desc_slots;
    public GameObject _card;
    Vector2 _card_destination_position;

    public int energy;

    void Start()
    {
        //_desc_slots = Deserialize_slots(_desc_slots_ser);
        _card_destination_position = new Vector2(_card.transform.position.x, _card.transform.position.y);
    }
    void Update()
    {

    }
    [Serializable]
    public struct _desc_slots_struct
    {
        public GameObject _object;
    }

    List<GameObject> Deserialize_slots(List<_desc_slots_struct> _desc_slots_ser)
    {
        List<GameObject> new_list = new List<GameObject>();
        foreach (var item in _desc_slots_ser)
        {
            new_list.Add(item._object);
        }
        return new_list;
    }
}
