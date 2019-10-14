using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Money
{
    public class GlobalConstants : MonoBehaviour
    {

        //Local paths
        public static string localAssetsPath = Application.persistentDataPath;

        //API               
        public static string API_URL = "https://api.exchangeratesapi.io/";
        public static string API_Key;

    }
}
