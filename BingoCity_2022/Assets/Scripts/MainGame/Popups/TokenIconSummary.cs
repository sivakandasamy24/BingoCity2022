using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BingoCity.Popups
{
    public class TokenIconSummary:MonoBehaviour
    {
        [SerializeField] private Image tokenIcon;
        [SerializeField] private TextMeshProUGUI tokenText;

        public void SetData(Sprite source,int count)
        {
            tokenIcon.sprite = source;
            tokenText.text = count.ToString();
        }
        
    }
}