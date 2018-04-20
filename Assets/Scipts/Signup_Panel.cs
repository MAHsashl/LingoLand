using System;
using Backtory.Core.Public;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Backtory.InAppPurchase.Public;

public class Signup_Panel : MonoBehaviour
{

    const string usernameKey = "userKey";
    const string emailKey = "emailKey";
    const string passKey = "passKey";
    const string alreadyRegistered = "alreadyRegisteredKey";

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
                // save local
                PlayerPrefs.SetString(usernameKey, newUser.Username);
                PlayerPrefs.SetString(emailKey, newUser.Email);
                PlayerPrefs.SetString(passKey, newUser.Password);

                // register complated and we sould login now
                LoginProcess(newUser.Username, newUser.Password,true);


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

    public void LogInClick()
    {
        string username = ""; // TODO: Get username from loginUsernameInputField
        string pass = ""; // TODO: Get username from loginUsernameInputField
        LoginProcess(username, pass, false);
    }

    public void LoginProcess(string username, string password,bool newUser)
    {
        BacktoryUser.LoginInBackground(username, password, loginResponse =>
        {

            // Login operation done (fail or success), handling it:
            if (loginResponse.Successful)
            {
                // حالا آی دی کاربر را داریم و اگر اولین بار است که لاگین میکنیم باید سن و جنسیت را به بکتوری بفرستیم.
                if (PlayerPrefs.GetInt(alreadyRegistered) != 1)
                {
                    if (newUser)
                    {
                        saveAgegen();
                    }
                    else
                    {
                        // کاربری بوده که قبلا عضو بوده ولی به دلیل عوض کردن گوشی یا پاک کردن دیتاهای برنامه اطلاعات پلیرپریف آن پاک شده است و اگر سن و جنسیت را لوکال لازم داریم اینجا میتوان از روی دیتابیس بکتوری آن اطلاعات را بخوانی
                        //TODO: LoadAgeGen()
                    }
                }

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

    public void saveAgegen()
    {

        BacktoryObject genderage = new BacktoryObject("GenderAge");
        genderage["gender"] = m_DropdownValueage;
        genderage["age"] = m_DropdownValuegen;
        genderage["userID"] = BacktoryUser.CurrentUser.UserId;


        genderage.SaveInBackground(response =>
        {
            if (response.Successful)
            {
                PlayerPrefs.SetInt(alreadyRegistered, 1);
                // successful save. good place for Debug.Log function.

            }
            else
            {
                // see response.Message to know the cause of error
            }
        });
    }
}
