// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QToolBarHost
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (QToolBarHost), "Resources.ControlImages.QToolBarHost.bmp")]
  [Designer(typeof (QToolBarHostDesigner), typeof (IDesigner))]
  public class QToolBarHost : QControl, ISupportInitialize, IQPersistableHost
  {
    private Guid m_oPersistGuid = Guid.NewGuid();
    private QToolBarRowCollection m_oRows;
    private bool m_bIsChangingToolBars;
    private QPadding m_oToolBarHostPadding;
    private QMargin m_oToolBarMargin;
    private int m_iToolBarInsertMargin = 10;
    private QCommandContainerOrientation m_eOrientation;
    private Size m_oMinimumSize;
    private bool m_bInitializing;

    public QToolBarHost()
    {
      this.SuspendLayout();
      this.SetStyle(ControlStyles.Selectable, false);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.m_oRows = new QToolBarRowCollection(this);
      QMisc.SetToDefaultValues((object) this);
      this.ResumeLayout(false);
    }

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QToolBarHostAppearance();

    [Description("Gets or sets the persistence Guid.")]
    [Category("QPersistence")]
    public Guid PersistGuid
    {
      get => this.m_oPersistGuid;
      set => this.m_oPersistGuid = value;
    }

    [Category("QAppearance")]
    [Description("Gets the QToolBarHostAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QToolBarHostAppearance Appearance => (QToolBarHostAppearance) base.Appearance;

    [DefaultValue(typeof (QPadding), "1,1,1,1")]
    [Category("QAppearance")]
    [Description("Gets or sets the Padding for the ToolBarHost")]
    public QPadding ToolBarHostPadding
    {
      get => this.m_oToolBarHostPadding;
      set
      {
        this.m_oToolBarHostPadding = value;
        this.PerformLayout();
      }
    }

    [Description("Gets or sets the margin for the ToolBars")]
    [DefaultValue(typeof (QMargin), "1,1,1,1")]
    [Category("QAppearance")]
    public QMargin ToolBarMargin
    {
      get => this.m_oToolBarMargin;
      set
      {
        this.m_oToolBarMargin = value;
        this.PerformLayout();
      }
    }

    [Description("Gets the margin a ToolBar must be located relative to the host before it gets inserted into the host.")]
    [DefaultValue(10)]
    [Category("QBehavior")]
    public int ToolBarInsertMargin
    {
      get => this.m_iToolBarInsertMargin;
      set => this.m_iToolBarInsertMargin = value;
    }

    [DefaultValue(typeof (Size), "0,0")]
    [Description("Gets or sets the MinimumSize. This is the Size the Host will have when no ToolBars are docked on it.")]
    [Localizable(true)]
    [Category("QBehavior")]
    public new Size MinimumSize
    {
      get => this.m_oMinimumSize;
      set
      {
        this.m_oMinimumSize = value;
        if (this.Width >= this.m_oMinimumSize.Width && this.Height >= this.m_oMinimumSize.Height)
          return;
        this.SetBounds(0, 0, Math.Max(this.Width, this.m_oMinimumSize.Width), Math.Max(this.Height, this.m_oMinimumSize.Height), BoundsSpecified.Width);
      }
    }

    [DefaultValue(QCommandContainerOrientation.None)]
    [Description("Gets or sets the Orientation the QToolBars on the Host will Have.  If Orientation is None then the Orientation is based on the DockStyle.")]
    [Category("QBehavior")]
    public QCommandContainerOrientation Orientation
    {
      get => this.m_eOrientation;
      set
      {
        if (this.m_eOrientation == value)
          return;
        this.m_eOrientation = value;
        this.RecalculateOrientation();
      }
    }

    internal QCommandContainerOrientation UsedOrientation
    {
      get
      {
        if (this.Orientation != QCommandContainerOrientation.None)
          return this.Orientation;
        return this.Dock != DockStyle.Left && this.Dock != DockStyle.Right ? QCommandContainerOrientation.Horizontal : QCommandContainerOrientation.Vertical;
      }
    }

    internal bool Horizontal => this.UsedOrientation == QCommandContainerOrientation.Horizontal;

    public override DockStyle Dock
    {
      get => base.Dock;
      set
      {
        QCommandContainerOrientation usedOrientation = this.UsedOrientation;
        base.Dock = value;
        if (this.UsedOrientation == usedOrientation)
          return;
        this.RecalculateOrientation();
      }
    }

    public virtual QMenuItemCollection RetrieveCustomizeMenu()
    {
      QMenuItemCollection qmenuItemCollection = new QMenuItemCollection();
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QMenuItemContainer)
        {
          QMenuItemContainer control = (QMenuItemContainer) this.Controls[index];
          QMenuItemCollection collection = control.RetrieveCustomizeMenu();
          QMenuItem menuItem = new QMenuItem(control.Text, control.Text);
          menuItem.MenuItems.AddRange(collection);
          qmenuItemCollection.Add(menuItem);
        }
      }
      return qmenuItemCollection;
    }

    public void BeginInit() => this.m_bInitializing = true;

    public void EndInit()
    {
      this.m_bInitializing = false;
      this.PerformLayout();
    }

    [Browsable(false)]
    public bool Initializing => this.m_bInitializing;

    protected override string BackColorPropertyName => "ToolBarHostBackground1";

    protected override string BackColor2PropertyName => "ToolBarHostBackground2";

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      base.SetBoundsCore(x, y, Math.Max(width, this.m_oMinimumSize.Width), Math.Max(height, this.m_oMinimumSize.Height), specified);
    }

    internal bool IsAvailableForPoint(Point screenPoint)
    {
      Rectangle screen = this.RectangleToScreen(this.ClientRectangle);
      screen.Inflate(this.m_iToolBarInsertMargin * 2, this.m_iToolBarInsertMargin * 2);
      return screen.Contains(screenPoint);
    }

    internal QToolBarRowCollection Rows => this.m_oRows;

    internal bool IsChangingToolBars => this.m_bIsChangingToolBars;

    internal int ToolBarsStartPosition => this.m_oToolBarHostPadding.Left;

    internal int ToolBarsAvailableSpace => (this.Horizontal ? this.Width : this.Height) - this.m_oToolBarHostPadding.Horizontal;

    internal int ToolBarsEndPosition => (this.Horizontal ? this.Width : this.Height) - this.m_oToolBarHostPadding.Right;

    protected override string BorderColorPropertyName => "ToolBarHostBorder";

    internal QToolBarRowPosition GetToolBarRowPosition(Point screenLocation)
    {
      Point client = this.PointToClient(screenLocation);
      Rectangle clientRectangle = this.ClientRectangle;
      clientRectangle.Inflate(this.m_iToolBarInsertMargin * 2, this.m_iToolBarInsertMargin * 2);
      QToolBarRowPosition toolBarRowPosition = new QToolBarRowPosition(-1, (QToolBarRow) null, QToolBarRowPositionType.OutsideBounds);
      int num1;
      if (this.Horizontal)
      {
        if (client.X < clientRectangle.X || client.X >= clientRectangle.Right)
          return toolBarRowPosition;
        num1 = client.Y;
      }
      else
      {
        if (client.Y < this.ClientRectangle.Y || client.Y >= this.ClientRectangle.Bottom)
          return toolBarRowPosition;
        num1 = client.X;
      }
      for (int index = 0; index < this.Rows.Count; ++index)
      {
        if (this.Rows[index].Start - this.ToolBarMargin.Top <= num1 && this.Rows[index].End + this.ToolBarMargin.Bottom > num1)
        {
          toolBarRowPosition.PositionType = this.Rows[index].GetPositionType(client);
          toolBarRowPosition.ToolBarRow = this.Rows[index];
          toolBarRowPosition.ToolBarRowIndex = this.Rows[index].PositionIndex;
          return toolBarRowPosition;
        }
      }
      int num2 = num1;
      int top = this.ToolBarHostPadding.Top;
      toolBarRowPosition.PositionType = num2 > top ? QToolBarRowPositionType.AfterToolBarRow : QToolBarRowPositionType.BeforeToolBarRow;
      return toolBarRowPosition;
    }

    private void RecalculateOrientation()
    {
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        this.m_bIsChangingToolBars = true;
        if (this.Controls[index] is IQToolBar)
        {
          IQToolBar control = (IQToolBar) this.Controls[index];
          control.LayoutToolBar(new Rectangle(control.Bounds.Top, control.Bounds.Left, control.Bounds.Height, control.Bounds.Width), this.UsedOrientation, QToolBarLayoutFlags.None);
        }
        this.m_bIsChangingToolBars = false;
      }
      this.PerformLayout();
    }

    internal void NotifyToolBarSizeChanged(IQToolBar toolBar)
    {
      if (!this.Controls.Contains((Control) toolBar))
        return;
      this.PerformLayout();
    }

    internal void LayoutToolBarHost(IQToolBar movingToolBar)
    {
      if (this.Initializing || this.Horizontal && this.Width <= 0 || !this.Horizontal && this.Height <= 0 || this.m_bIsChangingToolBars)
        return;
      this.m_bIsChangingToolBars = true;
      int num = Math.Max(this.DesignMode ? 20 : 0, this.Rows.LayoutToolBarRows(movingToolBar));
      BoundsSpecified specified = BoundsSpecified.None;
      if (this.Dock != DockStyle.Fill)
        specified = !this.Horizontal ? (this.Dock == DockStyle.Top || this.Dock == DockStyle.Bottom ? BoundsSpecified.None : BoundsSpecified.Width) : (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right ? BoundsSpecified.None : BoundsSpecified.Height);
      this.SetBounds(0, 0, num, num, specified);
      this.m_bIsChangingToolBars = false;
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
      if (e.Control.Dock != DockStyle.None)
        e.Control.Dock = DockStyle.None;
      if (e.Control.Anchor != (AnchorStyles.Top | AnchorStyles.Left))
        e.Control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
      IQToolBar qtoolBar = e.Control is IQToolBar ? (IQToolBar) e.Control : (IQToolBar) null;
      if (qtoolBar != null)
      {
        if (this.ParentForm != null)
          qtoolBar.OwnerWindow = (IWin32Window) this.ParentForm;
        if (qtoolBar.Horizontal != this.Horizontal)
        {
          this.m_bIsChangingToolBars = true;
          qtoolBar.LayoutToolBar(new Rectangle(0, 0, 0, 0), this.UsedOrientation, QToolBarLayoutFlags.DoNotSetBounds);
          this.m_bIsChangingToolBars = false;
        }
        if (qtoolBar.ParentToolBarRow == null || !this.Rows.Contains(qtoolBar.ParentToolBarRow))
          qtoolBar.DockToolBar(this, qtoolBar.RowIndex, qtoolBar.ToolBarPositionIndex, 0);
      }
      base.OnControlAdded(e);
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
      IQToolBar toolBar = e.Control is IQToolBar ? (IQToolBar) e.Control : (IQToolBar) null;
      if (toolBar != null && toolBar.ParentToolBarRow != null && this.Rows.Contains(toolBar.ParentToolBarRow))
      {
        this.m_bIsChangingToolBars = true;
        toolBar.ParentToolBarRow.Remove(toolBar);
        this.m_bIsChangingToolBars = false;
        this.PerformLayout();
      }
      base.OnControlRemoved(e);
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      if (!this.Visible)
        return;
      this.PerformLayout();
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      if (this.m_bIsChangingToolBars)
        return;
      long tickCount = QMisc.TickCount;
      this.LayoutToolBarHost((IQToolBar) null);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      base.OnPaintBackground(pevent);
      if (!this.DesignMode)
        return;
      StringFormat format = new StringFormat(StringFormat.GenericDefault);
      if (!this.Horizontal)
        format.FormatFlags |= StringFormatFlags.DirectionVertical;
      format.LineAlignment = StringAlignment.Center;
      Rectangle layoutRectangle = new QPadding(4, 2, 2, 4).InflateRectangleWithPadding(this.ClientRectangle, false, this.Horizontal);
      Brush brush = (Brush) new SolidBrush(Color.Gray);
      pevent.Graphics.DrawString("[QToolBarHost: " + this.Name + "]", this.Font, brush, (RectangleF) layoutRectangle, format);
      brush.Dispose();
      format.Dispose();
    }
  }
}
