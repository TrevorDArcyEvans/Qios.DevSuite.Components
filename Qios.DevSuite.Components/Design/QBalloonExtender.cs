// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QBalloonExtender
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Design
{
  [ProvideProperty("QBalloon", typeof (Control))]
  [ProvideProperty("QBalloonMarkupText", typeof (Control))]
  public class QBalloonExtender : IExtenderProvider
  {
    private ArrayList m_aPairs;

    public QBalloonExtender() => this.m_aPairs = new ArrayList();

    public bool CanExtend(object extendee) => extendee is Control;

    public void Merge(QBalloonExtender extender)
    {
      if (extender == null)
        return;
      for (int index = 0; index < extender.m_aPairs.Count; ++index)
      {
        QBalloonExtenderPair aPair = (QBalloonExtenderPair) extender.m_aPairs[index];
        if (!this.Contains(aPair.Component, aPair.Balloon))
          this.m_aPairs.Add((object) aPair);
      }
    }

    internal QBalloonExtenderPair[] GetPairs(QBalloon balloon)
    {
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < this.m_aPairs.Count; ++index)
      {
        QBalloonExtenderPair aPair = (QBalloonExtenderPair) this.m_aPairs[index];
        if (aPair.Balloon == balloon && aPair.Component is Control && !((Control) aPair.Component).IsDisposed)
          arrayList.Add((object) aPair);
      }
      return (QBalloonExtenderPair[]) arrayList.ToArray(typeof (QBalloonExtenderPair));
    }

    public Control[] GetControls(QBalloon balloon)
    {
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < this.m_aPairs.Count; ++index)
      {
        QBalloonExtenderPair aPair = (QBalloonExtenderPair) this.m_aPairs[index];
        if (aPair.Balloon == balloon && aPair.Component is Control && !((Control) aPair.Component).IsDisposed)
          arrayList.Add((object) (Control) aPair.Component);
      }
      return (Control[]) arrayList.ToArray(typeof (Control));
    }

    [Category("QBalloon")]
    [Localizable(true)]
    [DefaultValue(null)]
    [Description("Gets or sets the markupText that is used for displaying the tooltip")]
    public string GetQBalloonMarkupText(Control component) => this.GetPair((IComponent) component)?.Text;

    [Category("QBalloon")]
    [Description("Gets or sets which QBalloon is used for displaying the tooltip")]
    [DefaultValue(null)]
    public QBalloon GetQBalloon(Control component) => this.GetPair((IComponent) component)?.Balloon;

    public void SetQBalloon(Control component, QBalloon balloon)
    {
      QBalloonExtenderPair pair = this.GetPair((IComponent) component);
      if (balloon != null)
      {
        if (pair == null)
        {
          QBalloonControlListener listenerForControl = balloon.GetListenerForControl(component);
          this.m_aPairs.Add((object) new QBalloonExtenderPair(balloon, (IComponent) component, listenerForControl == null ? (string) null : listenerForControl.Text));
        }
        else
          pair.Balloon = balloon;
      }
      else
      {
        if (pair == null)
          return;
        this.m_aPairs.Remove((object) pair);
      }
    }

    [Localizable(true)]
    public void SetQBalloonMarkupText(Control component, string markupText)
    {
      QBalloonExtenderPair pair = this.GetPair((IComponent) component);
      if (markupText != null)
      {
        if (pair == null)
          this.m_aPairs.Add((object) new QBalloonExtenderPair((QBalloon) null, (IComponent) component, markupText));
        else
          pair.Text = markupText;
      }
      else
      {
        if (pair == null || pair.Balloon != null)
          return;
        this.m_aPairs.Remove((object) pair);
      }
    }

    internal QBalloonExtenderPair GetPair(IComponent component)
    {
      for (int index = 0; index < this.m_aPairs.Count; ++index)
      {
        QBalloonExtenderPair aPair = (QBalloonExtenderPair) this.m_aPairs[index];
        if (aPair.Component == component)
          return aPair;
      }
      return (QBalloonExtenderPair) null;
    }

    internal bool Contains(IComponent component, QBalloon balloon)
    {
      for (int index = 0; index < this.m_aPairs.Count; ++index)
      {
        QBalloonExtenderPair aPair = (QBalloonExtenderPair) this.m_aPairs[index];
        if (aPair.Balloon == balloon || aPair.Component == component)
          return true;
      }
      return false;
    }
  }
}
