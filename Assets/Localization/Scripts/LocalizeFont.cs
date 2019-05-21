using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.ResourceManagement.AsyncOperations;

[Serializable]
public class LocalizedAssetReferenceFont : LocalizedAssetReferenceT<Font> { }

public class LocalizeFont : LocalizationBehaviour
{
    [Serializable]
    public class LocalizationUnityEvent : UnityEvent<Font> { };

    [SerializeField]
    LocalizedAssetReferenceFont m_Reference = new LocalizedAssetReferenceFont();

    [SerializeField]
    LocalizationUnityEvent m_UpdateFont = new LocalizationUnityEvent();

    protected override void OnLocaleChanged(Locale newLocale)
    {

        var loadOp = m_Reference.LoadAssetAsync();
        loadOp.Completed += LoadCompleted;
    }

    private void LoadCompleted(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Font> obj)
    {
        if (obj.Status != AsyncOperationStatus.Succeeded)
        {
            var error = "Failed to load texture: " + m_Reference;
            if (obj.OperationException != null)
                error += "\n" + obj.OperationException;

            Debug.LogError(error, this);
            return;
        }

        m_UpdateFont.Invoke(obj.Result);
    }
}
