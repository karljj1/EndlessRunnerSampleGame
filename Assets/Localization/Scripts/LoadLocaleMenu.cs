using UnityEngine;

public class LoadLocaleMenu : MonoBehaviour
{
    static GameObject localeMenu;

    public GameObject menu;

    void Start()
    {
        if (localeMenu == null)
        {
            localeMenu = Instantiate(menu);
            DontDestroyOnLoad(localeMenu);
        }
        Destroy(gameObject);
    }
}
