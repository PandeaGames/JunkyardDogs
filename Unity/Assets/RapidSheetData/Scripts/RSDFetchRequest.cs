/// 
/// File:				RSDFetchRequest.cs
/// 
/// System:				Rapid Sheet Data (RSD) Unity3D client library
/// Version:			1.0.0
/// 
/// Language:			C#
/// 
/// License:				
/// 
/// Author:				Tasos Giannakopoulos (tasosg@voidinspace.com)
/// Date:				10 Mar 2017
/// 
/// Description:		Implements synchronous and asynchronous fetch requests to the Rapid Sheet Data backend
/// 


using System.Collections;
using UnityEngine;


namespace Lib.RapidSheetData
{
    /// 
    /// Class:       RSDFetchRequest
    /// Description: 
    ///
    public abstract class RSDFetchRequest
    {
        /// 
        /// Factory
        /// 

        #region Factory

        /// <summary>
        /// Factory function to create a sync or async request depending on a valid caller
        /// </summary>
        /// <param name="serverURL"></param>
        /// <param name="spreadsheetId"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public static RSDFetchRequest Create(string serverURL, string spreadsheetId, MonoBehaviour caller = null)
        {
            if (caller != null)
            {
                return new RSDFetchRequestAsync(serverURL, spreadsheetId, caller);
            }

            return new RSDFetchRequestSync(serverURL, spreadsheetId);
        }

        /// <summary>
        /// Factory function to create a sync or async request depending on a valid caller
        /// </summary>
        /// <param name="serverURL"></param>
        /// <param name="spreadsheetId"></param>
        /// <param name="requestData"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        public static RSDFetchRequest Create(string serverURL, string spreadsheetId, string requestData, MonoBehaviour caller = null)
        {
            if (caller != null)
            {
                return new RSDFetchRequestAsync(serverURL, spreadsheetId, requestData, caller);
            }

            return new RSDFetchRequestSync(serverURL, spreadsheetId, requestData);
        }

        #endregion /// Factory

        // 
        private string _serverUrl = "";
        private string _spreadsheetId = "";
        private string _requestData = "";

        public RSDFetchRequest(string inServerUrl, string inSpreadsheetId)
        {
            _serverUrl = AddTrailingSlash(inServerUrl);
            _spreadsheetId = inSpreadsheetId;
        }

        public RSDFetchRequest(string inServerUrl, string inSpreadsheetId, string requestData)
        {
            _serverUrl = AddTrailingSlash(inServerUrl);
            _spreadsheetId = inSpreadsheetId;
            _requestData = requestData;
        }

