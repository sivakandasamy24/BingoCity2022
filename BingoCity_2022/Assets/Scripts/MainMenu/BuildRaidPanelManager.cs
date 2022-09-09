using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildRaidPanelManager : MonoBehaviour
{
    [SerializeField] private Button RaidButton;
    [SerializeField] private Button BuildButton;
    [SerializeField] private Button GoButton;
    [SerializeField] private GameObject RaidPanel;
    [SerializeField] private GameObject BuildPanel;
    [SerializeField] private GameObject RaidAttackPanel;


    void Start()
    {
        RaidButton.onClick.AddListener(RaidButtonClicked);
        BuildButton.onClick.AddListener(BuildButtonClicked);
        GoButton.onClick.AddListener(GoButtonClicked);
    }

    private void RaidButtonClicked()
    {
        RaidPanel.SetActive(true);
        BuildPanel.SetActive(false);
    }
    private void BuildButtonClicked()
    {
        BuildPanel.SetActive(true);
        RaidPanel.SetActive(false);
    }
    private void GoButtonClicked()
    {
        RaidAttackPanel.SetActive(true);
        RaidPanel.SetActive(false);
        BuildPanel.SetActive(false);
    }
}
