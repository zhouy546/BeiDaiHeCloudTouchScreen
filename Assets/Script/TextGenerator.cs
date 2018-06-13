using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGenerator : UITextBase {
    char[] PatternUppercase = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

    char[] PatternLowercase = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };


    private string result;
    public string Result {
        get
        {
            return result;
        }
        set {
            result = value;
            text.text = value;
        }
    }

    // Use this for initialization
    void Start() {

        base.Start();
        //GenerateLine();

    }

    // Update is called once per frame
    void Update() {
        base.Update();
    }

    public override void initialization()
    {
        text = this.GetComponent<Text>();
        CurrentColor = Defaultcolor = text.color;
        HideText(0);
    }

    public override void HideText(float time)
    {
        StopAllCoroutines();
        ChangeTextAlpha(0, time, () => onDisplay = false);
        CleanText();
    }

    public override void ShowText(float time)
    {
        ChangeTextAlpha(1, time, () => onDisplay = false);
        GenerateTextBlock();
    }

    private void CleanText() {
        Result = "";
    }

    public void GenerateTextBlock()
    {
        StartCoroutine(IGenerateBlock());
    }

    IEnumerator UpdateText() {
        float time = Random.Range(1f, 2f);
        yield return new WaitForSeconds(time);
        text.text = GenerateBlock();
        yield return StartCoroutine(UpdateText());

    }

    IEnumerator IGenerateBlock(bool isUpdateText =true)
    {
        int Line = Random.Range(3, 5);
        for (int i = 0; i < Line; i++)
        {
            yield return StartCoroutine(IGenerateLine());
        }

        if (isUpdateText) {
            StartCoroutine(UpdateText());
        }
    }

    IEnumerator IGenerateLine(){
        int wordsEachLine = Random.Range(3, 7);
        yield return StartCoroutine( IGenerateWords(true));
        for (int i = 0; i < wordsEachLine; i++)
        {
       yield return  StartCoroutine(IGenerateWords());
        }
        Result += "\n";
    }

    IEnumerator IGenerateWords(bool isFirstWord = false)
    {
        int num = Random.Range(1, 10);
        if (isFirstWord)
        {        
            int n = PatternUppercase.Length;

            int rnd = Random.Range(0, n);
            Result += PatternUppercase[rnd];

            yield return new WaitForSeconds(.001f);

            for (int i = 1; i < num; i++)
            {
                int r = Random.Range(0, n);
                Result += PatternLowercase[r];
             yield return new WaitForSeconds(.001f);

            }
            Result += " ";
        }
        else {
            int n = PatternUppercase.Length;
            for (int i = 0; i < num; i++)
            {
                int r = Random.Range(0, n);
                Result += PatternLowercase[r];
                yield return new WaitForSeconds(.001f);

            }
            Result += " ";
        }
    }

    string GenerateBlock() {
        int Line = Random.Range(3, 5);
        string temp = "";
        for (int i = 0; i < Line; i++)
        {
            temp += GenerateLine();
        }
        return temp;
    }

    string GenerateLine()
    {
        string temp = "";
        int wordsEachLine = Random.Range(3, 7);
        temp+=   GenerateWords(true);
        for (int i = 0; i < wordsEachLine; i++)
        {
       temp+=      GenerateWords();
        }
        temp += "\n";
        return temp;
    }


    string GenerateWords(bool isFirstWord = false) {
        int num = Random.Range(1, 10);
        string   tempResult = "";
        if (isFirstWord)
        {
            int n = PatternUppercase.Length;
            int rnd = Random.Range(0, n);
            tempResult += PatternUppercase[rnd];
            for (int i = 1; i < num; i++)
            {
                int r = Random.Range(0, n);
                tempResult += PatternLowercase[r];
            }
            tempResult += " ";
        }
        else
        {
            int n = PatternUppercase.Length;
            for (int i = 0; i < num; i++)
            {
                int r = Random.Range(0, n);
                tempResult += PatternLowercase[r];
            }
            tempResult += " ";
        }

        return tempResult;
    }
}
