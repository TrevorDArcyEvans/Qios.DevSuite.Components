// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDockContainerControlCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QDockContainerControlCollection : Control.ControlCollection
  {
    private QDockContainer m_oOwner;

    public QDockContainerControlCollection(QDockContainer owner)
      : base((Control) owner)
    {
      this.m_oOwner = owner;
    }

    public override void Add(Control value)
    {
      base.Add(value);
      this.m_oOwner.HandleControlAdded(value);
    }

    public override void Remove(Control value)
    {
      base.Remove(value);
      this.m_oOwner.HandleControlRemoved(value);
    }
  }
}
