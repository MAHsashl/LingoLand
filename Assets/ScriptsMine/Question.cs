using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestionType
{
    Pic,Choice
}

[CreateAssetMenu(fileName = "Question", menuName = "", order = 1)]
public class Question : ScriptableObject {

    public QuestionType type;
    public string title;
    public int correctAnswer;
    public List<Sprite> picture;
    List<string> options;

}
