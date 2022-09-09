using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidManager : MonoBehaviour
{
    [SerializeField] private List<AttackCardScriptableObjects> Attack;
    [SerializeField] private Transform Content;
    [SerializeField] private GameObject AttackCard;

    private void OnEnable()
    {
        for (int i = 0; i < Attack.Count; i++)
        {
            var attack = Instantiate(AttackCard, Content);
            attack.GetComponent<AttackCardUi>().AssigningValues(Attack[i].AttackName,Attack[i].CardCount,Attack[i].CoinCount);
        }
    }
      
    private void OnDisable()
    {
        foreach (Transform item in Content.GetComponentInChildren<Transform>())
        {
            Destroy(item.gameObject);
        }
    }
}
