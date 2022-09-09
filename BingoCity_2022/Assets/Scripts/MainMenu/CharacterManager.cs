using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject CharacterCard;
    [SerializeField] private Transform Content;
    [SerializeField] private Image[] IconImages;
    [SerializeField] private List<CharacterCardScriptableObjects> Character;

    private void OnEnable()
    {
        List<Sprite> upgradedImages = new List<Sprite>();
        for (int i = 0; i < Character.Count; i++)
        {
            var prefab = Instantiate(CharacterCard, Content);
            bool upgraded = prefab.GetComponent<CharcterCardUI>().AssigningCharacterCardValues(Character[i].Ratings, 4,Character[i].CoinCount,Character[i].Character);
            if (upgraded)
            {
                upgradedImages.Add(Character[i].Character);
            }
        }
        UpdateUpgradedCard(upgradedImages);
    }

    private void UpdateUpgradedCard(List<Sprite> upgradedCharcter)
    {
        for (int i = 0; i < upgradedCharcter.Count; i++)
        {
            IconImages[i].sprite = upgradedCharcter[i];
        }
    }

    private void OnDisable()
    {
        foreach (Transform item in Content.GetComponentInChildren<Transform>())
        {
            Destroy(item.gameObject);
        }
    }
}
