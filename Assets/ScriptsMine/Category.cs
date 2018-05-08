using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Category", menuName = "", order = 1)]
public class Category : ScriptableObject {

    public List<Lesson> lessons;
}
