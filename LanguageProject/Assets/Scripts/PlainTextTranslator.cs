using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.UI;

// when attached to a text box, this will transform some or all of the english into alien text
public class PlainTextTranslator : MonoBehaviour
{
    [SerializeField] private GameObject LetterCover;

    void Start()
    {
        RectTransform box = GetComponent<RectTransform>();
        Text textBox = GetComponent<Text>();
        string startText = textBox.text;
        string[] words = startText.Split(' ');

        // turn letters into random characters
        //string newText = "";
        //for(int i = 0; i < startText.Length; i++) {
        //    if(startText[i] == ' ') {
        //        newText += " ";
        //    }
        //    else {
        //        newText += (char)(startText[i] + Random.Range(50, 100));
        //    }
        //}
        //textMesh.text = newText;

        // determine text dimensions and position
        //CharacterInfo jInfo;
        //if (!textMesh.font.sourceFontFile.GetCharacterInfo('j', out jInfo, (int)textMesh.fontSize))
        //{ // only add to the font once
        //    textMesh.font.sourceFontFile.RequestCharactersInTexture("j", (int)textMesh.fontSize); // 'j' is the tallest character, all have the same advance
        //    textMesh.font.sourceFontFile.GetCharacterInfo('j', out jInfo, (int)textMesh.fontSize);
        //}
        CharacterInfo jInfo;
        if (!textBox.font.GetCharacterInfo('j', out jInfo, (int)textBox.fontSize))
        { // only add to the font once
            textBox.font.RequestCharactersInTexture("j", (int)textBox.fontSize); // 'j' is the tallest character, all have the same advance
            textBox.font.GetCharacterInfo('j', out jInfo, (int)textBox.fontSize);
        }

        Vector2 charDims = new Vector2(jInfo.advance, jInfo.glyphHeight);
        LetterCover.GetComponent<RectTransform>().sizeDelta = charDims; // change the prefab because it is the same for all covers

        Vector3 start = new Vector3(box.localPosition.x - box.rect.width / 2 + charDims.x / 2, box.localPosition.y + charDims.y * 0.25f, 0); // baseline alignment has bottom on midline

        Debug.Log(startText.Length);

        for (int i = 0; i < startText.Length; i++)
        {
            if (startText[i] != ' ')
            {
                PlaceCover(i, start, charDims.x);
            }
        }
    }

    private void PlaceCover(int charIndex, Vector3 start, float charWidth)
    {
        GameObject cover = Instantiate(LetterCover);
        cover.transform.SetParent(transform.parent);
        cover.transform.localScale = new Vector3(1, 1, 1);
        cover.transform.localPosition = start + new Vector3(charWidth * charIndex, 0, 0);

        // TODO: account for line breaks
    }
}
