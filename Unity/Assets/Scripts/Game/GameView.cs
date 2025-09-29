using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameView : MonoBehaviour
{
    //UI 요소
    public Text playerNameText;
    public Text metalText;
    public Text crystalText;
    public Text deuteriumText;
    public InputField playerNameInput;

    public Button registerButton;
    public Button loginButton;
    public Button collectButton;
    public Button developButton;
    public Slider progressBar;
    

    public void SetPlayerName(string name)
    {
        playerNameText.text = name; 
    }

    public void UpdateResources(int metal, int crystal, int deuterium)
    {
        metalText.text = $"Metal : {metal}";    
        crystalText.text = $"Crystal : {crystal}";
        deuteriumText.text = $"deuterium : {deuterium}";
    }

    public void UpdateProgressBar(float value)
    {
        progressBar.value = value;  
    }

    //버튼 클릭 리스너 설정 함수

    public void SetRegisterButtonListener(UnityAction action)
    {
        registerButton.onClick.RemoveAllListeners();
        registerButton.onClick.AddListener(action);
    }

    public void SetLoginButtonListener(UnityAction action)
    {
        loginButton.onClick.RemoveAllListeners();
        loginButton.onClick.AddListener(action);
    }

    public void SetCollectButtonListener(UnityAction action)
    {
        collectButton.onClick.RemoveAllListeners();
        collectButton.onClick.AddListener(action);
    }

    public void SetDevelopButtonListener(UnityAction action)
    {
        developButton.onClick.RemoveAllListeners();
        developButton.onClick.AddListener(action);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
