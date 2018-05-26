using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Load : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        // Les : Easy,Inter,Diff;
        // l1 : q1 q5 q8
        // AQ answered question : AQ_CatBasic1_LesEasy_Que5 
        // 1 answered , 0 not
        Question myQuestion = new Question();
        //PlayerPrefs.SetInt("AQLes1Que5",1);
        bool isAnsweredCorrectly = false;
        isAnsweredCorrectly = true;
        int answer = 0;
        if (isAnsweredCorrectly)
        {
            answer = 1;
        }else{
            answer = 0;
        }
        PlayerPrefs.SetInt("AQ_"+"Cat"+myQuestion.type.ToString()+"_"+"Les"+myQuestion.Mode.ToString()+"_"+"Que"+myQuestion.QuestionNum.ToString(),answer);
        // loading 
        //int[] questionNumbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        List<int> QuestionsUnAnswered = new List<int>();
        for (int i = 0; i < 50; i++)
        {
            if(PlayerPrefs.GetInt("AQ_" + "CatBasic1" + "_" + "Les1"+ "_" + "Que" + (i+1).ToString())==0){
                QuestionsUnAnswered.Add(i+1);
            }
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
