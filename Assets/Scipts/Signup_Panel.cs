using System;
using Backtory.Core.Public;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Backtory.InAppPurchase.Public;

public class Signup_Panel : MonoBehaviour {

    // Signup Panel
    public InputField usernameInput;
    public InputField emailInput;
    public InputField passwordInput;
    int m_DropdownValueage;
    int m_DropdownValuegen;
    string userID;

    public void GetAge(int valage)
    {
        m_DropdownValueage = valage;
        Debug.Log(m_DropdownValueage);
    }

    public void GetGen(int valgen)
    {
        m_DropdownValuegen = valgen;
        Debug.Log(m_DropdownValuegen);
    }
    public void onRegisterClick()
    {

        BacktoryUser currentUser = BacktoryUser.CurrentUser;
        userID = currentUser.UserId;


        // First create a user and fill his/her data
        BacktoryUser newUser = new BacktoryUser
        {

            Username = usernameInput.text,
            Email = emailInput.text,
            Password = passwordInput.text,

        };
        // Registring user to backtory (in background)
        newUser.RegisterInBackground(response =>
        {
            // Checking result of operation
            if (response.Successful)
            {
                Debug.Log("Register Success; new username is " + response.Body.Username);
                // Get current user of system
            }
            else if (response.Code == (int)BacktoryHttpStatusCode.Conflict)
            {
                // Username is invalid
                Debug.Log("Bad username; a user with this username already exists.");
            }
            else
            {
                // General failure
                Debug.Log("Registration failed; for network or some other reasons.");
            }
        });
    }

    public void saveAgegen()
    {

        BacktoryObject genderage = new BacktoryObject("GenderAge");
        genderage["gender"] = m_DropdownValueage;
        genderage["age"] = m_DropdownValuegen;
        genderage["userID"] = userID;

       
        genderage.SaveInBackground(response =>
        {
            if (response.Successful)
            {
                // successful save. good place for Debug.Log function.

            }
            else
            {
                // see response.Message to know the cause of error
            }
        });
    }
}
