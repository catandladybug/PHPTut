using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using SimpleJSON;

public class Items : MonoBehaviour
{
    Action<string> _createItemsCallback;

    // Start is called before the first frame update
    void Start()
    {
        _createItemsCallback = (jsonArrayString) => { 
        
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        
        };

        CreateItems();
    }

    public void CreateItems()
    {
        string userID = Main.Instance.UserInfo.UserID;
        StartCoroutine(Main.Instance.Web.GetItemIDs(userID, _createItemsCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++) 
        {

            bool isDone = false;
            string itemId = jsonArray[i].AsObject["itemid"];
            string id = jsonArray[i].AsObject["ID"];

            JSONObject itemInfoJson = new JSONObject();

            Action<string> getItemInfoCallback = (itemInfo) =>
            {

                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;

            };

            StartCoroutine(Main.Instance.Web.GetItem(itemId, getItemInfoCallback));

            yield return new WaitUntil(() => isDone == true);

            GameObject item = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            Item item1 = item.AddComponent<Item>();

            item1.ID = id;
            item1.ItemID = itemId;

            item.transform.SetParent(this.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;

            item.transform.Find("Name").GetComponent<TMP_Text>().text = itemInfoJson["name"];
            item.transform.Find("Price").GetComponent<TMP_Text>().text = itemInfoJson["price"];
            item.transform.Find("Description").GetComponent<TMP_Text>().text = itemInfoJson["description"];

            item.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                string iId = itemId;
                string idInInventory = id;
                string uId = Main.Instance.UserInfo.UserID;

                StartCoroutine(Main.Instance.Web.SellItem(iId,uId,idInInventory));
            });

        }
    }
}
