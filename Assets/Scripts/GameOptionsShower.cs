using UnityEngine.UI;
using System;
using UnityEngine;

[Serializable]
public class GameOption
{
    public bool IsValid { get { return UseOvershootBarText.IsTextValid && UseOvershootBar && Data; } }
    public TextUiData UseOvershootBarText = new TextUiData();
    public Button UseOvershootBar;
    public GameData Data;
}
public class GameOptionsShower : BaseOptionsShower
{
    [SerializeField]
    private GameOption gameOptions = new GameOption();

    public override void ResetValues()
    {
        if (gameOptions.Data)
        {
            gameOptions.Data.Reset();
        }
        else
        {
            DebugError();
        }
        DirtyUi();
    }

    public void ToggleUseOvershootBar()
    {
        if (gameOptions.Data)
        {
            gameOptions.Data.UseOvershootBar = !gameOptions.Data.UseOvershootBar;
        }
        else
        {
            DebugError();
        }
    }
    protected override void UpdateText()
    {
        if (gameOptions.IsValid)
        {
            gameOptions.UseOvershootBarText.Text = gameOptions.UseOvershootBarText.Prefix + gameOptions.Data.UseOvershootBar + gameOptions.UseOvershootBarText.Suffix;
        }
        else
        {
            DebugError();
        }
    }
    private void OnDisable()
    {
        if (gameOptions.Data)
        {
            gameOptions.Data.Save();
        }
        else
        {
            DebugError();
        }
    }
    void DebugError()
    {
        Debug.LogErrorFormat("{0} of type {1} requires GameOption to be fully initialized correctly", this, this.GetType());
    }
}
