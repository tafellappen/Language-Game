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
        DislikeFood,
        WeekAgo,

        CinNiceMeet,
        CinInClass,
        CinNew,

        CinLunchFork,
        CinLunchRice,
        CinLunchBob,

        CinRocSure,
        CinRocLike,
        CinRocNeed,
        CinRocTomorrow,

        AlNewYes,
        AlNewMeet,
        AlNewName,

        AlSubMath,
        AlSubTenn,
        AlSubBurger,
        AlSubRoc,

        AlRocGL,
        AlRocEarth,
        AlRocIDK,

        CinClassMath,
        CinClassTry,
        CinClassWhy,
        CinClassHow,

        CinWeekHorr,
        CinWeekBurger,
        CinWeekHow,

        AlWhatsLunch,
        AlWhatsGreat,

        AlSchoolWhat,
        AlSchoolRoc,
        AlSchoolYes,

        AlPlansWork,
        AlPlansDislike,

        AlNoNext,
        AlNoGo,
        AlNoGuessSo
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
        //CreateResponseMap();
        dialogDataList.Add(new DialogData(" ")); //just a hack to not have the symbols in the way for now, since im not calling translate on anything
        dialogDataList.Add(new DialogData("Welcome to our class. I’m super excited to meet you, as the first human from earth.", nameBob));
        //dialogDataList[0].Callback = () => { translatorScript.PleaseTranslate = true; };


        //Bob

        DialogData data;
        data = new DialogData("I’m always interested in earth civilizations, so I have tons of questions about earth!");

        data.SelectList.Add(ResponseKeys.Me.ToString(), "Of course!"); //fp+
        responseMap    .Add(ResponseKeys.Me.ToString(), new DialogData(happyEmote + "I will! Let me consider what to ask first though."));

        data.SelectList.Add(ResponseKeys.You.ToString(), "I also have many questions about your home planet.");//fp+
        responseMap    .Add(ResponseKeys.You.ToString(), new DialogData(happyEmote + "Is that so? Then we will have a lot to talk about"));

        data.SelectList.Add(ResponseKeys.FromEarth.ToString(), "Yes, I am from earth.");
        responseMap    .Add(ResponseKeys.FromEarth.ToString(), new DialogData(confusedEmote + "Ok?..."));

        //data.Callback = () => ShowResponse();
        dialogDataList.Add(data);


        data = new DialogData("Is it true that gravity on earth is so strong that everything stays on the ground?");

        data.SelectList.Add(ResponseKeys.YesGravity.ToString(), "Of course!");
        responseMap    .Add(ResponseKeys.YesGravity.ToString(), new DialogData("I see…"));

        data.SelectList.Add(ResponseKeys.PredictGravity.ToString(), "Yes, it allows us to predict movement and organize our life accordingly."); //+
        responseMap    .Add(ResponseKeys.PredictGravity.ToString(), new DialogData("Nice! I never thought of gravity in that way."));

        data.SelectList.Add(ResponseKeys.GravityExceptions.ToString(), "Well, yes, but there are exceptions"); //+
        responseMap    .Add(ResponseKeys.GravityExceptions.ToString(), new DialogData("Cool. I have never been to a planet with that level of gravity"));

        dialogDataList.Add(data);


        data = new DialogData("Did you get used to life here? How do you like the food on this planet?");

        data.SelectList.Add(ResponseKeys.LikeFood.ToString(), "I like the food here, quite different from what we had on earth.");
        responseMap    .Add(ResponseKeys.LikeFood.ToString(), new DialogData("Cut it out, I know earth has much better food."));

        data.SelectList.Add(ResponseKeys.DislikeFood.ToString(), "It’s so boring. I miss that from earth."); //fp+
        responseMap    .Add(ResponseKeys.DislikeFood.ToString(), new DialogData("Exactly. You have quite a diverse food culture."));

        data.SelectList.Add(ResponseKeys.WeekAgo.ToString(), "I got here a week ago.");
        responseMap    .Add(ResponseKeys.WeekAgo.ToString(), new DialogData(confusedEmote + "OK?"));
        
        dialogDataList.Add(data);


        //Cindy
        
        data = new DialogData("Hi! You must be the new student from Earth! My name is Cindy!");

        data.SelectList.Add(ResponseKeys.CinNiceMeet.ToString(), "Hello. Nice to meet you.");
        responseMap    .Add(ResponseKeys.CinNiceMeet.ToString(), new DialogData("Nice to meet you too."));

        data.SelectList.Add(ResponseKeys.CinInClass.ToString(), "Are you in my class? I don’t remember seeing you in our classroom.");
        responseMap    .Add(ResponseKeys.CinInClass.ToString(), new DialogData("Oh, I missed class in the morning.")); //+

        data.SelectList.Add(ResponseKeys.CinNew.ToString(), "So I am not the only new student huh?");
        responseMap    .Add(ResponseKeys.CinNew.ToString(), new DialogData(confusedEmote + "What? No, I just missed class this morning."));

        dialogDataList.Add(data);


        //data = new DialogData("Anyways, what will you get for lunch?");

        //data.SelectList.Add(ResponseKeys.DislikeFood.ToString(), "Forks.");
        //responseMap    .Add(ResponseKeys.DislikeFood.ToString(), new DialogData(confusedEmote + "Wait, do humans eat forks?"));

        //data.SelectList.Add(ResponseKeys.DislikeFood.ToString(), "Fried Rice.");
        //responseMap    .Add(ResponseKeys.DislikeFood.ToString(), new DialogData("That’s my favorite food!")); //+

        //data.SelectList.Add(ResponseKeys.DislikeFood.ToString(), "Bob.");
        //responseMap    .Add(ResponseKeys.DislikeFood.ToString(), new DialogData("What?? I don’t think you understood me...")); //-


        //data = new DialogData("Do you want to sit next to me in the Rocket Science class this afternoon?");

        //data.SelectList.Add(ResponseKeys.DislikeFood.ToString(), "Sure.");
        //responseMap.Add(ResponseKeys.DislikeFood.ToString(), new DialogData(happyEmote + "See you then.")); //+

        //data.SelectList.Add(ResponseKeys.DislikeFood.ToString(), "I really like Rocket Science.");
        //responseMap.Add(ResponseKeys.DislikeFood.ToString(), new DialogData(confusedEmote + "Okay…"));

        //data.SelectList.Add(ResponseKeys.DislikeFood.ToString(), "Do you need anything from me?");
        //responseMap.Add(ResponseKeys.DislikeFood.ToString(), new DialogData(confusedEmote + "No, just being nice…"));

        //data.SelectList.Add(ResponseKeys.DislikeFood.ToString(), "How about tomorrow?");
        //responseMap.Add(ResponseKeys.DislikeFood.ToString(), new DialogData("I guess so."));

        //dialogDataList.Add(data);


        ////Alan

        //data = new DialogData("Are you the new student in our cohort? I am Alan.");

        //data.SelectList.Add(, "Yes, I am the new student.");
        //responseMap.Add(, new DialogData("Sure."));

        //data.SelectList.Add(, "Nice to meet you. I am from earth.");
        //responseMap.Add(, new DialogData(happyEmote + "Sounds like a very distant planet.")); //+

        //data.SelectList.Add(, "And you are?");
        //responseMap.Add(, new DialogData(confusedEmote + "I am your classmate, Alan."));

        //dialogDataList.Add(data);


        //data = new DialogData("There are so many courses in the school. So, what is your favorite subject?");

        //data.SelectList.Add(, "Math");
        //responseMap.Add(, new DialogData("Cindy also loves that."));

        //data.SelectList.Add(, "Space tennis");
        //responseMap.Add(, new DialogData(confusedEmote));

        //data.SelectList.Add(, "My favorite is burger.");
        //responseMap.Add(, new DialogData(confusedEmote));

        //data.SelectList.Add(, "Rocket Science");
        //responseMap.Add(, new DialogData(happyEmote + "Mine too!"));//++

        //dialogDataList.Add(data);



        //data = new DialogData("My favorite is Rocket Science. I want to build a spaceship and go wherever I want.");

        //data.SelectList.Add(, "That’s dope! Good luck with that.");
        //responseMap    .Add(, new DialogData(happyEmote + "I know, right? Thanks."));//+

        //data.SelectList.Add(, "You should visit earth.");
        //responseMap    .Add(, new DialogData("Maybe..."));

        //data.SelectList.Add(, "Really? I don’t know...");
        //responseMap    .Add(, new DialogData(sadEmote + "I will do it. After graduation, of course.")); //-

        //dialogDataList.Add(data);


        ////Classroom, the next day
        ////Cindy

        //data = new DialogData("Good morning! Our first class is math.");

        //data.SelectList.Add(, "That's my favorite class.");
        //responseMap    .Add(, new DialogData(happyEmote + "It’s also my favorite!"));//++

        //data.SelectList.Add(, "I try so hard but I can never get it right.");
        //responseMap    .Add(, new DialogData(happyEmote + "I can help you with it."));//+

        //data.SelectList.Add(, "Why bother? We have technology.");
        //responseMap    .Add(, new DialogData(sadEmote + "It’s about training your thoughts in a logical way"));//-

        //data.SelectList.Add(, "How are you?");
        //responseMap    .Add(, new DialogData(confusedEmote + "I’m ok? Thank you."));

        //dialogDataList.Add(data);



        //data = new DialogData("Finally, tomorrow is the weekend. What do you do on the weekend?");

        //data.SelectList.Add(, "Watch horror movies.");
        //responseMap    .Add(, new DialogData(happyEmote + "I love horror movies too!"));//+

        //data.SelectList.Add(, "Burger.");
        //responseMap    .Add(, new DialogData(confusedEmote + "Okay…"));

        //data.SelectList.Add(, "How about you?");
        //responseMap    .Add(, new DialogData("I watch horror movies with my family."));

        //dialogDataList.Add(data);


        ////Alan

        //data = new DialogData("Hey! What is up?");

        //data.SelectList.Add(, "Pretty good! What did you get for lunch?");
        //responseMap    .Add(, new DialogData(happyEmote + "I picked noodles today")); //+

        //data.SelectList.Add(, "I’m doing great! How’s your day?");
        //responseMap    .Add(, new DialogData(happyEmote + "I'm really looking forward to the experiment this afternoon."));//+

        //dialogDataList.Add(data);



        //data = new DialogData("You are from earth, right? What is school like over there?");

        //data.SelectList.Add(, "What school?");
        //responseMap    .Add(, new DialogData(confusedEmote + "You don’t have school on earth?"));

        //data.SelectList.Add(, "They also teach rocket science there.");
        //responseMap    .Add(, new DialogData(happyEmote + "Nice!")); //+

        //data.SelectList.Add(, "Yes");
        //responseMap    .Add(, new DialogData(confusedEmote + "Yes what?"));

        //dialogDataList.Add(data);



        ////Bob

        //data = new DialogData("What plans do you have for the weekend? Do you want to play space tennis with me?");

        //data.SelectList.Add(, "Sorry, I don’t have time for that. I need to catch up with schoolwork.");
        //responseMap    .Add(, new DialogData(sadEmote + "Come on. It's the weekend.")); //DONT -

        //data.SelectList.Add(, "I don’t like space tennis. It’s so aggressive but also pointless");//-
        //responseMap    .Add(, new DialogData(sadEmote + "Seriously? You don’t have to say that."));

        //dialogDataList.Add(data);




        //data = new DialogData("I get it. You just don’t want to hang out with me.");

        //data.SelectList.Add(, "That’s not true. Maybe we can do something together next week.");
        //responseMap    .Add(, new DialogData("Fine, I know you are busy.")); //++

        //data.SelectList.Add(, "Ok, I’ll go with you.");
        //responseMap    .Add(, new DialogData(happyEmote + "Let’s go."));//+

        //data.SelectList.Add(, "I guess so.");
        //responseMap    .Add(, new DialogData("Ok. Whatever."));

        //dialogDataList.Add(data);




        //data = new DialogData("");

        //data.SelectList.Add(, "");
        //responseMap    .Add(, new DialogData());

        //data.SelectList.Add(, "");
        //responseMap    .Add(, new DialogData());

        //data.SelectList.Add(, "");
        //responseMap    .Add(, new DialogData());

        //dialogDataList.Add(data);




        //data = new DialogData("");

        //data.SelectList.Add(, "");
        //responseMap    .Add(, new DialogData());

        //data.SelectList.Add(, "");
        //responseMap    .Add(, new DialogData());

        //data.SelectList.Add(, "");
        //responseMap    .Add(, new DialogData());

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
