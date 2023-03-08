using Doublsb.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The theory I will be testing out here is that you can just use a dictionary to map player responses to alien responses. 
/// </summary>
public class DialoguePath : MonoBehaviour
{
    [SerializeField] DialogManager DialogManager;
    //[SerializeField] private Translator translatorScript;
    Dictionary<string, DialogData> responseMap = new Dictionary<string, DialogData>();
    //idea for friendship values: we can probably map responses to corresponding friendship values as well, depending on how we want them to work...
    List<DialogData> dialogDataList = new List<DialogData>();

    enum ResponseKeys
    {
        //Correct,
        //Wrong,
        //Whatever,
        Me,
        You,
        Float,
        Predict,
        Fly,
        LikeFood,
        DislikeFood
    }

    private void Awake()
    {
        CreateResponseMap();
        dialogDataList.Add(new DialogData(" ")); //just a hack to not have the symbols in the way for now, since im not calling translate on anything
        dialogDataList.Add(new DialogData("Welcome to our class. I’m super excited to meet you, as the first human from earth.", "AlienCharacter"));
        //dialogDataList[0].Callback = () => { translatorScript.PleaseTranslate = true; };

        DialogData data;
        data = new DialogData("I’m always interested in earth civilizations, so I have tons of questions about earth!", "AlienCharacter");
        data.SelectList.Add(ResponseKeys.Me.ToString(), "Of course, go ahead and ask me!");
        data.SelectList.Add(ResponseKeys.You.ToString(), "I also have many questions about your home planet.");
        data.Callback = () => ShowResponse();
        dialogDataList.Add(data);

        data = new DialogData("Is it true that gravity on earth is so strong that everything stays on the ground?");
        data.SelectList.Add(ResponseKeys.Float.ToString(), "Yes, nothing really floats in the air.");
        data.SelectList.Add(ResponseKeys.Predict.ToString(), "Yes, it allows us to predict movement and organize our life accordingly.");
        data.SelectList.Add(ResponseKeys.Fly.ToString(), "Well, birds can fly, and we humans have invented planes to get us up in the air.");
        data.Callback = () => ShowResponse();
        dialogDataList.Add(data);


        data = new DialogData("Did you get used to life here? How do you like the food on this planet?");
        data.SelectList.Add(ResponseKeys.LikeFood.ToString(), "I like the food here, quite different from what we had on earth.");
        data.SelectList.Add(ResponseKeys.DislikeFood.ToString(), " It’s so bad. I miss food from earth.");
        //data.Callback = SecondResponse;
        dialogDataList.Add(data);


        DialogManager.Show(dialogDataList);


    }

    private void CreateResponseMap()
    {
        //responseMap.Add(ResponseKeys.Correct.ToString(), new DialogData("You are right."));
        //responseMap.Add(ResponseKeys.Wrong.ToString(), new DialogData("You are wrong."));
        //responseMap.Add(ResponseKeys.Whatever.ToString(), new DialogData("Right. You don't have to get the answer."));
        responseMap.Add(ResponseKeys.Me.ToString(), new DialogData("you are asked questions"));
        responseMap.Add(ResponseKeys.You.ToString(), new DialogData("you ask the alien questions"));
        responseMap.Add(ResponseKeys.LikeFood.ToString(), new DialogData("you like the food"));
        responseMap.Add(ResponseKeys.DislikeFood.ToString(), new DialogData("you dislike the food"));
        responseMap.Add(ResponseKeys.Float.ToString(), new DialogData("nothing floats?"));
        responseMap.Add(ResponseKeys.Predict.ToString(), new DialogData("prediction"));
        responseMap.Add(ResponseKeys.Fly.ToString(), new DialogData("tobenai tori"));
    }

    private void ShowResponse()
    {
        DialogManager.Show(responseMap[DialogManager.Result]);

    }

}
