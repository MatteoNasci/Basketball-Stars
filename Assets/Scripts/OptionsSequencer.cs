using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsSequencer : MonoSequencer<BaseOptionsShower>
{
    public void ResetCurrentOptionsValues()
    {
        BaseOptionsShower options = GetCurrentElement();
        if (options)
        {
            options.ResetValues();
        }
    }
}
