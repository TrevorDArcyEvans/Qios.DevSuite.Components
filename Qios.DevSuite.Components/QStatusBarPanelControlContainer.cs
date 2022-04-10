// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QStatusBarPanelControlContainer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  [TypeConverter(typeof (QStatusBarPanelConverter))]
  public class QStatusBarPanelControlContainer : QStatusBarPanel
  {
    private QWeakEventConsumerCollection m_oEventConsumers;
    private Control m_oControl;
    private Control m_oPreviousControlParent;
    private EventHandler m_oControlDockChangedEventHandler;

    public QStatusBarPanelControlContainer()
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oControlDockChangedEventHandler = new EventHandler(this.Control_DockChanged);
    }

    [Browsable(false)]
    [DefaultValue(null)]
    [Description("Gets or sets the Control that should be displayed on the QStatusBarPanel")]
    public Control Control
    {
      get => this.m_oControl;
      set
      {
        if (this.m_oControl != null)
        {
          this.m_oControl.Parent = this.m_oPreviousControlParent;
          this.m_oEventConsumers.DetachAndRemove((Delegate) this.m_oControlDockChangedEventHandler);
        }
        this.m_oControl = value;
        if (this.Parent == null)
          return;
        if (this.m_oControl != null)
        {
          this.m_oPreviousControlParent = this.m_oControl.Parent;
          this.m_oControl.Parent = (Control) this.Parent;
          this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) this.m_oControlDockChangedEventHandler, (object) this.m_oControl, "DockChanged"));
        }
        this.Parent.PerformLayout();
      }
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void SetBounds(Rectangle rectangle)
    {
      base.SetBounds(rectangle);
      if (this.Control == null)
        return;
      switch (this.Control.Dock)
      {
        case DockStyle.None:
          this.Control.SetBounds(this.Bounds.Left + this.Padding.Left, this.Bounds.Top + this.Padding.Top, this.Bounds.Width - (this.Padding.Left + this.Padding.Right), this.Bounds.Height - (this.Padding.Top + this.Padding.Bottom));
          break;
        case DockStyle.Top:
          this.Control.SetBounds(this.Bounds.Left + this.Padding.Left, this.Bounds.Top + this.Padding.Top, this.Bounds.Width - (this.Padding.Left + this.Padding.Right), this.Control.Height);
          break;
        case DockStyle.Bottom:
          this.Control.SetBounds(this.Bounds.Left + this.Padding.Left, this.Bounds.Height - this.Control.Height - this.Padding.Bottom, this.Bounds.Width - (this.Padding.Left + this.Padding.Right), this.Control.Height);
          break;
        case DockStyle.Left:
          this.Control.SetBounds(this.Bounds.Left + this.Padding.Left, this.Bounds.Top + this.Padding.Top, this.Control.Width, this.Bounds.Height - (this.Padding.Top + this.Padding.Bottom));
          break;
        case DockStyle.Right:
          this.Control.SetBounds(this.Bounds.Left + (this.Bounds.Width - (this.Padding.Right + this.Control.Width)), this.Bounds.Top + this.Padding.Top, this.Control.Width, this.Bounds.Height - (this.Padding.Top + this.Padding.Bottom));
          break;
        case DockStyle.Fill:
          this.Control.SetBounds(this.Bounds.Left + this.Padding.Left, this.Bounds.Top + this.Padding.Top, this.Bounds.Width - (this.Padding.Left + this.Padding.Right), this.Bounds.Height - (this.Padding.Top + this.Padding.Bottom));
          break;
      }
    }

    private void Control_DockChanged(object sender, EventArgs e)
    {
      if (this.Parent == null)
        return;
      this.Parent.PerformLayout();
    }
  }
}
