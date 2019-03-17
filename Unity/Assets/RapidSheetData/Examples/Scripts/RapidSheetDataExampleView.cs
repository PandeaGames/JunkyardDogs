/// 
/// File:				RapidSheetDataExampleView.cs
/// 
/// System:				Rapid Sheet Data (RSD) Unity3D client library
/// Version:			1.0.0
/// 
/// Language:			C#
/// 
/// License:				
/// 
/// Author:				Tasos Giannakopoulos (tasosg@voidinspace.com)
/// Date:				08 Mar 2017
/// 
/// Description:		
/// 


using UnityEngine;
using UnityEngine.UI;


namespace Lib.RapidSheetData.Examples
{
    /// 
    /// Class:       RapidSheetDataExampleView
    /// Description: 
    ///
    public class RapidSheetDataExampleView : MonoBehaviour
    {
        // 
        public System.Action OnPullDataClicked = delegate { };

        // 
        [SerializeField]
        private Text _scriptingBackendText = null;

        [SerializeField]
        private Text _offlineDataText = null;

        [SerializeField]
        private Text _liveDataText = null;

        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
#if ENABLE_IL2CPP
            _scriptingBackendText.text = "IL2CPP";
#endif

#if ENABLE_MONO
            _scriptingBackendText.text = "Mono";
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void SetOfflineDataText(string text)
        {
            _offlineDataText.text = string.Format("Offline Data:\n\n{0}", text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void SetLiveDataText(string text)
        {
            _liveDataText.text = string.Format("Live Data:\n\n{0}", text);
        }

        /// <summary>
        /// 
        /// </summary>
        public void PullDataButtonClicked()
        {
            if(OnPullDataClicked != null)
            {
                OnPullDataClicked();
            }
        }
    }
} /// Lib.RapidSheetData.Examples