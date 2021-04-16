using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GoodEventSystem : MonoBehaviour
{
    public EventSystem eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(FirstSelect());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select(Button myButton)
    { // Select the button
        myButton.Select(); // Or EventSystem.current.SetSelectedGameObject(myButton.gameObject)
                           // Highlight the button
        myButton.OnSelect(null);
    }
    public void SelectSlider(Slider slider)
    { // Select the button
        slider.Select(); // Or EventSystem.current.SetSelectedGameObject(myButton.gameObject)
                         // Highlight the button
        slider.OnSelect(null);
    }


    IEnumerator FirstSelect()
    {
        eventSystem.SetSelectedGameObject(null);
        yield return null;
        eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
    }
}
