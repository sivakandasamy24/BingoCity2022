using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BingoCity
{
    public class CardItemManager : MonoBehaviour
    {
        [SerializeField] private List<ItemContainers> itemContainers;
        [SerializeField] private string revealAnimName;

        private void Awake()
        {
            ResetItems();
        }

        private void SetItemVisible(bool canMakeVisibleOn)
        {
            foreach (var item in itemContainers)
            {
                item.ItemParent.SetActive(canMakeVisibleOn);
            }
        }

        public void RevealItem(int itemId)
        {
            var itemContainer = itemContainers.Find(x => x.PatternId == itemId);
            if (itemContainer != null )
            {
                var animator   = itemContainer.ItemParent.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.Play(revealAnimName);
                }
            }

        }
        public void ResetItems()
        {
            SetItemVisible(false);
        }

        public void SetItemPosition(int itemId, Vector2 itemCellPosition)
        {
            var itemContainer = itemContainers.Find(x => x.PatternId == itemId);
            if (itemContainer != null && !itemContainer.ItemParent.activeSelf)
            {
                itemContainer.ItemParent.GetComponent<RectTransform>().anchoredPosition = itemCellPosition;
                itemContainer.ItemParent.SetActive(true);
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
        public Image ItemImage => itemImage;
    }
}