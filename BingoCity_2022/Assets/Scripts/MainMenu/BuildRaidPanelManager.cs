using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildRaidPanelManager : MonoBehaviour
{
    [SerializeField] private Button GoButton;
    [SerializeField] private Button RaidBackButton;
    [SerializeField] private GameObject RaidPanel;
    [SerializeField] private GameObject BuildPanel;
    [SerializeField] private GameObject RaidAttackPanel;
    private Action<GameObject> backButtonClicked;


    void Start()
    {
        backButtonClicked = OnBackButtonClicked;
        GoButton.onClick.AddListener(GoButtonClicked);
        RaidBackButton.onClick.AddListener(() => backButtonClicked.Invoke(RaidAttackPanel));
    }

    public void RaidButtonClicked()
    {
        RaidPanel.SetActive(true);
        BuildPanel.SetActive(false);
    }

    public void BuildButtonClicked()
    {
        BuildPanel.SetActive(true);
        RaidPanel.SetActive(false);
    }

    public void MainMenuButtonClicked()
    {
        BuildPanel.SetActive(false);
        RaidPanel.SetActive(false);
    }

    public void GoButtonClicked()
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
