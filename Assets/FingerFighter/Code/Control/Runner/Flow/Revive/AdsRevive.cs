using FingerFighter.View.Common;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityUtils.Events;

namespace FingerFighter.Control.Runner.Flow.Revive
{
    public class AdsRevive : MonoBehaviour, IUnityAdsShowListener
    {
        [SerializeField] private GameEvent request;
        [SerializeField] private GameEvent reward;

        private string _adUnitId;
        
        private void Awake()
        {
            _adUnitId = Application.platform == RuntimePlatform.IPhonePlayer
                ? "Rewarded_iOS"
                : "Rewarded_Android";
            LoadAd();
            request.RegisterAction(OnReviveRequest);
        }

        private void OnDestroy()
        {
            request.UnregisterAction(OnReviveRequest);
        }

        private void LoadAd()
        {
            Advertisement.Load(_adUnitId);
        }

        private void OnReviveRequest()
        {
            ShowAd();
        }

        private void ShowAd()
        {
            Advertisement.Show(_adUnitId, this);
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.SKIPPED:
                {
                    UiNotifications.Show("No reward on skipped ad.");
                    break;
                }
                case UnityAdsShowCompletionState.COMPLETED:
                {
                    Debug.Log("UnityAdsShowComplete"); 
                    reward.Raise();
                    LoadAd();
                    break;
                }
                case UnityAdsShowCompletionState.UNKNOWN:
                {
                    UiNotifications.Show("Unknown ad error.");
                    break;
                }
            }
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            UiNotifications.Show("Couldn't load ad.\nCheck web connection.");
            Debug.LogWarning("UnityAdsShowFailure");
            LoadAd();
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log("UnityAdsShowStart");
#if UNITY_EDITOR
            OnUnityAdsShowComplete("", UnityAdsShowCompletionState.COMPLETED);
#endif
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.Log("UnityAdsShowClick");
        }
    }
}
