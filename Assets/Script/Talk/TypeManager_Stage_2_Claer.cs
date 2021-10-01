using UnityEngine.SceneManagement;
public class TypeManager_Stage_2_Claer : TypeManager
{
    protected override void SoundCheck()
    {
        if (i == 0 || i == 1 || i == 12 || TalkText[i][j] == ' ')
            print("Non Sound");
        else
            AudioManager.Instance.PlaySound("PlayerTalk");
    }

    protected override void LoadNextScence()
    {
        SceneManager.LoadScene("Stage 3");
    }
}
