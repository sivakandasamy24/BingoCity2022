using System.Collections;
using System.Collections.Generic;
using BingoCity;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaidAttackManager : MonoBehaviour
{
    [SerializeField] private AttackCardScriptableObjects Attack;
    [SerializeField] private Toggle DefaultAttackButton;
    [SerializeField] private Toggle BombAttackButton;
    [SerializeField] private Toggle RocketAttackButton;
    [SerializeField] private Button[] BuildingButton;
    [SerializeField] private GameObject[] Attacks;
    [SerializeField] private GameObject WinningPanel;
    [SerializeField] private TextMeshProUGUI StarsGained;
    [SerializeField] private TextMeshProUGUI CoinsGained;
    [SerializeField] private TextMeshProUGUI AttacksRemaining;
    [SerializeField] private TextMeshProUGUI BombCount;
    [SerializeField] private TextMeshProUGUI RocketCount;
    
    public AttackCardScriptableObjects AttackCardScriptableObjects => Attack;

    private int _totalAttacksRemaining;
    private Transform _attackPositon;
    private string _attackName;
    private int _starCount;

    void OnEnable()
    {
        int i = 0;
        _totalAttacksRemaining = 3;
        _starCount = 0;
        UpdateAttackCount();
        WinningPanel.SetActive(false);
        
        /*DefaultAttackButton.onClick.AddListener(DefaultAttack);
        BombAttackButton.onClick.AddListener(BombAttack);
        RocketAttackButton.onClick.AddListener(RocketAttack);*/
        
        BombCount.text = AttackCardScriptableObjects.attackData[0].CardCount.ToString();
        RocketCount.text = AttackCardScriptableObjects.attackData[1].CardCount.ToString();
        foreach (var building in BuildingButton)
            //for(i = 0; i < BuildingButton.Length; i++)
        {
            BuildingData buildingData = building.GetComponent<BuildingData>();
            //buildingData.ResetBuildingStats();

           
            Attack.attackBuildingData[buildingData.id].StarDestroyedCount =  0;
            AttackCardScriptableObjects.attackBuildingData[buildingData.id].Active = true;
            
            buildingData.SetStar(AttackCardScriptableObjects.attackBuildingData[buildingData.id].StarCount);
            buildingData.gameObject.GetComponent<Image>().sprite =
                AttackCardScriptableObjects.attackBuildingData[buildingData.id].BuildingImages[ AttackCardScriptableObjects.attackBuildingData[buildingData.id].StarCount - 1];
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
        /*DefaultAttackButton.onClick.RemoveListener(DefaultAttack);
        BombAttackButton.onClick.RemoveListener(BombAttack);
        RocketAttackButton.onClick.RemoveListener(RocketAttack);*/
        foreach (var button in BuildingButton)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void DefaultAttack()
    {
        _attackPositon = DefaultAttackButton.transform;
        _attackName = "DefaultAttack";
    }

    public void BombAttack()
    {
        if(AttackCardScriptableObjects.attackData[0].CardCount-1<0) return;
        
        _attackPositon = BombAttackButton.transform;
        _attackName = "BombAttack";
        
        
    }

    public void RocketAttack()
    {
        if( AttackCardScriptableObjects.attackData[1].CardCount-1<0) return;
        _attackPositon = RocketAttackButton.transform;
        _attackName = "RocketAttack";
        
    }

    private void OnBuildingClicked(Transform buildingPosition, BuildingData buildingData)
    {
        switch (_attackName)
        {
            case "DefaultAttack":
                BuildingAttack(Attacks[0], buildingPosition, buildingData, 1);
                SoundUtils.PlaySoundOnce(AudioTrackNames.DefaultFly);
                break;
            case "BombAttack":
                if(AttackCardScriptableObjects.attackData[0].CardCount-1<0) return;
                AttackCardScriptableObjects.attackData[0].CardCount--;
                BombCount.text = AttackCardScriptableObjects.attackData[0].CardCount.ToString();
                BuildingAttack(Attacks[1], buildingPosition, buildingData, 2);
                SoundUtils.PlaySoundOnce(AudioTrackNames.BombFly);
                break;
            case "RocketAttack":
                if( AttackCardScriptableObjects.attackData[1].CardCount-1<0) return;
                AttackCardScriptableObjects.attackData[1].CardCount--;
                RocketCount.text = AttackCardScriptableObjects.attackData[1].CardCount.ToString();
                BuildingAttack(Attacks[2], buildingPosition, buildingData, 3);
                SoundUtils.PlaySoundOnce(AudioTrackNames.RocketFly);
                break;
            default:
                Debug.Log($"Not a proper Attack");
                break;
        }
    }

    private void BuildingAttack(GameObject attack, Transform targetPosition, BuildingData buildingData, int power)
    {
        if (!AttackCardScriptableObjects.attackBuildingData[buildingData.id].Active) return;
        _totalAttacksRemaining--;
        var launchAttack = Instantiate(attack, _attackPositon);
        LeanTween.move(launchAttack, targetPosition, 1f).setDestroyOnComplete(true).setOnComplete(() =>
        {
            switch (power)
            {
                case 1:
                    SoundUtils.PlaySoundOnce(AudioTrackNames.DefaultDamage);
                    break;
                case 2:
                    SoundUtils.PlaySoundOnce(AudioTrackNames.BombDamage);
                    break;
                case 3:
                    SoundUtils.PlaySoundOnce(AudioTrackNames.RocketDamage);
                    break;
            }
            UpdateAttackCount();
            int starsCount = buildingData.GotHit(power, buildingData.id);
            if (starsCount >= AttackCardScriptableObjects.attackBuildingData[buildingData.id].StarCount)
                AttackCardScriptableObjects.attackBuildingData[buildingData.id].Active = false;
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
        AttacksRemaining.text = _totalAttacksRemaining.ToString()+" attacks remaining";
    }
}
