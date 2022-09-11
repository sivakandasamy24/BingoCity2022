using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingData : MonoBehaviour
{
    public Image Attackimage;
    public List<GameObject> Stars;
    public ParticleSystem smoke;

    public void GotHit(int attackPower)
    {
        smoke.Play();

        for (int i = 0; i < attackPower; i++)
        {
            if (Stars.Count > 0)
            {
                Stars[Stars.Count - 1].SetActive(false);
                Stars.RemoveAt(Stars.Count - 1);
            }
        }
        if (Stars.Count <= 0)
        {
            Attackimage.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

}

