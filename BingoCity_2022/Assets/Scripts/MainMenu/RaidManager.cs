using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaidManager : MonoBehaviour
{
    [SerializeField] private AttackCardScriptableObjects Attack;
    [SerializeField] private Transform Content;
    [SerializeField] private GameObject AttackCard;
    [SerializeField] private TextMeshProUGUI RaidTokenCollected;
    [SerializeField] private TextMeshProUGUI RaidCost;
    [SerializeField] private Button GoButton;
    public AttackCardScriptableObjects AttackCardScriptableObjects => Attack;
    private void OnEnable()
    {
        for (int i = 0; i < Attack.attackData.Count; i++)
        {
            var attack = Instantiate(AttackCard, Content);
            attack.GetComponent<AttackCardUi>().AssigningValues(AttackCardScriptableObjects.attackData[i].Icon,AttackCardScriptableObjects.attackData[i].CardCount,AttackCardScriptableObjects.attackData[i].CoinCount);
        }

        RaidTokenCollected.text = AttackCardScriptableObjects.RaidToken.ToString();
        RaidCost.text = AttackCardScriptableObjects.RaidCost.ToString();
        if (AttackCardScriptableObjects.RaidToken < AttackCardScriptableObjects.RaidCost)
        {
            GoButton.interactable = false;
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
