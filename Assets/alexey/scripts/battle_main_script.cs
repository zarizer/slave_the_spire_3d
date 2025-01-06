using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class battle_main_script : MonoBehaviour
{

    public int _hp;
    public int _def;
    public int _money;
    public int _overdrive;
    public float _energy;

    public GameObject Hand;
    public GameObject Getting;
    public GameObject Reset;
    public GameObject Deck;
    public GameObject RewardCards;
    public GameObject StoreCards;
    public GameObject SpecialCards;


    void Start()
    {

    }

    void Update()
    {

    }


    GameObject GetCardByName(string card_name) 
    {
        foreach (Transform card in RewardCards.transform) 
        {
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
}
