using UnityEngine.Networking;

namespace Money
{
	public static class UnityWebRequestExtensions
	{
        public static UnityWebRequest Clone(this UnityWebRequest requestToClone)
        {
            UnityWebRequest newRequest = new UnityWebRequest()
            {
                method = requestToClone.method,
                url = requestToClone.url,
                downloadHandler = new DownloadHandlerBuffer(),                                
            };
            if(requestToClone.uploadHandler != null)
            {
                newRequest.uploadHandler = new UploadHandlerRaw(requestToClone.uploadHandler.data);
                newRequest.uploadHandler.contentType = "application/json";
            }
           // newRequest.SetRequestHeader("x-api-key", requestToClone.GetRequestHeader("x-api-key"));
            return newRequest;
        }
    }
}