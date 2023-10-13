using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Search;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalace = 500;
    int currentBalance;
    public int CurrentBalance {get {return currentBalance;}}
    [SerializeField] TextMeshProUGUI displayBalance;
    // Start is called before the first frame update
    private void Awake() {
        currentBalance = startingBalace;
        UpdateToDisplay();
    }
    void Start()
    {
        
    }
    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateToDisplay();
    }
    public void WithDraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateToDisplay();
        if(currentBalance < 0)
        {
            ReloadScene();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateToDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
