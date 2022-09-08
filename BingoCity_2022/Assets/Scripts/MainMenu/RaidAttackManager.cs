using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaidAttackManager : MonoBehaviour
{
    [SerializeField] private Button DefaultAttackButton;
    [SerializeField] private Button BombAttackButton;
    [SerializeField] private Button RocketAttackButton;
    [SerializeField] private List<Button> BuildingButton;
    [SerializeField] private GameObject[] Attacks;
    private Transform AttackPositon;
    private string AttackName;

    void Start()
    {
        DefaultAttackButton.onClick.AddListener(DefaultAttack);
        BombAttackButton.onClick.AddListener(BombAttack);
        RocketAttackButton.onClick.AddListener(RocketAttack);
        foreach (var building in BuildingButton)
        {
            building.onClick.AddListener(()=> OnBuildingClicked(building.transform));
        }

    }

    private void DefaultAttack()
    {
        AttackPositon = DefaultAttackButton.transform;
        AttackName = "DefaultAttack";
    }

    private void BombAttack()
    {
        AttackPositon = BombAttackButton.transform;
        AttackName = "BombAttack";
    }

    private void RocketAttack()
    {
        AttackPositon = RocketAttackButton.transform;
        AttackName = "RocketAttack";
    }

    private void OnBuildingClicked(Transform buildingPosition)
    {
        switch (AttackName)
        {
            case "DefaultAttack":
                BuildingAttack(Attacks[0],buildingPosition);
                break;
            case "BombAttack":
                BuildingAttack(Attacks[1],buildingPosition);
                break;
            case "RocketAttack":
                BuildingAttack(Attacks[2],buildingPosition);
                break;
            default:
                Debug.Log($"Not a proper Attack");
                break;
        }
    }

    private void BuildingAttack(GameObject attack, Transform targetPosition)
    {
        var launchAttack = Instantiate(attack, AttackPositon);
        LeanTween.move(launchAttack, targetPosition, 1.2f).setDestroyOnComplete(true);
    }
}
