// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QCompositeMenuExtender
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Design
{
  [ProvideProperty("QCompositeMenu", typeof (Component))]
  public class QCompositeMenuExtender : IExtenderProvider
  {
    private ArrayList m_aMenuComponentPairs;

    public QCompositeMenuExtender() => this.m_aMenuComponentPairs = new ArrayList();

    public bool CanExtend(object extendee)
    {
      switch (extendee)
      {
        case Control _:
          return true;
        case NotifyIcon _:
          return true;
        default:
          return false;
      }
    }

    public void Merge(QCompositeMenuExtender extender)
    {
      if (extender == null)
        return;
      for (int index = 0; index < extender.m_aMenuComponentPairs.Count; ++index)
      {
        QCompositeMenuExtender.QMenuComponentPair menuComponentPair = (QCompositeMenuExtender.QMenuComponentPair) extender.m_aMenuComponentPairs[index];
        if (!this.Contains(menuComponentPair.Component, menuComponentPair.Menu))
          this.m_aMenuComponentPairs.Add((object) menuComponentPair);
      }
    }

    public Component[] GetComponents(QCompositeMenu menu)
    {
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < this.m_aMenuComponentPairs.Count; ++index)
      {
        QCompositeMenuExtender.QMenuComponentPair menuComponentPair = (QCompositeMenuExtender.QMenuComponentPair) this.m_aMenuComponentPairs[index];
        if (menuComponentPair.Menu == menu && menuComponentPair.Component is Component)
          arrayList.Add((object) (Component) menuComponentPair.Component);
      }
      return (Component[]) arrayList.ToArray(typeof (Component));
    }

    [Description("Gets or sets which QCompositeMenu is used when the right mouse button is pressed")]
    [DefaultValue(null)]
    [Category("QCompositeMenu")]
    public QCompositeMenu GetQCompositeMenu(Component component) => this.GetPair((IComponent) component)?.Menu;

    public void SetQCompositeMenu(Component component, QCompositeMenu menu)
    {
      QCompositeMenuExtender.QMenuComponentPair pair = this.GetPair((IComponent) component);
      if (menu != null)
      {
        if (pair == null)
          this.m_aMenuComponentPairs.Add((object) new QCompositeMenuExtender.QMenuComponentPair(menu, (IComponent) component));
        else
          pair.Menu = menu;
      }
      else
      {
        if (pair == null)
          return;
        this.m_aMenuComponentPairs.Remove((object) pair);
      }
    }

    internal QCompositeMenuExtender.QMenuComponentPair GetPair(
      IComponent component)
    {
      for (int index = 0; index < this.m_aMenuComponentPairs.Count; ++index)
      {
        QCompositeMenuExtender.QMenuComponentPair menuComponentPair = (QCompositeMenuExtender.QMenuComponentPair) this.m_aMenuComponentPairs[index];
        if (menuComponentPair.Component == component)
          return menuComponentPair;
      }
      return (QCompositeMenuExtender.QMenuComponentPair) null;
    }

    internal bool Contains(IComponent component, QCompositeMenu menu)
    {
      for (int index = 0; index < this.m_aMenuComponentPairs.Count; ++index)
      {
        QCompositeMenuExtender.QMenuComponentPair menuComponentPair = (QCompositeMenuExtender.QMenuComponentPair) this.m_aMenuComponentPairs[index];
        if (menuComponentPair.Menu == menu || menuComponentPair.Component == component)
          return true;
      }
      return false;
    }

    internal class QMenuComponentPair
    {
      public QCompositeMenu Menu;
      public IComponent Component;

      public QMenuComponentPair(QCompositeMenu menu, IComponent component)
      {
        this.Menu = menu;
        this.Component = component;
      }
    }
  }
}
