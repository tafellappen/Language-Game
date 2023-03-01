using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;

// when attached to a text box, this will transform some or all of the english into alien text
public class Translator : MonoBehaviour
{
    [SerializeField] private GameObject LetterCover;
    private const float manualShift = 0.05f; // decrease text advance manually through guess and check
    private const float verticalShift = 1.5f;
    private const int numLearnable = 3;

    private Vector3 coverStart;
    private Vector2 charDims;
    private float lineHeight;
    private int charsPerLine; // remember that words jump a line down before reaching the end

    private int learnableLeft;
    private List<GameObject> covers; // track covers so they can be deleted

    void Start()
    {
        RectTransform box = GetComponent<RectTransform>();
        Text textBox = GetComponent<Text>();

        // determine text dimensions and position
        CharacterInfo jInfo;
        if(!textBox.font.GetCharacterInfo('j', out jInfo, (int)textBox.fontSize)) { // only add to the font once
            textBox.font.RequestCharactersInTexture("j", (int)textBox.fontSize); // 'j' is the tallest character, all have the same advance because font is monospaced
            textBox.font.GetCharacterInfo('j', out jInfo, (int)textBox.fontSize);
        }

        charDims = new Vector2(jInfo.advance, jInfo.glyphHeight);
        charsPerLine = 46;// hard coded for consistency (int)(box.rect.width / (charDims.x - manualShift));
        lineHeight = textBox.font.lineHeight / 20f * textBox.fontSize * 1.05f; // font file value is for size 20 font
        coverStart = new Vector3(box.localPosition.x - box.rect.width / 2 + charDims.x / 2, box.localPosition.y + verticalShift + box.rect.max.y - 59f * textBox.fontSize / 94, 0); // y scalar found through guess and check at font size 94

        // Translate at the start
        Translate();
    }

    void Update() {
        if(learnableLeft > 0) {

        }
    }

    public void Translate()
    {
        // clear previous letter covers
        if(covers != null) { 
            foreach(GameObject cover in covers) {
                Destroy(cover);
            }
        }
        covers = new List<GameObject>();

        AlienDictionary dictionary = GameObject.Find("Dictionary").GetComponent<AlienDictionary>();
        Text textBox = GetComponent<Text>();
        string startText = textBox.text;

        // find all of the words from the text and add spaces when necessary
        string newText = "";
        List<string> words = new List<string>();
        string foundWord = "";
        foreach (char letter in startText) {
            if(alphabet.Contains(letter)) {
                foundWord += letter;
            } 
            else {
                if(!foundWord.Equals("")) {
                    // complete new word
                    words.Add(foundWord);
                    dictionary.RegisterWord(foundWord);

                    AlienWord alien = dictionary.GetWord(foundWord);
                    if(alien.LeftSpace) {
                        newText += '_'; // use underscores because text wrapping ignores spaces
                    }
                    newText += foundWord;
                    if(alien.RightSpace) {
                        newText += '_';
                    }

                    foundWord = "";
                }

                newText += letter;
            }
        }

        if(!foundWord.Equals("")) {
            // add last word in the case that the text does not end with punctuation
            words.Add(foundWord);
            dictionary.RegisterWord(foundWord);

            AlienWord alien = dictionary.GetWord(foundWord);
            if(alien.LeftSpace) {
                newText += '_';
            }
            newText += foundWord;
            if(alien.RightSpace) {
                newText += '_';
            }
        }

        textBox.text = newText;

        // place alien letters
        int row = 0;
        int col = 0;
        int nextWord = 0; // index from the word list
        for(int i = 0; i < newText.Length; i++) {
            if(newText[i] == '\n') {
                row++;
                col = 0;
            }
            else if(nextWord < words.Count && (newText[i] == words[nextWord][0] || newText[i] == '_')) { 
                AlienWord alien = dictionary.GetWord(words[nextWord]);
                int wordLength = alien.Letters.Length;
                
                // check if this word wraps to the next line
                if(col + wordLength > charsPerLine) {
                    row++;
                    col = 0;
                }

                // place covers for this word
                for(int let = 0; let < wordLength; let++) {
                    GameObject cover = PlaceCover(row, col);
                    cover.GetComponent<Image>().sprite = alien.Letters[let];
                    if(alien.Known) {
                        cover.GetComponent<Fader>().FadeOut();
                    }

                    col++;
                }

                i += wordLength - 1; // subtract 1 because loop auto adds 1 later
                nextWord++;
            }
            else { 
                col++;

                if(col >= charsPerLine - 1 && newText[i] == ' ' && i > 0 && newText[i-1] != '\n') {
                    // spaces that occur right before the text moves to a new line do not exist
                    col--;
                }

                if(col > charsPerLine - 1) {
                    row++;
                    col = 0;
                }
            }
        }
    }

    private GameObject PlaceCover(int row, int col) {
        GameObject cover = Instantiate(LetterCover);
        cover.transform.SetParent(transform.parent);
        cover.transform.localScale = new Vector3(1, 1, 1);
        cover.GetComponent<RectTransform>().sizeDelta = charDims * 1.05f; // expand letters a little to be safe
        cover.transform.localPosition = coverStart + new Vector3((charDims.x - manualShift) * col, -lineHeight * row, 0);
        covers.Add(cover);

        return cover;
    }

    // called whenever a piece of text should allow the player to learn new words
    public void AllowLearning() {
        learnableLeft = numLearnable;
    }

    private static List<char> alphabet = new List<char> {
        'a', 'b', 'c',
        'd', 'e', 'f',
        'g', 'h', 'i',
        'j', 'k', 'l',
        'm', 'n', 'o',
        'p', 'q', 'r',
        's', 't', 'u',
        'v', 'w', 'x',
        'y', 'z',
        '\'', // apostraphes are used in contractions and possessives (you're, Jerry's)
        'A', 'B', 'C',
        'D', 'E', 'F',
        'G', 'H', 'I',
        'J', 'K', 'L',
        'M', 'N', 'O',
        'P', 'Q', 'R',
        'S', 'T', 'U',
        'V', 'W', 'X',
        'Y', 'Z'
    };
}
