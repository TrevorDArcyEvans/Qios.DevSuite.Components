// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QStatusBar
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

namespace Qios.DevSuite.Components
{
  [ToolboxItem(true)]
  [Designer(typeof (QStatusBarDesigner), typeof (IDesigner))]
  [ToolboxBitmap(typeof (QStatusBar), "Resources.ControlImages.QStatusBar.bmp")]
  public class QStatusBar : QControl
  {
    private QStatusBarPanelCollection m_oPanels;
    private bool m_bSizingGrip = true;
    private bool m_bShowPanels = true;
    private QSizingGripStyle m_eSizingGripStyle;
    private bool m_bIsSizing;
    private QStatusBarPanel m_oMouseDownPanel;
    private Point m_oCachedLocationOnParentControl;
    private int m_iMinimumHeight = 15;
    private int m_iPanelMarginLeft = 1;
    private int m_iPanelMarginRight = 1;
    private int m_iPanelMarginTop = 2;
    private int m_iPanelMarginBottom = 1;
    private int m_iTextVerticalMargin;
    private int m_iSizingGripRightMargin = 1;
    private int m_iSizingGripSize = 16;
    private IContainer m_oComponents;
    private QStatusBarPanel m_oLastPanelWhereToolTipWasSetOn;
    private bool m_bPerformingLayout;
    private QAppearance m_oPanelAppearance;
    private EventHandler m_oPanelAppearanceChangedEventHandler;
    private QWeakDelegate m_oPanelClickDelegate;
    private QWeakDelegate m_oPanelDoubleClickDelegate;

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the user clicks on a panel.")]
    public event QStatusBarPanelEventHandler PanelClick
    {
      add => this.m_oPanelClickDelegate = QWeakDelegate.Combine(this.m_oPanelClickDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oPanelClickDelegate = QWeakDelegate.Remove(this.m_oPanelClickDelegate, (Delegate) value);
    }

    [Description("Gets raised when the user double clicks on a panel.")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QStatusBarPanelEventHandler PanelDoubleClick
    {
      add => this.m_oPanelDoubleClickDelegate = QWeakDelegate.Combine(this.m_oPanelDoubleClickDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oPanelDoubleClickDelegate = QWeakDelegate.Remove(this.m_oPanelDoubleClickDelegate, (Delegate) value);
    }

    public QStatusBar()
    {
      this.SuspendLayout();
      base.Dock = DockStyle.Bottom;
      this.m_oComponents = (IContainer) new Container();
      this.m_oPanels = new QStatusBarPanelCollection(this);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.m_oPanelAppearanceChangedEventHandler = new EventHandler(this.PanelAppearance_AppearanceChanged);
      this.m_oPanelAppearance = new QAppearance();
      this.m_oPanelAppearance.AppearanceChanged += this.m_oPanelAppearanceChangedEventHandler;
      this.ResumeLayout(false);
    }

    protected override CreateParams CreateParams
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        CreateParams createParams = base.CreateParams;
        createParams.ExStyle |= 8;
        return createParams;
      }
    }

    protected override QToolTipConfiguration CreateToolTipConfigurationInstance() => (QToolTipConfiguration) new QStatusBarToolTipConfiguration();

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QAppearance();

    public override string Text
    {
      get => base.Text;
      set
      {
        base.Text = value;
        this.PerformLayout();
        this.Invalidate();
      }
    }

    public override Color ForeColor
    {
      get => this.ColorScheme.TextColor.Current;
      set => this.ColorScheme.TextColor.Current = value;
    }

    [Description("Gets the QAppearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QAppearance Appearance => (QAppearance) base.Appearance;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the QStatusBarToolTipConfiguration")]
    public virtual QStatusBarToolTipConfiguration ToolTipConfiguration
    {
      get => (QStatusBarToolTipConfiguration) base.ToolTipConfiguration;
      set => this.ToolTipConfiguration = value;
    }

    [Category("QAppearance")]
    [Description("Gets or sets whether this QStatusBar should show a SizingGrip")]
    [DefaultValue(true)]
    public bool SizingGrip
    {
      get => this.m_bSizingGrip;
      set
      {
        if (this.m_bSizingGrip == value)
          return;
        this.m_bSizingGrip = value;
        if (this.PerformingLayout)
          return;
        this.PerformLayout();
        this.Refresh();
      }
    }

    [Description("Gets or sets whether the Panels should be visible")]
    [Category("QAppearance")]
    [DefaultValue(true)]
    public bool ShowPanels
    {
      get => this.m_bShowPanels;
      set
      {
        this.m_bShowPanels = value;
        if (this.PerformingLayout)
          return;
        this.PerformLayout();
        this.Refresh();
      }
    }

    [DefaultValue(QSizingGripStyle.Dots)]
    [Category("QAppearance")]
    [Description("The type of Sizinggrip to draw.")]
    public QSizingGripStyle SizingGripStyle
    {
      get => this.m_eSizingGripStyle;
      set
      {
        if (this.m_eSizingGripStyle == value)
          return;
        this.m_eSizingGripStyle = value;
        if (this.PerformingLayout)
          return;
        this.Refresh();
      }
    }

    [Category("QAppearance")]
    [Description("The Minimum height of the StatusBar")]
    [DefaultValue(15)]
    [Localizable(true)]
    public int MinimumHeight
    {
      get => this.m_iMinimumHeight;
      set
      {
        this.m_iMinimumHeight = value;
        if (this.PerformingLayout)
          return;
        this.PerformLayout();
        this.Refresh();
      }
    }

    [Description("Gets or sets the left margin between the panels and the QStatusBar")]
    [Category("QAppearance")]
    [DefaultValue(1)]
    public int PanelMarginLeft
    {
      get => this.m_iPanelMarginLeft;
      set
      {
        if (this.m_iPanelMarginLeft == value)
          return;
        this.m_iPanelMarginLeft = value;
        this.PerformLayout();
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets the top margin between the panels and the QStatusBar")]
    [DefaultValue(2)]
    public int PanelMarginTop
    {
      get => this.m_iPanelMarginTop;
      set
      {
        if (this.m_iPanelMarginTop == value)
          return;
        this.m_iPanelMarginTop = value;
        this.PerformLayout();
      }
    }

    [Description("Gets or sets the right margin between the panels and the QStatusBar")]
    [Category("QAppearance")]
    [DefaultValue(1)]
    public int PanelMarginRight
    {
      get => this.m_iPanelMarginRight;
      set
      {
        if (this.m_iPanelMarginRight == value)
          return;
        this.m_iPanelMarginRight = value;
        this.PerformLayout();
      }
    }

    [Description("Gets or sets the right margin between the panels and the QStatusBar")]
    [Category("QAppearance")]
    [DefaultValue(1)]
    public int PanelMarginBottom
    {
      get => this.m_iPanelMarginBottom;
      set
      {
        if (this.m_iPanelMarginBottom == value)
          return;
        this.m_iPanelMarginBottom = value;
        this.PerformLayout();
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets the margin between the text and the bottom and the top of the StatusBar")]
    [DefaultValue(1)]
    public int TextVerticalMargin
    {
      get => this.m_iTextVerticalMargin;
      set
      {
        if (this.m_iTextVerticalMargin == value)
          return;
        this.m_iTextVerticalMargin = value;
        this.PerformLayout();
      }
    }

    [Browsable(false)]
    public bool PerformingLayout => this.m_bPerformingLayout;

    [DefaultValue(DockStyle.Bottom)]
    public override DockStyle Dock
    {
      get => base.Dock;
      set => base.Dock = value;
    }

    public bool ShouldSerializePanelAppearance() => !this.PanelAppearance.IsSetToDefaultValues();

    public void ResetPanelAppearance() => this.PanelAppearance.SetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the QAppearance for the Panels. This can be overriden by every panel")]
    [Category("QAppearance")]
    public QAppearance PanelAppearance => this.m_oPanelAppearance;

    [Description("Contains the Panels of the StatusBar")]
    [Category("QBehavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QStatusBarPanelCollection Panels => this.m_oPanels;

    protected override string BackColorPropertyName => "StatusBarBackground1";

    protected override string BackColor2PropertyName => "StatusBarBackground2";

    protected override string BorderColorPropertyName => "StatusBarBorder";

    private static void DrawDot(
      int left,
      int top,
      Brush lightBrush,
      Brush darkBrush,
      Graphics graphics)
    {
      graphics.FillRectangle(lightBrush, left + 1, top + 1, 2, 2);
      graphics.FillRectangle(darkBrush, left, top, 2, 2);
    }

    private void DrawSizingGrip(PaintEventArgs e, int left, int width)
    {
      if (this.ColorScheme == null)
        return;
      int num1 = this.ClientRectangle.Height - this.m_iPanelMarginBottom;
      int num2 = left + width;
      int num3 = 4;
      if (this.m_eSizingGripStyle == QSizingGripStyle.Lines || this.ColorScheme.UseHighContrast)
      {
        Pen pen = !this.ColorScheme.UseHighContrast ? new Pen((Color) this.ColorScheme.StatusBarSizingGripDark) : new Pen((Color) this.ColorScheme.StatusBarSizingGripDark, 2f);
        for (int index = 1; index < 4; ++index)
        {
          int x1 = left + index * num3;
          int y2 = num1 - width + index * num3;
          e.Graphics.DrawLine(pen, x1, num1 - 1, num2 - 1, y2);
        }
        pen.Dispose();
      }
      else
      {
        if (this.m_eSizingGripStyle != QSizingGripStyle.Dots)
          return;
        Brush darkBrush = (Brush) new SolidBrush((Color) this.ColorScheme.StatusBarSizingGripDark);
        Brush lightBrush = (Brush) new SolidBrush((Color) this.ColorScheme.StatusBarSizingGripLight);
        int top = num1 - 3;
        int num4 = 3;
        for (int index1 = 0; index1 < 3; ++index1)
        {
          int left1 = num2 - 3;
          for (int index2 = 0; index2 < num4; ++index2)
          {
            QStatusBar.DrawDot(left1, top, lightBrush, darkBrush, e.Graphics);
            left1 -= 4;
          }
          --num4;
          top -= 4;
        }
        darkBrush.Dispose();
        lightBrush.Dispose();
      }
    }

    private void SetPanelsBounds(Graphics graphics)
    {
      int num1 = this.SizingGrip ? this.m_iSizingGripSize : 0;
      int iPanelMarginTop = this.m_iPanelMarginTop;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int width1 = this.ClientSize.Width;
      if (this.SizingGrip)
        width1 -= num1 + this.m_iSizingGripRightMargin;
      for (int index = 0; index < this.Panels.Count; ++index)
      {
        Size size = this.Panels[index].CalcuateSizeOfContents(graphics);
        if (num2 < size.Height)
          num2 = size.Height;
        if (this.Panels[index].AutoSize == StatusBarPanelAutoSize.None)
        {
          this.Panels[index].Width = this.Panels[index].RequestedWidth;
          if (this.Panels[index].Width < this.Panels[index].MinWidth)
            this.Panels[index].Width = this.Panels[index].MinWidth;
          num4 += this.Panels[index].Width + this.m_iPanelMarginLeft + this.m_iPanelMarginRight;
        }
        else if (this.Panels[index].AutoSize == StatusBarPanelAutoSize.Contents)
        {
          this.Panels[index].Width = size.Width;
          if (this.Panels[index].Width < this.Panels[index].MinWidth)
            this.Panels[index].Width = this.Panels[index].MinWidth;
          num4 += this.Panels[index].Width + this.m_iPanelMarginLeft + this.m_iPanelMarginRight;
        }
        else if (this.Panels[index].AutoSize == StatusBarPanelAutoSize.Spring)
        {
          ++num3;
          num4 += this.m_iPanelMarginLeft + this.m_iPanelMarginRight;
        }
      }
      int num5 = 0;
      if (num3 > 0)
        num5 = (int) Math.Floor((double) (width1 - num4) / (double) num3);
      int num6 = 0;
      for (int index = 0; index < this.Panels.Count; ++index)
      {
        if (this.Panels[index].AutoSize == StatusBarPanelAutoSize.Spring)
        {
          this.Panels[index].Width = num5;
          if (this.Panels[index].Width < this.Panels[index].MinWidth)
          {
            this.Panels[index].Width = this.Panels[index].MinWidth;
            num5 -= this.Panels[index].Width;
          }
        }
        num6 += this.m_iPanelMarginLeft + this.Panels[index].Width + this.m_iPanelMarginRight;
      }
      for (int index = this.Panels.Count - 1; index >= 0 && num6 > width1; --index)
      {
        if (this.Panels[index].Width > this.Panels[index].MinWidth)
        {
          int num7 = Math.Max(this.Panels[index].Width - (num6 - width1), this.Panels[index].MinWidth);
          num6 -= this.Panels[index].Width - num7;
          this.Panels[index].Width = num7;
        }
      }
      int iPanelMarginLeft = this.m_iPanelMarginLeft;
      for (int index = 0; index < this.Panels.Count; ++index)
      {
        int width2 = this.Panels[index].Width;
        if (iPanelMarginLeft + width2 + this.m_iPanelMarginRight > width1)
          width2 -= iPanelMarginLeft + width2 + this.m_iPanelMarginRight - width1;
        if (width2 < 0)
          width2 = 0;
        this.Panels[index].SetBounds(new Rectangle(iPanelMarginLeft, iPanelMarginTop, width2, num2 + this.m_iTextVerticalMargin));
        iPanelMarginLeft += this.m_iPanelMarginLeft + width2 + this.m_iPanelMarginRight;
      }
      int height = num2 + (this.m_iTextVerticalMargin + this.m_iPanelMarginBottom + this.m_iPanelMarginTop);
      if (height < this.m_iMinimumHeight)
        height = this.m_iMinimumHeight;
      this.SetBounds(0, 0, 0, height, BoundsSpecified.Height);
    }

    private QStatusBarPanel GetPanelOnPosition(int x)
    {
      for (int index = 0; index < this.Panels.Count; ++index)
      {
        if (this.Panels[index].Bounds.Left <= x && this.Panels[index].Bounds.Right >= x)
          return this.Panels[index];
      }
      return (QStatusBarPanel) null;
    }

    private bool IsOnSizingGrip(int x) => this.SizingGrip && x >= this.ClientSize.Width - (this.m_iSizingGripRightMargin + this.m_iSizingGripSize);

    internal void SetToolTipOfPanel(QStatusBarPanel panel)
    {
      if (panel != this.m_oLastPanelWhereToolTipWasSetOn)
        return;
      this.ToolTipText = panel.UsedToolTipText;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      QStatusBarPanel panelOnPosition = this.GetPanelOnPosition(e.X);
      if (panelOnPosition != null && panelOnPosition != this.m_oLastPanelWhereToolTipWasSetOn)
      {
        this.m_oLastPanelWhereToolTipWasSetOn = panelOnPosition;
        this.SetToolTipOfPanel(this.m_oLastPanelWhereToolTipWasSetOn);
      }
      else if (this.IsOnSizingGrip(e.X) || this.m_bIsSizing)
      {
        this.Cursor = Cursors.SizeNWSE;
        if (this.m_bIsSizing)
        {
          Point client = this.Parent.PointToClient(this.PointToScreen(new Point(e.X, e.Y)));
          this.Parent.SetBounds(0, 0, client.X + this.m_oCachedLocationOnParentControl.X, client.Y + this.m_oCachedLocationOnParentControl.Y, BoundsSpecified.Size);
          this.Parent.Refresh();
        }
      }
      else
        this.Cursor = Cursors.Default;
      base.OnMouseMove(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.IsOnSizingGrip(e.X))
      {
        if (this.Parent != null)
        {
          Point client = this.Parent.PointToClient(this.PointToScreen(new Point(e.X, e.Y)));
          this.m_oCachedLocationOnParentControl = new Point(this.Parent.Width - client.X, this.Parent.Height - client.Y);
          this.m_bIsSizing = true;
        }
      }
      else if (this.Visible && this.ShowPanels)
        this.m_oMouseDownPanel = this.GetPanelOnPosition(this.PointToClient(Control.MousePosition).X);
      base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.m_bIsSizing = false;
      base.OnMouseUp(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      int width = this.SizingGrip ? this.m_iSizingGripSize : 0;
      int iPanelMarginLeft = this.m_iPanelMarginLeft;
      bool flag = false;
      if (this.ShowPanels)
      {
        if (this.Panels != null)
        {
          for (int index = 0; index < this.Panels.Count; ++index)
          {
            this.Panels[index].DrawBackGround(e.Graphics);
            this.Panels[index].Draw(e.Graphics);
            iPanelMarginLeft += this.Panels[index].Width;
            if (flag)
              break;
          }
        }
      }
      else
      {
        Brush brush = (Brush) new SolidBrush((Color) this.ColorScheme.TextColor);
        e.Graphics.DrawString(this.Text, this.Font, brush, (float) this.m_iPanelMarginLeft, (float) (this.m_iPanelMarginTop + this.m_iTextVerticalMargin));
        brush.Dispose();
      }
      if (this.SizingGrip)
      {
        int num = this.ClientSize.Width - width;
        this.DrawSizingGrip(e, num - this.m_iSizingGripRightMargin, width);
      }
      base.OnPaint(e);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      if (this.m_bPerformingLayout)
        return;
      this.m_bPerformingLayout = true;
      Graphics graphics = this.CreateGraphics();
      if (this.ShowPanels)
        this.SetPanelsBounds(graphics);
      else
        this.SetBounds(0, 0, 0, (int) Math.Max((float) this.m_iMinimumHeight, graphics.MeasureString(this.Text, this.Font).Height + (float) this.m_iPanelMarginBottom + (float) this.m_iPanelMarginTop + (float) (this.m_iTextVerticalMargin * 2)), BoundsSpecified.Height);
      graphics.Dispose();
      this.m_bPerformingLayout = false;
      this.Refresh();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.m_oComponents != null)
      {
        this.m_oComponents.Dispose();
        this.m_oComponents = (IContainer) null;
      }
      base.Dispose(disposing);
    }

    protected override void OnClick(EventArgs e)
    {
      QStatusBarPanel panelOnPosition = this.GetPanelOnPosition(this.PointToClient(Control.MousePosition).X);
      if (panelOnPosition != null && panelOnPosition == this.m_oMouseDownPanel)
        this.OnPanelClick(new QStatusBarPanelEventArgs(this.m_oMouseDownPanel));
      this.m_oMouseDownPanel = (QStatusBarPanel) null;
      base.OnClick(e);
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      QStatusBarPanel panelOnPosition = this.GetPanelOnPosition(this.PointToClient(Control.MousePosition).X);
      if (panelOnPosition != null && panelOnPosition == this.m_oMouseDownPanel)
        this.OnPanelDoubleClick(new QStatusBarPanelEventArgs(this.m_oMouseDownPanel));
      this.m_oMouseDownPanel = (QStatusBarPanel) null;
      base.OnDoubleClick(e);
    }

    protected virtual void OnPanelClick(QStatusBarPanelEventArgs e) => this.m_oPanelClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oPanelClickDelegate, (object) this, (object) e);

    protected virtual void OnPanelDoubleClick(QStatusBarPanelEventArgs e) => this.m_oPanelDoubleClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oPanelDoubleClickDelegate, (object) this, (object) e);

    private void PanelAppearance_AppearanceChanged(object sender, EventArgs e)
    {
      this.PerformLayout();
      this.Refresh();
    }
  }
}
