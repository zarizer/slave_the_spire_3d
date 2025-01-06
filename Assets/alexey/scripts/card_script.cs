using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class card_script : MonoBehaviour
{

    public string _name;
    public string _ext_description;
    public string _state;
    public bool _picked;
    public bool _show_description;
    public bool _rare;
    public Vector2 _card_destination_position;
    public GameObject _name_text;
    public GameObject _ext_description_object;
    public GameObject _ext_description_text;
    public GameObject _particles; 
    public GameObject _card_image_object;
    
    public List<GameObject> _desc_slots;
    public GameObject _card;

    public int energy;

    void Start()
    {

    }
    void Update()
    {
        CheckPicked();
        CheckDescription();
        MoveCard();
    }
    [Serializable]
    public struct _desc_slots_struct
    {
        public GameObject _object;
    }

    void CheckPicked()
    {
        if (_picked)
        {
            _particles.SetActive(true);    
        }
        else
        {
            _particles.SetActive(false);
        }
    }
    void CheckDescription()
    {
        if (_show_description)
        {
            _ext_description_object.SetActive(true);
        }
        else
        {
            _ext_description_object.SetActive(false);
        }
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

    void MoveCard()
    {
        _card.transform.position = Vector3.Lerp(_card.transform.position, _card_destination_position, 0.01f);
    }

    
}
