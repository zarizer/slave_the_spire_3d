using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class resourses_initializer : MonoBehaviour
{
    public GameObject _reward_cards_canvas;
    public GameObject _card_prefab;
    public List<_desc_stats_map> _desc_stats_map_ser;
    Dictionary<string, GameObject> _desc_stats;
    void Start()
    {
        _desc_stats = Deserialize(_desc_stats_map_ser);
        InitializeRewardCards(_reward_cards_canvas);
    }


    void Update()
    {
        
    }

    GameObject InitializeCard(string card_name, string card_description, int energy, List<(string, int, bool)> desc_stats, GameObject canvas)
    {
        GameObject new_card = Instantiate(_card_prefab, canvas.transform);
        card_script new_script = new_card.GetComponent<card_script>();
        new_script._name_text.GetComponent<TextMeshProUGUI>().text = card_name;
        new_script.name = card_name;
        new_script._name = card_name;
        new_script._ext_description = card_description;
        new_script.energy = energy;

        foreach (var i in desc_stats)
        {
            
            if (_desc_stats.ContainsKey(i.Item1))
            {

                foreach (var j in new_script._desc_slots)
                {
                    
                    if (j.transform.childCount == 0)
                    {
                        InitializeStatPrefab(j, i.Item1, i.Item2, i.Item3);
                        break;
                    }
                }
            }
        }

        return new_card;
    }

    GameObject InitializeStatPrefab(GameObject parent, string type, int value, bool is_if = false)
    {
        GameObject new_prefab = Instantiate(_desc_stats[type], parent.transform);
        new_prefab.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = value.ToString();
        if (is_if)
        {
            new_prefab.transform.GetChild(2).gameObject.SetActive(true);
        }
        return new_prefab;
    }

    void InitializeRewardCards(GameObject parent)
    {

        var new_stats = new List<(string, int, bool)> { ("dmg", 6, false), ("eng", 1, false) };
        InitializeCard("Удар", "1 энергия\nнаносит 6 урона", 1, new_stats, _reward_cards_canvas);
    }

    [System.Serializable]
    public struct _desc_stats_map
    {
        public string name;
        public GameObject g_object;
    }

    Dictionary<string, GameObject> Deserialize(List<_desc_stats_map> list_dict)
    {
        Dictionary<string, GameObject> new_dict = new Dictionary<string, GameObject>();
        foreach (var item in list_dict) 
        {
            new_dict.Add(item.name, item.g_object);
        }
        return new_dict;
    }
}