        public abstract bool Fetch(System.Action<bool, string> onCompleted);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="onCompleted"></param>
        /// <returns></returns>
        public abstract bool FetchSheet(string sheetName, System.Action<bool, string> onCompleted);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        protected string ConstructFetchSheetURL(string sheetName)
        {
            return string.Format("{0}getData?spreadsheetId={1}&sheetName={2}",
                _serverUrl, _spreadsheetId, WWW.EscapeURL(sheetName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string ConstructFetchSheetURL()
        {
            return string.Format("{0}getSheets?spreadsheetId={1}&requestData={2}",
                _serverUrl, _spreadsheetId, WWW.EscapeURL(_requestData));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        string AddTrailingSlash(string url)
        {
            if (url[url.Length - 1] != '/')
            {
                url += "/";
            }
            return url;
        }
    }

    /// 
    /// Class:       RSDFetchRequestSync
    /// Description: 
    ///
    public class RSDFetchRequestSync : RSDFetchRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inSpreadsheetId"></param>
        public RSDFetchRequestSync(string inServerUrl, string inSpreadsheetId)
            : base(inServerUrl, inSpreadsheetId)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inServerUrl"></param>
        /// <param name="inSpreadsheetId"></param>
        /// <param name="requestData"></param>
        public RSDFetchRequestSync(string inServerUrl, string inSpreadsheetId, string requestData)
            : base(inServerUrl, inSpreadsheetId, requestData)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onCompleted"></param>
        /// <returns></returns>
        public override bool Fetch(System.Action<bool, string> onCompleted)
        {
            string targetUrl = ConstructFetchSheetURL();

            Debug.LogFormat("[RSDFetchRequestSync] FetchSheet : '{0}' ...", targetUrl);

            WWW www = new WWW(targetUrl);
            while (!www.isDone)
            { }

            if (string.IsNullOrEmpty(www.error))
            {
                onCompleted(true, www.text);
                return true;
            }
            else
            {
                Debug.LogErrorFormat("[RSDFetchRequestSync] FetchSheet : Server returned '{0}'", www.error);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="onCompleted"></param>
        /// <returns></returns>
        public override bool FetchSheet(string sheetName, System.Action<bool, string> onCompleted)
        {
            string targetUrl = ConstructFetchSheetURL(sheetName);

            Debug.LogFormat("[RSDFetchRequestSync] FetchSheet : '{0}' ...", targetUrl);

            WWW www = new WWW(targetUrl);
            while (!www.isDone) 
            { }

            if (string.IsNullOrEmpty(www.error))
            {
                onCompleted(true, www.text);
                return true;
            }
            else
            {
                Debug.LogErrorFormat("[RSDFetchRequestSync] FetchSheet : Server returned '{0}'", www.error);
            }

            return false;
        }
    }

    /// 
    /// Class:       RSDFetchRequestAsync
    /// Description: 
    ///
    public class RSDFetchRequestAsync : RSDFetchRequest
    {
        //
        private MonoBehaviour _caller = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inSpreadsheetId"></param>
        /// <param name="inCaller"></param>
        public RSDFetchRequestAsync(string inServerUrl, string inSpreadsheetId, MonoBehaviour inCaller)
            : base(inServerUrl, inSpreadsheetId)
        {
            _caller = inCaller;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inServerUrl"></param>
        /// <param name="inSpreadsheetId"></param>
        /// <param name="requestData"></param>
        /// <param name="inCaller"></param>
        public RSDFetchRequestAsync(string inServerUrl, string inSpreadsheetId, string requestData, MonoBehaviour inCaller)
            : base(inServerUrl, inSpreadsheetId, requestData)
        {
            _caller = inCaller;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onCompleted"></param>
        /// <returns></returns>
        public override bool Fetch(System.Action<bool, string> onCompleted)
        {
            if (_caller != null)
            {
                //string targetUrl = ConstructFetchSheetURL(sheetName);
                string targetUrl = ConstructFetchSheetURL();
                _caller.StartCoroutine(DoRequestCoroutine(targetUrl, onCompleted));
            }
            else
            {
                Debug.LogError("[RSDFetchRequestAsync] FetchSheet : Invalid caller passed. Please pass a valid MonoBehaviour as caller.");
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="onCompleted"></param>
        /// <returns></returns>
        public override bool FetchSheet(string sheetName, System.Action<bool, string> onCompleted)
        {
            if (_caller != null)
            {
                string targetUrl = ConstructFetchSheetURL(sheetName);
                _caller.StartCoroutine(DoRequestCoroutine(targetUrl, onCompleted));
            }
            else
            {
                Debug.LogError("[RSDFetchRequestAsync] FetchSheet : Invalid caller passed. Please pass a valid MonoBehaviour as caller.");
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetUrl"></param>
        /// <param name="onCompleted"></param>
        /// <returns></returns>
        IEnumerator DoRequestCoroutine(string targetUrl, System.Action<bool, string> onCompleted)
        {
            Debug.LogFormat("[RSDFetchRequestAsync] RequestCoroutine : '{0}' ...", targetUrl);

            WWW www = new WWW(targetUrl);
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                onCompleted(true, www.text);
                yield break;
            }
            else
            {
                Debug.LogErrorFormat("[RSDFetchRequestAsync] RequestCoroutine : Server returned '{0}'", www.error);
            }

            onCompleted(false, "{}");
        }
    }
} /// Lib.RapidSheetData