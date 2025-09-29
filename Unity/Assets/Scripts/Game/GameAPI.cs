using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using System;
using UnityEditor.PackageManager.Requests;
using Unity.VisualScripting;

public class GameAPI : MonoBehaviour
{
    private string baseUrl = "http://localhost:4000/api";                   //Node.js 서버의 URL

    //플레이어 레지스터
    public IEnumerator RegisterPlayer(string playerName, string password)
    {
        var requestData = new {name = playerName, password = password};
        string jsonData = JsonConvert.SerializeObject(requestData);
        Debug.Log($"Registering player: {jsonData}");

        using (UnityWebRequest request = new UnityWebRequest($"{baseUrl}/register", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error registering player : {request.result}");
            }
            else
            {
                Debug.Log("Player registered successfully");
            }
        }
    }

    //플레이어 로그인 메서드
    public IEnumerator LoginPlayer(string playerName, string password, Action<PlayerModel> onSuccess)
    {
        var requestData = new {name = playerName , password = password};
        string jsonData = JsonConvert.SerializeObject(requestData);

        using (UnityWebRequest request = new UnityWebRequest($"{baseUrl}/login", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error login player : {request.result}");
            }
            else
            {
                //응답을 처리하여 PlayerModel 생성
                string responseBody = request.downloadHandler.text;

                try
                {
                    var responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBody);

                    //서버 응답에서 PlayerModel 생성
                    PlayerModel playerMode = new PlayerModel(responseData["playerName"].ToString())
                    {
                        metal = Convert.ToInt32(responseData["metal"]),
                        crystal = Convert.ToInt32(responseData["crystal"]),
                        deuteriurm = Convert.ToInt32(responseData["deuterium"]),
                        Planets = new List<PlanetModel>()
                    };
                    onSuccess?.Invoke(playerMode);
                    Debug.Log("Login successful");
                }
                catch (Exception ex) 
                {
                    Debug.LogError($"Error processing login responce : {ex.Message}");  
                }
            }
        }

    }

}
