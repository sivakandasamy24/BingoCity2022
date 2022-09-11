using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaidAttackManager : MonoBehaviour
{
    [SerializeField] private Button DefaultAttackButton;
    [SerializeField] private Button BombAttackButton;
    [SerializeField] private Button RocketAttackButton;
    [SerializeField] private Button[] BuildingButton;
    [SerializeField] private GameObject[] Attacks;
    [SerializeField] private GameObject WinningPanel;
    [SerializeField] private TextMeshProUGUI StarsGained;
    [SerializeField] private TextMeshProUGUI CoinsGained;
    [SerializeField] private TextMeshProUGUI AttacksRemaining;

    private int _totalAttacksRemaining;
    private Transform _attackPositon;
    private string _attackName;
    private int _starCount;

    void OnEnable()
    {
        _totalAttacksRemaining = 3;
        _starCount = 0;
        UpdateAttackCount();
        WinningPanel.SetActive(false);
        DefaultAttackButton.onClick.AddListener(DefaultAttack);
        BombAttackButton.onClick.AddListener(BombAttack);
        RocketAttackButton.onClick.AddListener(RocketAttack);
        foreach (var building in BuildingButton)
        {
            BuildingData buildingData = building.GetComponent<BuildingData>();
            buildingData.ResetBuildingStats();
            building.onClick.AddListener(() =>
            {
                if (_totalAttacksRemaining > 0)
                {
                    OnBuildingClicked(building.transform, buildingData);
                }
            });
        }
    }

    private void OnDisable()
    {
        DefaultAttackButton.onClick.RemoveListener(DefaultAttack);
        BombAttackButton.onClick.RemoveListener(BombAttack);
        RocketAttackButton.onClick.RemoveListener(RocketAttack);
        foreach (var button in BuildingButton)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void DefaultAttack()
    {
        _attackPositon = DefaultAttackButton.transform;
        _attackName = "DefaultAttack";
    }

    private void BombAttack()
    {
        _attackPositon = BombAttackButton.transform;
        _attackName = "BombAttack";
    }

    private void RocketAttack()
    {
        _attackPositon = RocketAttackButton.transform;
        _attackName = "RocketAttack";
    }

    private void OnBuildingClicked(Transform buildingPosition, BuildingData buildingData)
    {
        switch (_attackName)
        {
            case "DefaultAttack":
                BuildingAttack(Attacks[0], buildingPosition, buildingData, 1);
                break;
            case "BombAttack":
                BuildingAttack(Attacks[1], buildingPosition, buildingData, 2);
                break;
            case "RocketAttack":
                BuildingAttack(Attacks[2], buildingPosition, buildingData, 3);
                break;
            default:
                Debug.Log($"Not a proper Attack");
                break;
        }
    }

    private void BuildingAttack(GameObject attack, Transform targetPosition, BuildingData buildingData, int power)
    {
        _totalAttacksRemaining--;
        var launchAttack = Instantiate(attack, _attackPositon);
        LeanTween.move(launchAttack, targetPosition, 1f).setDestroyOnComplete(true).setOnComplete(() =>
        {
            UpdateAttackCount();
            int starsCount = buildingData.GotHit(power);
            _starCount += starsCount;
            if (_totalAttacksRemaining <= 0)
                DisplayingAttackResult();
        });
    }

    private void DisplayingAttackResult()
    {
        WinningPanel.SetActive(true);
        StarsGained.text = _starCount.ToString();
        CoinsGained.text = (_starCount * 100).ToString();
    }

    private void UpdateAttackCount()
    {
        AttacksRemaining.text = _totalAttacksRemaining.ToString();
    }
}
