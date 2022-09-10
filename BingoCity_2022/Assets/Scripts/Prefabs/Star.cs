using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private GameObject ShillouteStar;
    [SerializeField] private GameObject OriginalStar;
    // Start is called before the first frame update
    void Awake()
    {
        ShillouteStar.SetActive(true);
        OriginalStar.SetActive(false);
    }

    public void SetStarActive()
    {
        ShillouteStar.gameObject.SetActive(false);
        OriginalStar.gameObject.SetActive(true);
    }

}
