using System;
using Backtory.Core.Public;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Backtory.InAppPurchase.Public;
using System.Text.RegularExpressions;
using System.Net.Mail;

public class Signup_Panel : MonoBehaviour
{

    const string usernameKey = "userKey";
    const string emailKey = "emailKey";
    const string passKey = "passKey";
    const string alreadyRegistered = "alreadyRegisteredKey";

    // Signup Panel
    public InputField usernameInputreg;
    public InputField emailInputreg;
    public InputField passwordInputreg;
    public Dropdown ageDropDown;
    public Dropdown sexDropDown;

    // Login Panel
    public InputField usernameInputlog;
    public InputField passwordInputlog;

    //Popup Window
    public GameObject englishusername;
    public GameObject emptyusername;
    public GameObject emptyemail;
    public GameObject emptypassword;
    public GameObject falsemail;




    public void Start()
    {
#if UNITY_EDITOR
        PlayerPrefs.DeleteAll();
#endif

       // if(olduser){
          //  LoginProcess();
     //   }

    }


    public void onRegisterClick()
    {

        // First create a user and fill his/her data
        BacktoryUser newUser = new BacktoryUser
        {

            Username = usernameInputreg.text,
            Email = emailInputreg.text,
            Password = passwordInputreg.text,

        };

        if(Regex.IsMatch(usernameInputreg.text, "^[a-zA-Z0-9]*$") && (usernameInputreg.text != "") && (emailInputreg.text != "") && (passwordInputreg.text != "") && (IsValidEmail(emailInputreg.text))){

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

                    // register complated and we should login now
                    LoginProcess(newUser.Username, newUser.Password, true);


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
        }else if(!(Regex.IsMatch(usernameInputreg.text, "^[a-zA-Z0-9]*$")))
        {
            Showenglishusername();
            //Debug.Log("Oops");
        }else if((usernameInputreg.text == "")){
            Showemptyusername();
            //Debug.Log("Oops");
        }else if((emailInputreg.text == ""))
        {
            Showemptyemail();
            //Debug.Log("Oops");
        }else if(!(IsValidEmail(emailInputreg.text)))
        {
            Showfalsemail();
            //Debug.Log("Oops");
        }else if((passwordInputreg.text == ""))
        {
            Showemptypassword();
            //Debug.Log("Oops");
        }

    }

    public void LogInClick()
    {
        string username = usernameInputlog.text; // TODO: Get username from loginUsernameInputField
        string pass = passwordInputlog.text; // TODO: Get username from loginUsernameInputField
        if (Regex.IsMatch(usernameInputlog.text, "^[a-zA-Z0-9]*$") && (usernameInputlog.text != "") && (passwordInputlog.text != "")){
            LoginProcess(username, pass, false);
        }else if (!(Regex.IsMatch(usernameInputlog.text, "^[a-zA-Z0-9]*$")))
        {
            Showenglishusername();
            //Debug.Log("Oops");
        }else if ((usernameInputlog.text == ""))
        {
            Showemptyusername();
            //Debug.Log("Oops");
        }else if ((passwordInputlog.text == ""))
        {
            Showemptypassword();
            //Debug.Log("Oops");
        }

    }

    public void LoginProcess(string username, string password,bool newUser)
    {
        
        BacktoryUser.LoginInBackground(username, password, loginResponse =>
        {

            // Login operation done (fail or success), handling it:
            if (loginResponse.Successful)
            {
                Debug.Log("Login Successful.");
                //We have UserId and if it is the first time that he logs in, we should send age and gender to Backtory.
                if (PlayerPrefs.GetInt(alreadyRegistered) != 1)
                {
                    if (newUser)
                    {
                        saveAgegen();
                    }
                    //If the user is a member of service and because of exchanging his phone or clearing his playerprefs' data,
                    //we can read his age/gen data locally. 
                    else
                    {
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
        genderage["gender"] = sexDropDown.value;
        genderage["age"] = ageDropDown.value;
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

    public void Showenglishusername(){
        
        englishusername.SetActive(true);
    }
    public void Showemptyusername()
    {

        emptyusername.SetActive(true);
    }
    public void Showemptyemail()
    {

        emptyemail.SetActive(true);
    }
    public void Showemptypassword()
    {

        emptypassword.SetActive(true);
    }
    public void Showfalsemail()
    {

        falsemail.SetActive(true);
    }

    bool IsValidEmail(string email)
    {
        try
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
