using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

[System.Serializable]
public class Player
{
    public int player_id;
    public string username;
    public int level;
}

[System.Serializable]
public class InveontoryItem
{
    public int item_id;
    public string name;
    public string description;
    public int value;
    public int quantity;
}

[System.Serializable]
public class Quest
{
    public int quest_id;
    public string title;
    public string description;
    public int reward_exp;
    public int reward_item_id;
    public string status;
}


public class GameDataManager : MonoBehaviour
{
    private string serverUrl = "http://localhost:3000";
    private Player currentPlayer;

    //데이터 리스트 
    public List<InveontoryItem> inveontoryItems = new List<InveontoryItem>();
    public List<Quest> playerQuests = new List<Quest>();

    //퀘스트 업데이트시 실행될 이벤트
    public delegate void OnQuestUpdateHandler(List<Quest> quests);
    public event OnQuestUpdateHandler OnQuestUpdate;

    void Start()
    {
        currentPlayer = new Player();
        currentPlayer.player_id = 1;

        StartCoroutine(GetQuest());
    }


    private IEnumerator GetQuest()
    {
        //if (currentPlayer == null) yield break;

        using (UnityWebRequest www = UnityWebRequest.Get($"{serverUrl}/quests/{currentPlayer.player_id}"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                playerQuests = JsonConvert.DeserializeObject<List<Quest>>(www.downloadHandler.text);
                Debug.Log("진행 중인 퀘스트 : ");
                foreach(var quest in playerQuests)
                {
                    Debug.Log($" - {quest.title}  {quest.status})");
                }

                OnQuestUpdate?.Invoke(playerQuests);
            }
            else
            {
                Debug.LogError("퀘스트 조회 실패 : " + www.error);
            }
        }
    }

    public Quest GetQuest(int questId)
    {
        return playerQuests.Find(quest => quest.quest_id == questId);
    }
}
