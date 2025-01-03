using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class resourses_initializer : MonoBehaviour
{
    public GameObject _reward_cards_canvas;
    public GameObject _special_cards_canvas;
    public GameObject _store_cards_canvas;
    public GameObject _card_prefab;
    public List<_desc_stats_map> _desc_stats_map_ser;
    Dictionary<string, GameObject> _desc_stats;
    public List<_card_image_map> _card_images_ser;
    Dictionary<string, Sprite> _card_images;
    void Start()
    {
        _desc_stats = Deserialize_stats(_desc_stats_map_ser);
        _card_images = Deserialize_images(_card_images_ser);
        InitializeRewardCards(_reward_cards_canvas);
        InitializeSpecialCards(_special_cards_canvas);
        InitializeStoreCards(_store_cards_canvas);
    }


    void Update()
    {
        
    }

    GameObject InitializeCard(string card_name, string card_description, int energy, List<(string, int, bool)> desc_stats, GameObject canvas, int number, bool rare)
    {
        GameObject new_card = Instantiate(_card_prefab, canvas.transform);
        card_script new_script = new_card.GetComponent<card_script>();
        new_script._name_text.GetComponent<TextMeshProUGUI>().text = card_name;
        new_script.name = card_name;
        new_script._name = card_name;
        new_script._ext_description = card_description;
        new_script.energy = energy;
        new_script._rare = rare;
        new_script._ext_description_object.SetActive(true);
        new_script._ext_description_object.GetComponent<Canvas>().overrideSorting = true;
        new_script._ext_description_object.SetActive(false);
        new_script._ext_description_text.GetComponent<TextMeshProUGUI>().text = card_description;
        new_script._card_image_object.GetComponent<UnityEngine.UI.Image>().sprite = _card_images[card_name];
        new_script._card_destination_position = new Vector2(number%20*250 - 5000, number/20*500 + 2000);

        

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
        if (value == -52)
        {
            new_prefab.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
        }
        if (is_if)
        {
            new_prefab.transform.GetChild(2).gameObject.SetActive(true);
        }
        return new_prefab;
    }
    void InitializeStoreCards(GameObject parent)
    {
        List<(string, int, bool)> new_stats;

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("def", 26, false), ("burn", -52, false) };
        InitializeCard("призрачные латы", "1 энергия\nдаёт 26 защиты\nсжигается", 1, new_stats, parent, 51, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("def", -52, true), ("other", -52, false) };
        InitializeCard("окоп", "1 энергия\nудваиват защиту", 1, new_stats, parent, 52, false);

        new_stats = new List<(string, int, bool)> { ("eng", 2, false), ("dmg", 23, false), ("rare", -52, false) };
        InitializeCard("резня", "2 энергии\nнаносит 23 урона", 2, new_stats, parent, 53, true);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("dmg", -52, true), ("other", -52, false), ("move", -52, false), ("rare", -52, false) };
        InitializeCard("вихрь", "1 энергия\n наносит 7 урона за каждую энергию", 1, new_stats, parent, 54, true);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("heal", 12, false), ("burn", -52, false) };
        InitializeCard("снадобье", "1 энергия\nдаёт 7 здоровья\nсжигается", 1, new_stats, parent, 55, false);

        new_stats = new List<(string, int, bool)> { ("eng", 0, false), ("take", 1, false), ("eng", -2, false), ("rare", -52, false) };
        InitializeCard("укол бодрости", "даёт 2 энергии\nдобирает 1 карту", 0, new_stats, parent, 56, true);

        new_stats = new List<(string, int, bool)> { ("eng", 0, false), ("def", 2, false), ("take", 1, false) };
        InitializeCard("спокойствие", "0 энергии\nдаёт 2 защиты\nдобирает карту", 0, new_stats, parent, 57, false);

        new_stats = new List<(string, int, bool)> { ("eng", 3, false), ("def", 60, false), ("rare", -52, false) };
        InitializeCard("полная концентрация", "3 энергии\nдаёт 60 защиты", 3, new_stats, parent, 58, true);

        new_stats = new List<(string, int, bool)> { ("eng", 0, false), ("active", -52, false), ("other", -52, false), ("rare", -52, false) };
        InitializeCard("магазинный подарок", "0 энергии\nвы получаете в руку случайную карту из магазина\nодноразовая", 0, new_stats, parent, 59, true);

        new_stats = new List<(string, int, bool)> { ("eng", 3, false), ("other", -52, false), ("rare", -52, false) };
        InitializeCard("на волю случая", "3 энергии\nзамешивает в колоду 10 случайных карт", 3, new_stats, parent, 60, true);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("dmg", -52, true), ("other", -52, false), ("rare", -52, false) };
        InitializeCard("оттачиваемый удар", "1 энергия\nнаносит 4 урона, каждое использование повышает урон на 1", 1, new_stats, parent, 61, true);

        new_stats = new List<(string, int, bool)> { ("eng", 2, false), ("dmg", 40, true), ("other", -52, false), ("rare", -52, false) };
        InitializeCard("бомба", "2 энергии\nкидает бомбу, которая при взрыве наносит 40 урона", 2, new_stats, parent, 62, true);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("burn", -52, false), ("other", -52, false), ("rare", -52, false) };
        InitializeCard("ритуал", "1 энергия\nдобавляет в колоду 'молитва', 'поклонение', 'жертвоприношение'\nсжигается", 1, new_stats, parent, 63, true);

        new_stats = new List<(string, int, bool)> { ("eng", 3, false), ("dmg", -52, true),("other", -52, false)};
        InitializeCard("подкуп", "3 энергии\nЕсли у вас больше бюджетов чем здоровья у противника, противник умирает", 3, new_stats, parent, 64, true);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("dmg", 20, false), ("mon", 10, true), ("burn", -52, false) };
        InitializeCard("ограбить", "1 энергия\nнаносит 20 урона\nесли эта карта убивает противника, вы получаете 10 бюджетов\nсжигается", 1, new_stats, parent, 65, true);

        new_stats = new List<(string, int, bool)> { ("eng", 2, false), ("def",-52, true), ("other", -52, false)};
        InitializeCard("подушка безопасности", "2 энергии\nвы получаете защиту в количестве ваших бюджетов", 2, new_stats, parent, 66, true);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("dmg", 35, true), ("mon", -4, false) };
        InitializeCard("денежное лезвие", "1 энергия\nнаносит 35 урона\nвы тратите 4 бюджета", 1, new_stats, parent, 67, true);

    }
    void InitializeSpecialCards(GameObject parent)
    {
        List<(string, int, bool)> new_stats;

        new_stats = new List<(string, int, bool)> { ("other", -52, false) };
        InitializeCard("рана", "неиграбельная", 999, new_stats, parent, 31, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("active", -52, false) };
        InitializeCard("слизь", "1 энергия\nудаляется из колоды", 1, new_stats, parent, 32, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("active", -52, false), ("other", -52, false)  };
        InitializeCard("сундук", "1 энергия\nвыберите 1 артефакт из 3 предложенных", 1, new_stats, parent, 33, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("active", -52, false), ("other", -52, false) };
        InitializeCard("магазин", "1 энергия\nвойдите в магазин", 1, new_stats, parent, 34, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("heal", 40, false), ("active", -52, false) };
        InitializeCard("костёр", "1 энергия\nвосстанавливает 40 здоровья", 1, new_stats, parent, 35, false);

        new_stats = new List<(string, int, bool)> { ("eng", 0, false), ("dmg", 4, false), ("take", 1, false) };
        InitializeCard("undefined", "0 энергии\nнаносит 4 урона\nдобирает карту", 0, new_stats, parent, 36, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("dmg", 2, false), ("efir", -52, false), ("active", -52, false) };
        InitializeCard("молитва", "1 энергия\nнаносит 2 урона", 1, new_stats, parent, 37, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("def", 2, false), ("efir", -52, false), ("active", -52, false) };
        InitializeCard("поклон", "1 энергия\nдаёт 2 защиты", 1, new_stats, parent, 38, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("heal", -2, false), ("efir", -52, false), ("active", -52, false) };
        InitializeCard("жертвоприношение", "1 энергия\nвы теряете 2 здоровья", 1, new_stats, parent, 39, false);

        new_stats = new List<(string, int, bool)> { ("eng", 0, false), ("dmg", 60, false), ("efir", -52, false), ("active", -52, false) };
        InitializeCard("небесная кара", "0 энергии\nнаносит 60 урона", 0, new_stats, parent, 40, false);

        new_stats = new List<(string, int, bool)> { ("eng", 2, false), ("other", -52, false), ("active", -52, false) };
        InitializeCard("сюжет", "2 энергии\nиспользуйте 3 таких, чтобы получить хентай-новеллу\nодноразовая", 2, new_stats, parent, 41, false);

        new_stats = new List<(string, int, bool)> { ("eng", 0, false), ("dmg", 999, false), ("active", -52, false) };
        InitializeCard("новелла", "0 энергии\nнаносит 999 урона\nодноразовая", 0, new_stats, parent, 42, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("eng", -3, false), ("other", -52, false) };
        InitializeCard("качалка", "1 энергия\nдаёт 3 энергии\nодноразовая\nзамешивает 3 своих копии в стопку добора", 1, new_stats, parent, 43, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("dmg", 18, false), ("heal", 20, true), ("other", -52, false) };
        InitializeCard("свежее мясо", "1 энергия\nнаносит 18 урона\nесли эта карта убивает врага, вы получаете 20 здоровья", 1, new_stats, parent, 44, false);

        new_stats = new List<(string, int, bool)> { ("eng", 2, false), ("dmg", 10, false), ("other", -52, false), ("burn", -52, false) };
        InitializeCard("фотошоп", "2 энергии\nнаносит 10 урона\nудваивает здоровье врага\nзаменяет врага на случайного\nсжигается", 2, new_stats, parent, 45, false);

        new_stats = new List<(string, int, bool)> { ("eng", 0, false), ("def", 20, false), ("active", -52, false) };
        InitializeCard("заботать", "0 энергии\nдаёт 20 защиты\nодноразовая", 0, new_stats, parent, 46, false);

        new_stats = new List<(string, int, bool)> { ("eng", 0, false), ("mon", 15, false), ("active", -52, false) };
        InitializeCard("продать конспект", "0 энергии\nдаёт 15 бюджетов\nодноразовая", 0, new_stats, parent, 47, false);

        new_stats = new List<(string, int, bool)> { ("eng", 0, false), ("heal", 20, false), ("active", -52, false) };
        InitializeCard("покушать", "0 энергии\nдаёт 20 здоровья\nодноразовая\n\n\nперекус студента чек", 0, new_stats, parent, 48, false);

        new_stats = new List<(string, int, bool)> { ("other", -52, false) };
        InitializeCard("картридж", "Неиграбельная\nЕсли у вас будет 3 картриджа в руке, получите благославление качалки по окончании хода", 999, new_stats, parent, 49, false);
    }

    void InitializeRewardCards(GameObject parent)
    {
        List<(string, int, bool)> new_stats;

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("dmg", 6, false) };
        InitializeCard("удар", "1 энергия\nнаносит 6 урона", 1, new_stats, parent, 1, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("def", 5, false) };
        InitializeCard("оборона", "1 энергия\nдаёт 6 защиты", 1, new_stats, parent, 2, false);

        new_stats = new List<(string, int, bool)> {  ("eng", 2, false), ("dmg", 23, false), ("rare", -52, false)};
        InitializeCard("резня", "2 энергии\nнаносит 23 урона", 2, new_stats, parent, 3, true);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("effect", -52, false) };
        InitializeCard("ярость", "1 энергия\nдаёт эффект ярости", 1, new_stats, parent, 4, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("dmg", 12, false) };
        InitializeCard("разрез", "1 энергия\nнаносит 12 урона", 1, new_stats, parent, 5, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("def", 26, false), ("burn", -52, false) };
        InitializeCard("призрачные латы", "1 энергия\nдаёт 26 защиты\nсжигается", 1, new_stats, parent, 6, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("def", -52, true), ("other", -52, false) };
        InitializeCard("окоп", "1 энергия\nудваиват защиту", 1, new_stats, parent, 7, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("dmg", 25, false), ("heal", -3, false) };
        InitializeCard("не щадя себя", "1 энергия\nнаносит 25 урона\nвы теряете 3 здоровья", 1, new_stats, parent, 8, false);

        new_stats = new List<(string, int, bool)> { ("eng", 0, false), ("def", 35, false), ("other", -52, false) };
        InitializeCard("стиснуть зубы", "0 энергии\nдаёт 35 защиты\nдобавляет в колоду 2 раны", 0, new_stats, parent, 9, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("dmg", -52, true), ("other", -52, false), ("move", -52, false), ("rare", -52, false) };
        InitializeCard("вихрь", "1 энергия\n наносит 7 урона за каждую энергию", 1, new_stats, parent, 10, true);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("heal", 12, false), ("burn", -52, false) };
        InitializeCard("снадобье", "1 энергия\nдаёт 7 здоровья\nсжигается", 1, new_stats, parent, 11, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("def", 14, false), ("other", -52, false)};
        InitializeCard("жертва", "1 энергия\nдаёт 14 защиты\nсбрасывает случайную карту в руке", 1, new_stats, parent, 12, false);

        new_stats = new List<(string, int, bool)> { ("eng", 0, false), ("take", 1, false), ("eng", -2, false), ("rare", -52, false) };
        InitializeCard("укол бодрости", "даёт 2 энергии\nдобирает 1 карту", 0, new_stats, parent, 13, true);

        new_stats = new List<(string, int, bool)> { ("eng", 2, false), ("dmg", 15, false), ("def", 15, false), ("move", -52, false) };
        InitializeCard("перекат за спину", "2 энергии\nдаёт 15 защиты\nнаносит 15 урона\nвы совершаете перекат", 2, new_stats, parent, 14, false);

        new_stats = new List<(string, int, bool)> { ("eng", 0, false), ("def", 2, false), ("take", 1, false) };
        InitializeCard("спокойствие", "0 энергии\nдаёт 2 защиты\nдобирает карту", 0, new_stats, parent, 15, false);

        new_stats = new List<(string, int, bool)> { ("eng", 3, false), ("def", 60, false), ("rare", -52, false) };
        InitializeCard("полная концентрация", "3 энергии\nдаёт 60 защиты", 3, new_stats, parent, 16, true);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("effect", -52, true), ("rare", 1, false) };
        InitializeCard("всё или ничего", "1 энергия\nС 50% вероятностью до окончания боя вы или противник наносите лишние 8 урона картами", 1, new_stats, parent, 17, true);

        new_stats = new List<(string, int, bool)> { ("eng", 3, false), ("other", -52, false), ("rare", -52, false) };
        InitializeCard("на волю случая", "3 энергии\nзамешивает в колоду 10 случайных карт", 3, new_stats, parent, 18, true);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("dmg", -52, true), ("other", -52, false), ("rare", -52, false) };
        InitializeCard("оттачиваемый удар", "1 энергия\nнаносит 4 урона, каждое использование повышает урон на 1", 1, new_stats, parent, 19, true);

        new_stats = new List<(string, int, bool)> { ("eng", 2, false), ("dmg", 40, true), ("other", -52, false), ("rare", -52, false) };
        InitializeCard("бомба", "2 энергии\nкидает бомбу, которая при взрыве наносит 40 урона", 2, new_stats, parent, 20, true);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("burn", -52, false), ("other", -52, false), ("rare", -52, false) };
        InitializeCard("ритуал", "1 энергия\nдобавляет в колоду 'молитва', 'поклонение', 'жертвоприношение'\nсжигается", 1, new_stats, parent, 21, true);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("dmg", -52, false), ("other", -52, false) };
        InitializeCard("удар щитом", "1 энергия\nнаносит 50% защиты уроном\nвы теряете 50% защиты", 1, new_stats, parent, 22, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("take", 3, false) };
        InitializeCard("шуллер", "1 энергия\nвы берёте 3 карты", 1, new_stats, parent, 23, false);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("ovd", 3, false), ("burn", -52, false) };
        InitializeCard("разгон процессора", "1 энергия\nвы получаете 3 разгона\nсжигается", 1, new_stats, parent, 24, false);

        new_stats = new List<(string, int, bool)> { ("eng", 0, false), ("ovd", 1, false), ("take", 1, false), ("move", -52, false)};
        InitializeCard("прыжок с трамплина", "0 энергии\nвы получаете 1 разгон\nвы берёте 1 карту\nвы совершаете прыжок", 0, new_stats, parent, 25, false);

        new_stats = new List<(string, int, bool)> { ("eng", 0, false), ("active", -52, false), ("other", -52, false), ("rare", -52, false) };
        InitializeCard("магазинный подарок", "0 энергии\nвы получаете в руку случайную карту из магазина\nодноразовая", 0, new_stats, parent, 26, true);

        new_stats = new List<(string, int, bool)> { ("eng", 3, false),  ("other", -52, false), ("rare", -52, false) };
        InitializeCard("на волю случая 2", "3 энергии\nс вероятностью 5/6 снижает здоровье всем противникам до 1 здоровье\nиначе ваше здоровье снижается до 1", 3, new_stats, parent, 27, true);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("active", -52, false), ("other", -52, false), ("rare", -52, false) };
        InitializeCard("джин", "1 энергия\nускоряет восстановление энергии на 50%\nодноразовая", 1, new_stats, parent, 28, true);

        new_stats = new List<(string, int, bool)> { ("eng", 1, false), ("def", -52, true), ("other", -52, false) };
        InitializeCard("профит", "1 энергия\nдаёт 1 защиту за каждые 4 разыгранные на уровне карты", 1, new_stats, parent, 29, false);
    }

    [System.Serializable]
    public struct _desc_stats_map
    {
        public string name;
        public GameObject g_object;
    }
    Dictionary<string, GameObject> Deserialize_stats(List<_desc_stats_map> list_dict)
    {
        Dictionary<string, GameObject> new_dict = new Dictionary<string, GameObject>();
        foreach (var item in list_dict)
        {
            new_dict.Add(item.name, item.g_object);
        }
        return new_dict;
    }
    [System.Serializable]
    public struct _card_image_map
    {
        public string name;
        public Sprite texture;
    }
    Dictionary<string, Sprite> Deserialize_images(List<_card_image_map> list_dict)
    {
        Dictionary<string, Sprite> new_dict = new Dictionary<string, Sprite>();
        foreach (var item in list_dict)
        {
            new_dict.Add(item.name, item.texture);
        }
        return new_dict;
    }

}
