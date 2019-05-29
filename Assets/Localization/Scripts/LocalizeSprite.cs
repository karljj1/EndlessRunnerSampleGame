using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.ResourceManagement.AsyncOperations;

[Serializable]
public class LocalizedAssetReferenceSprite : LocalizedAssetReferenceT<Sprite> { }


public class LocalizeSprite : LocalizationBehaviour
{
    [Serializable]
    public class LocalizationUnityEvent : UnityEvent<Sprite> { };

    [SerializeField]
    LocalizedAssetReferenceSprite m_Reference = new LocalizedAssetReferenceSprite();

    [SerializeField]
    LocalizationUnityEvent m_UpdateSprite = new LocalizationUnityEvent();

    protected override void OnLocaleChanged(Locale newLocale)
    {
        var loadOp = m_Reference.LoadAssetAsync();
        loadOp.Completed += LoadCompleted;
    }

    private void LoadCompleted(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Sprite> obj)
    {
        if (obj.Status != AsyncOperationStatus.Succeeded)
        {
            var error = "Failed to load sprite: " + m_Reference;
            if (obj.OperationException != null)
                error += "\n" + obj.OperationException;

            Debug.LogError(error, this);
            return;
        }

        m_UpdateSprite.Invoke(obj.Result);
    }
}
