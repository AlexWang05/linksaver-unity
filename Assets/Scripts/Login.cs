using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class Login : MonoBehaviour
{
    
    // assigned via editor
    public TMP_InputField emailField, passwordField;
    public TMP_Text emailText;
    public TMP_Text passwordText;

    public TMP_Text placeholderEmail, placeholderPassword;

    private int inputSelected;

    [SerializeField] string formURL;

    // Start is called before the first frame update
    void Start()
    {
        formURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfLsejFcaimOPAr1p9FAUvXQFxt2tdi-JLl-cpBGOJYrQ7-Iw/formResponse";

        string storedEmail = PlayerPrefs.GetString("email");
        string storedPassword = PlayerPrefs.GetString("password");

        Debug.Log(storedEmail+ " " + storedPassword);

        // set placeholder text to what was previously stored
        placeholderEmail.SetText(storedEmail);
        placeholderPassword.SetText(storedPassword);

    }

    // Update is called once per frame
    void Update()
    {
        // tab and shift-tab switching
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inputSelected == 0)
            {
                inputSelected++;
            }
            else if (inputSelected == 1)
            {
                inputSelected--;
            }
            else
            {
                Debug.Log("Input Selected: " + inputSelected);
            }
            // inputSelected++;
            // if (inputSelected > 1) inputSelected = 0;
            selectInputField();
        }

        void selectInputField()
        {
            switch (inputSelected)
            {
                case 0:
                    emailField.Select();
                    break;
                case 1:
                    passwordField.Select();
                    break;
            }
        }
    }

    public void emailInputSelected() => inputSelected = 0;
    public void passwordInputSelected() => inputSelected = 1;

    IEnumerator Post(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1446210731", email);
        form.AddField("entry.419014447", password);

        UnityWebRequest www = UnityWebRequest.Post(formURL, form);
        yield return www.SendWebRequest();
    }


    // OnClick() Function for Log In Button
    public void LaunchApp()
    {
        // save player data
        string email = emailField.text;
        string password = passwordField.text;

        // post to database
        StartCoroutine(Post(email, password));

        // save local preferences
        PlayerPrefs.SetString("email", email);
        PlayerPrefs.SetString("password", password);

        // launch main screen
        SceneManager.LoadScene("Main Menu");
    }

}
