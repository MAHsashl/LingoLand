using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestionMode { Easy, Intermed, Diffy }
public enum QuestionType { Basics1, Basics2, Animal}
public enum QuestionStruct {Choice, Pic }


[CreateAssetMenu(fileName = "Question", menuName = "", order = 1)]
public class Question : ScriptableObject {
    
    public string Title;
    public string Que;
    public QuestionMode Mode;
    public QuestionType type;
    public int correctAnswer;
    public List<string> options;
    public List<Sprite> pics;
}


