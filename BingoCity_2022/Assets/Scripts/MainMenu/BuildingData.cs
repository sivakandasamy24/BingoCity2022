using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingData : MonoBehaviour
{
    [SerializeField] private AttackCardScriptableObjects Attack;
    public Image Attackimage;
    public List<GameObject> Stars;
    public ParticleSystem smoke;
    private List<GameObject> _starList;
    public int id;

    private void Start()
    {
        //ResetBuildingStats();
    }

    public void SetStar(int starcount)
    {
        _starList = new List<GameObject>();
        for (var i = 0; i < starcount; i++)
        {
            Stars[i].gameObject.SetActive(true);
            _starList.Add(Stars[i]);
        }
        Attackimage.gameObject.SetActive(true);
    }

    public void ResetBuildingStats()
    {
        _starList = new List<GameObject>(Stars);
        foreach (var star in Stars)
        {
            star.SetActive(true);
        }
       
    }

    public int GotHit(int attackPower, int id)
    {
        smoke.Play();
        int starsDestroyed = 0;
        //var remainingStar = _starList - attackPower;
        if (_starList.Count < attackPower)
        {
            for (int i = 0; i <= _starList.Count; i++)
            {
                _starList[_starList.Count - 1].SetActive(false);
                _starList.RemoveAt(_starList.Count - 1);
                Attack.attackBuildingData[id].StarDestroyedCount++;
                starsDestroyed = Attack.attackBuildingData[id].StarDestroyedCount;
            }
        }
        else
        {
            for (int i = 0; i < attackPower; i++)
            {
                if (_starList.Count > 0)
                {
                    _starList[_starList.Count - 1].SetActive(false);
                   _starList.RemoveAt(_starList.Count - 1);
                    //starsDestroyed++;
                    Attack.attackBuildingData[id].StarDestroyedCount++;
                    starsDestroyed = Attack.attackBuildingData[id].StarDestroyedCount;
                    Debug.Log(Stars.Count);
                }
            }
        }
        
        /*for (int i = 0; i < attackPower; i++)
        {
            if (_starList.Count > 0)
            {
                _starList[_starList.Count - 1].SetActive(false);
                _starList.RemoveAt(_starList.Count - 1);
                starsDestroyed++;
                Debug.Log(Stars.Count);
            }
        }*/
        if (_starList.Count <= 0)
        {
            Attackimage.gameObject.SetActive(false);
        }

        return starsDestroyed;
    }

}

