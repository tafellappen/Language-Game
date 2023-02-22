using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

// when attached to a text box, this will transform some or all of the english into alien text
public class Translator : MonoBehaviour
{
    [SerializeField] private GameObject LetterCover;

    void Start()
    {
        Rect box = GetComponent<RectTransform>().rect;
        TMPro.TextMeshProUGUI textMesh = GetComponent<TMPro.TextMeshProUGUI>();
        string startText = textMesh.text;

        // turn letters into random characters
        string newText = "";
        for(int i = 0; i < startText.Length; i++) {
            if(startText[i] == ' ') {
                newText += " ";
            }
            else {
                newText += (char)(startText[i] + Random.Range(50, 100));
            }
        }
        textMesh.text = newText;

        //GameObject cover = Instantiate(LetterCover);
        //cover.transform.parent = transform.parent;
        //cover.transform.localPosition = box.min;
    }

    void Update()
    {
        
    }
}
