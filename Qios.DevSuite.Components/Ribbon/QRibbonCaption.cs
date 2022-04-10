// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonCaption
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Ribbon
{
  [Designer(typeof (QRibbonCaptionDesigner), typeof (IDesigner))]
  [ToolboxBitmap(typeof (QCompositeControl), "Resources.ControlImages.QRibbonCaption.bmp")]
  [ToolboxItem(true)]
  public class QRibbonCaption : QCompositeControl, IQComponentHost
  {
    private QRibbonLaunchBar m_oLaunchBar;
    private QRibbonApplicationButton m_oApplicationButton;
    private QWeakEventConsumerCollection m_oEventConsumers;
    private QRibbonCaptionControlListener m_oListener;
    private Control m_oAttachedControl;
    private bool m_bMaximized;
    private bool m_bMinimizeBox = true;
    private bool m_bMaximizeBox = true;
    private bool m_bControlBox = true;
    private bool m_bActive;
    private bool m_bChangingCaptionControl;

    public QRibbonCaption()
    {
      this.Dock = DockStyle.Top;
      this.PaintTransparentBackground = false;
      this.SuspendChangeNotification();
      this.ResumeChangeNotification(false);
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
    }

    protected override QComposite CreateComposite() => (QComposite) new QRibbonCaptionComposite(this);

    [DefaultValue(DockStyle.Top)]
    public override DockStyle Dock
    {
      get => base.Dock;
      set => base.Dock = value;
    }

    public bool ShouldSerializeCaptionFont() => this.RibbonCaptionComposite.CaptionFont.ToString() != QRibbonHelper.GetRibbonCaptionFont().ToString();

    public void ResetCaptionFont() => this.RibbonCaptionComposite.CaptionFont = QRibbonHelper.GetRibbonCaptionFont();

    [Category("QAppearance")]
    [Description("Gets or sets the Font for the caption.")]
    public Font CaptionFont
    {
      get => this.RibbonCaptionComposite.CaptionFont;
      set => this.RibbonCaptionComposite.CaptionFont = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QRibbonCaptionComposite RibbonCaptionComposite => this.Composite as QRibbonCaptionComposite;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Localizable(true)]
    public string ApplicationText
    {
      get => this.RibbonCaptionComposite.ApplicationText;
      set => this.RibbonCaptionComposite.ApplicationText = value;
    }

    [Localizable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string DocumentText
    {
      get => this.RibbonCaptionComposite.DocumentText;
      set => this.RibbonCaptionComposite.DocumentText = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Control AttachedToControl => this.m_oAttachedControl;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Form AttachedToForm => this.m_oAttachedControl as Form;

    [DefaultValue(false)]
    public override bool PaintTransparentBackground
    {
      get => base.PaintTransparentBackground;
      set => base.PaintTransparentBackground = value;
    }

    [Description("Gets or sets the LaunchBar that must be hosted on this QRibbonCaption.")]
    [Category("QBehavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(null)]
    public QRibbonLaunchBar LaunchBar
    {
      get => this.m_oLaunchBar;
      set => this.SetLaunchBar(value, true);
    }

    internal void SetLaunchBar(QRibbonLaunchBar value, bool removeFromCurrentHost)
    {
      if (this.m_oLaunchBar == value)
        return;
      this.SuspendChangeNotification();
      if (this.m_oLaunchBar != null)
      {
        this.m_oLaunchBar.SetCustomComponentHost((IQComponentHost) null, false);
        this.LaunchBarAreaPart.Parts.Remove((IQPart) this.m_oLaunchBar);
      }
      this.m_oLaunchBar = value;
      if (this.m_oLaunchBar != null)
      {
        this.m_oLaunchBar.SetCustomComponentHost((IQComponentHost) this, removeFromCurrentHost);
        this.m_oLaunchBar.Active = this.Active;
        this.m_oLaunchBar.DrawShape = true;
        this.LaunchBarAreaPart.Parts.Add((IQPart) this.m_oLaunchBar, false);
      }
      this.ResumeChangeNotification(true);
    }

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonCaption)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonSeparator)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeBalloon)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItemBar)]
    [Category("QAppearance")]
    [Description("The ColorScheme that is used.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonLaunchBar)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItem)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("QBehavior")]
    [DefaultValue(null)]
    [Description("Gets or sets the ApplicationButton that must be hosted on this QRibbonCaption.")]
    public QRibbonApplicationButton ApplicationButton
    {
      get => this.m_oApplicationButton;
      set => this.SetApplicationButton(value, true);
    }

    internal void SetApplicationButton(QRibbonApplicationButton value, bool removeFromCurrentHost)
    {
      if (this.m_oApplicationButton == value)
        return;
      this.SuspendChangeNotification();
      if (this.m_oApplicationButton != null)
      {
        this.m_oApplicationButton.SetCustomComponentHost((IQComponentHost) null, false);
        this.ApplicationButtonAreaPart.Parts.Remove((IQPart) this.m_oApplicationButton);
      }
      this.m_oApplicationButton = value;
      if (this.m_oApplicationButton != null)
      {
        this.m_oApplicationButton.SetCustomComponentHost((IQComponentHost) this, removeFromCurrentHost);
        this.ApplicationButtonAreaPart.Parts.Add((IQPart) this.m_oApplicationButton, false);
      }
      this.ResumeChangeNotification(true);
    }

    [Localizable(true)]
    public override string Text
    {
      get => this.RibbonCaptionComposite.Text;
      set
      {
        if (base.Text == value)
          return;
        this.RibbonCaptionComposite.Text = value;
        base.Text = value;
        if (this.m_oAttachedControl != null)
        {
          this.m_bChangingCaptionControl = true;
          this.m_oAttachedControl.Text = value;
          this.m_bChangingCaptionControl = false;
        }
        this.HandleChildObjectChanged(true, Rectangle.Empty);
      }
    }

    public override QPartCollection Items => this.ItemAreaPart.Parts;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected override IList AssociatedComponents
    {
      get
      {
        ArrayList associatedComponents = new ArrayList((ICollection) this.ItemAreaPart.Parts);
        if (this.ApplicationButton != null)
          associatedComponents.Add((object) this.ApplicationButton);
        if (this.LaunchBar != null)
          associatedComponents.Add((object) this.LaunchBar);
        return (IList) associatedComponents;
      }
    }

    [Description("Gets the configuration of the QRibbonCaption")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonCaptionConfiguration Configuration => base.Configuration as QRibbonCaptionConfiguration;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QRibbonCaptionButton CloseButton => this.RibbonCaptionComposite.CloseButton;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QRibbonCaptionButton MinimizeButton => this.RibbonCaptionComposite.MinimizeButton;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QRibbonCaptionButton MaximizeButton => this.RibbonCaptionComposite.MaximizeButton;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QRibbonCaptionButton RestoreButton => this.RibbonCaptionComposite.RestoreButton;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QRibbonCaptionButton CurrentRestoreButton => this.IsMaximized ? this.RibbonCaptionComposite.RestoreButton : this.RibbonCaptionComposite.MaximizeButton;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QCompositeIcon IconPart => this.RibbonCaptionComposite.IconPart;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QPart ItemAreaPart => this.RibbonCaptionComposite.ItemAreaPart;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QPart ButtonAreaPart => this.RibbonCaptionComposite.ButtonAreaPart;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QPart ApplicationButtonAreaPart => this.RibbonCaptionComposite.ApplicationButtonAreaPart;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QPart LaunchBarAreaPart => this.RibbonCaptionComposite.LaunchBarAreaPart;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QPart TextAreaPart => this.RibbonCaptionComposite.TextAreaPart;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool ControlBox
    {
      get => this.m_bControlBox;
      set
      {
        if (this.m_bControlBox == value)
          return;
        this.m_bControlBox = value;
        this.IconPart.Visible = value;
        this.MinimizeButton.Visible = value && this.m_bMinimizeBox;
        this.CurrentRestoreButton.Visible = value && this.m_bMaximizeBox;
        this.CloseButton.Visible = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MinimizeBox
    {
      get => this.m_bMinimizeBox;
      set
      {
        if (this.m_bMinimizeBox == value)
          return;
        this.m_bMinimizeBox = value;
        this.MinimizeButton.Visible = value && this.m_bControlBox;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MaximizeBox
    {
      get => this.m_bMaximizeBox;
      set
      {
        if (this.m_bMaximizeBox == value)
          return;
        this.m_bMaximizeBox = value;
        this.CurrentRestoreButton.Visible = value && this.m_bControlBox;
      }
    }

    public bool IsMaximized => this.m_bMaximized;

    internal void PutMaximized(bool value)
    {
      if (this.m_bMaximized == value)
        return;
      this.m_bMaximized = value;
      if (this.m_bMaximized)
      {
        if (this.m_bMaximizeBox && this.m_bControlBox)
          this.RestoreButton.Visible = true;
        this.MaximizeButton.Visible = false;
      }
      else
      {
        this.RestoreButton.Visible = false;
        if (!this.m_bMaximizeBox || !this.m_bControlBox)
          return;
        this.MaximizeButton.Visible = true;
      }
    }

    protected override string BackColorPropertyName => "RibbonCaptionBackground1";

    protected override string BackColor2PropertyName => "RibbonCaptionBackground2";

    protected override string BorderColorPropertyName => "RibbonCaptionBorder";

    internal bool Active
    {
      get => this.m_bActive || this.DesignMode;
      set
      {
        this.m_bActive = value;
        if (this.m_oLaunchBar != null)
          this.m_oLaunchBar.Active = this.Active;
        this.HandleChildObjectChanged(false, this.ClientRectangle);
        this.Update();
      }
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      if (this.Parent is Form parent && parent.MdiParent != null && this.Dock == DockStyle.Top && NativeHelper.GetCurrentFormState(parent) == FormWindowState.Maximized)
        base.SetBoundsCore(x, y, width, SystemInformation.CaptionHeight, specified);
      else
        base.SetBoundsCore(x, y, width, height, specified);
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 132)
      {
        Point client = this.PointToClient(new Point(m.LParam.ToInt32()));
        if (!this.DesignMode && !this.PointIsOnItemArea(client))
        {
          m.Result = new IntPtr(-1);
          return;
        }
      }
      base.WndProc(ref m);
    }

    protected override void OnParentChanged(EventArgs e)
    {
      base.OnParentChanged(e);
      this.AttachToParentControl();
    }

    protected override void OnItemActivated(QCompositeEventArgs e)
    {
      if (this.m_oAttachedControl != null && !this.m_oAttachedControl.IsDisposed)
      {
        if (e.Item == this.CloseButton)
          Qios.DevSuite.Components.NativeMethods.SendMessage(this.m_oAttachedControl.Handle, 16, IntPtr.Zero, IntPtr.Zero);
        else if (e.Item == this.MaximizeButton)
          Qios.DevSuite.Components.NativeMethods.ShowWindow(this.m_oAttachedControl.Handle, 3);
        else if (e.Item == this.MinimizeButton)
          Qios.DevSuite.Components.NativeMethods.ShowWindow(this.m_oAttachedControl.Handle, 6);
        else if (e.Item == this.RestoreButton)
          Qios.DevSuite.Components.NativeMethods.ShowWindow(this.m_oAttachedControl.Handle, 9);
      }
      base.OnItemActivated(e);
    }

    internal bool PointIsOnItemArea(Point localPoint) => QCompositeHelper.GetMouseHandlingItemAtPointRecursive((IQPart) this.Composite, localPoint) != null;

    internal bool PointIsOnIconArea(Point localPoint) => this.RibbonCaptionComposite.IconPart.CalculatedProperties.Bounds.Contains(localPoint);

    private void AttachToParentControl()
    {
      if (this.m_oAttachedControl == this.Parent)
        return;
      if (this.m_oAttachedControl != null)
      {
        if (this.m_oListener != null)
          this.m_oListener.ReleaseHandle();
        this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.AttachedControl_TextChanged));
        this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.AttachedControl_StyleChanged));
      }
      this.m_oAttachedControl = this.Parent;
      if (this.m_oAttachedControl != null)
      {
        this.m_oListener = new QRibbonCaptionControlListener(this, this.m_oAttachedControl, this.DesignMode);
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.AttachedControl_TextChanged), (object) this.m_oAttachedControl, "TextChanged"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.AttachedControl_StyleChanged), (object) this.m_oAttachedControl, "StyleChanged"));
        this.UpdateTextFromAttachedControl();
        this.Active = Form.ActiveForm == this.m_oAttachedControl;
        this.PutMaximized(this.AttachedControlIsMaximized);
      }
      else
      {
        this.Text = (string) null;
        this.Active = false;
        this.PutMaximized(false);
      }
      this.AdjustToControlStyle();
      if (this.m_oListener != null)
        this.m_oListener.AdjustControlStyle();
      this.AdjustToControlIcon();
    }

    private bool AttachedControlIsMaximized
    {
      get
      {
        if (!this.m_oAttachedControl.IsHandleCreated)
          return false;
        Qios.DevSuite.Components.NativeMethods.WINDOWPLACEMENT lpwndpl = new Qios.DevSuite.Components.NativeMethods.WINDOWPLACEMENT();
        lpwndpl.length = Marshal.SizeOf((object) lpwndpl);
        Qios.DevSuite.Components.NativeMethods.GetWindowPlacement(this.m_oAttachedControl.Handle, ref lpwndpl);
        return lpwndpl.showCmd == 3;
      }
    }

    protected virtual void AdjustToControlStyle()
    {
      if (this.AttachedToForm != null)
      {
        this.ControlBox = this.AttachedToForm.ControlBox;
        this.MinimizeBox = this.AttachedToForm.MinimizeBox;
        this.MaximizeBox = this.AttachedToForm.MaximizeBox;
      }
      else
      {
        this.ControlBox = false;
        this.MinimizeBox = false;
        this.MaximizeBox = false;
      }
    }

    protected internal virtual void AdjustToControlIcon()
    {
      if (this.AttachedToForm != null)
        this.RibbonCaptionComposite.IconPart.Icon = this.AttachedToForm.Icon;
      else
        this.RibbonCaptionComposite.IconPart.Icon = (Icon) null;
    }

    internal void UpdateTextFromAttachedControl()
    {
      if (!this.Configuration.AutoUpdateText || this.m_oAttachedControl == null)
        return;
      if (this.m_oAttachedControl is Form oAttachedControl && oAttachedControl.IsMdiContainer)
      {
        QControlHelper.GetActiveMaximizedMdiChild(QControlHelper.GetMdiClient(oAttachedControl));
        if (!this.RibbonCaptionComposite.SetMDIParentText(this.m_oAttachedControl.Text))
          return;
        base.Text = this.RibbonCaptionComposite.Text;
      }
      else
      {
        if (!(this.Text != this.m_oAttachedControl.Text))
          return;
        this.Text = this.m_oAttachedControl.Text;
      }
    }

    void IQComponentHost.SetComponent(object previousValue, object newValue)
    {
      if (previousValue == null)
        return;
      if (previousValue == this.m_oApplicationButton)
      {
        this.SetApplicationButton(newValue as QRibbonApplicationButton, true);
      }
      else
      {
        if (previousValue != this.m_oLaunchBar)
          return;
        this.SetLaunchBar(newValue as QRibbonLaunchBar, true);
      }
    }

    private void AttachedControl_TextChanged(object sender, EventArgs e)
    {
      if (this.m_bChangingCaptionControl)
        return;
      this.UpdateTextFromAttachedControl();
    }

    private void AttachedControl_StyleChanged(object sender, EventArgs e)
    {
      this.AdjustToControlStyle();
      if (this.m_oListener == null)
        return;
      this.m_oListener.AdjustControlStyle();
    }
  }
}
