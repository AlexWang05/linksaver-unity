using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AppManager : MonoBehaviour
{
    public GameObject AddCoursePanel;
    private int buttonNumber;

    // Start is called before the first frame update
    void Start()
    {
        // starts by hiding add course panel to show main panel
        if (AddCoursePanel)
        {
            AddCoursePanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // gets the name of the button that was clicked
    int GetCurrentObjectName()
    {
        // gets name of button
        string objectName = EventSystem.current.currentSelectedGameObject.name;

        // gets int value of last character in name
        return (int)Char.GetNumericValue(objectName[objectName.Length - 1]);
    }

    // onClick function for edit buttons
    public void LaunchAddCourseWindow()
    {
        // gets button clicked
        buttonNumber = GetCurrentObjectName();

        // launches add course panel
        if (AddCoursePanel != null)
        {
            AddCoursePanel.SetActive(true);
        }
        
    }


}
