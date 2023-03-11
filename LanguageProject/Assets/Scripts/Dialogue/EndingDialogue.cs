using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class EndingDialogue : MonoBehaviour
{
    public DialogManager DialogManager;
    [SerializeField] private Translator translatorScript;

    // Start is called before the first frame update
    void Awake()
    {
        DialogData data;
        List<DialogData> dialogTexts = new List<DialogData>();
        data = new DialogData("Who do you want to team up with?");
        data.SelectList.Add("", "");
        data.SelectList.Add("", "");
        data.SelectList.Add("", "");
        data.Callback = Check_FP;

        dialogTexts.Add(data);
        DialogManager.Show(dialogTexts);
    }

    private void FirstResponse()
    {
        List<DialogData> dialogTexts = new List<DialogData>();
        DialogData data = new DialogData("", "");

        //switch (DialogManager.Result)
        //{
        //    case "me":
        //        data = new DialogData("I will! Let me consider what to ask first though.");
        //        break;
        //    case "you":
        //        data = new DialogData("Is that so? Then we will have a lot to talk about.");
        //        break;
        //}
        //data.Callback = () => { Debug.Log("hello?"); translatorScript.PleaseTranslate = true; };
        //dialogTexts.Add(data);

        data = new DialogData("Is it true that gravity on earth is so strong that everything stays on the ground?");
        data.SelectList.Add("float", "Yes, nothing really floats in the air.");
        data.SelectList.Add("predict", "Yes, it allows us to predict movement and organize our life accordingly.");
        data.SelectList.Add("fly", "Well, birds can fly, and we humans have invented planes to get us up in the air.");
        //data.Callback = SecondResponse;
        dialogTexts.Add(data);

        DialogManager.Show(dialogTexts);
        translatorScript.Translate(true);
    }

    private void SecondResponse()
    {
        Debug.Log("here");
        List<DialogData> dialogTexts = new List<DialogData>();
        DialogData data = new DialogData("", "");

        data = new DialogData("Did you get used to life here? How do you like the food on this planet?");
        data.SelectList.Add("float", "I like the food here, quite different from what we had on earth.");
        data.SelectList.Add("predict", " It’s so bad. I miss food from earth.");
        //data.Callback = SecondResponse;
        dialogTexts.Add(data);

        DialogManager.Show(dialogTexts);
        translatorScript.Translate(true);
    }
}
