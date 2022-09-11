using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace BingoCity
{
    public class CardItemManager : MonoBehaviour
    {
        [SerializeField] private List<ItemContainers> itemContainers;
        [SerializeField] private string revealAnimName;

        // private void Start()
        // {
        //     ResetItems();
        // }

        private void SetItemVisible(bool canMakeVisibleOn)
        {
            foreach (var item in itemContainers)
            {
                item.ItemParent.SetActive(canMakeVisibleOn);
            }
        }

        public void RevealItem(int itemId)
        {
            Observable.ReturnUnit()
                .Delay(TimeSpan.FromSeconds(0.5))
                .Do(_ =>
                {
                    var itemContainer = itemContainers.Find(x => x.PatternId == itemId);

                    if (itemContainer != null)
                    {
                        GameSummary.UpdateInventoryRewards(itemId,1);
                        
                        if (!gameObject.activeInHierarchy)
                        {
                            itemContainer.ItemParent.gameObject.SetActive(false);
                            return;
                        }

                        var animator = itemContainer.ItemParent.GetComponent<Animator>();
                        if (animator != null)
                        {
                            SoundUtils.PlaySoundOnce(AudioTrackNames.GsItemReveal);
                            animator.Play(revealAnimName);
                        }
                    }
                }).Subscribe().AddTo(this);
        }

        public void ResetItems()
        {
            SetItemVisible(false);
        }

        public void SetItemPosition(int itemId, Vector2 itemCellPosition)
        {
            var inventoryData = GameConfigs.InventoryAssetData.GetInventoryData(itemId);
            var itemContainer = itemContainers.Find(x => x.PatternId == itemId);
            if (itemContainer != null && !itemContainer.ItemParent.activeSelf)
            {
                itemContainer.ItemParent.GetComponent<RectTransform>().anchoredPosition = itemCellPosition;
                itemContainer.ItemParent.SetActive(true);
                if (inventoryData != null)
                    itemContainer.ItemImage.sprite = inventoryData.GsCardImage;
            }
        }
    }

    [Serializable]
    public class ItemContainers
    {
        [SerializeField] private int patternId;
        [SerializeField] private GameObject itemParent;
        [SerializeField] private Image itemImage;

        public int PatternId => patternId;
        public GameObject ItemParent => itemParent;

        public Image ItemImage
        {
            get => itemImage;
            set => itemImage = value;
        }
    }
}