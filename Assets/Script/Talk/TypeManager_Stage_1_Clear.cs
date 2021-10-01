using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypeManager_Stage_1_Clear : TypeManager
{
    protected override void SoundCheck()
    {
        if (i == 2 || i == 10 || i == 3 || TalkText[i][j] == ' ') 
            print("Non Sound");
        else
            AudioManager.Instance.PlaySound("PlayerTalk");
    }

    protected override void LoadNextScence()
    {
        SceneManager.LoadScene("Stage 2");
    }
    public override void DoTextSkip()

    {
        print("OnCLick");
        ToTextSkip = true;
    }
}
