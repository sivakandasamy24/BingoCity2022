using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingData : MonoBehaviour
{
    public Image Attackimage;
    public List<GameObject> Stars;
    public ParticleSystem smoke;
    private List<GameObject> _starList;

    private void Start()
    {
        ResetBuildingStats();
    }

    public void ResetBuildingStats()
    {
        Debug.Log(Stars.Count);
        _starList = Stars;
        foreach (var star in Stars)
        {
            star.SetActive(true);
        }
        Attackimage.gameObject.SetActive(true);
    }

    public int GotHit(int attackPower)
    {
        smoke.Play();
        int starsDestroyed = 0;
        for (int i = 0; i < attackPower; i++)
        {
            if (_starList.Count > 0)
            {
                _starList[_starList.Count - 1].SetActive(false);
                _starList.RemoveAt(_starList.Count - 1);
                starsDestroyed++;
                Debug.Log(Stars.Count);
            }
        }
        if (_starList.Count <= 0)
        {
            Attackimage.gameObject.SetActive(false);
        }
        return starsDestroyed;
    }

}

