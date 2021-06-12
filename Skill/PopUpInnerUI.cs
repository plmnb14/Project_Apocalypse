using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpInnerUI : PopUpUI
{
    #region Public Events
    public virtual void OpenSetUp(bool isSimple = false) { }
    public virtual void OpenDetailPopUp() { }
    public override void RemoveEvents() { base.RemoveEvents(); }
    #endregion
}
