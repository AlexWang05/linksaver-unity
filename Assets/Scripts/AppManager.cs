using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AppManager : MonoBehaviour
{
    public GameObject AddCoursePanel;
    private int buttonNumber;
    
    // assigned via editor
    public TMP_InputField nameInputField, courseLinkInputField, meetingLinkInputField;
    public TMP_Text[] courseName = new TMP_Text[8];
    public TMP_Text[] courseInputText = new TMP_Text[3];

    // Start is called before the first frame update
    void Start()
    {
        // starts by hiding add course panel to show main panel
        if (AddCoursePanel)
        {
            AddCoursePanel.SetActive(false);
        }
        
        // initialize player preferences and course name text GameObjects
        for (int i = 1; i <= 8; i++)
        {
            PlayerPrefs.SetString("courseName" + i, "");
            PlayerPrefs.SetString("courseLink" + i, "");
            PlayerPrefs.SetString("meetingLink" + i, "");
        }

        updateCourseNames();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // updates the displayed course names to ones saved in player preferences
    void updateCourseNames()
    {
        for (int i = 1; i <= 8; i++)
        {
            // set course name
            courseName[i - 1].SetText(PlayerPrefs.GetString("courseName" + i));
        }
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

        // set textbox content to saved content
        nameInputField.text = PlayerPrefs.GetString("courseName" + buttonNumber);
        courseLinkInputField.text = PlayerPrefs.GetString("courseLink" + buttonNumber);
        meetingLinkInputField.text = PlayerPrefs.GetString("meetingLink" + buttonNumber);
    }

    // called when user saves and exits from Add Course Window
    public void CloseAddCourseWindow()
    {
        // gets information from all input fields
        string courseName = nameInputField.text;
        string courseLink = courseLinkInputField.text;
        string meetingLink = meetingLinkInputField.text;

        // saves in player preferences
        PlayerPrefs.SetString("courseName" + buttonNumber, courseName);
        PlayerPrefs.SetString("courseLink" + buttonNumber, courseLink);
        PlayerPrefs.SetString("meetingLink" + buttonNumber, meetingLink);

        // setActive to false for window
        AddCoursePanel.SetActive(false);

        // clear all fields since all courses use the same field
        nameInputField.text = "";
        courseLinkInputField.text = "";
        meetingLinkInputField.text = "";

        updateCourseNames();
    }

    // opens all links associated with course
    public void LaunchCourse()
    {
        int courseNum = GetCurrentObjectName();

        string courseLink = PlayerPrefs.GetString("courseLink" + courseNum);
        string meetingLink = PlayerPrefs.GetString("meetingLink" + courseNum);

        if (!courseLink.StartsWith("http"))
            courseLink = "https://" + courseLink;
        if (!meetingLink.StartsWith("http"))
            meetingLink = "https://" + meetingLink;

        Application.OpenURL(courseLink);
        Application.OpenURL(meetingLink);
    }

}
