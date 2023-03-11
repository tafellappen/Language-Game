using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.SceneManagement;

public class EndingDialogue : MonoBehaviour
{
    public DialoguePath UsingDialoguePath;
    public DialogManager DialogManager;
    [SerializeField] private Translator translatorScript;

    // Start is called before the first frame update
    void Awake()
    {
        UsingDialoguePath = GameObject.Find("TextWriter").GetComponent<DialoguePath>();
        DialogData data;
        List<DialogData> dialogTexts = new List<DialogData>();
        data = new DialogData("Who do you want to partner?");
        data.SelectList.Add("Alan", "Alan");
        data.SelectList.Add("Bob", "Bob");
        data.SelectList.Add("Cindy", "Cindy");

        data.Callback = Check_FP;
        dialogTexts.Add(data);
        DialogManager.Show(dialogTexts);
    }

    private void Check_FP()
    {
        DialogData data;
        List<DialogData> dialogTexts = new List<DialogData>();
        switch (DialogManager.Result)
        {
            case "Alan":
                if (UsingDialoguePath.CharacterFriendshipStatus["Alan"] >= 3)
                {
                    data = new DialogData("Congratulations! Alan is willing to partner you! Wish your teamwork happiness and success!");
                    dialogTexts.Add(data);
                    DialogManager.Show(dialogTexts);
                }
                else
                {
                    data = new DialogData("Sorry, Alan is unwilling to partner you. You might ask others for partnership.");
                    dialogTexts.Add(data);
                    DialogManager.Show(dialogTexts);
                }
                break;
            case "Bob":
                if (UsingDialoguePath.CharacterFriendshipStatus["Bob"] >= 3)
                {
                    data = new DialogData("Congratulations! Bob is willing to partner you! Wish your teamwork happiness and success!");
                    dialogTexts.Add(data);
                    DialogManager.Show(dialogTexts);
                }
                else
                {
                    data = new DialogData("Sorry, Bob is unwilling to partner you. You might ask others for partnership.");
                    dialogTexts.Add(data);
                    DialogManager.Show(dialogTexts);
                }
                break;
            case "Cindy":
                if (UsingDialoguePath.CharacterFriendshipStatus["Cindy"] >= 3)
                {
                    data = new DialogData("Congratulations! Cindy is willing to partner you! Wish your teamwork happiness and success!");
                    dialogTexts.Add(data);
                    DialogManager.Show(dialogTexts);
                }
                else
                {
                    data = new DialogData("Sorry, Cindy is unwilling to partner you. You might ask others for partnership.");
                    dialogTexts.Add(data);
                    DialogManager.Show(dialogTexts);
                }
                break;
        }

        Destroy(UsingDialoguePath.gameObject);
        Invoke("DelayExit", 5.0f);
    }

    public void DelayExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
