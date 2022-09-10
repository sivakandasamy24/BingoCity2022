using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject CharacterCard;
    [SerializeField] private List<GameObject> UpgradeImageContainer;
    [SerializeField] private Transform Content;
    [SerializeField] private Image[] IconImages;
    [SerializeField] private CharacterCardScriptableObjects characterCardScriptableObjects;
    
    public CharacterCardScriptableObjects CharacterCardScriptableObjects => characterCardScriptableObjects;
    private CharacterManager _self;

    private void OnEnable()
    {
        _self = this;
        for (int i = 0; i < CharacterCardScriptableObjects.characterData.Count; i++)
        {
            var prefab = Instantiate(CharacterCard, Content);
            prefab.GetComponent<CharcterCardUI>().AssigningCharacterCardValues(CharacterCardScriptableObjects.characterData[i], _self);
        }
    }
    
    private void OnDisable()
    {
        foreach (Transform item in Content.GetComponentInChildren<Transform>())
        {
            Destroy(item.gameObject);
        }
    }

    public void AttachUpgradedCharacter(int index, Sprite upgradedImage)
    {
        var image = UpgradeImageContainer[index].GetComponent<Image>();
        if (image)
        {
            image.sprite = upgradedImage;
        }
    }
}
