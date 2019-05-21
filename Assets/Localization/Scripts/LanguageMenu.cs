using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;

public class LanguageMenu : MonoBehaviour
{
    public Dropdown dropDown;
    IEnumerator Start()
    {
        dropDown.enabled = false;
        dropDown.options = new List<Dropdown.OptionData>() { new Dropdown.OptionData("Loading") };
        dropDown.SetValueWithoutNotify(0);
        yield return LocalizationSettings.InitializationOperation;
        dropDown.enabled = true;

        dropDown.options.Clear();
        foreach(var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            dropDown.options.Add(new Dropdown.OptionData(locale.name));
        }

        SelectedLocaleChanged(LocalizationSettings.SelectedLocale);
        LocalizationSettings.SelectedLocaleChanged += SelectedLocaleChanged;
        dropDown.onValueChanged.AddListener(LocaleSelected);
    }

    private void SelectedLocaleChanged(Locale obj)
    {
        for(int i = 0; i < dropDown.options.Count; ++i)
        {
            if (dropDown.options[i].text == obj.name)
            {
                if (dropDown.value == i)
                    dropDown.RefreshShownValue();
                else
                    dropDown.SetValueWithoutNotify(i);
                return;
            }
        }
        Debug.LogError("Could not update locale dropdown, the selected locale could not be found: " + obj);
    }

    void LocaleSelected(int index)
    {
        var locale = LocalizationSettings.AvailableLocales.Locales.Find(l => l.name == dropDown.options[index].text);
        LocalizationSettings.SelectedLocale = locale;
    }
}
