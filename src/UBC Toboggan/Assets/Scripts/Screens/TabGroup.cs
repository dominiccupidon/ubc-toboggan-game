using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    List<Tab> tabs;
    Tab selectedTab;

    public void AddTab(Tab t, bool isDefaultTab)
    {
        if (tabs == null)
        {
            tabs = new List<Tab>();
        }

        tabs.Add(t);
        if (isDefaultTab)
        {
            selectedTab = t;
        }
    }

    public void OnTabEnter(Tab t)
    {
        ResetTabs();
        if (selectedTab != null && t != selectedTab)
        {
            t.OnHover();
        }
    }
    
    public void OnTabExit(Tab t)
    {
        ResetTabs();
    }

    public void OnTabSelected(Tab t)
    {
        selectedTab = t;
        ResetTabs();
        t.OnActive();
    }

    void ResetTabs()
    {
        foreach(Tab t in tabs)
        {
            if (selectedTab != null && t != selectedTab)
            {
                t.Reset();
            }
        }
    }
}
