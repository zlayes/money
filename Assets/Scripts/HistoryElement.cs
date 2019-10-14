using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Money
{
    public class HistoryElement : MonoBehaviour
    {

        [SerializeField] private TMP_Text originCurrencyName;
        [SerializeField] private TMP_Text originCurrencyValue;
        [SerializeField] private Image originCurFlag;


        [SerializeField] private TMP_Text destCurrencyName;
        [SerializeField] private TMP_Text destCurrencyValue;
        [SerializeField] private Image destCurFlag;

        public static HistoryElement current;
        private void Awake()
        {
            
        }
        public void Init(string oCurName, string oCurValue, string dCurName, string dCurValue, Sprite oFlag, Sprite dFlag)
        {
            originCurrencyName.text = oCurName;
            originCurrencyValue.text = oCurValue;

            destCurrencyName.text = dCurName;
            destCurrencyValue.text = dCurValue;


            originCurFlag.sprite = oFlag;
            destCurFlag.sprite = dFlag;
        }

    }
}
