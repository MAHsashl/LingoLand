using System;
using Backtory.Core.Public;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Backtory.InAppPurchase.Public;

public class SaveStuff : MonoBehaviour {


    public InputField usernameText;
    public InputField passwordText;
    string userText;
    string passText;

    void Start(){

        userText = PlayerPrefs.GetString("userTextKeyName");
        passText = PlayerPrefs.GetString("passTextKeyName");
        usernameText.text = userText;
        passwordText.text = passText;
         
    }

    public void SaveThis(){

        userText = usernameText.text;
        passText = passwordText.text;

        PlayerPrefs.SetString("userTextKeyName", userText);
        PlayerPrefs.SetString("passTextKeyName", passText);

    }

    public void onLoginClick()
    {
        // Pass user info to login 
        BacktoryUser.LoginInBackground(userText, passText, loginResponse => {

            // Login operation done (fail or success), handling it:
            if (loginResponse.Successful)
            {
                // Login successful
                Debug.Log("Welcome " + userText);
            }
            else if (loginResponse.Code == (int)BacktoryHttpStatusCode.Unauthorized)
            {
                // Username 'mohx' with password '123456' is wrong
                Debug.Log("Either username or password is wrong.");
            }
            else
            {
                // Operation generally failed, maybe internet connection issue
                Debug.Log("Login failed for other reasons like network issues.");
            }
        });
    }



}
