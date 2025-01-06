using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;


public class battle_main_script : MonoBehaviour
{

    public int _hp;
    public int _def;
    public int _money;
    public int _overdrive;
    public float _energy;
    public float _max_energy;
    public int _hand_size;

    public GameObject Hand;
    public GameObject Getting;
    public GameObject Reset;
    public GameObject Deck;
    public GameObject RewardCards;
    public GameObject StoreCards;
    public GameObject SpecialCards;

    public List<GameObject> CharacterStats;

    public int _picked_card_number = 0;
    bool _is_cards_given = false;

    


    void Start()
    {
        LeanTween.delayedCall(2f, StartDeckActions);
    }

    void Update()
    {
        UpdateCharacterStatsInterface();
    }

    private void FixedUpdate()
    {
        RecountCardsDestinationPosition();
        OnMouseWheel();
    }

    void UpdateCharacterStatsInterface()
    {
        CharacterStats[0].GetComponent<Text>().text = _hp.ToString();
        CharacterStats[1].GetComponent<Text>().text = _def.ToString();
        CharacterStats[2].GetComponent<Text>().text = _overdrive.ToString();
        CharacterStats[3].GetComponent<Text>().text = _money.ToString();
    }

    GameObject GetCardByName(string card_name) 
    {
        foreach (Transform card in RewardCards.transform) 
        {
            Debug.Log(card.name);
            if (card.name == card_name)
            {
                return card.gameObject;
            }
        }
        foreach (Transform card in StoreCards.transform)
        {
            if (card.name == card_name)
            {
                return card.gameObject;
            }
        }
        foreach (Transform card in SpecialCards.transform)
        {
            if (card.name == card_name)
            {
                return card.gameObject;
            }
        }
        return null;
    }

    GameObject InsCard(GameObject card, GameObject parent)
    {
        GameObject new_card = Instantiate(card, parent.transform);
        return new_card;
    }

    public void InstantiateStartCards()
    {
        InsCard(GetCardByName("удар"), Deck);
        InsCard(GetCardByName("удар"), Deck);
        InsCard(GetCardByName("удар"), Deck);
        InsCard(GetCardByName("удар"), Deck);
        InsCard(GetCardByName("удар"), Deck);
        InsCard(GetCardByName("оборона"), Deck);
        InsCard(GetCardByName("оборона"), Deck);
        InsCard(GetCardByName("оборона"), Deck);
        InsCard(GetCardByName("оборона"), Deck);
        InsCard(GetCardByName("ярость"), Deck);
    }

    void RecountCardsDestinationPosition()
    {
        for (int i = 0; i < Hand.transform.childCount; i++)
        {
            RecountCardDestinationPosition(Hand.transform.GetChild(i), Hand, i);
        }
        for (int i = 0; i < Getting.transform.childCount; i++)
        {
            RecountCardDestinationPosition(Getting.transform.GetChild(i), Getting, i);
        }
        for (int i = 0; i < Reset.transform.childCount; i++)
        {
            RecountCardDestinationPosition(Reset.transform.GetChild(i), Reset, i);
        }
        for (int i = 0; i < Deck.transform.childCount; i++)
        {
            RecountCardDestinationPosition(Deck.transform.GetChild(i), Deck, i);
        }
    }

    void StartDeckActions()
    {
        InstantiateStartCards();
        CardMovesOnBattleStart();
        _is_cards_given = true;
    }

    void CardMovesOnBattleStart()
    {
        foreach(Transform card in Deck.transform)
        {
            ChangeParent(card, Getting);
        } 
        for(int i = 0; i < _hand_size; i++)
        {
            TakeCard();
        }
    }

    void RecountCardDestinationPosition(Transform card, GameObject parent, int number)
    {
        card.gameObject.GetComponent<card_script>()._card_destination_position = new Vector2(parent.transform.position.x + number * 170, parent.transform.position.y);
    }

    

    GameObject TakeCard()
    {
        if (Getting.transform.childCount == 0)
        {
            foreach (GameObject card in Reset.transform) 
            {
                ChangeParent(card, Getting);
            }
        }
        if (Getting.transform.childCount == 0)
        {
            return null;
        }
        GameObject cur_card = Getting.transform.GetChild(UnityEngine.Random.Range(0, Getting.transform.childCount - 1)).gameObject;
        ChangeParent(cur_card, Hand);
        return cur_card;
    }

    void ChangeParent(GameObject obj, GameObject new_parent)
    {
        obj.transform.SetParent(new_parent.transform);
    }
    void ChangeParent(Transform obj, GameObject new_parent)
    {
        obj.SetParent(new_parent.transform);
    }
    void ChangeParent(Transform obj, Transform new_parent)
    {
        obj.SetParent(new_parent);
    }
    void ChangeParent(GameObject obj, Transform new_parent)
    {
        obj.transform.SetParent(new_parent);
    }

    void OnMouseWheel()
    {
        if (_is_cards_given)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                Hand.transform.GetChild(_picked_card_number).GetComponent<card_script>()._picked = false;
            }
            if (scroll > 0)
            {
                _picked_card_number++;
                if (_picked_card_number > Hand.transform.childCount-1)
                {
                    _picked_card_number = 0;
                }
            }
            if (scroll < 0)
            {
                _picked_card_number--;
                if (_picked_card_number < 0)
                {
                    _picked_card_number = Hand.transform.childCount-1;
                }
            }
            Hand.transform.GetChild(_picked_card_number).GetComponent<card_script>()._picked = true;
        }
        
    }

}
