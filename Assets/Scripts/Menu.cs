using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System.Text;
using System;

[System.Serializable]
public class UserProfile
{
    public string ID;
    public string Password;
    public string Email;
}

public class Menu : MonoBehaviour
{
    [SerializeField] TMP_InputField username;
    [SerializeField] TMP_InputField password;
    [SerializeField] GameObject loginPanel;
    [SerializeField] GameObject loginPromp;
    [SerializeField] GameObject loginBtn;
    [SerializeField] GameObject registerBtn;
    [SerializeField] GameObject email;
    [SerializeField] GameObject profileBoard;
    [SerializeField] Text profileName;
    [SerializeField] Text profileScore;
    [SerializeField] Text profileEmail;


    public void GoToTraining()
    {
        SceneManager.LoadScene("Training");
    }

    public void SignUp()
    {
        loginPromp.SetActive(false);
        registerBtn.SetActive(true);
        loginBtn.SetActive(false);
        email.SetActive(true);
    }

    public void Logout()
    {
        StartCoroutine(GetUnityTest());
    }

    public void Login()
    {
        loginPromp.SetActive(false);
        StartCoroutine(UnityPOST());
    }

    public void Register()
    {
        StartCoroutine(RegisterPUT());
    }

    public void Profile()
    {
        if (profileBoard.activeSelf)
            profileBoard.SetActive(false);
        else StartCoroutine(GetProfile());
    }

    

    IEnumerator GetUnityTest()
    {
        string link = "https://nhgg4aqoy7.execute-api.us-east-1.amazonaws.com/default/LogInOut?Username=" + PlayerPrefs.GetString("Username");
        UnityWebRequest www = UnityWebRequest.Get(link);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            loginPanel.SetActive(true);
            Debug.Log(www.downloadHandler.text);
        }

    }



    IEnumerator GetProfile()
    {
        string link = "https://ceqc731jn9.execute-api.us-east-1.amazonaws.com/default/ScoreManager?GameName=Break Pong&Username=" + PlayerPrefs.GetString("Username");
        UnityWebRequest www = UnityWebRequest.Get(link);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {

            string jsonString = " {\"GameName\": \"" + "Break Pong" + "\", \"Username\": \"" + username.text + "\", \"Score\": \"" + 0 + "\"}";
            Debug.Log(jsonString);
            byte[] myData = Encoding.UTF8.GetBytes(jsonString);
            UnityWebRequest send = UnityWebRequest.Put("https://r7hp7byop1.execute-api.us-east-1.amazonaws.com/default/UserManager", myData);

            send.SetRequestHeader("Content-Type", "application/json");
            yield return send.SendWebRequest();

            profileBoard.SetActive(true);
            profileName.text = "Username: " + PlayerPrefs.GetString("Username") + "\n" + "Password: " + PlayerPrefs.GetString("Password");
            //profilePassword.text = "Password: " + PlayerPrefs.GetString("Password");
            profileScore.text = "Score: 0";
        }
        else
        {
            string data = www.downloadHandler.text;
            int foundS1 = data.IndexOf("(");
            data = data.Substring(foundS1 + 1, data.Length - 34 - foundS1);

            profileBoard.SetActive(true);
            profileName.text = "Username: " + PlayerPrefs.GetString("Username") + "\n" + "Password: " + PlayerPrefs.GetString("Password");
            //profilePassword.text = "Password: " + PlayerPrefs.GetString("Password");
            profileScore.text = "Score: " + data;
        }

    }

    IEnumerator UnityPOST()
    {
        string jsonString = " {\"Username\": \"" + username.text + "\", \"Password\": \"" + password.text + "\"}";
        Debug.Log(jsonString);
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(jsonString);
        UnityWebRequest www = UnityWebRequest.Put("https://nhgg4aqoy7.execute-api.us-east-1.amazonaws.com/default/LogInOut", myData);

        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            loginPromp.GetComponent<Text>().text = "Username or Password is incorrect";
            loginPromp.SetActive(true);
            Debug.Log(www.error);
        }
        else
        {
            PlayerPrefs.SetString("Username", username.text);
            PlayerPrefs.SetString("Password", password.text);
            loginPanel.SetActive(false);
            Debug.Log(www.downloadHandler.text);
            username.text = "";
            password.text = "";
        }
    }

    IEnumerator RegisterPUT()
    {
        string jsonString = " {\"Email\": \"" + email.GetComponentInChildren< TMP_InputField >().text + "\", \"Username\": \"" + username.text + "\", \"Password\": \"" + password.text + "\"}";
        Debug.Log(jsonString);
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(jsonString);
        UnityWebRequest www = UnityWebRequest.Put("https://r7hp7byop1.execute-api.us-east-1.amazonaws.com/default/UserManager", myData);

        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            
            loginPromp.SetActive(true);
            loginPromp.GetComponent<Text>().text = "Input is not valid";
            Debug.Log(www.error);
        }
        else
        {
            registerBtn.SetActive(false);
            loginBtn.SetActive(true);
            email.SetActive(false);
            Debug.Log(www.downloadHandler.text);
            username.text = "";
            password.text = "";
        }
    }
}

