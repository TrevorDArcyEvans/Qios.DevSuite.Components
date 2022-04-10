// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDockControl
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  public abstract class QDockControl : QContainerControl, IQTabButtonSource, IQPersistableObject
  {
    private QWeakEventConsumerCollection m_oEventConsumers;
    private QTabButtonPaintParams m_oTabButtonPaintParams;
    private QTabButton m_oTabButton;
    private Guid m_oPersistGuid = Guid.NewGuid();
    private bool m_bPersistObject = true;
    private bool m_bCreateNew;
    private bool m_bIsPersisted;
    private Region m_oCurrentRegion;
    private Icon m_oIcon;
    private bool m_bUseControlBackgroundForTabButton = true;
    private bool m_bCanClose = true;
    private bool m_bCanDockTop = true;
    private bool m_bCanDockLeft = true;
    private bool m_bCanDockRight = true;
    private bool m_bCanDockBottom = true;
    private bool m_bCanDockOnFormBorder;
    private bool m_bCanDockOnlyNearDockBar = true;
    private bool m_bCanDockOnOtherControlLeft = true;
    private bool m_bCanDockOnOtherControlRight = true;
    private bool m_bCanDockOnOtherControlTop = true;
    private bool m_bCanDockOnOtherControlBottom = true;
    private bool m_bCanDockOnOtherControlTabbed = true;
    private QDragRectangleType m_eDragRectangleType;
    private QDockPointCollection m_aDockPoints;
    private QDockBar m_oDockBar;
    private Form m_oOwner;
    private bool m_bVisibleWhenParentVisible = true;
    private bool m_bRealTimeDocking;
    private int m_iMouseMoveBeforeDrag = 10;
    private QDockPosition m_eDockPosition;
    private QDockPoint m_oCurrentDockPoint;
    private Point m_oDragStartMousePosition = Point.Empty;
    private Control m_oCurrentTopLevelControl;
    private QDockForm m_oDockForm;
    private Size m_oDockedSize = Size.Empty;
    private SizeF m_oRequestedSize = SizeF.Empty;
    private QDockControl.WindowAction m_eCurrentAction;
    private QDockControl.QWindowDockStyle m_eWindowDockStyle;
    private EventHandler m_oCurrentTopLevelControlEnterHandler;
    private EventHandler m_oCurrentTopLevelControlLeaveHandler;
    private CancelEventHandler m_oOwnerClosingHandler;
    private EventHandler m_oOwnerClosedHandler;
    private QWeakDelegate m_oIconChangedDelegate;
    private QWeakDelegate m_oClosedDelegate;
    private QWeakDelegate m_oClosingDelegate;
    private QWeakDelegate m_oChildControlsChangedDelegate;

    protected QDockControl()
    {
      this.SuspendLayout();
      this.PutWindowDockStyle(QDockControl.QWindowDockStyle.None);
      this.m_aDockPoints = new QDockPointCollection();
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oCurrentTopLevelControlEnterHandler = new EventHandler(this.CurrentTopLevelControl_Enter);
      this.m_oCurrentTopLevelControlLeaveHandler = new EventHandler(this.CurrentTopLevelControl_Leave);
      this.m_oOwnerClosingHandler = new CancelEventHandler(this.Owner_Closing);
      this.m_oOwnerClosedHandler = new EventHandler(this.Owner_Closed);
      this.ResumeLayout(false);
    }

    [QWeakEvent]
    [Category("QChangeEvents")]
    [Description("Gets raised when the Icon changes.")]
    public event EventHandler IconChanged
    {
      add => this.m_oIconChangedDelegate = QWeakDelegate.Combine(this.m_oIconChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oIconChangedDelegate = QWeakDelegate.Remove(this.m_oIconChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the form gets closed")]
    public event EventHandler Closed
    {
      add => this.m_oClosedDelegate = QWeakDelegate.Combine(this.m_oClosedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oClosedDelegate = QWeakDelegate.Remove(this.m_oClosedDelegate, (Delegate) value);
    }

    [Description("Gets raised before the Form gets Closed. This event can be canceled")]
    [QWeakEvent]
    [Category("QEvents")]
    public event CancelEventHandler Closing
    {
      add => this.m_oClosingDelegate = QWeakDelegate.Combine(this.m_oClosingDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oClosingDelegate = QWeakDelegate.Remove(this.m_oClosingDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when child controls are added or removed to this QDockControl or to a Child QDockControl")]
    public event EventHandler ChildControlsChanged
    {
      add => this.m_oChildControlsChangedDelegate = QWeakDelegate.Combine(this.m_oChildControlsChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oChildControlsChangedDelegate = QWeakDelegate.Remove(this.m_oChildControlsChangedDelegate, (Delegate) value);
    }

    [Category("QPersistence")]
    [Description("Gets or sets the PersistGuid. With this Guid the control is identified in the persistence files.")]
    public Guid PersistGuid
    {
      get => this.m_oPersistGuid;
      set => this.m_oPersistGuid = value;
    }

    [Category("QPersistence")]
    [DefaultValue(true)]
    [Description("Gets or sets whether this object must be persisted.")]
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
    public bool RequiresUnload => true;

    [Category("QPersistence")]
    [Description("Gets or sets whether a new instance of this PersistableObject must be created when it is loaded from file. If this is false the persistableObject must match an existing persistableObject in the QPersistenceManager.PersistableObjects collection.")]
    [DefaultValue(false)]
    public virtual bool CreateNew
    {
      get => this.m_bCreateNew;
      set => this.m_bCreateNew = value;
    }

    string IQPersistableObject.Name
    {
      get => base.Name;
      set => base.Name = value;
    }

    public new string Name
    {
      get => base.Name;
      set => base.Name = value;
    }

    [Category("QBehavior")]
    [DefaultValue(true)]
    [Description("Indicates if this control can close")]
    public virtual bool CanClose
    {
      get => this.m_bCanClose;
      set
      {
        this.m_bCanClose = value;
        this.PerformLayout();
        this.PerformNonClientAreaLayout();
      }
    }

    [Description("Indicates if this control can dock on the topside of the Owner Form")]
    [Category("QBehavior")]
    [DefaultValue(true)]
    public virtual bool CanDockTop
    {
      get => this.m_bCanDockTop;
      set => this.m_bCanDockTop = value;
    }

    [Description("Indicates if this control can dock on the leftside of the Owner Form")]
    [Category("QBehavior")]
    [DefaultValue(true)]
    public virtual bool CanDockLeft
    {
      get => this.m_bCanDockLeft;
      set => this.m_bCanDockLeft = value;
    }

    [Category("QBehavior")]
    [Description("Indicates if this control can dock on the rightside of the Owner Form")]
    [DefaultValue(true)]
    public virtual bool CanDockRight
    {
      get => this.m_bCanDockRight;
      set => this.m_bCanDockRight = value;
    }

    [Description("Indicates if this control can dock on the bottomside of the Owner Form")]
    [Category("QBehavior")]
    [DefaultValue(true)]
    public virtual bool CanDockBottom
    {
      get => this.m_bCanDockBottom;
      set => this.m_bCanDockBottom = value;
    }

    [Category("QBehavior")]
    [Description("Indicates if this control can dock on border of the Owner Form")]
    [DefaultValue(false)]
    public virtual bool CanDockOnFormBorder
    {
      get => this.m_bCanDockOnFormBorder;
      set => this.m_bCanDockOnFormBorder = value;
    }

    [Description("Indicates that a this control can only dock near other QDockingWindows and the QDockBar")]
    [Category("QBehavior")]
    [DefaultValue(true)]
    public virtual bool CanDockOnlyNearDockBar
    {
      get => this.m_bCanDockOnlyNearDockBar;
      set => this.m_bCanDockOnlyNearDockBar = value;
    }

    [Category("QBehavior")]
    [Description("Indicates if this control can dock on the leftside of an other Control")]
    [DefaultValue(true)]
    public virtual bool CanDockOnOtherControlLeft
    {
      get => this.m_bCanDockOnOtherControlLeft;
      set => this.m_bCanDockOnOtherControlLeft = value;
    }

    [Category("QBehavior")]
    [Description("Indicates if this control can dock on the rightside of an other Control")]
    [DefaultValue(true)]
    public virtual bool CanDockOnOtherControlRight
    {
      get => this.m_bCanDockOnOtherControlRight;
      set => this.m_bCanDockOnOtherControlRight = value;
    }

    [Description("Indicates if this control can dock on the topside of an other Control")]
    [Category("QBehavior")]
    [DefaultValue(true)]
    public virtual bool CanDockOnOtherControlTop
    {
      get => this.m_bCanDockOnOtherControlTop;
      set => this.m_bCanDockOnOtherControlTop = value;
    }

    [Category("QBehavior")]
    [Description("Indicates if this control can dock on the bottomside of an other Control")]
    [DefaultValue(true)]
    public virtual bool CanDockOnOtherControlBottom
    {
      get => this.m_bCanDockOnOtherControlBottom;
      set => this.m_bCanDockOnOtherControlBottom = value;
    }

    [Description("Indicates if this control can dock in a Tabbed way on an other Control")]
    [Category("QBehavior")]
    [DefaultValue(true)]
    public virtual bool CanDockOnOtherControlTabbed
    {
      get => this.m_bCanDockOnOtherControlTabbed;
      set => this.m_bCanDockOnOtherControlTabbed = value;
    }

    [Description("Gets or sets the type of DragRectangle that is drawn when the control is dragged")]
    [Category("QAppearance")]
    [DefaultValue(QDragRectangleType.TransparentForm)]
    public virtual QDragRectangleType DragRectangleType
    {
      get => this.m_eDragRectangleType;
      set => this.m_eDragRectangleType = value;
    }

    [DefaultValue(null)]
    [Category("QAppearance")]
    [Description("Is used to get or set an icon.")]
    public virtual Icon Icon
    {
      get => this.m_oIcon;
      set
      {
        this.PutIcon(value);
        if (this.TabButtonCreated)
          this.TabButton.Icon = value;
        this.OnIconChanged(EventArgs.Empty);
      }
    }

    private void PutIcon(Icon value)
    {
      if (value != null)
        this.m_oIcon = new Icon(value, this.IconSize);
      else
        this.m_oIcon = (Icon) null;
    }

    [Category("QAppearance")]
    [DefaultValue(true)]
    [Description("Indicates if the background of this control should be used as background for the TabButton when the TabButton is painted. This makes sure the TabButton seems part of the control.")]
    public bool UseControlBackgroundForTabButton
    {
      get => this.m_bUseControlBackgroundForTabButton;
      set
      {
        this.m_bUseControlBackgroundForTabButton = value;
        if (!this.TabButtonCreated)
          return;
        this.TabButton.Configuration.AppearanceActive.UseControlBackgroundForTabButton = value;
      }
    }

    [DefaultValue(typeof (Size), "16,16")]
    [Category("QAppearance")]
    [Description("Gets or sets the Size of the Icon")]
    public Size IconSize
    {
      get => this.TabButtonCreated ? this.TabButton.Configuration.IconSize : new Size(16, 16);
      set
      {
        this.TabButton.Configuration.IconSize = value;
        this.Icon = this.Icon;
      }
    }

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QDockAppearance();

    [Description("Gets the QAppearance.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QDockAppearance Appearance => (QDockAppearance) base.Appearance;

    public override string Text
    {
      get => base.Text;
      set
      {
        base.Text = value;
        if (this.TabButtonCreated)
          this.TabButton.Text = value;
        this.RefreshNoClientArea();
      }
    }

    [Browsable(false)]
    public virtual Form Owner
    {
      get => this.m_oOwner;
      set
      {
        if (this.m_oOwner == value)
          return;
        if (this.m_oOwner != null)
        {
          this.m_oEventConsumers.DetachAndRemove((Delegate) this.m_oOwnerClosingHandler);
          this.m_oEventConsumers.DetachAndRemove((Delegate) this.m_oOwnerClosedHandler);
        }
        this.m_oOwner = value;
        if (this.m_oOwner == null)
          return;
        this.m_oEventConsumers.Add((QWeakEventConsumer) new QWeakCancelEventConsumer((Delegate) this.m_oOwnerClosingHandler, (object) this.m_oOwner, "Closing"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) this.m_oOwnerClosedHandler, (object) this.m_oOwner, "Closed"));
      }
    }

    public QDockControl.QWindowDockStyle WindowDockStyle => this.m_eWindowDockStyle;

    internal void PutWindowDockStyle(QDockControl.QWindowDockStyle value) => this.m_eWindowDockStyle = value;

    [Browsable(false)]
    public virtual QDockPosition DockPosition
    {
      get => this.m_eDockPosition;
      set => this.m_eDockPosition = value;
    }

    [Browsable(false)]
    public bool VisibleWhenParentVisible => this.m_bVisibleWhenParentVisible;

    [Browsable(false)]
    public bool IsIdle => this.CurrentAction == QDockControl.WindowAction.None;

    [Browsable(false)]
    public bool IsDocked => this.m_eWindowDockStyle == QDockControl.QWindowDockStyle.Docked;

    [Browsable(false)]
    public bool IsDockedOnDockBar => this.m_eWindowDockStyle == QDockControl.QWindowDockStyle.Docked && this.DockBar != null;

    [Browsable(false)]
    public bool IsInSlideMode => this.m_eWindowDockStyle == QDockControl.QWindowDockStyle.SlideMode;

    [Browsable(false)]
    public bool IsUndocked => this.m_eWindowDockStyle == QDockControl.QWindowDockStyle.Undocked;

    [Browsable(false)]
    public bool IsDragging => this.CurrentAction == QDockControl.WindowAction.Dragging;

    [Browsable(false)]
    public bool IsPreparingForDragging => this.CurrentAction == QDockControl.WindowAction.PrepareDragging;

    [Browsable(false)]
    public bool IsActivated => this.ContainsFocus && this.ParentForm != null && this.ParentForm.Handle == NativeMethods.GetForegroundWindow();

    [Browsable(false)]
    public virtual QDockingWindow CurrentWindow => (QDockingWindow) null;

    private void SecureTabButton()
    {
      if (this.m_oTabButton != null)
        return;
      this.m_oTabButton = new QTabButton((IQTabButtonSource) this);
      this.m_oTabButton.Text = this.Text;
      this.m_oTabButton.Icon = this.Icon;
      this.m_oTabButton.Visible = this.Visible;
      this.m_oTabButton.Enabled = this.Enabled;
      this.m_oTabButton.Configuration.AppearanceActive.UseControlBackgroundForTabButton = this.m_bUseControlBackgroundForTabButton;
    }

    internal bool TabButtonCreated => this.m_oTabButton != null;

    internal QTabButton TabButton
    {
      get
      {
        this.SecureTabButton();
        return this.m_oTabButton;
      }
    }

    public virtual QDockContainer CreateDockContainerInstance(
      QDockControl creatingControl,
      Size size,
      int controlIndex,
      QDockPosition dockPosition,
      Control parent,
      QDockOrientation orientation,
      bool doLayout)
    {
      return new QDockContainer(creatingControl, size, controlIndex, dockPosition, parent, orientation, doLayout);
    }

    [Browsable(false)]
    public QDockContainer DockContainer => !(this.Parent is QDockContainer) ? (QDockContainer) null : (QDockContainer) this.Parent;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QDockBar DockBar => this.m_oDockBar;

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void PutDockBar(QDockBar dockBar)
    {
      if (this.m_oDockBar != null)
        this.m_oDockBar.CurrentDockControls.Remove(this);
      this.m_oDockBar = dockBar;
      if (this.m_oDockBar != null)
        this.m_oDockBar.CurrentDockControls.Add(this);
      this.RefreshNoClientArea();
    }

    internal void GetCurrentDockingWindows(QDockControlCollection collectionToFill)
    {
      switch (this)
      {
        case QDockingWindow _:
          if (collectionToFill.Contains(this))
            break;
          collectionToFill.Add(this);
          break;
        case QDockContainer _:
          for (int index = 0; index < this.Controls.Count; ++index)
          {
            QDockingWindow control1 = this.Controls[index] as QDockingWindow;
            QDockContainer control2 = this.Controls[index] as QDockContainer;
            if (control1 != null)
            {
              if (!collectionToFill.Contains((QDockControl) control1))
                collectionToFill.Add((QDockControl) control1);
            }
            else
              control2?.GetCurrentDockingWindows(collectionToFill);
          }
          break;
      }
    }

    [Browsable(false)]
    internal bool IsOnDockContainer => this.DockContainer != null;

    [Browsable(false)]
    internal bool IsOnDockForm => this.ParentForm is QDockForm;

    [Browsable(false)]
    internal bool IsOnDockContainerTabbed => this.IsOnDockContainer && this.DockContainer.Orientation == QDockOrientation.Tabbed;

    [Browsable(false)]
    internal bool IsOnDockContainerVertical => this.IsOnDockContainer && this.DockContainer.Orientation == QDockOrientation.Vertical;

    [Browsable(false)]
    internal bool IsOnDockContainerHorizontal => this.IsOnDockContainer && this.DockContainer.Orientation == QDockOrientation.Horizontal;

    [Browsable(false)]
    internal Point DragStartMousePosition
    {
      get => this.m_oDragStartMousePosition;
      set => this.m_oDragStartMousePosition = value;
    }

    internal Size DockedSize
    {
      get
      {
        Size minimumSize = this.MinimumSize;
        if (this.m_oDockedSize.Width < minimumSize.Width || this.m_oDockedSize.Height < minimumSize.Height)
          this.m_oDockedSize = new Size(Math.Max(minimumSize.Width, this.m_oDockedSize.Width), Math.Max(minimumSize.Height, this.m_oDockedSize.Height));
        return this.m_oDockedSize;
      }
      set => this.m_oDockedSize = value;
    }

    internal float RequestedWidth
    {
      get => this.m_oRequestedSize.Width;
      set => this.m_oRequestedSize.Width = value;
    }

    internal float RequestedHeight
    {
      get => this.m_oRequestedSize.Height;
      set => this.m_oRequestedSize.Height = value;
    }

    internal SizeF RequestedSize
    {
      get => this.m_oRequestedSize;
      set => this.m_oRequestedSize = value;
    }

    internal QDockForm DockForm
    {
      get => this.m_oDockForm;
      set => this.m_oDockForm = value;
    }

    internal Size DockFormSize => this.m_oDockedSize;

    internal QDockControl.WindowAction CurrentAction
    {
      get => this.m_eCurrentAction;
      set => this.m_eCurrentAction = value;
    }

    public abstract bool MustBePersistedAfter(IQPersistableObject persistableObject);

    public abstract IXPathNavigable SavePersistableObject(
      QPersistenceManager manager,
      IXPathNavigable parentElement);

    public abstract bool LoadPersistableObject(
      QPersistenceManager manager,
      IXPathNavigable persistableObjectElement,
      object parentState);

    public virtual void UnloadPersistableObject()
    {
      this.ClearParent();
      this.PutWindowDockStyle(QDockControl.QWindowDockStyle.None);
      this.PutDockBar((QDockBar) null);
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual bool NoControlCanClose()
    {
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QDockControl && !((QDockControl) this.Controls[index]).NoControlCanClose())
          return false;
      }
      return true;
    }

    internal bool RaiseOnClosing(bool raiseClosingForChildControls)
    {
      if (this.IsDisposed)
        return true;
      if (raiseClosingForChildControls)
      {
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          if (this.Controls[index] is QDockControl && !((QDockControl) this.Controls[index]).RaiseOnClosing(raiseClosingForChildControls))
            return false;
        }
      }
      CancelEventArgs e = new CancelEventArgs(false);
      this.OnClosing(e);
      return !e.Cancel;
    }

    internal void RaiseOnClosed(bool raiseClosedForChildControls)
    {
      if (this.IsDisposed)
        return;
      if (raiseClosedForChildControls)
      {
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          if (this.Controls[index] is QDockControl)
            ((QDockControl) this.Controls[index]).RaiseOnClosed(raiseClosedForChildControls);
        }
      }
      this.OnClosed(EventArgs.Empty);
    }

    internal bool CloseControl(bool raiseClosing)
    {
      if (!this.CanClose || raiseClosing && !this.RaiseOnClosing(true))
        return false;
      bool flag = true;
      for (int index = this.Controls.Count - 1; index >= 0; --index)
      {
        if (this.Controls[index] is QDockControl && !((QDockControl) this.Controls[index]).CloseControl(false))
          flag = false;
      }
      if (flag)
      {
        this.ClearParent();
        this.PutWindowDockStyle(QDockControl.QWindowDockStyle.None);
        this.PutDockBar((QDockBar) null);
        this.RaiseOnClosed(false);
        this.Dispose();
      }
      return flag;
    }

    internal void SetParent(
      Control parent,
      int controlIndex,
      DockStyle dockStyle,
      Size size,
      bool doLayout)
    {
      this.SetParent(parent, controlIndex, dockStyle, new Rectangle(this.Location, size), doLayout);
    }

    internal void SetParent(
      Control parent,
      int controlIndex,
      DockStyle dockStyle,
      Rectangle bounds,
      bool doLayout)
    {
      this.SuspendLayout();
      Control activeControl = this.ActiveControl;
      this.Parent = (Control) null;
      this.ClearRegion(false);
      this.Size = new Size(0, 0);
      this.Dock = dockStyle;
      this.Parent = parent;
      if (this.Parent != null)
      {
        if (controlIndex >= 0)
          this.ControlIndex = controlIndex;
        this.SetCanSizePropertiesOnOrientation();
        this.CurrentTopLevelControl = this.Parent.TopLevelControl != null ? this.Parent.TopLevelControl : this.Parent;
        if (this.Dock == DockStyle.None)
          this.SetBounds(bounds.Left, bounds.Top, bounds.Width, bounds.Height);
        else
          this.Size = bounds.Size;
        activeControl?.Select();
        this.PerformNonClientAreaLayout(true);
        this.RefreshNoClientArea(true);
      }
      else
        this.CurrentTopLevelControl = (Control) null;
      this.ResumeLayout(true);
    }

    internal void ClearParent() => this.SetParent((Control) null, this.ControlIndex, this.Dock, this.Size, false);

    internal static DockStyle FromDockPosition(QDockPosition dockPosition)
    {
      switch (dockPosition)
      {
        case QDockPosition.Top:
          return DockStyle.Top;
        case QDockPosition.Left:
          return DockStyle.Left;
        case QDockPosition.Bottom:
          return DockStyle.Bottom;
        case QDockPosition.Right:
          return DockStyle.Right;
        default:
          return DockStyle.None;
      }
    }

    internal static QDockPosition FromDockStyle(DockStyle dockStyle)
    {
      switch (dockStyle)
      {
        case DockStyle.Top:
          return QDockPosition.Top;
        case DockStyle.Bottom:
          return QDockPosition.Bottom;
        case DockStyle.Left:
          return QDockPosition.Left;
        case DockStyle.Right:
          return QDockPosition.Right;
        default:
          return QDockPosition.None;
      }
    }

    internal Control CurrentTopLevelControl
    {
      get => this.m_oCurrentTopLevelControl;
      set
      {
        if (this.m_oCurrentTopLevelControl == value)
          return;
        if (this.CurrentTopLevelControl != null)
        {
          if (this.m_oCurrentTopLevelControl is Form)
          {
            this.m_oEventConsumers.DetachAndRemove((Delegate) this.m_oCurrentTopLevelControlEnterHandler);
            this.m_oEventConsumers.DetachAndRemove((Delegate) this.m_oCurrentTopLevelControlLeaveHandler);
          }
          else
          {
            this.m_oEventConsumers.DetachAndRemove((Delegate) this.m_oCurrentTopLevelControlEnterHandler);
            this.m_oEventConsumers.DetachAndRemove((Delegate) this.m_oCurrentTopLevelControlLeaveHandler);
          }
        }
        this.m_oCurrentTopLevelControl = value;
        if (this.CurrentTopLevelControl != null)
        {
          if (this.CurrentTopLevelControl is Form)
          {
            this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) this.m_oCurrentTopLevelControlEnterHandler, (object) this.CurrentTopLevelControl, "Activated"));
            this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) this.m_oCurrentTopLevelControlLeaveHandler, (object) this.CurrentTopLevelControl, "Deactivate"));
          }
          else
          {
            this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) this.m_oCurrentTopLevelControlEnterHandler, (object) this.CurrentTopLevelControl, "Enter"));
            this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) this.m_oCurrentTopLevelControlLeaveHandler, (object) this.CurrentTopLevelControl, "Leave"));
          }
        }
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          if (this.Controls[index] is QDockControl)
            ((QDockControl) this.Controls[index]).CurrentTopLevelControl = this.CurrentTopLevelControl;
        }
      }
    }

    internal void SetRegion(Region region, bool redraw)
    {
      if (this.IsDisposed)
        return;
      if (this.m_oCurrentRegion != null)
      {
        this.m_oCurrentRegion.Dispose();
        this.m_oCurrentRegion = (Region) null;
      }
      this.m_oCurrentRegion = region;
      IntPtr hRgn = IntPtr.Zero;
      if (this.m_oCurrentRegion != null)
      {
        Graphics graphics = this.CreateGraphics();
        hRgn = this.m_oCurrentRegion.GetHrgn(graphics);
        graphics.Dispose();
      }
      NativeMethods.SetWindowRgn(this.Handle, hRgn, redraw ? 1 : 0);
    }

    internal void ClearRegion(bool redraw) => this.SetRegion((Region) null, redraw);

    internal bool ThisOrChildControlContains(QDockControl childControl)
    {
      Control parent = childControl.Parent;
      while (parent != null && parent != this)
        parent = parent.Parent;
      return parent != null;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual QDockContainer GetDockContainerForOrientation(
      QDockControl control,
      QDockOrientation orientation,
      ref int controlIndex)
    {
      if (this.DockContainer != null && (this.DockContainer.Orientation == orientation || this.DockContainer.CanChangeOrientation(control)))
        return this.DockContainer;
      if (this.DockContainer == null || !this.DockContainer.IsTabbed)
        return this.CreateNewParentDockContainer();
      return this.DockContainer.DockContainer != null && this.DockContainer.DockContainer.Orientation == QDockOrientation.None ? this.DockContainer.DockContainer : this.DockContainer.CreateNewChildControlContainer(control);
    }

    private QDockContainer CreateNewParentDockContainer()
    {
      QDockContainer dockContainer = this.DockContainer;
      dockContainer?.SuspendLayout();
      QDockContainer containerInstance = this.CreateDockContainerInstance(this, this.Size, this.ControlIndex, QDockPosition.None, (Control) this.DockContainer, QDockOrientation.None, false);
      containerInstance.SuspendLayout();
      this.SetParent((Control) containerInstance, 0, DockStyle.None, Size.Empty, false);
      containerInstance.ResumeLayout(false);
      dockContainer?.ResumeLayout(false);
      return containerInstance;
    }

    internal Rectangle CalculateDestinationRectangleForThis(
      QDockPoint dockPoint,
      QDockControl control)
    {
      int num1 = 0;
      int num2 = 0;
      int width = Math.Min((int) Math.Ceiling((double) this.Parent.ClientSize.Width / ((double) this.Parent.Controls.Count + 1.0)), control.DockedSize.Width);
      int height = Math.Min((int) Math.Ceiling((double) this.Parent.ClientSize.Height / ((double) this.Parent.Controls.Count + 1.0)), control.DockedSize.Height);
      int num3 = dockPoint.InsertIndex;
      if (num3 < 0)
        num3 = 0;
      if (num3 > this.ControlIndex)
      {
        num1 = this.Width - width;
        num2 = this.Height - height;
      }
      if (dockPoint.DockContainerOrientation == QDockOrientation.Tabbed)
        return this.RectangleToScreen(new Rectangle(-this.ClientAreaMarginLeft, -this.ClientAreaMarginTop, this.Width, this.Height));
      if (dockPoint.DockContainerOrientation == QDockOrientation.Horizontal)
        return this.RectangleToScreen(new Rectangle(num1 - this.ClientAreaMarginLeft, -this.ClientAreaMarginTop, width, this.Height));
      return dockPoint.DockContainerOrientation == QDockOrientation.Vertical || dockPoint.DockContainerOrientation == QDockOrientation.None ? this.RectangleToScreen(new Rectangle(-this.ClientAreaMarginLeft, num2 - this.ClientAreaMarginTop, this.Width, height)) : Rectangle.Empty;
    }

    internal Rectangle CalculateDestinationRectangleForSideDock(QDockPoint dockPoint)
    {
      if (dockPoint.Parent == null)
        return Rectangle.Empty;
      Control parent = dockPoint.Parent;
      Control control = parent.Controls[dockPoint.InsertIndex];
      if (control.Dock != QDockControl.FromDockPosition(dockPoint.DockPosition))
      {
        if (dockPoint.DockPosition == QDockPosition.Left)
          return parent.RectangleToScreen(new Rectangle(parent.ClientRectangle.Left, parent.ClientRectangle.Top, this.DockedSize.Width, parent.ClientRectangle.Height));
        if (dockPoint.DockPosition == QDockPosition.Top)
          return parent.RectangleToScreen(new Rectangle(parent.ClientRectangle.Left, parent.ClientRectangle.Top, parent.ClientRectangle.Width, this.DockedSize.Height));
        if (dockPoint.DockPosition == QDockPosition.Right)
          return parent.RectangleToScreen(new Rectangle(parent.ClientRectangle.Right - this.DockedSize.Width, parent.ClientRectangle.Top, this.DockedSize.Width, parent.ClientRectangle.Height));
        if (dockPoint.DockPosition == QDockPosition.Bottom)
          return parent.RectangleToScreen(new Rectangle(parent.ClientRectangle.Left, parent.ClientRectangle.Bottom - this.DockedSize.Height, parent.ClientRectangle.Width, this.DockedSize.Height));
      }
      else
      {
        if (dockPoint.DockPosition == QDockPosition.Left)
          return parent.RectangleToScreen(new Rectangle(control.Right, control.Top, this.DockedSize.Width, control.Height));
        if (dockPoint.DockPosition == QDockPosition.Top)
          return parent.RectangleToScreen(new Rectangle(control.Left, control.Bottom, control.Width, this.DockedSize.Height));
        if (dockPoint.DockPosition == QDockPosition.Right)
          return parent.RectangleToScreen(new Rectangle(control.Left - this.DockedSize.Width, control.Top, this.DockedSize.Width, control.Height));
        if (dockPoint.DockPosition == QDockPosition.Bottom)
          return parent.RectangleToScreen(new Rectangle(control.Left, control.Top - this.DockedSize.Height, control.Width, this.DockedSize.Height));
      }
      return Rectangle.Empty;
    }

    protected void SetCanSizePropertiesOnOrientation()
    {
      if (this.DockContainer != null)
      {
        if (!this.DockContainer.IsLastVisibleChildControl((Control) this))
        {
          if (this.DockContainer.Orientation == QDockOrientation.Vertical)
            this.SetCanSizeProperties(false, false, false, true);
          else if (this.DockContainer.Orientation == QDockOrientation.Horizontal)
            this.SetCanSizeProperties(false, false, true, false);
          else
            this.SetCanSizeProperties(false, false, false, false);
        }
        else
          this.SetCanSizeProperties(false, false, false, false);
      }
      else if (this.Parent is QDockForm)
        this.SetCanSizeProperties(false, false, false, false);
      else
        this.SetCanSizeProperties(this.DockPosition == QDockPosition.Right, this.DockPosition == QDockPosition.Bottom, this.DockPosition == QDockPosition.Left, this.DockPosition == QDockPosition.Top);
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QDockControl && this.Controls[index].Visible)
          ((QDockControl) this.Controls[index]).SetCanSizePropertiesOnOrientation();
      }
    }

    internal void CacheDockedSize()
    {
      if (this.IsOnDockForm)
      {
        if (this.Size.Width > 0)
          this.m_oDockedSize.Width = this.Size.Width;
        if (this.Size.Height <= 0)
          return;
        this.m_oDockedSize.Height = this.Size.Height;
      }
      else
      {
        if ((this.DockPosition == QDockPosition.Left || this.DockPosition == QDockPosition.Right || this.DockPosition == QDockPosition.None) && this.Size.Width > 0)
          this.m_oDockedSize.Width = this.Size.Width;
        if (this.DockPosition != QDockPosition.Top && this.DockPosition != QDockPosition.Bottom && this.DockPosition != QDockPosition.None || this.Size.Height <= 0)
          return;
        this.m_oDockedSize.Height = this.Size.Height;
      }
    }

    internal void SynchronizeRequestedSize()
    {
      if (this.DockContainer != null && this.DockContainer.IsPerformingLayout)
        return;
      this.m_oRequestedSize = (SizeF) this.Size;
    }

    internal void SecureDockPoints()
    {
      if (this.m_aDockPoints.Count > 0 || this.Owner == null)
        return;
      this.m_aDockPoints.CalculateDockPoints(this.Owner, this);
    }

    internal void ClearDockPoints() => this.m_aDockPoints.Clear();

    private QDockPoint GetPossibleDockParent(Point point)
    {
      if (this.Owner == null)
        return (QDockPoint) null;
      this.SecureDockPoints();
      for (int index = 0; index < this.m_aDockPoints.Count; ++index)
      {
        QDockPoint aDockPoint = this.m_aDockPoints[index];
        if (aDockPoint.DockRectangle.Contains(point))
          return aDockPoint;
      }
      return (QDockPoint) null;
    }

    internal void DockControl(QDockPoint point)
    {
      this.SetToDockPoint(point);
      this.Select();
    }

    private void DrawDragRectangle(Rectangle rectangle) => QDragRectangle.Draw(rectangle, this.Text, this.DragRectangleType, this.Owner, this);

    private static void ClearDragRectangle() => QDragRectangle.ClearRectangle();

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal abstract void ClearCachedMinimumClientSize();

    internal int GetControlIndexWithoutControl(QDockControl control, bool tabbed)
    {
      QDockControl qdockControl = control;
      while (qdockControl != null && qdockControl.Parent != null && qdockControl.Parent != this.Parent && qdockControl.IsSingleChildControl)
        qdockControl = !(qdockControl.Parent is QDockControl) ? (QDockControl) null : (QDockControl) qdockControl.Parent;
      if (qdockControl != null && qdockControl.Parent == this.Parent && qdockControl.ControlIndex < this.ControlIndex)
        return this.ControlIndex - 1;
      return !tabbed && this.DockContainer != null && this.DockContainer.IsTabbed ? this.DockContainer.ControlIndex : this.ControlIndex;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal abstract void UndockControl(int x, int y);

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void ParentSetToDockPoint(QDockPoint point)
    {
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QDockControl)
          ((QDockControl) this.Controls[index]).ParentSetToDockPoint(point);
      }
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void SetToDockPoint(QDockPoint point)
    {
      this.ParentSetToDockPoint(point);
      this.PutDockBar((QDockBar) null);
      this.ClearRegion(false);
      this.PutWindowDockStyle(QDockControl.QWindowDockStyle.Docked);
      this.DockPosition = point.DockPosition;
      if (point.ControlToPlaceOnContainer != null)
      {
        QDockContainer containerForOrientation = point.ControlToPlaceOnContainer.GetDockContainerForOrientation(this, point.DockContainerOrientation, ref point.InsertIndex);
        containerForOrientation.SuspendLayout();
        containerForOrientation.Orientation = point.DockContainerOrientation;
        this.SetParent((Control) containerForOrientation, point.InsertIndex, this.Dock, this.DockedSize, false);
        containerForOrientation.ResumeLayout(false);
        containerForOrientation.PerformLayout((Control) this, "");
      }
      else if (point.DockContainer != null)
      {
        QDockContainer containerForOrientation = point.DockContainer.GetDockContainerForOrientation(this, point.DockContainerOrientation, ref point.InsertIndex);
        containerForOrientation.SuspendLayout();
        containerForOrientation.Orientation = point.DockContainerOrientation;
        this.SetParent((Control) containerForOrientation, point.InsertIndex, this.Dock, this.DockedSize, false);
        containerForOrientation.ResumeLayout(false);
        containerForOrientation.PerformLayout((Control) this, "");
      }
      else if (!(this is QDockContainer))
      {
        QDockContainer containerInstance = this.CreateDockContainerInstance(this, this.DockedSize, point.InsertIndex, point.DockPosition, point.Parent, point.DockContainerOrientation, false);
        containerInstance.SuspendLayout();
        containerInstance.PutDockBar(point.DockBar);
        this.SetParent((Control) containerInstance, 0, this.Dock, this.DockedSize, false);
        containerInstance.ResumeLayout(false);
        containerInstance.PerformLayout((Control) this, "");
      }
      else
      {
        DockStyle dockStyle = QDockControl.FromDockPosition(point.DockPosition);
        if (point.Parent != this.Parent || point.InsertIndex != this.ControlIndex + 1 || dockStyle != this.Dock)
          this.SetParent(point.Parent, point.InsertIndex, dockStyle, this.DockedSize, true);
      }
      this.PutDockBar(point.DockBar);
      this.m_oCurrentDockPoint = point;
      this.Invalidate();
    }

    private void CalculateDragStartPositionInDockForm()
    {
      if (this.DockFormSize.Width < this.m_oDragStartMousePosition.X)
        this.m_oDragStartMousePosition.X = (int) Math.Round((double) this.m_oDragStartMousePosition.X / (double) this.Width * (double) this.DockFormSize.Width);
      if (this.DockFormSize.Height >= this.m_oDragStartMousePosition.Y)
        return;
      this.m_oDragStartMousePosition.Y = this.DockFormSize.Height - 10;
    }

    internal void StartDragging(Point controlMousePosition)
    {
      if (!this.Capture)
        this.Capture = true;
      this.CurrentAction = QDockControl.WindowAction.Dragging;
      this.m_oDragStartMousePosition = controlMousePosition;
      this.ClearDockPoints();
      this.Cursor = Cursors.SizeAll;
    }

    internal bool RealTimeDocking
    {
      get => this.m_bRealTimeDocking;
      set => this.m_bRealTimeDocking = value;
    }

    internal void StopDragging()
    {
      if (this.Capture)
        this.Capture = false;
      this.CurrentAction = QDockControl.WindowAction.None;
      this.Cursor = Cursors.Default;
      if (this.m_bRealTimeDocking)
        return;
      QDockControl.ClearDragRectangle();
    }

    internal void FinishDragging()
    {
      this.StopDragging();
      QDockPoint possibleDockParent = this.GetPossibleDockParent(Control.MousePosition);
      if (possibleDockParent != null)
        this.DockControl(possibleDockParent);
      else
        this.UndockControl(Control.MousePosition.X - this.m_oDragStartMousePosition.X, Control.MousePosition.Y - this.m_oDragStartMousePosition.Y);
    }

    internal void PrepareDragging(Point controlMousePosition)
    {
      if (!this.Capture)
        this.Capture = true;
      this.CurrentAction = QDockControl.WindowAction.PrepareDragging;
      this.DragStartMousePosition = controlMousePosition;
      this.Cursor = Cursors.SizeAll;
    }

    internal void StopPrepareDragging()
    {
      if (this.Capture)
        this.Capture = false;
      this.CurrentAction = QDockControl.WindowAction.None;
      this.Cursor = Cursors.Default;
    }

    internal void DragWindow()
    {
      QDockPoint possibleDockParent = this.GetPossibleDockParent(Control.MousePosition);
      if (this.m_bRealTimeDocking)
      {
        if (possibleDockParent != null)
        {
          if (!this.IsUndocked && this.m_oCurrentDockPoint != null && this.m_oCurrentDockPoint.DockRectangle.Contains(Control.MousePosition))
            return;
          this.DockControl(possibleDockParent);
          this.ClearDockPoints();
        }
        else
        {
          if (this.m_oCurrentDockPoint != null && this.m_oCurrentDockPoint.DockRectangle.Contains(Control.MousePosition))
            return;
          if (this.IsDocked || this.IsInSlideMode)
          {
            this.CalculateDragStartPositionInDockForm();
            this.ClearDockPoints();
          }
          this.UndockControl(Control.MousePosition.X - this.m_oDragStartMousePosition.X, Control.MousePosition.Y - this.m_oDragStartMousePosition.Y);
        }
      }
      else
      {
        Rectangle rectangle = Rectangle.Empty;
        if (possibleDockParent != null && this.DockModifierKeysPressed)
        {
          rectangle = possibleDockParent.ControlToPlaceOnContainer == null ? (possibleDockParent.DockContainer == null ? this.CalculateDestinationRectangleForSideDock(possibleDockParent) : possibleDockParent.DockContainer.CalculateDestinationRectangleForThis(possibleDockParent, this)) : possibleDockParent.ControlToPlaceOnContainer.CalculateDestinationRectangleForThis(possibleDockParent, this);
        }
        else
        {
          this.CalculateDragStartPositionInDockForm();
          rectangle = new Rectangle(Control.MousePosition.X - this.m_oDragStartMousePosition.X, Control.MousePosition.Y - this.m_oDragStartMousePosition.Y, this.DockFormSize.Width, this.DockFormSize.Height);
        }
        this.DrawDragRectangle(rectangle);
      }
    }

    internal virtual bool DockModifierKeysPressed => true;

    [Browsable(false)]
    protected override int ClientAreaMarginLeft => !this.IsOnDockContainer ? this.ResizeBorderWidth : 0;

    [Browsable(false)]
    protected override int ClientAreaMarginTop => !this.IsOnDockContainer ? this.ResizeBorderWidth : 0;

    [Browsable(false)]
    protected override int ClientAreaMarginRight
    {
      get
      {
        if (!this.IsOnDockContainer)
          return this.ResizeBorderWidth;
        return this.IsOnDockContainerHorizontal && !this.IsLastControl ? this.ResizeBorderWidth : 0;
      }
    }

    [Browsable(false)]
    protected override int ClientAreaMarginBottom
    {
      get
      {
        if (!this.IsOnDockContainer)
          return this.ResizeBorderWidth;
        return this.IsOnDockContainerVertical && !this.IsLastControl ? this.ResizeBorderWidth : 0;
      }
    }

    [Browsable(false)]
    internal int NonClientAreaMarginWidth => this.ClientAreaMarginLeft + this.ClientAreaMarginRight;

    [Browsable(false)]
    internal int NonClientAreaMarginHeight => this.ClientAreaMarginTop + this.ClientAreaMarginBottom;

    [Browsable(false)]
    protected override int ResizeBorderWidth => SystemInformation.Border3DSize.Width;

    [Browsable(false)]
    protected override string BorderColorPropertyName => (string) null;

    protected override void OnSizeChanged(EventArgs e)
    {
      this.CacheDockedSize();
      this.SynchronizeRequestedSize();
      base.OnSizeChanged(e);
    }

    protected override void SetVisibleCore(bool value)
    {
      this.m_bVisibleWhenParentVisible = value;
      base.SetVisibleCore(value);
      if (this.DockContainer != null)
        this.DockContainer.HandleChildControlVisibilityChanged(this);
      else if (this.Parent is QDockForm)
        this.Parent.Visible = value;
      QDockingWindow dockControl = this as QDockingWindow;
      if (this.DockBar == null || dockControl == null)
        return;
      this.DockBar.HandleChildControlVisibilityChanged((QDockControl) dockControl);
    }

    protected override void Dispose(bool disposing)
    {
      if (!this.IsDisposed && disposing && this.m_oCurrentRegion != null)
      {
        this.m_oCurrentRegion.Dispose();
        this.m_oCurrentRegion = (Region) null;
      }
      base.Dispose(disposing);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      Point control = this.PointToControl(new Point(e.X, e.Y));
      if (this.IsPreparingForDragging && (!QMath.ValueInMargin(control.X, this.DragStartMousePosition.X, this.m_iMouseMoveBeforeDrag) || !QMath.ValueInMargin(control.Y, this.DragStartMousePosition.Y, this.m_iMouseMoveBeforeDrag)))
        this.StartDragging(this.DragStartMousePosition);
      if (this.IsDragging)
        this.DragWindow();
      base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.IsDragging)
      {
        if (this.m_bRealTimeDocking)
          this.StopDragging();
        else
          this.FinishDragging();
      }
      else if (this.IsPreparingForDragging)
        this.StopPrepareDragging();
      base.OnMouseUp(e);
    }

    protected virtual void OnIconChanged(EventArgs e) => this.m_oIconChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oIconChangedDelegate, (object) this, (object) e);

    protected virtual void OnChildControlsChanged(EventArgs e) => this.m_oChildControlsChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oChildControlsChangedDelegate, (object) this, (object) e);

    protected virtual void OnClosing(CancelEventArgs e) => this.m_oClosingDelegate = QWeakDelegate.InvokeDelegate(this.m_oClosingDelegate, (object) this, (object) e);

    protected virtual void OnClosed(EventArgs e) => this.m_oClosedDelegate = QWeakDelegate.InvokeDelegate(this.m_oClosedDelegate, (object) this, (object) e);

    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      if (!this.TabButtonCreated)
        return;
      this.TabButton.Visible = this.Visible;
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      if (!this.TabButtonCreated)
        return;
      this.TabButton.Enabled = this.Enabled;
    }

    private void CurrentTopLevelControl_Enter(object sender, EventArgs e) => this.RefreshNoClientArea();

    private void CurrentTopLevelControl_Leave(object sender, EventArgs e) => this.RefreshNoClientArea();

    private void Owner_Closing(object sender, CancelEventArgs e)
    {
      if (e.Cancel)
        return;
      e.Cancel = !this.RaiseOnClosing(false);
    }

    private void Owner_Closed(object sender, EventArgs e) => this.RaiseOnClosed(false);

    void IQTabButtonSource.HandleScrollStep()
    {
    }

    Rectangle IQTabButtonSource.GetBoundsForBackgroundFill() => this.Bounds;

    void IQTabButtonSource.DeactivateSource()
    {
    }

    void IQTabButtonSource.ActivateSource() => this.Select();

    QTabButtonPaintParams IQTabButtonSource.RetrieveTabButtonPaintParams()
    {
      if (this.m_oTabButtonPaintParams == null)
        this.m_oTabButtonPaintParams = new QTabButtonPaintParams();
      this.m_oTabButtonPaintParams.ButtonBackground1 = Color.Empty;
      this.m_oTabButtonPaintParams.ButtonBackground2 = Color.Empty;
      this.m_oTabButtonPaintParams.ButtonBorder = (Color) this.ColorScheme.DockingWindowTabButtonBorderNotActive;
      this.m_oTabButtonPaintParams.ButtonText = (Color) this.ColorScheme.DockingWindowTabButtonTextNotActive;
      this.m_oTabButtonPaintParams.ButtonTextDisabled = (Color) this.ColorScheme.DockingWindowTabButtonTextNotActive;
      this.m_oTabButtonPaintParams.ButtonActiveBackground1 = (Color) this.ColorScheme.DockingWindowTabButton1;
      this.m_oTabButtonPaintParams.ButtonActiveBackground2 = (Color) this.ColorScheme.DockingWindowTabButton2;
      this.m_oTabButtonPaintParams.ButtonActiveBorder = (Color) this.ColorScheme.DockingWindowTabButtonBorder;
      this.m_oTabButtonPaintParams.ButtonActiveText = (Color) this.ColorScheme.DockingWindowTabButtonText;
      this.m_oTabButtonPaintParams.ButtonHotBackground1 = Color.Empty;
      this.m_oTabButtonPaintParams.ButtonHotBackground2 = Color.Empty;
      this.m_oTabButtonPaintParams.ButtonHotBorder = (Color) this.ColorScheme.DockingWindowTabButtonBorderNotActive;
      this.m_oTabButtonPaintParams.ButtonHotText = (Color) this.ColorScheme.DockingWindowTabButtonBorderNotActive;
      this.m_oTabButtonPaintParams.IconReplace = Color.FromArgb((int) byte.MaxValue, 0, 0);
      this.m_oTabButtonPaintParams.IconReplaceColorWith = (Color) this.ColorScheme.DockingWindowTabButtonText;
      return this.m_oTabButtonPaintParams;
    }

    public enum QWindowDockStyle
    {
      None,
      Docked,
      SlideMode,
      Undocked,
    }

    internal enum WindowAction
    {
      None,
      PrepareDragging,
      Dragging,
      Sizing,
      Sliding,
    }
  }
}
