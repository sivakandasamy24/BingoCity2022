using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaidAttackManager : MonoBehaviour
{
    [SerializeField] private Button DefaultAttackButton;
    [SerializeField] private Button BombAttackButton;
    [SerializeField] private Button RocketAttackButton;
    [SerializeField] private Button[] BuildingButton;
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
            var buildingData = building.GetComponent<BuildingData>();
            building.onClick.AddListener(() => OnBuildingClicked(building.transform, buildingData));
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

    private void OnBuildingClicked(Transform buildingPosition, BuildingData buildingData)
    {
        switch (AttackName)
        {
            case "DefaultAttack":
                BuildingAttack(Attacks[0],buildingPosition, buildingData, 1);
                break;
            case "BombAttack":
                BuildingAttack(Attacks[1],buildingPosition, buildingData, 2);
                break;
            case "RocketAttack":
                BuildingAttack(Attacks[2],buildingPosition, buildingData, 3);
                break;
            default:
                Debug.Log($"Not a proper Attack");
                break;
        }
    }

    private void BuildingAttack(GameObject attack, Transform targetPosition, BuildingData buildingData,int power)
    {
        var launchAttack = Instantiate(attack, AttackPositon);
        LeanTween.move(launchAttack, targetPosition, 1f).setDestroyOnComplete(true).setOnComplete(()=>buildingData.GotHit(power));
    }
}
