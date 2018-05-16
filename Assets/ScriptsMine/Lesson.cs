using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lesson", menuName = "", order = 1)]
public class Lesson : ScriptableObject
{
    public QuestionMode Mode;
    public QuestionType type;
    public string title;
}