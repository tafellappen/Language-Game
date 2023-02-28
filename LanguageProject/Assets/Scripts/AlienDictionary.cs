using System.Collections.Generic;
using UnityEngine;

// tracks which alien words the player knows as well as how to translate them
public class AlienDictionary : MonoBehaviour
{
    [SerializeField] private Sprite[] alienLetters;

    private Dictionary<string, AlienWord> dictionary = new Dictionary<string, AlienWord>();

    // gets the alien version of the english word. Does not verify if word is in the dictionary
    public AlienWord GetWord(string english) {
        string lowered = english.ToLower();
        return dictionary[english];
    }

    // does not register anything if the word is already registered
    public void RegisterWord(string english) {
        string lowered = english.ToLower();
        if(dictionary.ContainsKey(english)) {
            return;
        }

        dictionary[english] = new AlienWord(english.Length, alienLetters);
    }
}
