using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class Web : MonoBehaviour
{
    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetDate("http://localhost/UnityTut/GetData.php"));
        //StartCoroutine(GetUsers("http://localhost/UnityTut/GetUsers.php"));
        //StartCoroutine(Login("testuser", "123456"));
        //StartCoroutine(RegisterUser("testuser3", "123456"));



    }

    //public void ShowUserItems()
    //{
       // StartCoroutine(GetItemIDs(Main.Instance.UserInfo.UserID));
    //}

    public IEnumerator GetDate(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    public IEnumerator GetUsers(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityTut/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                Main.Instance.UserInfo.SetInfo(username, password);
                Main.Instance.UserInfo.SetID(www.downloadHandler.text);

                if (www.downloadHandler.text.Contains("Wrong Credentials.") || www.downloadHandler.text.Contains("Username does not exist."))
                {

                    Debug.Log("Try Again");

                }
                else 
                { 
                    Main.Instance.UserProfile.SetActive(true);
                    Main.Instance.Login2.SetActive(false);
                }

            }
        }
    }

    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityTut/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetItemIDs(string userID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityTut/GetItemIDs.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                string jsonArray = www.downloadHandler.text;

                callback(jsonArray);

            }
        }
    }

    public IEnumerator GetItem(string itemID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemid", itemID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityTut/GetItem.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                string jsonArray = www.downloadHandler.text;

                callback(jsonArray);

            }
        }
    }

    public IEnumerator SellItem(string itemID, string userID, string ID)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemid", itemID);
        form.AddField("userid", userID);
        form.AddField("ID", ID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityTut/SellItem.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

            }
        }
    }
}
