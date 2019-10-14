using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Money
{
    public class SetElementDetails : MonoBehaviour
    {
        public enum ElementType
        {
            CurrencySource,
            CurrencyDestination
        }

        [SerializeField] private TMP_Text currencyName;
        [SerializeField] private TransformData selectedElement;
        [SerializeField] private Image backColor;
        [SerializeField] private Image curFlag;
        [SerializeField] private ElementType type;
        [SerializeField] private ListData flagList;

        [SerializeField] private BoolData isRemoteInTime;
        [SerializeField] private StringData todayDate;
        [SerializeField] private StringData selectedDate;

        //[SerializeField] private StringData selectedDate;

        public float value { get; set; }
        private bool init = false;

        public void SetNatureElementsDetails(string name, float curValue, Sprite flag)
        {
            currencyName.text = name;
            value = curValue;
            curFlag.sprite = flag;
        }

        public void Init()
        {
            selectedElement.Value = this.transform;
            selectedElement.Value = this.transform;
            ActiveElement();
        }

        public void SeletectCurrentElement()
        {
            if (selectedElement.Value != null)
            {
                selectedElement.Value.GetComponent<SetElementDetails>().InactiveElement();
            }

            selectedElement.Value = this.transform;
            ActiveElement();
            if (type == ElementType.CurrencyDestination)
            {
                ConvertionManager.current.ConvertVal();
            }
            else
            {
                if (isRemoteInTime.Value)
                    ExchangeRatesManager.current.SwitchCurren(currencyName.text.ToString());
                else
                    ExchangeRatesManager.current.SwitchCurrencyByDate(selectedDate.Value, todayDate.Value);
            }
        }

        public void InactiveElement()
        {
            backColor.color = new Color32(255, 255, 255, 255);
        }

        public void ActiveElement()
        {
            backColor.color = new Color32(128, 156, 169, 100);
        }

        public float GetCurrentValue()
        {
            return value;
        }

        public string GetCurName()
        {
            return currencyName.text;
        }

        public Sprite GetFlag()
        {
            return curFlag.sprite;
        }

        /*  public string GetCurrentMetaName()
          {
              string value = natureName.text;
              return value;
          }*/
    }
}