using UnityEngine.UI;
using System;
[Serializable]
public class TextUiData
{
    public string Text { get { return Ui.text; } set { Ui.text = value; } }
    public bool IsTextValid { get { return Ui && Prefix != null && Suffix != null; } }
    public string Prefix = string.Empty;
    public string Suffix = string.Empty;
    public Text Ui;
}