using UnityEngine;
using UnityEngine.Advertisements;
using UnityUtils.Events;

namespace FingerFighter.Control.Combat.Flow.Revive
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
            if(showCompletionState != UnityAdsShowCompletionState.COMPLETED) return;
            Debug.Log("UnityAdsShowComplete"); // TODO find a way to test in editor 
            reward.Raise();
            LoadAd();
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.LogWarning("UnityAdsShowFailure");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.LogWarning("UnityAdsShowStart");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.LogWarning("UnityAdsShowClick");
        }
    }
}
