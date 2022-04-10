// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCustomToolWindow
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QCustomToolWindowDesigner), typeof (IRootDesigner))]
  [Designer(typeof (ParentControlDesigner), typeof (IDesigner))]
  [ToolboxItem(false)]
  public class QCustomToolWindow : QContainerControl, IQMenuKeyPassThrough
  {
    private bool m_bCanSize = true;
    private bool m_bWindowsXPThemeTried;
    private IntPtr m_hWindowsXPTheme = IntPtr.Zero;
    private static QCustomToolWindowCollection m_oAllWindows;
    private QWeakDelegate m_oLoadDelegate;

    public QCustomToolWindow() => this.InternalConstruct();

    public QCustomToolWindow(IWin32Window owner)
      : base(owner)
    {
      this.InternalConstruct();
    }

    private void InternalConstruct()
    {
      this.SuspendLayout();
      if (QCustomToolWindow.m_oAllWindows == null)
        QCustomToolWindow.m_oAllWindows = new QCustomToolWindowCollection();
      QCustomToolWindow.m_oAllWindows.Add(this);
      this.SetTopLevel(true);
      this.Visible = false;
      this.SetCanSizeProperties(this.m_bCanSize, this.m_bCanSize, this.m_bCanSize, this.m_bCanSize);
      this.Text = QResources.GetGeneral("QCustomToolWindow_Text");
      this.ResumeLayout(false);
    }

    [QWeakEvent]
    public event EventHandler Load
    {
      add => this.m_oLoadDelegate = QWeakDelegate.Combine(this.m_oLoadDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oLoadDelegate = QWeakDelegate.Remove(this.m_oLoadDelegate, (Delegate) value);
    }

    public static QCustomToolWindowCollection AllWindows
    {
      get
      {
        if (QCustomToolWindow.m_oAllWindows == null)
          QCustomToolWindow.m_oAllWindows = new QCustomToolWindowCollection();
        return QCustomToolWindow.m_oAllWindows;
      }
    }

    public override ISite Site
    {
      get => base.Site;
      set
      {
        base.Site = value;
        if (value == null || !value.DesignMode)
          return;
        this.SetTopLevel(false);
        this.SetCanSizeProperties(false, false, false, false);
      }
    }

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QCustomToolWindowAppearance();

    [Browsable(false)]
    public override Size MinimumSize
    {
      get
      {
        Size minimumSize = base.MinimumSize;
        return new Size(Math.Max(minimumSize.Width, this.ClientAreaMarginLeft + 10 + this.ClientAreaMarginRight), Math.Max(minimumSize.Height, this.ClientAreaMarginTop + 10 + this.ClientAreaMarginBottom));
      }
    }

    [Category("QAppearance")]
    [Description("Gets the QAppearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QCustomToolWindowAppearance Appearance => (QCustomToolWindowAppearance) base.Appearance;

    [Category("QBehavior")]
    [Description("Gets or sets whether the QCustomToolWindow can be sized")]
    [DefaultValue(true)]
    public bool CanSize
    {
      get => this.m_bCanSize;
      set
      {
        this.m_bCanSize = value;
        this.SetCanSizeProperties(this.m_bCanSize, this.m_bCanSize, this.m_bCanSize, this.m_bCanSize);
      }
    }

    public bool ShouldSerializeCaptionColor1() => false;

    public void ResetCaptionColor1() => this.ColorScheme[this.CaptionColor1PropertyName].Reset();

    [Description("Gets or sets the first caption Color.")]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public virtual Color CaptionColor1
    {
      get => this.ColorScheme[this.CaptionColor1PropertyName].Current;
      set => this.ColorScheme[this.CaptionColor1PropertyName].Current = value;
    }

    public bool ShouldSerializeCaptionColor2() => false;

    public void ResetCaptionColor2() => this.ColorScheme[this.CaptionColor2PropertyName].Reset();

    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [Description("Gets or sets the first caption Color.")]
    public virtual Color CaptionColor2
    {
      get => this.ColorScheme[this.CaptionColor2PropertyName].Current;
      set => this.ColorScheme[this.CaptionColor2PropertyName].Current = value;
    }

    public virtual Form Owner
    {
      get => this.OwnerWindow as Form;
      set
      {
        if (this.OwnerWindow == value)
          return;
        this.OwnerWindow = (IWin32Window) value;
      }
    }

    [Browsable(false)]
    public override Rectangle CaptionBounds => new Rectangle(this.ClientAreaMarginLeft + this.Appearance.CaptionMargin.Left, this.BorderMarginTop + this.Appearance.CaptionMargin.Top, this.CurrentBounds.Width - (this.ClientAreaMarginLeft + this.ClientAreaMarginRight + this.Appearance.CaptionMargin.Horizontal), this.UsedCaptionHeight);

    [Browsable(false)]
    public virtual Rectangle CaptionTitleBounds => this.CaptionBounds;

    protected override string BackColorPropertyName => "CustomToolWindowBackground1";

    protected override string BackColor2PropertyName => "CustomToolWindowBackground2";

    protected override string BorderColorPropertyName => "CustomToolWindowBorder";

    protected virtual string CaptionColor1PropertyName => "CustomToolWindowCaption1";

    protected virtual string CaptionColor2PropertyName => "CustomToolWindowCaption2";

    protected virtual int UsedCaptionHeight => Math.Max(this.Appearance.CaptionHeight, SystemInformation.ToolWindowCaptionHeight);

    protected int BorderMarginTop => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderTop ? 0 : this.Appearance.BorderWidth;

    protected override int ClientAreaMarginTop
    {
      get
      {
        int num = 0;
        if (this.Appearance.ShowBorders && this.Appearance.ShowBorderTop)
          num += this.Appearance.BorderWidth;
        return num + this.UsedCaptionHeight + this.Appearance.CaptionMargin.Vertical;
      }
    }

    protected override int ClientAreaMarginLeft => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderLeft ? 0 : this.Appearance.BorderWidth;

    protected override int ClientAreaMarginRight => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderRight ? 0 : this.Appearance.BorderWidth;

    protected override int ClientAreaMarginBottom => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderBottom ? 0 : this.Appearance.BorderWidth;

    protected override Size DefaultSize => new Size(50, 50);

    protected override void SetClientSizeCore(int x, int y)
    {
      Size size = new Size(x + this.ClientAreaMarginLeft + this.ClientAreaMarginRight, y + this.ClientAreaMarginTop + this.ClientAreaMarginBottom);
      if (!(size != this.Size))
        return;
      this.SetBounds(0, 0, size.Width, size.Height, BoundsSpecified.Size);
    }

    protected virtual void OnLoad(EventArgs e) => this.m_oLoadDelegate = QWeakDelegate.InvokeDelegate(this.m_oLoadDelegate, (object) this, (object) e);

    protected override void OnCreateControl()
    {
      base.OnCreateControl();
      this.OnLoad(EventArgs.Empty);
    }

    protected override void OnWindowsXPThemeChanged(EventArgs e)
    {
      base.OnWindowsXPThemeChanged(e);
      this.CloseWindowsXpTheme();
    }

    protected override int ResizeBorderWidth => this.Appearance.BorderWidth;

    protected override CreateParams CreateParams
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        CreateParams createParams = base.CreateParams;
        if (!this.DesignMode)
        {
          createParams.Style |= int.MinValue;
          createParams.Style &= -1073741825;
          createParams.Style &= -268435457;
          createParams.ExStyle |= 128;
        }
        if (this.Owner != null)
          createParams.Parent = QControlHelper.GetUndisposedHandle((IWin32Window) this.Owner);
        return createParams;
      }
    }

    private void SecureWindowsXpTheme()
    {
      if (this.m_bWindowsXPThemeTried || !NativeHelper.WindowsXP)
        return;
      this.m_hWindowsXPTheme = NativeMethods.OpenThemeData(this.Handle, "Window");
      this.m_bWindowsXPThemeTried = true;
    }

    private void CloseWindowsXpTheme()
    {
      if (this.m_hWindowsXPTheme != IntPtr.Zero)
      {
        NativeMethods.CloseThemeData(this.m_hWindowsXPTheme);
        this.m_hWindowsXPTheme = IntPtr.Zero;
      }
      this.m_bWindowsXPThemeTried = false;
    }

    Control IQMenuKeyPassThrough.PassToControl => this.OwnerWindow as Control;

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      if (QMainMenu.PassSCKeyMenu(m, this.OwnerWindow as Control))
        return;
      base.WndProc(ref m);
    }

    protected override void OnPaintNonClientArea(PaintEventArgs e)
    {
      base.OnPaintNonClientArea(e);
      this.SecureWindowsXpTheme();
      Rectangle bounds = new Rectangle(0, 0, this.CurrentBounds.Width, this.CurrentBounds.Height);
      Rectangle captionBounds = this.CaptionBounds;
      QRectanglePainter.Default.Paint(bounds, (IQAppearance) this.Appearance, new QColorSet(this.BackColor, this.BackColor2, this.BorderColor), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, e.Graphics);
      QAppearanceWrapper appearance = new QAppearanceWrapper((IQAppearance) null);
      QRectanglePainter.Default.FillBackground(captionBounds, (IQAppearance) appearance, new QColorSet(this.CaptionColor1, this.CaptionColor2), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, e.Graphics);
      QControlPaint.DrawSmallCaptionText(this.m_hWindowsXPTheme, this.Text, true, (RectangleF) this.CaptionTitleBounds, e.Graphics);
    }

    protected override void Dispose(bool disposing)
    {
      this.CloseWindowsXpTheme();
      if (disposing && QCustomToolWindow.m_oAllWindows != null)
        QCustomToolWindow.m_oAllWindows.Remove(this);
      base.Dispose(disposing);
    }
  }
}
