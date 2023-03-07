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
    Dictionary<string, DialogData> responseMap = new Dictionary<string, DialogData>();
    //idea for friendship values: we can probably map responses to corresponding friendship values as well, depending on how we want them to work...
    List<DialogData> dialogDataList = new List<DialogData>();

    enum ResponseKeys
    {
        Float,
        Predict,
        Fly,
        LikeFood,
        DislikeFood
    }

    private void Awake()
    {
        CreateResponseMap();
        dialogDataList.Add(new DialogData("Hi! You must be the new student from Earth! My name is Cindy! What are you getting for lunch?", "AlienCharacter"));
        
        //DialogData Text1 = new DialogData("What is 2 times 5?");
        //Text1.SelectList.Add(ResponseKeys.Correct.ToString(), "10");
        //Text1.SelectList.Add(ResponseKeys.Wrong.ToString(), "7");
        //Text1.SelectList.Add(ResponseKeys.Whatever.ToString(), "Why should I care?");
        //dialogDataList.Add(Text1);

        //Text1.Callback = () => ShowResponse();

        DialogManager.Show(dialogDataList);


    }

    private void CreateResponseMap()
    {
        //responseMap.Add(ResponseKeys.Correct.ToString(), new DialogData("You are right."));
        //responseMap.Add(ResponseKeys.Wrong.ToString(), new DialogData("You are wrong."));
        //responseMap.Add(ResponseKeys.Whatever.ToString(), new DialogData("Right. You don't have to get the answer."));
    }

    private void ShowResponse()
    {
        DialogManager.Show(responseMap[DialogManager.Result]);

    }

}
