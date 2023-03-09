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
    /// <summary>
    /// Map the player's responses to the DialogData of how the character will respond
    /// </summary>
    Dictionary<string, DialogData> responseMap = new Dictionary<string, DialogData>();
    /// <summary>
    /// Map the player's responses to the change in friendship value with a particular character
    /// </summary>
    Dictionary<string, FriendshipValChange> friendshipMap = new Dictionary<string, FriendshipValChange>();
    Dictionary<string, int> characterFriendshipStatus = new Dictionary<string, int>();
    //idea for friendship values: we can probably map responses to corresponding friendship values as well, depending on how we want them to work...
    List<DialogData> dialogDataList = new List<DialogData>();

    enum ResponseKeys //The dialogue system requires they be strings, so ToString() should be called on this before passing it to any of those methods
    {
        Me,
        You,
        FromEarth,

        YesGravity,
        PredictGravity,
        GravityExceptions,

        LikeFood,
        DislikeFood
    }

    string happyEmote = "/emote:Happy/";
    string sadEmote = "/emote:Sad/";
    string confusedEmote = "/emote:Confused/";

    string nameBob = "Bob";
    string nameCindy = "Cindy";
    string nameAlan = "Alan";

    struct FriendshipValChange
    {
        public string characterName { get; set; }
        public int amount { get; set; }
}

    private void Awake()
    {
        CreateResponseMap();
        dialogDataList.Add(new DialogData(" ")); //just a hack to not have the symbols in the way for now, since im not calling translate on anything
        dialogDataList.Add(new DialogData("Welcome to our class. I’m super excited to meet you, as the first human from earth.", nameBob));
        //dialogDataList[0].Callback = () => { translatorScript.PleaseTranslate = true; };

        DialogData data;
        data = new DialogData("I’m always interested in earth civilizations, so I have tons of questions about earth!");
        data.SelectList.Add(ResponseKeys.Me.ToString(), "Of course, go ahead and ask me!");
        data.SelectList.Add(ResponseKeys.You.ToString(), "I also have many questions about your home planet.");
        data.SelectList.Add(ResponseKeys.FromEarth.ToString(), "Yes, I am from earth.");
        data.Callback = () => ShowResponse();
        dialogDataList.Add(data);

        data = new DialogData("Is it true that gravity on earth is so strong that everything stays on the ground?");
        data.SelectList.Add(ResponseKeys.YesGravity.ToString(), "Of course!");
        data.SelectList.Add(ResponseKeys.PredictGravity.ToString(), "Yes, it allows us to predict movement and organize our life accordingly.");
        data.SelectList.Add(ResponseKeys.GravityExceptions.ToString(), "Well, yes, but there are exceptions");
        data.Callback = () => ShowResponse();
        dialogDataList.Add(data);


        data = new DialogData("Did you get used to life here? How do you like the food on this planet?");
        data.SelectList.Add(ResponseKeys.LikeFood.ToString(), "I like the food here, quite different from what we had on earth.");
        data.SelectList.Add(ResponseKeys.DislikeFood.ToString(), " It’s so bad. I miss food from earth.");
        data.Callback = () => ShowResponse();
        dialogDataList.Add(data);

        //data = new DialogData("Hi! You must be the new student from Earth! My name is Cindy!");
        //data.SelectList.Add(, "Hello. Nice to meet you. ");
        //data.SelectList.Add(, "Are you in my class? I don’t remember seeing you in our classroom.");
        //data.SelectList.Add(, "So I am not the only new student huh?");
        //data.Callback = () => ShowResponse();
        //dialogDataList.Add(data);

        //data = new DialogData("Anyways, what would you get for lunch?");
        //data.SelectList.Add(, "Burgers");
        //data.SelectList.Add(, "Porridge");
        //data.SelectList.Add(, "Fried Rice");
        //data.SelectList.Add(, "As you wish.");
        //data.Callback = () => ShowResponse();
        //dialogDataList.Add(data);

        //data = new DialogData("");
        //data.SelectList.Add(, "");
        //data.SelectList.Add(, "");
        //data.SelectList.Add(, "");
        //data.Callback = () => ShowResponse();
        //dialogDataList.Add(data);



        DialogManager.Show(dialogDataList);


    }

    private void CreateResponseMap()
    {
        responseMap.Add(ResponseKeys.Me.ToString(), new DialogData(happyEmote + "I will! Let me consider what to ask first though."));
        responseMap.Add(ResponseKeys.You.ToString(), new DialogData(happyEmote + "Is that so? Then we will have a lot to talk about"));
        responseMap.Add(ResponseKeys.FromEarth.ToString(), new DialogData(confusedEmote + "Ok?..."));

        responseMap.Add(ResponseKeys.YesGravity.ToString(), new DialogData("I see…"));
        responseMap.Add(ResponseKeys.PredictGravity.ToString(), new DialogData("Nice! I never thought of gravity in that way."));
        responseMap.Add(ResponseKeys.GravityExceptions.ToString(), new DialogData("Cool. I have never been to a planet with that level of gravity"));

        responseMap.Add(ResponseKeys.LikeFood.ToString(), new DialogData("you like the food"));
        responseMap.Add(ResponseKeys.DislikeFood.ToString(), new DialogData("you dislike the food"));
    }

    private void ShowResponse()
    {
        DialogManager.Show(responseMap[DialogManager.Result]);

        if(friendshipMap.ContainsKey(DialogManager.Result))
        {
            FriendshipValChange change = friendshipMap[DialogManager.Result];

            characterFriendshipStatus[change.characterName] += change.amount;

        }

    }

}
