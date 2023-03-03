using System.Collections.Generic;
using UnityEngine;

// a container for alien words
public class AlienWord
{
    private bool leftSpace; // extra letter on the left 
    private bool rightSpace; // or right of the word
    private Sprite[] letters; // the sequence of alien letters that make the alien translation of this word

    public bool Known { get; set; }
    public bool LeftSpace { get { return leftSpace; } }
    public bool RightSpace { get { return rightSpace; } }
    public Sprite[] Letters { get { return letters; } }

    public AlienWord(int englishLength, Sprite[] alienLetters) {
        Known = false;

        // generate alien version
        leftSpace = Random.Range(0, 3) == 0;
        rightSpace = Random.Range(0, 3) == 0;

        leftSpace = false;
        rightSpace = false;

        letters = new Sprite[englishLength + (leftSpace ? 1 : 0) + (rightSpace ? 1 : 0)];
        for(int i = 0; i < letters.Length; i++) {
            letters[i] = alienLetters[Random.Range(0, alienLetters.Length)];
        }
    }
}
