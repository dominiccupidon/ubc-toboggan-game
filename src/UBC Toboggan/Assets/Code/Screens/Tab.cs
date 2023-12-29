using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class Tab : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    public GameObject body;
    Image background;
    TextMeshProUGUI prompt;
    Color dark;

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        prompt = GetComponentInChildren<TextMeshProUGUI>();
        float cv = 50f / 255f;
        dark = new Color(cv, cv, cv);

        if (body.activeInHierarchy)
        {
            OnActive();
        }
        tabGroup.AddTab(this, body.activeInHierarchy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnActive()
    {
        background.color = dark;
        prompt.color = Color.white;
        body.SetActive(true);
    }

    public void OnHover()
    {
        background.color = dark;
        prompt.color = Color.grey;
    }

    public void Reset()
    {
        background.color = Color.white;
        prompt.color = dark; 
        body.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }
}
