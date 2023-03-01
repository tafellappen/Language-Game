using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doublsb.Dialog;

public class DDSysTesting : MonoBehaviour
{
    public DialogManager DialogManager;
    [SerializeField] private Translator translatorScript;

    void Awake()
    {
        //DialogData data = new DialogData("Hi! You must be the new student from Earth! My name is Cindy! What are you getting for lunch?", "AlienCharacter");
        DialogData data = new DialogData("Hi! You must bej the new student from Earth! My name is Cindy! What are you getting forj lunch?", "AlienCharacter");
        data.SelectList.Add("idk", "I don’t know. Do you have any recommendations?");
        data.SelectList.Add("burger", "Burger");
        data.SelectList.Add("fu", "Fu");

        data.Callback = CheckResponse;

        List<DialogData> dialogTexts = new List<DialogData>();
        dialogTexts.Add(data);
        DialogManager.Show(dialogTexts);
    }

    void CheckResponse()
    {

        List<DialogData> dialogTexts = new List<DialogData>();

        //switch (DialogManager.Result)
        //{
        //    case "nani":
        //        dialogTexts.Add(new DialogData("rip"));
        //        DialogManager.Show(dialogTexts);
        //        break;
        //    case "teleport":
        //        dialogTexts.Add(new DialogData("hoho, you're approaching me?"));
        //        DialogManager.Show(dialogTexts);
        //        break;
        //    case "god":
        //        dialogTexts.Add(new DialogData("AAAAAAAAAAAAAAA"));
        //        DialogManager.Show(dialogTexts);
        //        break;
        //    default:
        //        break;
        //}
        //DialogData favFood = new DialogData("Well, fu is my favorite food. If you want something from earth, I heard fried rice is pretty good.");
        //favFood.Callback = translatorScript.Translate;
        //dialogTexts.Add(favFood);
        DialogData subject = new DialogData("Well, fu is my favorite food. I heard fried rice is pretty good. Anyway, what is your favorite subject?");
        subject.SelectList.Add("rocket", "Rocket Science");
        subject.SelectList.Add("pe", "PE");
        subject.SelectList.Add("math", "Math");
        dialogTexts.Add(subject);
        DialogManager.Show(dialogTexts);

        translatorScript.Translate();
    }

    void CheckSubject() {
        List<DialogData> dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("My favorite is math."));
        DialogManager.Show(dialogTexts);
        translatorScript.Translate();
    }
}
