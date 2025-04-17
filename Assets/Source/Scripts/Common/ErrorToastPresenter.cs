using System;
using EnvilopeChako.Modules.Authentication.Domain;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace EnvilopeChako.Common
{
    public class ErrorToastPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject toastPrefab;
        private IDisposable _sub;

        private void Awake()
        {
            _sub = ErrorBus.Errors.Subscribe(ShowToast);
        }

        private void ShowToast(Error err)
        {
            var go = Instantiate(toastPrefab, transform);
            var txt = go.GetComponentInChildren<Text>();
            txt.text = err.Message;
            Destroy(go, 3f);
        }

        private void OnDestroy()
        {
            _sub.Dispose();
        }
    }
}