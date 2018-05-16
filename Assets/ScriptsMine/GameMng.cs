using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMng : MonoBehaviour {
    
    public GameObject CategoryScreen;
    public GameObject LessonSelectionScreen;
    public GameObject QuestionScreen;
    //public GameObject ResultsScreen;

    public Text Ltext1;
    public Text Ltext2;
    public Text Ltext3;




    public void ShowCategoryScreen(){
        
        CategoryScreen.SetActive(true);
        LessonSelectionScreen.SetActive(false);
        QuestionScreen.SetActive(false);
        //ResultsScreen.SetActive(false);

       // Toolbox.Instance.lessonList[index]; 
       // Toolbox.Instance.questionList[index];

          
    }

    public void ShowLessonSelectionScreen()
    {

        CategoryScreen.SetActive(false);
        LessonSelectionScreen.SetActive(true);
        QuestionScreen.SetActive(false);
        //ResultsScreen.SetActive(false);

        Ltext1.text = selectedCategory.lessons[0].title;
        Ltext2.text = selectedCategory.lessons[1].title;
        Ltext3.text = selectedCategory.lessons[2].title;

    }
    public void ShowQuestionScreen()
    {

        CategoryScreen.SetActive(false);
        LessonSelectionScreen.SetActive(false);
        QuestionScreen.SetActive(true);
       // ResultsScreen.SetActive(false);

    }
    public void ShowResultsScreen()
    {

        CategoryScreen.SetActive(false);
        LessonSelectionScreen.SetActive(false);
        QuestionScreen.SetActive(false);
       // ResultsScreen.SetActive(true);

    }

    Category selectedCategory;
    public void CategoryButtonHandler(int index)
    {
        selectedCategory = Toolbox.Instance.categorylist[index];
        ShowLessonSelectionScreen();
    }

    Lesson selectedLesson;
    public void LessonButtonHandler(int index)
    {
        selectedLesson = selectedCategory.lessons[index];


    }
    public void QuestionHandler(int index)
    {
        
        List<Question> localList;


    }

    public void StartNewGame(){

        ShowQuestionScreen();
    }
	// Use this for initialization
	void Start () {

        ShowCategoryScreen();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
