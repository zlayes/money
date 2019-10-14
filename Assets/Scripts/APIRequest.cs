using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;


namespace Money
{
    public class APIRequest : MonoBehaviourSingletonPersistent<APIRequest>
    {

            Dictionary<string, int> attemptsCounter = new Dictionary<string, int>();
            const int maxAttempts = 5;

            public void Get(string requestBody, string options = null, Action<string> onSucceed = null, Action onError = null, bool printSucceed = false, bool printError = false)
            {
                UnityWebRequest request = CreateBasicRequest(requestBody, UnityWebRequest.kHttpVerbGET);
                if (options != null)
                    request.url += options;
                StartCoroutine(InternalRequest(request, onSucceed, onError, printSucceed, printError));
            }

            IEnumerator InternalRequest(UnityWebRequest request, Action<string> onSucceed, Action onError, bool printSucceed = false, bool printError = false)
            {
                if (attemptsCounter.ContainsKey(request.url))
                    attemptsCounter[request.url]++;
                else
                    attemptsCounter[request.url] = 1;

                yield return request.SendWebRequest();

                if (request.isNetworkError || request.isHttpError)
                {
                    if (attemptsCounter[request.url] < maxAttempts)
                    {
                        print(request.error);
                        print("New attempt " + request.method + " " + attemptsCounter[request.url] + " for " + UnityWebRequest.UnEscapeURL(request.url));
                        StartCoroutine(InternalRequest(request.Clone(), onSucceed, onError, printSucceed, printError));
                    }
                    else
                    {
                        onError?.Invoke();
                        if (printError)
                            print("Failed code:" + request.responseCode + " after " + maxAttempts + " attempts " + request.error + " (" + UnityWebRequest.UnEscapeURL(request.url) + ")");

                        attemptsCounter.Remove(request.url);
                    }
                }
                else
                {
                    onSucceed?.Invoke(request.downloadHandler.text);
                    if (printSucceed)
                        print("print onSucceed : " + request.url + "\n" + request.downloadHandler.text);

                    attemptsCounter.Remove(request.url);
                }
            }

            UnityWebRequest CreateBasicRequest(string table, string method)
            {
                string url = GlobalConstants.API_URL + table;
                UnityWebRequest request = new UnityWebRequest(url, method);
                request.downloadHandler = new DownloadHandlerBuffer();
              //  request.SetRequestHeader("x-api-key", GlobalConstants.API_Key);
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json;charset=UTF-8");
                return request;
            }
        
    }
}
