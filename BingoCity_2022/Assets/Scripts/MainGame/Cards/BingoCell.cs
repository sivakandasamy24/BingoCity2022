using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
namespace BingoCity
{
   
       
    public class BingoCell : MonoBehaviour
    {
        [SerializeField] private GameObject cellBg;
        [SerializeField] private GameObject daubedIcon;
        [SerializeField] private GameObject winBingoIcon;
        [SerializeField] private Text cellNumberText;
        [SerializeField] private Text debugCellIdTxt;
        [SerializeField] private GameObject daubAnimationPrefab ;
        
        private readonly ReactiveProperty<int> _cellNumber = new ReactiveProperty<int>();
        private readonly ReactiveProperty<int> _cellId = new ReactiveProperty<int>();

        public int CellId => _cellId.Value;
        public bool IsDaubed => _isDaubed;

        private bool _isDaubed;
        private Action<int> _daubCallBack;
        private void Awake()
        {
            _cellNumber.SubscribeToText(cellNumberText);//update direct value
            _cellId.SubscribeToText(debugCellIdTxt);//update direct value
        }

        public void SetData(int cellNumber,int cellId,Action<int> daubCallBack)
        {
            UpdateCellDaubUI(false);
            _cellNumber.Value = cellNumber;
            _cellId.Value = cellId;
            _daubCallBack = daubCallBack;

        }

        public void OnUserCellDaub()
        {
            _daubCallBack?.Invoke(_cellNumber.Value);
        }

        public void DoMarkCellAsDaub()
        {
            UpdateCellDaubUI(true);
            // Destroy(gameObject);
        }

        private void UpdateCellDaubUI(bool isDaubed)
        {
            _isDaubed = isDaubed;
            cellBg.SetActive(!isDaubed);
            cellNumberText.gameObject.SetActive(!isDaubed);
            //debugCellIdTxt.gameObject.SetActive(!isDaubed);
            daubedIcon.gameObject.SetActive(isDaubed);
            daubAnimationPrefab.SetActive(isDaubed);
        }

        public void ShowWinBingoIcon()
        {
            daubedIcon.gameObject.SetActive(false);
            winBingoIcon.SetActive(true);
        }
    }
}