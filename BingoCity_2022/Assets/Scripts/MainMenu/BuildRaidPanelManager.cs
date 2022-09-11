using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildRaidPanelManager : MonoBehaviour
{
    [SerializeField] private Button RaidButton;
    [SerializeField] private Button BuildButton;
    [SerializeField] private Button GoButton;
    [SerializeField] private Button RaidBackButton;
    [SerializeField] private GameObject RaidPanel;
    [SerializeField] private GameObject BuildPanel;
    [SerializeField] private GameObject RaidAttackPanel;
    private Action<GameObject> backButtonClicked;


    void Start()
    {
        backButtonClicked = OnBackButtonClicked;
        RaidButton.onClick.AddListener(RaidButtonClicked);
        BuildButton.onClick.AddListener(BuildButtonClicked);
        GoButton.onClick.AddListener(GoButtonClicked);
        RaidBackButton.onClick.AddListener(() => backButtonClicked.Invoke(RaidAttackPanel));
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

    private void OnBackButtonClicked(GameObject panel)
    {
        panel.SetActive(false);
    }
}
