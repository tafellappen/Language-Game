using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class DDSysTesting : MonoBehaviour
{
    public DialogManager DialogManager;
    [SerializeField] private Translator translatorScript;

    // Start is called before the first frame update
    void Awake()
    {
        DialogData data = new DialogData("omae wa mou shindeiru jjj\nthis is A newline with jyjy", "AlienCharacter");
        data.SelectList.Add("nani", "NANI???");
        data.SelectList.Add("teleport", "*teleports behind u*");
        data.SelectList.Add("god", "power of god and anime");

        data.Callback = () => CheckResponse();

        List<DialogData> dialogTexts = new List<DialogData>();
        dialogTexts.Add(data);
        DialogManager.Show(dialogTexts);
    }

    void CheckResponse()
    {

        List<DialogData> dialogTexts = new List<DialogData>();

        switch (DialogManager.Result)
        {
            case "nani":
                dialogTexts.Add(new DialogData("rip"));
                DialogManager.Show(dialogTexts);
                break;
            case "teleport":
                dialogTexts.Add(new DialogData("hoho, you're approaching me?"));
                DialogManager.Show(dialogTexts);
                break;
            case "god":
                dialogTexts.Add(new DialogData("AAAAAAAAAAAAAAA"));
                DialogManager.Show(dialogTexts);
                break;
            default:
                break;
        }

        translatorScript.Translate();
    }
}
