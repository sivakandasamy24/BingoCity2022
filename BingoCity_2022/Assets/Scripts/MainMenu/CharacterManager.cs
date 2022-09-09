using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject CharacterCard;
    [SerializeField] private Transform Content;
    [SerializeField] private Image[] IconImages;
    [SerializeField]private Image image;
    private static int CharacterCount = 5;
    private string _imageLocation;

    private void OnEnable()
    {
        List<Image> upgradedImages = new List<Image>();
        for (int i = 0; i < CharacterCount; i++)
        {
            var prefab = Instantiate(CharacterCard, Content);
            bool upgraded = prefab.GetComponent<CharcterCardUI>().AssigningCharacterCardValues(3,4,50);
            if (upgraded)
            {
                upgradedImages.Add(image);
            }
        }
        UpdateUpgradedCard(upgradedImages);
    }

    private void UpdateUpgradedCard(List<Image> upgradedCharcter)
    {
        for (int i = 0; i < upgradedCharcter.Count; i++)
        {
            IconImages[i] = upgradedCharcter[i];
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
