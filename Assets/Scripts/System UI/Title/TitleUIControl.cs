using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIControl : MonoBehaviour
{
    public Button[] TitleButtons;
    public GameObject SettingUI;

    public Button TitleFirstSelected;
    public Button SettingFirstSelected;

    public void SettingIN()
    {
        foreach(Button button in TitleButtons)
        {
            button.enabled = false;
        }
        SettingUI.SetActive(true);

        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(SettingFirstSelected.gameObject);
        SettingFirstSelected.OnSelect(null);
    }

    public void SettingOut()
    {
        SettingUI.SetActive(false);
        foreach (Button button in TitleButtons)
        {
            button.enabled = true;
        }

        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(TitleFirstSelected.gameObject);
        TitleFirstSelected.OnSelect(null);
    }
}
