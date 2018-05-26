using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameMng : MonoBehaviour {
    
    public GameObject CategoryScreen;
    public GameObject LessonSelectionScreen;
    public GameObject ChoiceQuestionScreen;
    public GameObject PicQuestionScreen;
    public GameObject ResultsScreen;

    public Text Ltext1;
    public Text Ltext2;
    public Text Ltext3;


    public List<Question> QuestionList;

    // For choice question Title
    public Text QTitleText1;

    // For pic questions Title and que
    public Text QPicTitleText;
    public Text QPicQueText;

    // For pic question options' texts
    /*
    public Text QPicOptionText1;
    public Text QPicOptionText2;
    public Text QPicOptionText3;
    public Text QPicOptionText4;
*/
    // For pic question options' sprites
    public Image QPicOptionSprite1;
    public Image QPicOptionSprite2;
    public Image QPicOptionSprite3;
    public Image QPicOptionSprite4;




    public int CurrentQuestionIndex;

    // For choice question options
    public Text QOptionText1;
    public Text QOptionText2;
    public Text QOptionText3;
    public Text QOptionText4;

    public void ShowCategoryScreen(){
        
        CategoryScreen.SetActive(true);
        LessonSelectionScreen.SetActive(false);
        ChoiceQuestionScreen.SetActive(false);
        PicQuestionScreen.SetActive(false);
        ResultsScreen.SetActive(false);
          
    }

    public void ShowLessonSelectionScreen()
    {

        CategoryScreen.SetActive(false);
        LessonSelectionScreen.SetActive(true);
        ChoiceQuestionScreen.SetActive(false);
        PicQuestionScreen.SetActive(false);
        ResultsScreen.SetActive(false);

        Ltext1.text = selectedCategory.lessons[0].title;
        Ltext2.text = selectedCategory.lessons[1].title;
        Ltext3.text = selectedCategory.lessons[2].title;

    }

    public void ShowChoiceQuestionScreen()
    {

        CategoryScreen.SetActive(false);
        LessonSelectionScreen.SetActive(false);
        ChoiceQuestionScreen.SetActive(true);
        PicQuestionScreen.SetActive(false);
        ResultsScreen.SetActive(false);

    }

    public void ShowPicQuestionScreen()
    {

        CategoryScreen.SetActive(false);
        LessonSelectionScreen.SetActive(false);
        ChoiceQuestionScreen.SetActive(false);
        PicQuestionScreen.SetActive(true);
        ResultsScreen.SetActive(false);
    }

    public void ShowResultsScreen()
    {

        CategoryScreen.SetActive(false);
        LessonSelectionScreen.SetActive(false);
        ChoiceQuestionScreen.SetActive(false);
        PicQuestionScreen.SetActive(false);
        ResultsScreen.SetActive(true);

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
        System.Random rnd = new System.Random();

        QuestionList = selectedLesson.questions.OrderBy(x => rnd.Next()).Take(3).ToList();

        CurrentQuestionIndex = 0;
        SelectQuestionPanel();

    }
    public void ShowChoiceQuestion()
    {

        // Questions' Title
        QTitleText1.text = QuestionList[CurrentQuestionIndex].Title;

        // Buttons' Text
        QOptionText1.text = QuestionList[CurrentQuestionIndex].options[0];
        QOptionText2.text = QuestionList[CurrentQuestionIndex].options[1];
        QOptionText3.text = QuestionList[CurrentQuestionIndex].options[2];
        QOptionText4.text = QuestionList[CurrentQuestionIndex].options[3];


    }

    public void ShowPicQuestion(){

        // Questions' Title and Que
        QPicTitleText.text = QuestionList[CurrentQuestionIndex].Title;
        QPicQueText.text = QuestionList[CurrentQuestionIndex].Que;

        // Buttons' sprites
        QPicOptionSprite1.sprite = QuestionList[CurrentQuestionIndex].pics[0];
        QPicOptionSprite2.sprite = QuestionList[CurrentQuestionIndex].pics[1];
        QPicOptionSprite3.sprite = QuestionList[CurrentQuestionIndex].pics[2];
        QPicOptionSprite4.sprite = QuestionList[CurrentQuestionIndex].pics[3];
    }
   
    public void ShowNextQuestion(){


        if(CurrentQuestionIndex <= 2){

            CurrentQuestionIndex++;
        }
        else
            ShowResultsScreen();
        

        //SelectQuestionPanel();
        
    }

    public void SelectQuestionPanel(){

        if(QuestionList[CurrentQuestionIndex].structure == QuestionStruct.Choice)
        {
            ShowChoiceQuestionScreen();
            ShowChoiceQuestion();
        }
        else if(QuestionList[CurrentQuestionIndex].structure == QuestionStruct.Pic)
        {
            ShowPicQuestionScreen();
            ShowPicQuestion();
        }
    }


	void Start () {

        ShowCategoryScreen();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
