using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Money
{
    public class ExchangeRatesManager : MonoBehaviour
    {
        [SerializeField] private BoolData isConnectedBuildData;
        [SerializeField] private TransformData selectedDestination;
        [SerializeField] private TransformData selectedSouce;

        [SerializeField] private Transform sourceCurrList;
        [SerializeField] private Transform destCurrList;
        [SerializeField] private Transform historyList;

        [SerializeField] private GameObject sourceElementPrefab;
        [SerializeField] private GameObject destElementPrefab;
        [SerializeField] private GameObject historyElementPrefab;

        [SerializeField] private Dictionary<string, CurrencyDataSet> curencyDico = new Dictionary<string, CurrencyDataSet>();
        [SerializeField] private Dictionary<CurrencyDataSet, GameObject> sourceCurrencyItem = new Dictionary<CurrencyDataSet, GameObject>();
        [SerializeField] private Dictionary<string, GameObject> destCurrencyItem = new Dictionary<string, GameObject>();
        [SerializeField] private Dictionary<string, GameObject> sourCurrencyItem = new Dictionary<string, GameObject>();
        [SerializeField] private CurencyList defaultSrcCurencySelected;

        [SerializeField] private List<Sprite> flagList;
        [SerializeField] private ListData flaglistData;
        [SerializeField] private GameObject docGb;
        [SerializeField] private GameObject reconnecButton;

        public static ExchangeRatesManager current;

        [SerializeField] private TMP_Text helloText;

        // Start is called before the first frame update
        private void Awake()
        {
            current = this;
            UpdateApiRates();
        }

        public void UpdateApiRates()
        {

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                isConnectedBuildData.Value = false;
                helloText.text = "Vous etes hors ligne,veuillez vous connecter afin d'avoir les taux actualisées";
                LoadApiRates();
                reconnecButton.SetActive(true);
            }
            else
            {
                isConnectedBuildData.Value = true;
                helloText.text = "";
                GetAllCurrency();
                reconnecButton.SetActive(false);
            }
        }

        private void LoadApiRates()
        {
            //Load From local database to optimise app load 
        }
        private void SaveApiRates()
        {
            //Save To local database to optimise app load 
        }



        private IEnumerator LateCall()
        {
            yield return new WaitForSeconds(1);
            docGb.SetActive(false);
        }

        public void SwitchCurrencyByDate(string selecteDate, string todayDate)
        {
            if (DateTime.Parse(todayDate) < DateTime.Parse(selecteDate))
            {
                docGb.SetActive(true);
                StartCoroutine(LateCall());
                return;
            }

            string newCurrency = selectedSouce.Value.GetComponent<SetElementDetails>().GetCurName();

            if (newCurrency == null)
                return;

            selectedDestination.Value = null;
            selectedSouce.Value = null;
            //curencyDico.Clear();
            sourceCurrencyItem.Clear();
            destCurrencyItem.Clear();
            GetOutDatedSpecific(newCurrency, selecteDate);

            foreach (Transform child in sourceCurrList.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            foreach (Transform child in destCurrList.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public void SwitchCurren(string newCurrency)
        {
            if (newCurrency == null)
                return;

            selectedDestination.Value = null;
            selectedSouce.Value = null;
            //curencyDico.Clear();
            sourceCurrencyItem.Clear();
            destCurrencyItem.Clear();
            GetSpecific(newCurrency);

            foreach (Transform child in sourceCurrList.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            foreach (Transform child in destCurrList.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public void SearchSourceCur(TMP_InputField searchingField)
        {
            if (searchingField.text == "" || searchingField.text == "*")
            {
                foreach (GameObject item in sourCurrencyItem.Values)
                    item.SetActive(true);
            }
            else
            {
                string sanitizedField = searchingField.text.Sanitize();
                foreach (string meta in sourCurrencyItem.Keys)
                    sourCurrencyItem[meta].SetActive(meta.Sanitize().Contains(sanitizedField) || meta.ToString().Sanitize().Contains(sanitizedField));
            }
        }

        public void SearchDestCur(TMP_InputField searchingField)
        {
            if (searchingField.text == "" || searchingField.text == "*")
            {
                foreach (GameObject item in destCurrencyItem.Values)
                    item.SetActive(true);
            }
            else
            {
                string sanitizedField = searchingField.text.Sanitize();
                foreach (string meta in destCurrencyItem.Keys)
                    destCurrencyItem[meta].SetActive(meta.Sanitize().Contains(sanitizedField) || meta.ToString().Sanitize().Contains(sanitizedField));
            }
        }

        private void GetOutDatedSpecific(string newCurrency, string date)
        {
            curencyDico.Clear();
            Action<string> onSucceed = OnApiSucceed;
            string option = date + "?base=" + newCurrency;
            APIRequest.Instance.Get("", option, (string jsonResponse) => OnGetGetAllCurrency(jsonResponse), () => OnAPIRequestFailed(), printError: true);
        }

        private void GetSpecific(string newCurrency)
        {
            curencyDico.Clear();
            Action<string> onSucceed = OnApiSucceed;
            string option = "?base=" + newCurrency;
            APIRequest.Instance.Get("latest", option, (string jsonResponse) => OnGetGetAllCurrency(jsonResponse), () => OnAPIRequestFailed(), printError: true);
        }

        private void GetAllCurrency()
        {
            curencyDico.Clear();
            Action<string> onSucceed = OnApiSucceed;
            string option = "";
            APIRequest.Instance.Get("latest", option, (string jsonResponse) => OnGetGetAllCurrency(jsonResponse), () => OnAPIRequestFailed(), printError: true);
        }

        private void OnGetGetAllCurrency(string jsonResponse)
        {
            CurrencyDataSet currentCurency = JsonConvert.DeserializeObject<CurrencyDataSet>(jsonResponse);
            if (currentCurency.Base != null)
            {
                curencyDico.Add(currentCurency.Base, currentCurency);
            }
            FillLists();
        }

        private void FillLists()
        {
            sourCurrencyItem.Clear();
            destCurrencyItem.Clear();

            foreach (KeyValuePair<string, CurrencyDataSet> entry in curencyDico)
            {
                Sprite tempFalg = flagList.Where(obj => obj.name.ToUpper() == entry.Key.ToString()).SingleOrDefault();
                GameObject newElementOflist = Instantiate(sourceElementPrefab, sourceCurrList, false);
                newElementOflist.GetComponent<SetElementDetails>().SetNatureElementsDetails(entry.Key.ToString(), 1.00f, tempFalg);
                newElementOflist.GetComponent<SetElementDetails>().Init();
                sourceCurrencyItem.Add(entry.Value, newElementOflist);
            }

            foreach (KeyValuePair<string, CurrencyDataSet> entry in curencyDico)
            {
                foreach (KeyValuePair<string, double> curen in entry.Value.Rates)
                {
                    Sprite tempFalg = flagList.Where(obj => obj.name.ToUpper() == curen.Key.ToString()).SingleOrDefault();
                    Sprite tempFalg2 = flagList.Where(obj => obj.name.ToUpper() == curen.Key.ToString()).SingleOrDefault();
                    GameObject newElementDestOflist = Instantiate(destElementPrefab, destCurrList, false);
                    GameObject newElementSourOflist = Instantiate(sourceElementPrefab, sourceCurrList, false);

                    newElementSourOflist.GetComponent<SetElementDetails>().SetNatureElementsDetails(curen.Key.ToString(), (float)curen.Value, tempFalg);
                    newElementDestOflist.GetComponent<SetElementDetails>().SetNatureElementsDetails(curen.Key.ToString(), (float)curen.Value, tempFalg2);

                    sourCurrencyItem.Add(curen.Key.ToString(), newElementSourOflist);
                    destCurrencyItem.Add(curen.Key.ToString(), newElementDestOflist);
                }
            }
            InitiatStartingCurency();
        }

        private void InitiatStartingCurency()
        {
            foreach (KeyValuePair<string, GameObject> element in destCurrencyItem)
            {
                if (element.Key == defaultSrcCurencySelected.ToString())
                {
                    element.Value.transform.SetAsFirstSibling();
                    element.Value.GetComponent<SetElementDetails>().Init();
                }
            }
            ConvertionManager.current.ConvertVal();
            SaveApiRates();
        }

        public void AddToHistory(string source, string destination)
        {
            string sCurName = selectedSouce.Value.GetComponent<SetElementDetails>().GetCurName();
            string dCurName = selectedDestination.Value.GetComponent<SetElementDetails>().GetCurName();

            Sprite sFlag = selectedSouce.Value.GetComponent<SetElementDetails>().GetFlag();
            Sprite dFlag = selectedDestination.Value.GetComponent<SetElementDetails>().GetFlag();

            GameObject newElementHistory = Instantiate(historyElementPrefab, historyList, false);
            newElementHistory.GetComponent<HistoryElement>().Init(sCurName, source, dCurName, destination, sFlag, dFlag);

        }

        private void OnApiSucceed(string result = null)
        {
            print("Template upload successful");
        }

        private void OnAPIRequestFailed()
        {
            print("Template failed successful");
        }
    }
}