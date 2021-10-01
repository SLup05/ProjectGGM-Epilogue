using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypeManager : MonoBehaviour
{
    [SerializeField] protected Text Talktxt;

    [SerializeField] protected TextAsset txtFile = null;
    protected string[] TalkText;
    private int lineSize;

    public GameObject StageChange = null;

    protected bool ToTextSkip = false;

    protected int i, j;
    void Awake()
    {
        SetString();
        StartCoroutine(Typing());
    }
    
    private void SetString()
    {
        string currentText = txtFile.text.Substring(0, txtFile.text.Length - 1);
        string[] line = currentText.Split('\n');
        lineSize = line.Length;
        TalkText = new string[lineSize];

        for(int i = 0; i < lineSize; i++)
        {
            TalkText[i] = line[i];
            //print(TalkText[i]);
        }
        
    }

    protected virtual IEnumerator Typing()
    {
        //print("In Typing");
        for(i = 0; i < TalkText.Length; i++)
        {
            ToTextSkip = false;

            for (j = 0; j < TalkText[i].Length; j++)
            {
                SoundCheck();

                if (ToTextSkip) j = TalkText[i].Length-1;
                Talktxt.text = TalkText[i].Substring(0, j);

                if(TalkText[i][j] == '.')
                    yield return new WaitForSeconds(0.3f);
                else if(TalkText[i][j] == ',')
                    yield return new WaitForSeconds(0.2f);
                else
                    yield return new WaitForSeconds(0.07f);
            }
            yield return new WaitForSeconds(1.5f);
        }
        LoadNextScence();
    }
    protected virtual void SoundCheck()
    {
        if ( i == 3 || i == 14 || i == 4 || i == 15 || TalkText[i][j] == ' ') { print("Non Sound"); }
        else 
        {
            AudioManager.Instance.PlaySound("PlayerTalk");
        }
    }

    protected virtual void LoadNextScence()
    {
        StageChange.GetComponent<OpenAndClose>().ScenceFadein();
        SceneManager.LoadScene("Tutorial");
    }

    public virtual void DoTextSkip()

    {
        ToTextSkip = true;
    }
    public virtual void DoAllSkip()
    {
        LoadNextScence();
    }
}

