// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabControl
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  [ToolboxBitmap(typeof (QTabControl), "Resources.ControlImages.QTabControl.bmp")]
  [DefaultEvent("ActivePageChanged")]
  [ToolboxItem(true)]
  [Designer(typeof (QTabControlDesigner), typeof (IDesigner))]
  public class QTabControl : 
    QContainerControl,
    IQTabStripHost,
    ISupportInitialize,
    IQPersistableObject
  {
    private const int TabStripAnimateTimer = 17;
    private QTabControlPainter m_oPainter;
    private Guid m_oPersistGuid = Guid.NewGuid();
    private bool m_bPersistObject = true;
    private bool m_bIsPersisted;
    private Point m_oMouseDownAtPoint = Point.Empty;
    private QTabButton m_oMouseDownAtButton;
    private bool m_bShowDropIndicator = true;
    private bool m_bShowDropWindow = true;
    private bool m_bUserIsDragging;
    private bool m_bAllowDrag;
    private bool m_bAllowExternalDrop;
    private bool m_bAllowExternalDrag;
    private QTabButton m_oDraggingButton;
    private QTabControl.QTranslucentTabButtonWindow m_oDraggingButtonWindow;
    private Cursor m_oDraggingCursor;
    private Keys m_eActivatePreviousKey = Keys.Tab | Keys.Shift | Keys.Control;
    private Keys m_eActivateNextKey = Keys.Tab | Keys.Control;
    private ArrayList m_aTabStripsListeningToTimer;
    private QTabPageCloseBehavior m_eTabPageCloseBehavior;
    private bool m_bFocusTabButtons = true;
    private bool m_bWrapTabButtonNavigationAround = true;
    private QTabStrip[] m_aTabStrips;
    private QTabStrip m_oUpdateRequestedFrom;
    private QTabStripPaintParams m_oTabStripPaintParams;
    private QTabControlPaintParams m_oTabControlPaintParams;
    private QTabControlConfiguration m_oConfiguration;
    private Rectangle m_oTabPagesBounds;
    private Rectangle m_oContentShapeBounds;
    private bool m_bTabControlIsChangingActiveButton;
    private bool m_bActiveButtonIsChanging;
    private bool m_bSuspendingRedraw;
    private bool m_bInitializing;
    private QTabButton m_oActiveButton;
    private QTabPage m_oActiveTabPageDesign;
    private QWeakDelegate m_oActivePageChangingDelegate;
    private QWeakDelegate m_oActivePageChangedDelegate;
    private QWeakDelegate m_oHotPageChangedDelegate;
    private QWeakDelegate m_oTabButtonDraggingDelegate;
    private QWeakDelegate m_oTabButtonDroppedDelegate;
    private QWeakDelegate m_oTabButtonDragOverDelegate;

    public QTabControl()
      : this(true, true, true, true)
    {
    }

    internal QTabControl(
      bool topTabStrip,
      bool leftTabStrip,
      bool rightTabStrip,
      bool bottomTabStrip)
    {
      this.SuspendLayout();
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.m_oPainter = this.CreatePainter();
      this.m_oConfiguration = this.CreateTabControlConfiguration();
      this.m_oConfiguration.ConfigurationChanged += new EventHandler(this.Configuration_ConfigurationChanged);
      this.m_oTabStripPaintParams = new QTabStripPaintParams();
      this.m_oTabControlPaintParams = new QTabControlPaintParams();
      this.m_aTabStrips = new QTabStrip[4];
      if (topTabStrip)
        this.m_aTabStrips[0] = this.CreateTabStrip(DockStyle.Top);
      if (leftTabStrip)
        this.m_aTabStrips[3] = this.CreateTabStrip(DockStyle.Left);
      if (rightTabStrip)
        this.m_aTabStrips[1] = this.CreateTabStrip(DockStyle.Right);
      if (bottomTabStrip)
        this.m_aTabStrips[2] = this.CreateTabStrip(DockStyle.Bottom);
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null)
        {
          this.m_aTabStrips[index].ActiveButtonChanged += new QTabStripButtonChangeEventHandler(this.TabStrip_ActiveButtonChanged);
          this.m_aTabStrips[index].ActiveButtonChanging += new QTabStripButtonChangeEventHandler(this.TabStrip_ActiveButtonChanging);
          this.m_aTabStrips[index].HotButtonChanged += new QTabStripButtonChangeEventHandler(this.TabStrip_HotButtonChanged);
          this.m_aTabStrips[index].CloseButtonClick += new EventHandler(this.TabStrip_CloseButtonClick);
        }
      }
      this.ResumeLayout(false);
    }

    protected virtual System.Type AllowedDragDropTabButtonType => typeof (QTabButton);

    protected virtual QTabControlConfiguration CreateTabControlConfiguration() => new QTabControlConfiguration();

    protected virtual QTabControlPainter CreatePainter() => new QTabControlPainter();

    protected virtual QTabStrip CreateTabStrip(DockStyle dock) => new QTabStrip((IQTabStripHost) this, this.Font, dock);

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QAppearance();

    protected override QToolTipConfiguration CreateToolTipConfigurationInstance() => (QToolTipConfiguration) new QEnabledToolTipConfiguration();

    [Description("Gets raised when a QTabButton is about to be dragged from its position")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QTabButtonDragEventHandler TabButtonDragging
    {
      add => this.m_oTabButtonDraggingDelegate = QWeakDelegate.Combine(this.m_oTabButtonDraggingDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oTabButtonDraggingDelegate = QWeakDelegate.Remove(this.m_oTabButtonDraggingDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when a QTabButton is dropped on this QTabControl on a valid position")]
    public event QTabButtonDragEventHandler TabButtonDropped
    {
      add => this.m_oTabButtonDroppedDelegate = QWeakDelegate.Combine(this.m_oTabButtonDroppedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oTabButtonDroppedDelegate = QWeakDelegate.Remove(this.m_oTabButtonDroppedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when a QTabButton is dragged over a QTabButton or a QTabStrip where it can be dropped")]
    public event QTabButtonDragEventHandler TabButtonDragOver
    {
      add => this.m_oTabButtonDragOverDelegate = QWeakDelegate.Combine(this.m_oTabButtonDragOverDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oTabButtonDragOverDelegate = QWeakDelegate.Remove(this.m_oTabButtonDragOverDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when the activate QTabPage is about to change")]
    [QWeakEvent]
    public event QTabPageChangeEventHandler ActivePageChanging
    {
      add => this.m_oActivePageChangingDelegate = QWeakDelegate.Combine(this.m_oActivePageChangingDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oActivePageChangingDelegate = QWeakDelegate.Remove(this.m_oActivePageChangingDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the activate QTabPage is changed")]
    public event QTabPageChangeEventHandler ActivePageChanged
    {
      add => this.m_oActivePageChangedDelegate = QWeakDelegate.Combine(this.m_oActivePageChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oActivePageChangedDelegate = QWeakDelegate.Remove(this.m_oActivePageChangedDelegate, (Delegate) value);
    }

    [Description("Gets raised when the Hot QTabPage is changed change")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QTabPageChangeEventHandler HotPageChanged
    {
      add => this.m_oHotPageChangedDelegate = QWeakDelegate.Combine(this.m_oHotPageChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oHotPageChangedDelegate = QWeakDelegate.Remove(this.m_oHotPageChangedDelegate, (Delegate) value);
    }

    internal bool IsDesignMode => this.DesignMode;

    internal bool UsedAllowExternalDrop => this.DesignMode || this.AllowExternalDrop;

    internal bool UsedAllowExternalDrag => this.DesignMode || this.AllowExternalDrag;

    public override Font Font
    {
      get => base.Font;
      set
      {
        base.Font = value;
        if (this.m_aTabStrips == null)
          return;
        for (int index = 0; index < this.m_aTabStrips.Length; ++index)
        {
          if (this.m_aTabStrips[index] != null)
            this.m_aTabStrips[index].Font = value;
        }
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QTabControlPainter Painter
    {
      get => this.m_oPainter;
      set
      {
        this.m_oPainter = value;
        this.PerformLayout();
        this.Refresh();
      }
    }

    public bool ShouldSerializeConfiguration() => !this.m_oConfiguration.IsSetToDefaultValues();

    public void ResetConfiguration() => this.m_oConfiguration.SetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the Configuration for the QTabControl")]
    [Category("QAppearance")]
    public QTabControlConfiguration Configuration => this.m_oConfiguration;

    [DefaultValue(false)]
    [Category("QBehavior")]
    [Description("Gets or sets a value indicating whether the user can drop QTabButtons on this QTabControl.")]
    public override bool AllowDrop
    {
      get => base.AllowDrop;
      set => base.AllowDrop = value;
    }

    [Category("QBehavior")]
    [DefaultValue(false)]
    [Description("Determines if users can drop QTabButtons from other QTabControls on this QTabControl if the AllowDrop property is also enabled.")]
    public bool AllowExternalDrop
    {
      get => this.m_bAllowExternalDrop;
      set => this.m_bAllowExternalDrop = value;
    }

    [DefaultValue(false)]
    [Category("QBehavior")]
    [Description("Determines if users can drag QTabButton from this QTabButtons to drop it on another QTabControl.")]
    public bool AllowExternalDrag
    {
      get => this.m_bAllowExternalDrag;
      set => this.m_bAllowExternalDrag = value;
    }

    [Description("Gets or sets a value indicating whether the user can drag QTabButtons from this QTabControl.")]
    [DefaultValue(false)]
    [Category("QBehavior")]
    public bool AllowDrag
    {
      get => this.m_bAllowDrag;
      set => this.m_bAllowDrag = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string ToolTipText
    {
      get => base.ToolTipText;
      set => base.ToolTipText = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public QTabControlControlsCollection Controls => (QTabControlControlsCollection) base.Controls;

    [Browsable(false)]
    [DefaultValue(Keys.Tab | Keys.Control)]
    public Keys ActivateNextKey
    {
      get => this.m_eActivateNextKey;
      set => this.m_eActivateNextKey = value;
    }

    [DefaultValue(Keys.Tab | Keys.Shift | Keys.Control)]
    [Browsable(false)]
    public Keys ActivatePreviousKey
    {
      get => this.m_eActivatePreviousKey;
      set => this.m_eActivatePreviousKey = value;
    }

    [Browsable(false)]
    public Rectangle TabPagesBounds => this.m_oTabPagesBounds;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Size TabPagesSize
    {
      get => this.m_oTabPagesBounds.Size;
      set
      {
        Size size1 = this.Size;
        Size size2 = this.m_oTabPagesBounds.Size;
        size1.Width += value.Width - size2.Width;
        size1.Height += value.Height - size2.Height;
        size1.Width = Math.Max(0, size1.Width);
        size1.Height = Math.Max(0, size1.Height);
        this.Size = size1;
      }
    }

    [Browsable(false)]
    public Rectangle ContentShapeBounds => this.m_oContentShapeBounds;

    [Browsable(false)]
    public new ScrollableControl.DockPaddingEdges DockPadding => base.DockPadding;

    public bool ShouldSerializeTabStripLeftConfiguration() => this.TabStripLeft != null && !this.TabStripLeft.Configuration.IsSetToDefaultValues();

    public void ResetTabStripLeftConfiguration()
    {
      if (this.TabStripLeft == null)
        return;
      this.TabStripLeft.Configuration.SetToDefaultValues();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets the configuration for the left QTabStrip.")]
    public QTabStripConfiguration TabStripLeftConfiguration => this.TabStripLeft == null ? (QTabStripConfiguration) null : this.TabStripLeft.Configuration;

    public bool ShouldSerializeTabStripTopConfiguration() => this.TabStripTop != null && !this.TabStripTop.Configuration.IsSetToDefaultValues();

    public void ResetTabStripTopConfiguration()
    {
      if (this.TabStripTop == null)
        return;
      this.TabStripTop.Configuration.SetToDefaultValues();
    }

    [Description("Gets the configuration for the top QTabStrip.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QTabStripConfiguration TabStripTopConfiguration => this.TabStripTop == null ? (QTabStripConfiguration) null : this.TabStripTop.Configuration;

    public bool ShouldSerializeTabStripRightConfiguration() => this.TabStripRight != null && !this.TabStripRight.Configuration.IsSetToDefaultValues();

    public void ResetTabStripRightConfiguration()
    {
      if (this.TabStripRight == null)
        return;
      this.TabStripRight.Configuration.SetToDefaultValues();
    }

    [Description("Gets the configuration for the right QTabStrip.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QTabStripConfiguration TabStripRightConfiguration => this.TabStripRight == null ? (QTabStripConfiguration) null : this.TabStripRight.Configuration;

    public bool ShouldSerializeTabStripBottomConfiguration() => this.TabStripBottom != null && !this.TabStripBottom.Configuration.IsSetToDefaultValues();

    public void ResetTabStripBottomConfiguration()
    {
      if (this.TabStripBottom == null)
        return;
      this.TabStripBottom.Configuration.SetToDefaultValues();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets the configuration for the bottom QTabStrip.")]
    public QTabStripConfiguration TabStripBottomConfiguration => this.TabStripBottom == null ? (QTabStripConfiguration) null : this.TabStripBottom.Configuration;

    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the QAppearance.")]
    public virtual QAppearance Appearance => (QAppearance) base.Appearance;

    [Browsable(false)]
    public QTabPage ActiveTabPageRuntime => this.m_oActiveButton == null ? (QTabPage) null : this.m_oActiveButton.TabButtonSource as QTabPage;

    [Category("QAppearance")]
    [DefaultValue(null)]
    [TypeConverter(typeof (QTabPageSelectorTypeConverter))]
    [Description("Gets or sets the active QTabPage.")]
    public QTabPage ActiveTabPage
    {
      get => this.DesignMode ? this.m_oActiveTabPageDesign : this.ActiveTabPageRuntime;
      set
      {
        if (this.DesignMode)
          this.m_oActiveTabPageDesign = value;
        this.ActivateTabPage(value);
      }
    }

    [Browsable(false)]
    public QTabStrip TabStripTop => this.m_aTabStrips[0];

    [Browsable(false)]
    public QTabStrip TabStripLeft => this.m_aTabStrips[3];

    [Browsable(false)]
    public QTabStrip TabStripBottom => this.m_aTabStrips[2];

    [Browsable(false)]
    public QTabStrip TabStripRight => this.m_aTabStrips[1];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Obsolete("Obsolete since 1.0.7.20, use QAppearance.BorderWidth instead")]
    [Browsable(false)]
    public int BorderWidth
    {
      get => this.Appearance.BorderWidth;
      set => this.Appearance.BorderWidth = value;
    }

    [Description("Gets or sets the behavior that occurs when a QTabPage is closed.")]
    [Category("QBehavior")]
    [DefaultValue(QTabPageCloseBehavior.Dispose)]
    public QTabPageCloseBehavior TabPageCloseBehavior
    {
      get => this.m_eTabPageCloseBehavior;
      set => this.m_eTabPageCloseBehavior = value;
    }

    [Description("Gets or sets whether the drop indicator is visible when dragging a QTabButton")]
    [Category("QBehavior")]
    [DefaultValue(true)]
    public bool ShowDropIndicator
    {
      get => this.m_bShowDropIndicator;
      set => this.m_bShowDropIndicator = value;
    }

    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether a 50% transparent window with the QTabButton is visible under the cursor when dragging a QTabButton.")]
    [Category("QBehavior")]
    public bool ShowDropWindow
    {
      get => this.m_bShowDropWindow;
      set => this.m_bShowDropWindow = value;
    }

    [Description("Gets or sets whether the QTabButtons can receive the focus.")]
    [Category("QBehavior")]
    [DefaultValue(true)]
    public virtual bool FocusTabButtons
    {
      get => this.m_bFocusTabButtons;
      set => this.m_bFocusTabButtons = value;
    }

    [Browsable(false)]
    [Description("Returns whether the user is dragging a QTabButton of this QTabControl.")]
    bool IQTabStripHost.UserIsDraggingTabButton => this.m_oDraggingButton != null;

    [Description("Returns whether the user is dragging a QTabButton from another QTabControl.")]
    [Browsable(false)]
    bool IQTabStripHost.UserIsDragging => this.m_bUserIsDragging;

    [DefaultValue(true)]
    [Category("QBehavior")]
    [Description("Gets or sets whether the key navigation (with arrows when focused or CTRL+TAB) of QTabButtons should wrap around.")]
    public virtual bool WrapTabButtonNavigationAround
    {
      get => this.m_bWrapTabButtonNavigationAround;
      set => this.m_bWrapTabButtonNavigationAround = value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AutoScroll
    {
      get => false;
      set
      {
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Size AutoScrollMinSize
    {
      get => Size.Empty;
      set
      {
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public new Size AutoScrollMargin
    {
      get => Size.Empty;
      set
      {
      }
    }

    public bool ActivateTabPage(QTabPage page)
    {
      if (page != null)
      {
        if (this.Controls.Contains((Control) page))
        {
          if (!page.TabButton.IsAccessible)
            return false;
          page.TabButton.TabStrip.SetActiveButton(page.TabButton, true, true, true);
          return true;
        }
        if (!this.m_bInitializing)
          throw new InvalidOperationException(QResources.GetException("QTabControl_NonExisitingActiveTabPage"));
        this.m_oActiveButton = page.TabButton;
        return true;
      }
      if (this.m_oActiveButton == null || this.m_oActiveButton.TabStrip == null)
        return false;
      this.m_oActiveButton.TabStrip.SetActiveButton((QTabButton) null, true, true, true);
      return true;
    }

    public bool ActivateNextTabPage(bool loopAround)
    {
      QTabPage accessibleTabPage = this.GetNextAccessibleTabPage(this.ActiveTabPageRuntime, loopAround);
      if (accessibleTabPage != null)
        this.ActivateTabPage(accessibleTabPage);
      return accessibleTabPage != null;
    }

    public bool ActivatePreviousTabPage(bool loopAround)
    {
      QTabPage accessibleTabPage = this.GetPreviousAccessibleTabPage(this.ActiveTabPageRuntime, loopAround);
      if (accessibleTabPage != null)
        this.ActivateTabPage(accessibleTabPage);
      return accessibleTabPage != null;
    }

    public QTabPage GetNextAccessibleTabPage(QTabPage fromPage, bool loopAround)
    {
      QTabButton accessibleTabButton = this.GetNextAccessibleTabButton(fromPage?.TabButton, loopAround);
      return accessibleTabButton == null ? (QTabPage) null : accessibleTabButton.TabButtonSource as QTabPage;
    }

    public QTabPage GetPreviousAccessibleTabPage(QTabPage fromPage, bool loopAround)
    {
      QTabButton accessibleTabButton = this.GetPreviousAccessibleTabButton(fromPage?.TabButton, loopAround);
      return accessibleTabButton == null ? (QTabPage) null : accessibleTabButton.TabButtonSource as QTabPage;
    }

    public QTabPage GetPageWithButtonAtPoint(Point point)
    {
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null)
        {
          QTabButton accessibleButtonAtPoint = this.m_aTabStrips[index].GetAccessibleButtonAtPoint(point);
          if (accessibleButtonAtPoint != null)
            return accessibleButtonAtPoint.TabButtonSource as QTabPage;
        }
      }
      return (QTabPage) null;
    }

    public bool IsOnStripPart(Point point)
    {
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null && this.m_aTabStrips[index].IsOnStripPart(point))
          return true;
      }
      return false;
    }

    public QTabButton GetNextAccessibleTabButton(QTabButton fromButton, bool loopAround)
    {
      QTabStrip fromStrip = fromButton != null ? fromButton.TabStrip : this.GetNextAccessibleTabStrip((QTabStrip) null, false);
      QTabButton qtabButton = (QTabButton) null;
      while (fromStrip != null && qtabButton == null)
      {
        qtabButton = fromStrip.GetNextAccessibleButtonForNavigation(fromButton, false);
        if (qtabButton == null)
        {
          fromStrip = this.GetNextAccessibleTabStrip(fromStrip, false);
          fromButton = (QTabButton) null;
        }
      }
      return qtabButton == null && loopAround ? this.GetNextAccessibleTabButton((QTabButton) null, false) : qtabButton;
    }

    public QTabButton GetPreviousAccessibleTabButton(
      QTabButton fromButton,
      bool loopAround)
    {
      QTabStrip fromStrip = fromButton != null ? fromButton.TabStrip : this.GetPreviousAccessibleTabStrip((QTabStrip) null, false);
      QTabButton qtabButton = (QTabButton) null;
      while (fromStrip != null && qtabButton == null)
      {
        qtabButton = fromStrip.GetPreviousAccessibleButtonForNavigation(fromButton, false);
        if (qtabButton == null)
        {
          fromStrip = this.GetPreviousAccessibleTabStrip(fromStrip, false);
          fromButton = (QTabButton) null;
        }
      }
      return qtabButton == null && loopAround ? this.GetPreviousAccessibleTabButton((QTabButton) null, false) : qtabButton;
    }

    public QTabStrip GetNextAccessibleTabStrip(QTabStrip fromStrip, bool loopAround)
    {
      int index = fromStrip == null ? 0 : Array.IndexOf<QTabStrip>(this.m_aTabStrips, fromStrip) + 1;
      QTabStrip qtabStrip = (QTabStrip) null;
      while (index < this.m_aTabStrips.Length && qtabStrip == null)
      {
        if (this.m_aTabStrips[index] != null && this.m_aTabStrips[index].AccessibleButtonCount > 0)
          qtabStrip = this.m_aTabStrips[index];
        else
          ++index;
      }
      return qtabStrip == null && loopAround ? this.GetNextAccessibleTabStrip((QTabStrip) null, false) : qtabStrip;
    }

    public QTabStrip GetPreviousAccessibleTabStrip(QTabStrip fromStrip, bool loopAround)
    {
      int index = fromStrip == null ? this.m_aTabStrips.Length - 1 : Array.IndexOf<QTabStrip>(this.m_aTabStrips, fromStrip) - 1;
      QTabStrip qtabStrip = (QTabStrip) null;
      while (index >= 0 && qtabStrip == null)
      {
        if (this.m_aTabStrips[index] != null && this.m_aTabStrips[index].AccessibleButtonCount > 0)
          qtabStrip = this.m_aTabStrips[index];
        else
          --index;
      }
      return qtabStrip == null && loopAround ? this.GetPreviousAccessibleTabStrip((QTabStrip) null, false) : qtabStrip;
    }

    public virtual void BeginInit()
    {
      this.m_bInitializing = true;
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null)
          this.m_aTabStrips[index].BeginInit();
      }
      this.SuspendDraw();
      this.SuspendLayout();
    }

    public virtual void EndInit()
    {
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null)
          this.m_aTabStrips[index].EndInit();
      }
      this.m_bInitializing = false;
      if (this.ActiveTabPage != null && !this.Controls.Contains((Control) this.ActiveTabPage))
        throw new InvalidOperationException(QResources.GetException("QTabControl_EndInitNonExistingActiveTabPage"));
      if (this.DesignMode)
      {
        for (int index = 0; index < 3; ++index)
          this.ResumeLayout(false);
        this.PerformLayout();
      }
      else
        this.ResumeLayout(true);
      this.ResumeDraw(true);
    }

    public void SuspendDraw() => this.m_bSuspendingRedraw = true;

    public void ResumeDraw(bool redraw)
    {
      this.m_bSuspendingRedraw = false;
      if (!redraw)
        return;
      this.Refresh();
    }

    protected override int ClientAreaMarginTop => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderTop ? 0 : this.Appearance.BorderWidth;

    protected override int ClientAreaMarginLeft => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderLeft ? 0 : this.Appearance.BorderWidth;

    protected override int ClientAreaMarginRight => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderRight ? 0 : this.Appearance.BorderWidth;

    protected override int ClientAreaMarginBottom => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderBottom ? 0 : this.Appearance.BorderWidth;

    protected override string BackColorPropertyName => "TabControlBackground1";

    protected override string BackColor2PropertyName => "TabControlBackground2";

    protected override string BorderColorPropertyName => "TabControlBorder";

    protected override Control.ControlCollection CreateControlsInstance() => (Control.ControlCollection) new QTabControlControlsCollection(this);

    public override void Refresh()
    {
      if (this.m_bSuspendingRedraw)
        return;
      base.Refresh();
    }

    internal void EnableCloseButtons(bool enabled)
    {
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null)
          this.m_aTabStrips[index].CloseButtonEnabled = enabled;
      }
    }

    private void StartDraggingButton(QTabButton button, Control dragSource)
    {
      if (button == null || !(button.Control is QTabPage) || !button.TabStrip.UsedAllowDrag)
        return;
      QTabButtonDragEventArgs e = new QTabButtonDragEventArgs(button, (QTabButton) null, (QTabStrip) null, QTabButtonDockStyle.Left, -1, false, true, false);
      this.OnTabButtonDragging(e);
      if (e.Cancel)
      {
        this.m_oMouseDownAtButton = (QTabButton) null;
        this.m_oMouseDownAtPoint = Point.Empty;
      }
      else
      {
        this.m_oDraggingButton = button;
        this.m_oDraggingCursor = this.Cursor;
        this.m_oDraggingButtonWindow = (QTabControl.QTranslucentTabButtonWindow) null;
        if (this.m_oDraggingButtonWindow == null && this.ShowDropWindow)
        {
          Bitmap bitmap = new Bitmap(this.m_oDraggingButton.BoundsToControl.Width + this.m_oDraggingButton.Configuration.Appearance.BorderWidth, this.m_oDraggingButton.BoundsToControl.Height + this.m_oDraggingButton.Configuration.Appearance.BorderWidth);
          Graphics graphics = Graphics.FromImage((Image) bitmap);
          graphics.TranslateTransform((float) -this.m_oDraggingButton.BoundsToControl.X, (float) -this.m_oDraggingButton.BoundsToControl.Y);
          this.m_oDraggingButton.DrawingOnBitmap = true;
          this.m_oDraggingButton.TabStrip.Painter.DrawTabButton(this.m_oDraggingButton, graphics);
          this.m_oDraggingButton.DrawingOnBitmap = false;
          graphics.Dispose();
          this.m_oDraggingButtonWindow = new QTabControl.QTranslucentTabButtonWindow();
          Rectangle boundsToControl = this.m_oMouseDownAtButton.BoundsToControl;
          this.m_oDraggingButtonWindow.LocationOffset = new Point(this.m_oMouseDownAtPoint.X - boundsToControl.X, this.m_oMouseDownAtPoint.Y - boundsToControl.Y);
          this.m_oDraggingButtonWindow.Owner = this.ParentForm;
          this.m_oDraggingButtonWindow.Opacity = 0.5;
          this.m_oDraggingButtonWindow.BackgroundImage = (Image) bitmap;
          this.m_oDraggingButtonWindow.SetLocation(Control.MousePosition);
          this.m_oDraggingButtonWindow.Show();
        }
        int num = (int) dragSource.DoDragDrop((object) this.m_oDraggingButton, DragDropEffects.Move);
      }
    }

    private void EndDraggingButton(QTabButton dropButton)
    {
      if (dropButton != null)
      {
        for (int dockStyle = 0; dockStyle < this.m_aTabStrips.Length; ++dockStyle)
        {
          if (this.m_aTabStrips[dockStyle] != null && this.m_aTabStrips[dockStyle].DropArea != null && this.m_aTabStrips[dockStyle].DropArea.AllowDrop)
          {
            this.OnTabButtonDropped(new QTabButtonDragEventArgs(dropButton, this.m_aTabStrips[dockStyle].DropArea.Button, this.m_aTabStrips[dockStyle], this.m_aTabStrips[dockStyle].DropArea.Dock, this.m_aTabStrips[dockStyle].DropArea.DropButtonOrder, true, false, false));
            if (this.m_aTabStrips[dockStyle] == dropButton.TabStrip)
              dropButton.SetButtonOrder(this.m_aTabStrips[dockStyle].DropArea.DropButtonOrder, true);
            else if (this == dropButton.TabStrip.TabStripHost)
            {
              dropButton.TabStrip.TabButtons.Remove(dropButton, false);
              this.m_aTabStrips[dockStyle].TabButtons.Add(dropButton, this.m_aTabStrips[dockStyle].DropArea.DropButtonOrder, dropButton.Enabled);
              if (dropButton.Control is QTabPage)
                ((QTabPage) dropButton.Control).SetButtonDockStyleWithoutChange((QTabButtonDockStyle) dockStyle);
            }
            else if (dropButton.TabStrip.TabStripHost is Control tabStripHost && tabStripHost.Controls.Contains(dropButton.Control) && dropButton.Control is QTabPage)
            {
              QTabPage control = dropButton.Control as QTabPage;
              tabStripHost.Controls.Remove((Control) control);
              dropButton.ButtonOrder = this.m_aTabStrips[dockStyle].DropArea.DropButtonOrder;
              control.ButtonDockStyle = (QTabButtonDockStyle) dockStyle;
              this.Controls.Add(dropButton.Control);
              this.ActivateTabPage(dropButton.Control as QTabPage);
            }
            this.m_aTabStrips[dockStyle].DropArea = (QTabButtonDropArea) null;
            break;
          }
        }
      }
      this.m_bUserIsDragging = false;
      this.PerformLayout();
      this.Refresh();
      this.m_oDraggingButton = (QTabButton) null;
      this.m_oMouseDownAtButton = (QTabButton) null;
      this.Cursor = this.m_oDraggingCursor;
      if (this.m_oDraggingButtonWindow != null)
      {
        this.m_oDraggingButtonWindow.Dispose();
        this.m_oDraggingButtonWindow = (QTabControl.QTranslucentTabButtonWindow) null;
      }
      this.Invalidate();
    }

    private void SetHotButton(QTabPage tabPage)
    {
      QTabStrip qtabStrip = (QTabStrip) null;
      QTabButton button = (QTabButton) null;
      if (tabPage != null)
      {
        button = tabPage.TabButton;
        qtabStrip = tabPage.TabButton.TabStrip;
      }
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null)
        {
          if (this.m_aTabStrips[index] != qtabStrip)
            this.m_aTabStrips[index].PutHotButton((QTabButton) null);
          else
            this.m_aTabStrips[index].PutHotButton(button);
        }
      }
    }

    private QTabStrip AddTabButton(
      QTabPage tabPage,
      QTabButtonDockStyle dockStyle,
      int buttonOrder,
      bool activateButton)
    {
      if (this.m_aTabStrips[(int) dockStyle] == null)
        throw new InvalidOperationException(QResources.GetException("QTabControl_TabStripWithDockNotDefined", (object) dockStyle));
      this.m_aTabStrips[(int) dockStyle].TabButtons.Add(tabPage.TabButton, buttonOrder, activateButton);
      return this.m_aTabStrips[(int) dockStyle];
    }

    private void RemoveTabButton(
      QTabPage tabPage,
      QTabButtonDockStyle dockStyle,
      bool activateNewTabWhenActive)
    {
      if (this.m_aTabStrips[(int) dockStyle] == null)
        return;
      this.m_aTabStrips[(int) dockStyle].TabButtons.Remove(tabPage.TabButton, activateNewTabWhenActive);
    }

    internal void HandleTabButtonDockChange(
      QTabPage tabPage,
      QTabButtonDockStyle fromDock,
      QTabButtonDockStyle toDock)
    {
      if (this.m_aTabStrips[(int) toDock] == null)
        throw new InvalidOperationException(QResources.GetException("QTabControl_TabStripWithDockNotDefined", (object) toDock));
      bool isActivated = tabPage.TabButton.IsActivated;
      this.m_bTabControlIsChangingActiveButton = true;
      this.RemoveTabButton(tabPage, fromDock, false);
      try
      {
        this.AddTabButton(tabPage, toDock, -1, isActivated);
        if (!isActivated)
          return;
        this.m_oActiveButton = tabPage.TabButton;
      }
      finally
      {
        this.m_bTabControlIsChangingActiveButton = false;
      }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Right && this.FocusTabButtons && this.ActiveTabPage != null && this.ActiveTabPage.Focused)
      {
        if (this.ActivateNextTabPage(this.WrapTabButtonNavigationAround))
          return true;
      }
      else if (keyData == Keys.Left && this.FocusTabButtons && this.ActiveTabPage != null && this.ActiveTabPage.Focused)
      {
        if (this.ActivatePreviousTabPage(this.WrapTabButtonNavigationAround))
          return true;
      }
      else if (keyData == this.m_eActivateNextKey)
      {
        if (this.ActivateNextTabPage(this.WrapTabButtonNavigationAround))
          return true;
      }
      else if (keyData == this.m_eActivatePreviousKey && this.ActivatePreviousTabPage(this.WrapTabButtonNavigationAround))
        return true;
      return base.ProcessCmdKey(ref msg, keyData);
    }

    internal void HandleMouseDown(MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      this.m_oMouseDownAtButton = (QTabButton) null;
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null)
        {
          this.m_aTabStrips[index].HandleMouseDown(e);
          if (this.m_oMouseDownAtButton == null)
          {
            this.m_oMouseDownAtPoint = new Point(e.X, e.Y);
            this.m_oMouseDownAtButton = this.m_aTabStrips[index].GetButtonAtPoint(this.m_oMouseDownAtPoint, QTabButtonSelectionTypes.MustBeVisible);
          }
        }
      }
    }

    internal void HandleMouseUp(MouseEventArgs e)
    {
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null)
          this.m_aTabStrips[index].HandleMouseUp(e);
      }
    }

    internal bool HandleMouseMoveBeginDrag(MouseEventArgs e, Control dragSource)
    {
      if (e.Button != MouseButtons.Left || ((IQTabStripHost) this).UserIsDraggingTabButton || this.m_oMouseDownAtButton == null || QMath.ValueInMargin(e.X, this.m_oMouseDownAtPoint.X, SystemInformation.DragSize.Width) && QMath.ValueInMargin(e.Y, this.m_oMouseDownAtPoint.Y, SystemInformation.DragSize.Height))
        return false;
      this.StartDraggingButton(this.m_oMouseDownAtButton, dragSource);
      return true;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.HandleMouseDown(e);
      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null)
          this.m_aTabStrips[index].HandleMouseMove(e);
      }
      this.HandleMouseMoveBeginDrag(e, (Control) this);
    }

    internal bool StartDraggingFromPoint(MouseEventArgs e, Control dragSource)
    {
      QTabButton qtabButton = (QTabButton) null;
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null)
          qtabButton = this.m_aTabStrips[index].GetButtonAtPoint(e.Location, QTabButtonSelectionTypes.MustBeVisible);
      }
      if (qtabButton == null)
        return false;
      this.m_oMouseDownAtButton = qtabButton;
      this.m_oMouseDownAtPoint = e.Location;
      this.StartDraggingButton(this.m_oMouseDownAtButton, dragSource);
      return true;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.HandleMouseUp(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      this.HandleMouseLeave();
      base.OnMouseLeave(e);
    }

    internal void HandleMouseLeave()
    {
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null)
          this.m_aTabStrips[index].HandleMouseLeave();
      }
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
      if (e.Control is QTabPage control)
      {
        control.ColorScheme.SetBaseColorScheme((QColorSchemeBase) this.ColorScheme, false);
        if (this.m_aTabStrips[(int) control.ButtonDockStyle] == null)
          throw new InvalidOperationException(QResources.GetException("QTabControl_TabStripWithDockNotDefined", (object) control.ButtonDockStyle));
        if (this.m_oActiveButton != null)
        {
          if (!this.m_oActiveButton.IsActivated && control.TabButton == this.m_oActiveButton)
            this.AddTabButton(control, control.ButtonDockStyle, control.ButtonOrder, true);
          else
            this.AddTabButton(control, control.ButtonDockStyle, control.ButtonOrder, false);
        }
        else if (this.Controls.AccessibleTabPagesCount == 1 && control.Enabled && control.ButtonVisible)
          this.AddTabButton(control, control.ButtonDockStyle, control.ButtonOrder, true);
        else
          this.AddTabButton(control, control.ButtonDockStyle, control.ButtonOrder, false);
      }
      base.OnControlAdded(e);
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
      if (e.Control is QTabPage control)
      {
        control.ColorScheme.SetBaseColorScheme((QColorSchemeBase) null, false);
        if (this.DesignMode && this.m_oActiveTabPageDesign == control)
          this.m_oActiveTabPageDesign = (QTabPage) null;
        this.RemoveTabButton(control, control.ButtonDockStyle, true);
      }
      this.PerformLayout();
      base.OnControlRemoved(e);
    }

    internal void PerformTabStripsLayout()
    {
      int x = 0;
      int y = 0;
      int width1 = this.ClientSize.Width;
      int height1 = this.ClientSize.Height;
      int height2 = this.ClientSize.Height;
      int width2 = this.ClientSize.Width;
      bool flag = false;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      while (!flag)
      {
        flag = true;
        for (int index = 0; index < this.m_aTabStrips.Length; ++index)
        {
          if (this.m_aTabStrips[index] != null)
          {
            QTabStrip aTabStrip = this.m_aTabStrips[index];
            if (aTabStrip.IsVisible)
            {
              QTabStripAppearance appearance = this.m_aTabStrips[index].Configuration.Appearance;
              Size availableSize = Size.Empty;
              if (aTabStrip.Dock == DockStyle.Top)
              {
                availableSize = new Size(width1 - (num2 + num3), height1 - num4);
                if (aTabStrip.CalculateLayout(availableSize, false))
                  flag = false;
                num1 = this.m_aTabStrips[index].CalculatedSize.Height + this.m_aTabStrips[index].Configuration.StripMargin.Vertical;
              }
              else if (aTabStrip.Dock == DockStyle.Bottom)
              {
                availableSize = new Size(width1 - (num2 + num3), height1 - num1);
                if (aTabStrip.CalculateLayout(availableSize, false))
                  flag = false;
                num4 = this.m_aTabStrips[index].CalculatedSize.Height + this.m_aTabStrips[index].Configuration.StripMargin.Vertical;
              }
              else if (aTabStrip.Dock == DockStyle.Left)
              {
                availableSize = new Size(width1 - num3, height1 - (num1 + num4));
                if (aTabStrip.CalculateLayout(availableSize, false))
                  flag = false;
                num2 = this.m_aTabStrips[index].CalculatedSize.Width + this.m_aTabStrips[index].Configuration.StripMargin.Vertical;
              }
              else if (aTabStrip.Dock == DockStyle.Right)
              {
                availableSize = new Size(width1 - num2, height1 - (num1 + num4));
                if (aTabStrip.CalculateLayout(availableSize, false))
                  flag = false;
                num3 = this.m_aTabStrips[index].CalculatedSize.Width;
                num2 += this.m_aTabStrips[index].Configuration.StripMargin.Vertical;
              }
            }
            else
              aTabStrip.PutBounds(Rectangle.Empty);
          }
        }
      }
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null)
        {
          QTabStrip aTabStrip = this.m_aTabStrips[index];
          QTabStripAppearance appearance = this.m_aTabStrips[index].Configuration.Appearance;
          Size empty = Size.Empty;
          if (aTabStrip.Dock == DockStyle.Top)
          {
            int height3 = (aTabStrip.IsVisible ? aTabStrip.CalculatedSize.Height : 0) + (aTabStrip.IsVisible ? aTabStrip.Configuration.StripMargin.Vertical : 0);
            aTabStrip.PutOuterBounds(new Rectangle(x + num2, y, width1 - (num2 + num3), height3));
          }
          else if (aTabStrip.Dock == DockStyle.Bottom)
          {
            int height4 = (aTabStrip.IsVisible ? aTabStrip.CalculatedSize.Height : 0) + (aTabStrip.IsVisible ? aTabStrip.Configuration.StripMargin.Vertical : 0);
            aTabStrip.PutOuterBounds(new Rectangle(x + num2, height2 - height4, width1 - (num2 + num3), height4));
          }
          else if (aTabStrip.Dock == DockStyle.Left)
          {
            int width3 = (aTabStrip.IsVisible ? aTabStrip.CalculatedSize.Width : 0) + (aTabStrip.IsVisible ? aTabStrip.Configuration.StripMargin.Vertical : 0);
            aTabStrip.PutOuterBounds(new Rectangle(x, y + num1, width3, height1 - (num1 + num4)));
          }
          else if (aTabStrip.Dock == DockStyle.Right)
          {
            int width4 = (aTabStrip.IsVisible ? aTabStrip.CalculatedSize.Width : 0) + (aTabStrip.IsVisible ? aTabStrip.Configuration.StripMargin.Vertical : 0);
            aTabStrip.PutOuterBounds(new Rectangle(width2 - width4, y + num1, width4, height1 - (num1 + num4)));
          }
          if (aTabStrip.IsVisible)
            aTabStrip.CorrectScrollAfterPossibleResize(false);
        }
      }
      int num5 = this.TabStripLeft != null ? this.TabStripLeft.OuterBounds.Right : 0;
      int num6 = this.TabStripTop != null ? this.TabStripTop.OuterBounds.Bottom : 0;
      int right = this.TabStripRight != null ? this.TabStripRight.OuterBounds.Left : width2;
      int bottom = this.TabStripBottom != null ? this.TabStripBottom.OuterBounds.Top : height2;
      int num7 = 1;
      this.m_oContentShapeBounds = Rectangle.FromLTRB(num5 + (this.TabStripLeft == null || !this.TabStripLeft.IsVisible ? 0 : num7), num6 + (this.TabStripTop == null || !this.TabStripTop.IsVisible ? 0 : num7), right, bottom);
      this.m_oContentShapeBounds = this.Configuration.ContentMargin.InflateRectangleWithMargin(this.m_oContentShapeBounds, false, true);
      this.m_oTabPagesBounds = this.m_oConfiguration.ContentAppearance.Shape.CalculateContentBounds(this.m_oContentShapeBounds, DockStyle.None);
      if (this.m_oTabPagesBounds.Width < 0)
        this.m_oTabPagesBounds.Width = 0;
      if (this.m_oTabPagesBounds.Height >= 0)
        return;
      this.m_oTabPagesBounds.Height = 0;
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      if (!this.m_bActiveButtonIsChanging)
      {
        this.PerformTabStripsLayout();
        if (this.m_bUserIsDragging)
        {
          for (int index = 0; index < this.m_aTabStrips.Length; ++index)
          {
            if (this.m_aTabStrips[index] != null)
              this.m_aTabStrips[index].CalculateDropAreas();
          }
        }
      }
      base.OnLayout(levent);
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QTabPage control && (control.Visible || control.ShouldLayoutWhenInvisible()))
          control.SetBounds(this.m_oTabPagesBounds.Left, this.m_oTabPagesBounds.Top, this.m_oTabPagesBounds.Width, this.m_oTabPagesBounds.Height);
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.Painter.DrawContentShape(this, this.ContentShapeBounds, this.Configuration, this.RetrieveTabControlPaintParams(), e.Graphics);
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null && e.ClipRectangle.IntersectsWith(this.m_aTabStrips[index].Bounds) && this.m_aTabStrips[index].IsVisible)
        {
          this.m_aTabStrips[index].Draw(e.Graphics);
          if (this.ShowDropIndicator && this.m_aTabStrips[index].DropArea != null)
            this.m_aTabStrips[index].DrawDropArea(e.Graphics);
        }
      }
    }

    internal bool CanHandleDragData(IDataObject data) => data.GetDataPresent(this.AllowedDragDropTabButtonType);

    internal void HandleDragEnter(DragEventArgs drgevent)
    {
      if (!drgevent.Data.GetDataPresent(this.AllowedDragDropTabButtonType))
      {
        this.m_bUserIsDragging = false;
        this.PerformLayout();
        this.Refresh();
      }
      else
      {
        if (!((IQTabStripHost) this).UserIsDraggingTabButton)
        {
          QTabButton data = (QTabButton) drgevent.Data.GetData(this.AllowedDragDropTabButtonType);
          QTabControl qtabControl = data.TabStrip == null ? (QTabControl) null : data.TabStrip.TabStripHost as QTabControl;
          for (int index = 0; index < this.m_aTabStrips.Length; ++index)
          {
            if (this.m_aTabStrips[index] != null)
            {
              if (this.UsedAllowExternalDrop && qtabControl != null && qtabControl.UsedAllowExternalDrag)
              {
                this.m_aTabStrips[index].CalculateDropAreas();
                this.m_bUserIsDragging = true;
                this.PerformLayout();
                this.Refresh();
              }
              else
                this.m_aTabStrips[index].ClearDropAreas();
            }
          }
        }
        else
        {
          this.m_bUserIsDragging = true;
          this.PerformLayout();
          this.Refresh();
        }
        drgevent.Effect = DragDropEffects.Move;
      }
    }

    internal void HandleDragOver(DragEventArgs drgevent)
    {
      if (!drgevent.Data.GetDataPresent(this.AllowedDragDropTabButtonType))
      {
        if (this.DesignMode)
          return;
        drgevent.Effect = DragDropEffects.None;
      }
      else
      {
        QTabButton data = (QTabButton) drgevent.Data.GetData(this.AllowedDragDropTabButtonType);
        for (int index = 0; index < this.m_aTabStrips.Length; ++index)
        {
          if (this.m_aTabStrips[index] != null && this.m_aTabStrips[index].HandleDragging(this.PointToClient(new Point(drgevent.X, drgevent.Y)), data))
          {
            QTabButtonDragEventArgs e = new QTabButtonDragEventArgs(data, this.m_aTabStrips[index].DropArea.Button, this.m_aTabStrips[index], this.m_aTabStrips[index].DropArea.Dock, this.m_aTabStrips[index].DropArea.DropButtonOrder, true, false, true);
            this.OnTabButtonDragOver(e);
            if (!e.AllowDrop)
            {
              this.m_aTabStrips[index].DropArea.AllowDrop = false;
              break;
            }
            break;
          }
        }
        drgevent.Effect = DragDropEffects.Move;
      }
    }

    internal void HandleQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
    {
      if (qcdevent.Action == DragAction.Cancel)
        this.EndDraggingButton((QTabButton) null);
      else if (qcdevent.Action == DragAction.Drop)
        this.EndDraggingButton((QTabButton) null);
      if (this.m_oDraggingButtonWindow == null)
        return;
      this.m_oDraggingButtonWindow.SetLocation(Control.MousePosition);
    }

    internal void HandleDragDrop(DragEventArgs drgevent)
    {
      if (drgevent.Data.GetDataPresent(this.AllowedDragDropTabButtonType))
        this.EndDraggingButton(drgevent.Data.GetData(this.AllowedDragDropTabButtonType) as QTabButton);
      else
        this.EndDraggingButton((QTabButton) null);
    }

    internal void HandleDragLeave()
    {
      this.m_bUserIsDragging = false;
      this.PerformLayout();
      this.Refresh();
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null)
          this.m_aTabStrips[index].DropArea = (QTabButtonDropArea) null;
      }
      this.Invalidate();
    }

    protected override void OnDragEnter(DragEventArgs drgevent)
    {
      base.OnDragEnter(drgevent);
      this.HandleDragEnter(drgevent);
    }

    protected override void OnDragOver(DragEventArgs drgevent)
    {
      base.OnDragOver(drgevent);
      this.HandleDragOver(drgevent);
    }

    protected override void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
    {
      base.OnQueryContinueDrag(qcdevent);
      this.HandleQueryContinueDrag(qcdevent);
    }

    protected override void OnDragDrop(DragEventArgs drgevent)
    {
      base.OnDragDrop(drgevent);
      this.HandleDragDrop(drgevent);
    }

    protected override void OnDragLeave(EventArgs e)
    {
      base.OnDragLeave(e);
      this.HandleDragLeave();
    }

    private bool RaiseActivePageChanging(QTabPage fromPage, ref QTabPage toPage)
    {
      QTabPageChangeEventArgs e = new QTabPageChangeEventArgs(fromPage, toPage, true);
      this.OnActivePageChanging(e);
      toPage = e.ToPage;
      return !e.Cancel;
    }

    private void RaiseActivePageChanged(QTabPage fromPage, QTabPage toPage) => this.OnActivePageChanged(new QTabPageChangeEventArgs(fromPage, toPage, false));

    private void RaiseHotPageChanged(QTabPage fromPage, QTabPage toPage) => this.OnHotPageChanged(new QTabPageChangeEventArgs(fromPage, toPage, false));

    protected virtual void OnTabButtonDragging(QTabButtonDragEventArgs e) => this.m_oTabButtonDraggingDelegate = QWeakDelegate.InvokeDelegate(this.m_oTabButtonDraggingDelegate, (object) this, (object) e);

    protected virtual void OnTabButtonDropped(QTabButtonDragEventArgs e) => this.m_oTabButtonDroppedDelegate = QWeakDelegate.InvokeDelegate(this.m_oTabButtonDroppedDelegate, (object) this, (object) e);

    protected virtual void OnTabButtonDragOver(QTabButtonDragEventArgs e) => this.m_oTabButtonDragOverDelegate = QWeakDelegate.InvokeDelegate(this.m_oTabButtonDragOverDelegate, (object) this, (object) e);

    protected virtual void OnActivePageChanging(QTabPageChangeEventArgs e) => this.m_oActivePageChangingDelegate = QWeakDelegate.InvokeDelegate(this.m_oActivePageChangingDelegate, (object) this, (object) e);

    protected virtual void OnActivePageChanged(QTabPageChangeEventArgs e) => this.m_oActivePageChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oActivePageChangedDelegate, (object) this, (object) e);

    protected virtual void OnHotPageChanged(QTabPageChangeEventArgs e) => this.m_oHotPageChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oHotPageChangedDelegate, (object) this, (object) e);

    protected override void OnTimerElapsed(QControlTimerEventArgs e)
    {
      base.OnTimerElapsed(e);
      if (e.TimerId != 17 || this.m_aTabStripsListeningToTimer == null)
        return;
      for (int index = this.m_aTabStripsListeningToTimer.Count - 1; index >= 0; --index)
        ((QTabStrip) this.m_aTabStripsListeningToTimer[index]).HandleAnimateTimerTick();
    }

    protected virtual void HandleConfigurationChanged()
    {
    }

    private void CalculateButtonStatesOnActiveButton()
    {
      if (this.m_oActiveButton != null)
        this.EnableCloseButtons(this.m_oActiveButton.CanClose);
      else
        this.EnableCloseButtons(false);
    }

    private void TabStrip_ActiveButtonChanging(object sender, QTabStripButtonChangeEventArgs e)
    {
      QTabStrip fromStrip = this.m_oActiveButton != null ? this.m_oActiveButton.TabStrip : (QTabStrip) null;
      QTabStrip qtabStrip1 = sender as QTabStrip;
      QTabStrip qtabStrip2 = sender as QTabStrip;
      if (!this.m_bTabControlIsChangingActiveButton)
      {
        QTabPage activeTabPageRuntime = this.ActiveTabPageRuntime;
        QTabPage toPage = e.ToButton != null ? e.ToButton.TabButtonSource as QTabPage : (QTabPage) null;
        if (e.ToButton == null)
        {
          QTabStrip accessibleTabStrip = this.GetNextAccessibleTabStrip(fromStrip, true);
          if (accessibleTabStrip != null)
            toPage = accessibleTabStrip.GetNextAccessibleButtonForNavigation((QTabButton) null, false).TabButtonSource as QTabPage;
        }
        if (!this.RaiseActivePageChanging(activeTabPageRuntime, ref toPage))
        {
          e.Cancel = true;
        }
        else
        {
          this.m_bActiveButtonIsChanging = true;
          QTabButton tabButton = toPage?.TabButton;
          QTabStrip tabStrip = tabButton?.TabStrip;
          if (tabStrip != null && (fromStrip != null && tabStrip != fromStrip || tabStrip != qtabStrip1))
          {
            e.Cancel = true;
            this.m_bTabControlIsChangingActiveButton = true;
            if (fromStrip != null && fromStrip != tabStrip && fromStrip.ActiveButton != null)
              fromStrip.SetActiveButton((QTabButton) null, true, false, true);
            if (qtabStrip1 != null && qtabStrip1 != tabStrip && qtabStrip1.ActiveButton != null)
              qtabStrip1.SetActiveButton((QTabButton) null, true, false, true);
            tabStrip.SetActiveButton(tabButton, false, true, true);
            this.m_bTabControlIsChangingActiveButton = false;
          }
          else
            e.ToButton = tabButton;
        }
      }
      else
        this.m_bActiveButtonIsChanging = true;
    }

    private void TabStrip_ActiveButtonChanged(object sender, QTabStripButtonChangeEventArgs e)
    {
      if (e.ToButton == null && this.m_bTabControlIsChangingActiveButton)
        return;
      QTabPage activeTabPageRuntime = this.ActiveTabPageRuntime;
      QTabPage toPage = e.ToButton != null ? e.ToButton.TabButtonSource as QTabPage : (QTabPage) null;
      this.m_oActiveButton = e.ToButton;
      this.CalculateButtonStatesOnActiveButton();
      if (this.DesignMode && this.m_oActiveTabPageDesign == null)
        this.m_oActiveTabPageDesign = this.m_oActiveButton != null ? this.m_oActiveButton.TabButtonSource as QTabPage : (QTabPage) null;
      this.m_bActiveButtonIsChanging = false;
      this.RaiseActivePageChanged(activeTabPageRuntime, toPage);
      if (!this.Configuration.ContentAppearance.UseControlBackgroundForShape)
        return;
      this.Invalidate(this.m_oContentShapeBounds, false);
    }

    private void TabStrip_HotButtonChanged(object sender, QTabStripButtonChangeEventArgs e)
    {
      string str = e.ToButton != null ? e.ToButton.UsedToolTipText : (string) null;
      if (this.ToolTipText != str)
        this.ToolTipText = str;
      this.RaiseHotPageChanged(e.FromButton != null ? e.FromButton.TabButtonSource as QTabPage : (QTabPage) null, e.ToButton != null ? e.ToButton.TabButtonSource as QTabPage : (QTabPage) null);
    }

    private void TabStrip_CloseButtonClick(object sender, EventArgs e) => (this.m_oActiveButton != null ? this.m_oActiveButton.TabButtonSource as QTabPage : (QTabPage) null)?.Close();

    private void Configuration_ConfigurationChanged(object sender, EventArgs e)
    {
      this.HandleConfigurationChanged();
      this.PerformLayout();
      this.Refresh();
    }

    [Description("Gets or sets the PersistGuid. With this Guid the control is identified in the persistence files.")]
    [Category("QPersistence")]
    public virtual Guid PersistGuid
    {
      get => this.m_oPersistGuid;
      set => this.m_oPersistGuid = value;
    }

    [Category("QPersistence")]
    [Description("Gets or sets whether this object must be persisted.")]
    [DefaultValue(true)]
    public virtual bool PersistObject
    {
      get => this.m_bPersistObject;
      set => this.m_bPersistObject = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsPersisted
    {
      get => this.m_bIsPersisted;
      set => this.m_bIsPersisted = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool RequiresUnload => false;

    [DefaultValue(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets whether a new instance of this PersistableObject must be created when it is loaded from file. If this is false the persistableObject must match an existing persistableObject in the QPersistenceManager.PersistableObjects collection.")]
    [Category("QPersistence")]
    public bool CreateNew
    {
      get => false;
      set
      {
      }
    }

    string IQPersistableObject.Name
    {
      get => this.Name;
      set => this.Name = value;
    }

    public virtual bool MustBePersistedAfter(IQPersistableObject persistableObject) => false;

    public virtual IXPathNavigable SavePersistableObject(
      QPersistenceManager manager,
      IXPathNavigable parentElement)
    {
      IXPathNavigable parentElement1 = manager != null ? manager.CreatePersistableObjectElement((IQPersistableObject) this, parentElement) : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      for (int index = 0; index < this.m_aTabStrips.Length; ++index)
      {
        if (this.m_aTabStrips[index] != null)
          this.m_aTabStrips[index].SavePersistableObject(manager, parentElement1);
      }
      return parentElement1;
    }

    public virtual bool LoadPersistableObject(
      QPersistenceManager manager,
      IXPathNavigable persistableObjectElement,
      object parentState)
    {
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      XPathNodeIterator xpathNodeIterator = persistableObjectElement.CreateNavigator().SelectChildren("persistableObject", "");
      this.SuspendDraw();
      this.SuspendLayout();
      while (xpathNodeIterator.MoveNext())
      {
        IXPathNavigable navigableFromNavigator = QXmlHelper.GetNavigableFromNavigator(xpathNodeIterator.Current);
        manager.GetPersistableObject(navigableFromNavigator)?.LoadPersistableObject(manager, navigableFromNavigator, (object) persistableObjectElement);
      }
      this.ResumeLayout(true);
      this.ResumeDraw(true);
      return true;
    }

    public virtual void UnloadPersistableObject()
    {
    }

    protected virtual void UpdateTabStripPaintParams(QTabStripPaintParams paintParams)
    {
      paintParams.Border = (Color) this.ColorScheme.TabStripBorder;
      paintParams.Background1 = (Color) this.ColorScheme.TabStripBackground1;
      paintParams.Background2 = (Color) this.ColorScheme.TabStripBackground2;
      paintParams.DropIndicatorBackground = (Color) this.ColorScheme.TabControlDropIndicatorBackground;
      paintParams.DropIndicatorBorder = (Color) this.ColorScheme.TabControlDropIndicatorBorder;
      paintParams.NavigationButtonReplace = Color.FromArgb((int) byte.MaxValue, 0, 0);
      paintParams.NavigationButtonBackground1 = (Color) this.ColorScheme.TabButtonBackground1;
      paintParams.NavigationButtonBackground2 = (Color) this.ColorScheme.TabButtonBackground2;
      paintParams.NavigationButtonBorder = (Color) this.ColorScheme.TabButtonBorder;
      paintParams.NavigationButtonBackground1Hot = (Color) this.ColorScheme.TabButtonHotBackground1;
      paintParams.NavigationButtonBackground2Hot = (Color) this.ColorScheme.TabButtonHotBackground2;
      paintParams.NavigationButtonBorderHot = (Color) this.ColorScheme.TabButtonHotBorder;
      paintParams.NavigationButtonBackground1Active = (Color) this.ColorScheme.TabButtonActiveBackground1;
      paintParams.NavigationButtonBackground2Active = (Color) this.ColorScheme.TabButtonActiveBackground2;
      paintParams.NavigationButtonBorderActive = (Color) this.ColorScheme.TabButtonActiveBorder;
      paintParams.NavigationButtonReplaceWith = (Color) this.ColorScheme.TabButtonText;
      paintParams.NavigationButtonReplaceWithHot = (Color) this.ColorScheme.TabButtonHotText;
      paintParams.NavigationButtonReplaceWithActive = (Color) this.ColorScheme.TabButtonActiveText;
      paintParams.NavigationButtonReplaceWithDisabled = (Color) this.ColorScheme.TabButtonTextDisabled;
      paintParams.NavigationAreaBackground1 = (Color) this.ColorScheme.TabStripNavigationAreaBackground1;
      paintParams.NavigationAreaBackground2 = (Color) this.ColorScheme.TabStripNavigationAreaBackground2;
      paintParams.NavigationAreaBorder = (Color) this.ColorScheme.TabStripNavigationAreaBorder;
    }

    protected virtual void UpdateTabControlPaintParams(QTabControlPaintParams paintParams)
    {
      paintParams.ContentBackground1 = (Color) this.ColorScheme.TabControlContentBackground1;
      paintParams.ContentBackground2 = (Color) this.ColorScheme.TabControlContentBackground2;
      paintParams.ContentBorder = (Color) this.ColorScheme.TabControlContentBorder;
      paintParams.ContentShade = (Color) this.ColorScheme.TabControlContentShade;
    }

    private QTabControlPaintParams RetrieveTabControlPaintParams()
    {
      this.UpdateTabControlPaintParams(this.m_oTabControlPaintParams);
      return this.m_oTabControlPaintParams;
    }

    void IQTabStripHost.StartAnimateTimer(QTabStrip sender)
    {
      if (this.m_aTabStripsListeningToTimer == null)
        this.m_aTabStripsListeningToTimer = new ArrayList();
      if (!this.m_aTabStripsListeningToTimer.Contains((object) sender))
        this.m_aTabStripsListeningToTimer.Add((object) sender);
      if (this.m_aTabStripsListeningToTimer.Count != 1)
        return;
      this.StartTimer(17, this.Configuration.AnimationInterval);
      sender.HandleAnimateTimerTick();
    }

    void IQTabStripHost.StopAnimateTimer(QTabStrip sender)
    {
      if (this.m_aTabStripsListeningToTimer == null)
        return;
      this.m_aTabStripsListeningToTimer.Remove((object) sender);
      if (this.m_aTabStripsListeningToTimer.Count != 0)
        return;
      this.StopTimer(17);
    }

    QTabStripPaintParams IQTabStripHost.RetrieveTabStripPaintParams()
    {
      this.UpdateTabStripPaintParams(this.m_oTabStripPaintParams);
      return this.m_oTabStripPaintParams;
    }

    void IQTabStripHost.HandleTabStripUiRequest(
      QTabStrip sender,
      QCommandUIRequest request,
      Rectangle invalidateBounds)
    {
      if (this.IsDisposed)
        return;
      switch (request)
      {
        case QCommandUIRequest.PerformLayout:
          this.m_oUpdateRequestedFrom = sender;
          this.PerformLayout();
          this.Refresh();
          this.m_oUpdateRequestedFrom = (QTabStrip) null;
          break;
        case QCommandUIRequest.Redraw:
          Rectangle bounds = sender.Bounds;
          int num = (int) Math.Ceiling((double) sender.Configuration.Appearance.BorderWidth / 2.0);
          bounds.Inflate(num, num);
          Rectangle rc = invalidateBounds != Rectangle.Empty ? Rectangle.Intersect(bounds, invalidateBounds) : bounds;
          if (this.m_bSuspendingRedraw)
            break;
          this.Invalidate(rc, false);
          break;
      }
    }

    private class QTranslucentTabButtonWindow : QTranslucentWindow
    {
      private Point m_oLocationOffset = Point.Empty;

      protected override void SetVisibleCore(bool value)
      {
        if (value)
          NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0, 339U);
        base.SetVisibleCore(value);
        QControlHelper.UpdateControlRoot((Control) this);
      }

      internal Point LocationOffset
      {
        get => this.m_oLocationOffset;
        set => this.m_oLocationOffset = value;
      }

      internal void SetLocation(Point location)
      {
        location.Offset(-this.m_oLocationOffset.X, -this.m_oLocationOffset.Y);
        this.Location = location;
      }

      protected override CreateParams CreateParams
      {
        get
        {
          CreateParams createParams = base.CreateParams;
          createParams.ExStyle |= 32;
          return createParams;
        }
      }
    }
  }
}
