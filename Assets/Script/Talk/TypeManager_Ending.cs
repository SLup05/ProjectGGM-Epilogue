using System.Collections;
using UnityEngine;

public class TypeManager_Ending : TypeManager
{
    protected override IEnumerator Typing()
    {
        //print("In Typing");
        for (i = 0; i < TalkText.Length; i++)
        {
            if (i == 3)
                AudioManager.Instance.PlaySound("Exp1st");
            if (i == 6)
                AudioManager.Instance.PlaySound("Exp2nd");
            for (j = 0; j < TalkText[i].Length; j++)
            {
                SoundCheck();

                Talktxt.text = TalkText[i].Substring(0, j);

                if (TalkText[i][j] == '.')
                    yield return new WaitForSeconds(0.3f);
                else if (TalkText[i][j] == ',')
                    yield return new WaitForSeconds(0.2f);
                else
                    yield return new WaitForSeconds(0.07f);
            }

            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(1f);
        LoadNextScence();
    }
    protected override void SoundCheck()
    {
        if (i == 6 || TalkText[i][j] == ' ' || TalkText[i][j] == '\n')
            print("Non Sound");
        else if (i == 3 || i == 7 || i == 8)
        { }
        else if (i == 9)
            AudioManager.Instance.PlaySound("UITalk");
        else
            AudioManager.Instance.PlaySound("PlayerTalk");
    }

    protected override void LoadNextScence()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
    }

    public override void DoTextSkip()
    {
    }

    public override void DoAllSkip()
    {
    }
}
