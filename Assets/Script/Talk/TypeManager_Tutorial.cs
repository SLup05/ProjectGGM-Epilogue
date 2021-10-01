using UnityEngine.SceneManagement;

public class TypeManager_Tutorial : TypeManager
{
    
    protected override void SoundCheck()
    {
        AudioManager.Instance.PlaySound("UITalk");
    }

    protected override void LoadNextScence()
    {
        StageChange.GetComponent<OpenAndClose>().ScenceFadein();
        SceneManager.LoadScene("Stage 1");
    }
}
