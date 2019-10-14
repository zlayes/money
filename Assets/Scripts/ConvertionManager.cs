using System.Globalization;
using TMPro;
using UnityEngine;

namespace Money
{
    public class ConvertionManager : MonoBehaviour
    {
        [SerializeField] private TransformData selectedDestination;
        [SerializeField] private TransformData selectedSouce;
        [SerializeField] private TMP_InputField currIn;
        [SerializeField] private TMP_InputField currOut;
        public static ConvertionManager current;

        private void Awake()
        {
            current = this;
        }

        // Update is called once per frame
        public void ConvertVal()
        {
            if (currIn.text == "")
            {
                currOut.text = "";
                return;
            }

            float amountIn = float.Parse(currIn.text, new CultureInfo("en-US").NumberFormat);
            float targetCurRatio = selectedDestination.Value.GetComponent<SetElementDetails>().GetCurrentValue();
            float result = amountIn * targetCurRatio;

            currOut.text = result.ToString();

            ExchangeRatesManager.current.AddToHistory(currIn.text.ToString(), currOut.text.ToString());
        }



    }
}