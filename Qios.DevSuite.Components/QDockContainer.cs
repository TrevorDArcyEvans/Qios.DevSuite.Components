// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDockContainer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  public class QDockContainer : QDockControl, IContainerControl, IQTabStripHost
  {
    private QWeakEventConsumerCollection m_oEventConsumers;
    private QDockOrientation m_eOrientation;
    private QDockOrientation m_ePreviousOrientation;
    private Size m_oCachedMinimumClientSize = Size.Empty;
    private QTabStrip m_oTabStrip;
    private bool m_bChangingTabButtons;
    private QTabStripPaintParams m_oTabStripPaintParams;
    private int m_iTabStripHeight = 20;
    private QTabButton m_oDraggingButton;
    private Rectangle m_oBoundsBeforeUserSize = Rectangle.Empty;
    private Rectangle m_oBoundsOfLastRepositionedButton;
    private Point m_oMouseDownAtPoint = Point.Empty;
    private QTabButton m_oMouseDownAtButton;
    private int m_iMouseMoveBeforeDrag = 5;
    private bool m_bIsPerformingLayout;
    private bool m_bIsLoadingPersistence;
    private QDockControl m_oCurrentSizingChildControl;
    private EventHandler m_oUserStartsSizingHandler;
    private EventHandler m_oUserEndsSizingHandler;
    private EventHandler m_oChildControlColorsChanged;
    private EventHandler m_oChildControlsChanged;
    private EventHandler m_oChildControlAppearanceChanded;
    private QUserSizingEventHandler m_oUserSizingHandler;
    private QWeakDelegate m_oActiveControlChangedDelegate;
    private QWeakDelegate m_oTabStripHostPropertyChangedDelegate;

    public QDockContainer() => this.InternalConstruct();

    public QDockContainer(
      QDockControl creatingControl,
      Size size,
      int controlIndex,
      QDockPosition dockPosition,
      Control parent,
      QDockOrientation orientation,
      bool doLayout)
    {
      if (creatingControl == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (creatingControl)));
      this.InternalConstruct();
      this.InitializeControl(creatingControl.Owner, size, controlIndex, dockPosition, parent, orientation, doLayout);
    }

    protected override Control.ControlCollection CreateControlsInstance() => (Control.ControlCollection) new QDockContainerControlCollection(this);

    private void InitializeControl(
      Form owner,
      Size size,
      int controlIndex,
      QDockPosition dockPosition,
      Control parent,
      QDockOrientation orientation,
      bool doLayout)
    {
      this.SuspendLayout();
      this.DockPosition = dockPosition;
      this.Owner = owner;
      this.SetParent(parent, controlIndex, QDockControl.FromDockPosition(dockPosition), Size.Empty, false);
      this.SetOrientation(orientation, false);
      this.DockedSize = size;
      this.ResumeLayout(doLayout);
    }

    private void InternalConstruct()
    {
      this.SetQControlStyles(QControlStyles.DoNothingOnUserSize, true);
      this.m_oTabStrip = new QTabStrip((IQTabStripHost) this, this.Font, DockStyle.Bottom);
      this.ConfigureTabStrip(this.m_oTabStrip);
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oUserStartsSizingHandler = new EventHandler(this.ChildControl_UserStartsSizing);
      this.m_oUserEndsSizingHandler = new EventHandler(this.ChildControl_UserEndsSizing);
      this.m_oUserSizingHandler = new QUserSizingEventHandler(this.ChildControl_UserSizing);
      this.m_oChildControlAppearanceChanded = new EventHandler(this.ChildControl_AppearanceChanged);
      this.m_oChildControlColorsChanged = new EventHandler(this.ChildControl_ColorsChanged);
      this.m_oChildControlsChanged = new EventHandler(this.ChildControl_ChildControlsChanged);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    }

    [QWeakEvent]
    public event EventHandler ActiveControlChanged
    {
      add => this.m_oActiveControlChangedDelegate = QWeakDelegate.Combine(this.m_oActiveControlChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oActiveControlChangedDelegate = QWeakDelegate.Remove(this.m_oActiveControlChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    public event EventHandler TabStripHostPropertyChanged
    {
      add => this.m_oTabStripHostPropertyChangedDelegate = QWeakDelegate.Combine(this.m_oTabStripHostPropertyChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oTabStripHostPropertyChangedDelegate = QWeakDelegate.Remove(this.m_oTabStripHostPropertyChangedDelegate, (Delegate) value);
    }

    public override Font Font
    {
      get => base.Font;
      set
      {
        base.Font = value;
        if (this.m_oTabStrip == null)
          return;
        this.m_oTabStrip.Font = value;
      }
    }

    [Description("Gets whether this QDockContainer must be persisted. This depends on the child objects.")]
    [Browsable(false)]
    [Category("QPersistence")]
    public override bool PersistObject
    {
      get => this.PersistChildControlsCount > 0;
      set
      {
      }
    }

    [Browsable(false)]
    public int PersistChildControlsCount
    {
      get
      {
        int childControlsCount = 0;
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          if (this.Controls[index] is IQPersistableObject control && control.PersistObject)
            ++childControlsCount;
        }
        return childControlsCount;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool CreateNew
    {
      get => true;
      set
      {
      }
    }

    [Browsable(false)]
    public override bool CanDockTop
    {
      get => this.CurrentWindow == null ? base.CanDockTop : this.CurrentWindow.CanDockTop;
      set => base.CanDockTop = value;
    }

    [Browsable(false)]
    public override bool CanDockLeft
    {
      get => this.CurrentWindow == null ? base.CanDockLeft : this.CurrentWindow.CanDockLeft;
      set => base.CanDockLeft = value;
    }

    [Browsable(false)]
    public override bool CanDockRight
    {
      get => this.CurrentWindow == null ? base.CanDockRight : this.CurrentWindow.CanDockRight;
      set => base.CanDockRight = value;
    }

    [Browsable(false)]
    public override bool CanDockBottom
    {
      get => this.CurrentWindow == null ? base.CanDockBottom : this.CurrentWindow.CanDockBottom;
      set => base.CanDockRight = value;
    }

    [Browsable(false)]
    public override bool CanDockOnFormBorder
    {
      get => this.CurrentWindow == null ? base.CanDockOnFormBorder : this.CurrentWindow.CanDockOnFormBorder;
      set => base.CanDockOnFormBorder = value;
    }

    [Browsable(false)]
    public override bool CanDockOnlyNearDockBar
    {
      get => this.CurrentWindow == null ? base.CanDockOnlyNearDockBar : this.CurrentWindow.CanDockOnlyNearDockBar;
      set => base.CanDockOnlyNearDockBar = value;
    }

    [Browsable(false)]
    public override bool CanDockOnOtherControlLeft
    {
      get => this.CurrentWindow == null ? base.CanDockOnOtherControlLeft : this.CurrentWindow.CanDockOnOtherControlLeft;
      set => base.CanDockOnOtherControlLeft = value;
    }

    [Browsable(false)]
    public override bool CanDockOnOtherControlRight
    {
      get => this.CurrentWindow == null ? base.CanDockOnOtherControlRight : this.CurrentWindow.CanDockOnOtherControlRight;
      set => base.CanDockOnOtherControlRight = value;
    }

    [Browsable(false)]
    public override bool CanDockOnOtherControlTop
    {
      get => this.CurrentWindow == null ? base.CanDockOnOtherControlTop : this.CurrentWindow.CanDockOnOtherControlTop;
      set => base.CanDockOnOtherControlTop = value;
    }

    [Browsable(false)]
    public override bool CanDockOnOtherControlBottom
    {
      get => this.CurrentWindow == null ? base.CanDockOnOtherControlBottom : this.CurrentWindow.CanDockOnOtherControlBottom;
      set => base.CanDockOnOtherControlBottom = value;
    }

    [Browsable(false)]
    public override bool CanDockOnOtherControlTabbed
    {
      get => this.CurrentWindow == null ? base.CanDockOnOtherControlTabbed : this.CurrentWindow.CanDockOnOtherControlTabbed;
      set => base.CanDockOnOtherControlTabbed = value;
    }

    [Browsable(false)]
    public override QDragRectangleType DragRectangleType
    {
      get => this.CurrentWindow == null ? base.DragRectangleType : this.CurrentWindow.DragRectangleType;
      set => base.DragRectangleType = value;
    }

    public override QColorScheme ColorScheme
    {
      get => this.CurrentWindow == null ? base.ColorScheme : this.CurrentWindow.ColorScheme;
      set => base.ColorScheme = value;
    }

    public override QDockAppearance Appearance => this.CurrentWindow == null ? base.Appearance : (QDockAppearance) this.CurrentWindow.DockContainerAppearance;

    public override string Text
    {
      get => this.CurrentWindow != null ? this.CurrentWindow.Text : base.Text;
      set
      {
        if (this.CurrentWindow != null)
          this.CurrentWindow.Text = value;
        else
          base.Text = value;
      }
    }

    public override Size MinimumClientSize
    {
      get => this.GetMinimumClientSize(true);
      set => base.MinimumClientSize = value;
    }

    public override QDockPosition DockPosition
    {
      get => base.DockPosition;
      set
      {
        base.DockPosition = value;
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          if (this.Controls[index] is QDockControl)
            ((QDockControl) this.Controls[index]).DockPosition = value;
        }
      }
    }

    public override QDockingWindow CurrentWindow
    {
      get
      {
        if (this.ActiveControl is QDockingWindow)
          return (QDockingWindow) this.ActiveControl;
        for (QDockContainer qdockContainer = this; qdockContainer != null && qdockContainer.ActiveControl != null && qdockContainer.ActiveControl is QDockControl; qdockContainer = qdockContainer.ActiveControl as QDockContainer)
        {
          if (qdockContainer.ActiveControl is QDockingWindow)
            return qdockContainer.ActiveControl as QDockingWindow;
        }
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          if (this.Controls[index] is QDockControl)
            return ((QDockControl) this.Controls[index]).CurrentWindow;
        }
        return (QDockingWindow) null;
      }
    }

    public bool UserIsSizingChildControl => this.m_oCurrentSizingChildControl != null;

    public bool UserIsDraggingTabButton => this.m_oDraggingButton != null;

    public QDockOrientation Orientation
    {
      get => this.m_eOrientation;
      set => this.SetOrientation(value, true);
    }

    public bool IsTabbed => this.m_eOrientation == QDockOrientation.Tabbed;

    public override DockStyle Dock
    {
      get => base.Dock;
      set
      {
        if (base.Dock == value)
          return;
        this.SetCanSizePropertiesOnOrientation();
        base.Dock = value;
      }
    }

    [Browsable(false)]
    protected override string BackColorPropertyName => "DockContainerBackground1";

    [Browsable(false)]
    protected override string BackColor2PropertyName => "DockContainerBackground2";

    public override bool MustBePersistedAfter(IQPersistableObject persistableObject)
    {
      if (persistableObject is QDockControl qdockControl)
      {
        if (qdockControl.Parent == this.Parent)
          return this.ControlIndex - qdockControl.ControlIndex > 0;
        if (qdockControl.ThisOrChildControlContains((QDockControl) this))
          return true;
      }
      return false;
    }

    public override IXPathNavigable SavePersistableObject(
      QPersistenceManager manager,
      IXPathNavigable parentElement)
    {
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      QDockContainer qdockContainer = this;
      while (qdockContainer.PersistChildControlsCount == 1 && qdockContainer.Controls[0] is QDockContainer control)
      {
        qdockContainer = control;
        qdockContainer.IsPersisted = true;
      }
      if (qdockContainer.PersistChildControlsCount == 0)
      {
        qdockContainer.IsPersisted = true;
        return (IXPathNavigable) null;
      }
      IXPathNavigable persistableObjectElement = manager.CreatePersistableObjectElement((IQPersistableObject) this, parentElement);
      QXmlHelper.AddElement(persistableObjectElement, "dockBar", this.DockBar != null ? (object) this.DockBar.PersistGuid : (object) null);
      QXmlHelper.AddElement(persistableObjectElement, "dock", (object) this.Dock);
      QXmlHelper.AddElement(persistableObjectElement, "windowDockStyle", (object) this.WindowDockStyle);
      QXmlHelper.AddElement(persistableObjectElement, "dockPosition", (object) this.DockPosition);
      QXmlHelper.AddElement(persistableObjectElement, "controlIndex", (object) this.ControlIndex);
      QXmlHelper.AddElement(persistableObjectElement, "size", (object) this.Size);
      QXmlHelper.AddElement(persistableObjectElement, "dockedSize", (object) this.DockedSize);
      QXmlHelper.AddElement(persistableObjectElement, "visible", (object) this.Visible);
      QXmlHelper.AddElement(persistableObjectElement, "enabled", (object) this.Enabled);
      if (this.IsUndocked)
        QXmlHelper.AddElement(persistableObjectElement, "dockFormBounds", (object) this.DockForm.Bounds);
      QXmlHelper.AddElement(persistableObjectElement, "orientation", (object) qdockContainer.Orientation);
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < manager.PersistableObjects.Count; ++index)
      {
        if (manager.PersistableObjects[index] is QDockControl persistableObject && persistableObject.PersistObject && persistableObject.Parent == qdockContainer && !persistableObject.IsPersisted)
          arrayList.Add((object) persistableObject);
      }
      for (int index = 0; index < arrayList.Count; ++index)
      {
        IQPersistableObject qpersistableObject = arrayList[index] as IQPersistableObject;
        qpersistableObject.SavePersistableObject(manager, persistableObjectElement);
        qpersistableObject.IsPersisted = true;
      }
      return persistableObjectElement;
    }

    public override bool LoadPersistableObject(
      QPersistenceManager manager,
      IXPathNavigable persistableObjectElement,
      object parentState)
    {
      if (persistableObjectElement == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (persistableObjectElement)));
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      this.m_bIsLoadingPersistence = true;
      try
      {
        XPathNodeIterator xpathNodeIterator = persistableObjectElement.CreateNavigator().SelectChildren("persistableObject", "");
        if (xpathNodeIterator.Count == 0)
          return true;
        QDockPoint qdockPoint = new QDockPoint();
        qdockPoint.Parent = parentState as Control;
        if (!QMisc.IsEmpty((object) QXmlHelper.GetChildElementString(persistableObjectElement, "dockBar")))
        {
          Guid childElementGuid = QXmlHelper.GetChildElementGuid(persistableObjectElement, "dockBar");
          qdockPoint.DockBar = manager.GetPersistableHost(childElementGuid, (IQPersistableObject) this) as QDockBar;
          if (qdockPoint.DockBar == null)
            return false;
        }
        if (qdockPoint.Parent == null)
          qdockPoint.Parent = qdockPoint.DockBar == null ? manager.OwnerControl : qdockPoint.DockBar.Parent;
        object childElementEnum1 = (object) QXmlHelper.GetChildElementEnum(persistableObjectElement, "windowDockStyle", typeof (QDockControl.QWindowDockStyle));
        QDockControl.QWindowDockStyle qwindowDockStyle = childElementEnum1 != null ? (QDockControl.QWindowDockStyle) childElementEnum1 : QDockControl.QWindowDockStyle.None;
        Rectangle rectangle = Rectangle.Empty;
        if (qwindowDockStyle == QDockControl.QWindowDockStyle.Undocked)
          rectangle = QXmlHelper.GetChildElementRectangle(persistableObjectElement, "dockFormBounds");
        object childElementEnum2 = (object) QXmlHelper.GetChildElementEnum(persistableObjectElement, "dockPosition", typeof (QDockPosition));
        qdockPoint.DockPosition = childElementEnum2 != null ? (QDockPosition) childElementEnum2 : QDockPosition.None;
        object childElementEnum3 = (object) QXmlHelper.GetChildElementEnum(persistableObjectElement, "orientation", typeof (QDockOrientation));
        qdockPoint.DockContainerOrientation = childElementEnum3 != null ? (QDockOrientation) childElementEnum3 : QDockOrientation.None;
        qdockPoint.InsertIndex = qdockPoint.DockContainer == null ? QXmlHelper.GetChildElementInt(persistableObjectElement, "controlIndex") : -1;
        object childElementEnum4 = (object) QXmlHelper.GetChildElementEnum(persistableObjectElement, "dock", typeof (DockStyle));
        this.Dock = childElementEnum4 != null ? (DockStyle) childElementEnum4 : DockStyle.None;
        this.Visible = QXmlHelper.GetChildElementBool(persistableObjectElement, "visible");
        this.Enabled = QXmlHelper.GetChildElementBool(persistableObjectElement, "enabled");
        this.DockPosition = qdockPoint.DockPosition;
        this.PutDockBar(qdockPoint.DockBar);
        this.SetOrientation(qdockPoint.DockContainerOrientation, false);
        if (this.Owner == null)
          this.Owner = manager.OwnerForm;
        this.Size = QXmlHelper.GetChildElementSize(persistableObjectElement, "size");
        this.DockedSize = QXmlHelper.GetChildElementSize(persistableObjectElement, "dockedSize");
        while (xpathNodeIterator.MoveNext())
        {
          IXPathNavigable navigableFromNavigator = QXmlHelper.GetNavigableFromNavigator(xpathNodeIterator.Current);
          if (navigableFromNavigator != null)
          {
            IQPersistableObject persistableObject = manager.GetPersistableObject(navigableFromNavigator);
            if (persistableObject != null)
            {
              persistableObject.LoadPersistableObject(manager, navigableFromNavigator, (object) this);
              manager.TriggerPersistableObjectLoaded(persistableObject, navigableFromNavigator);
            }
          }
        }
        switch (qwindowDockStyle)
        {
          case QDockControl.QWindowDockStyle.None:
          case QDockControl.QWindowDockStyle.Docked:
            this.PutWindowDockStyle(qwindowDockStyle);
            if (qdockPoint.DockContainer != null)
              qdockPoint.DockContainer.SuspendLayout();
            this.SetParent(qdockPoint.Parent, qdockPoint.InsertIndex, this.Dock, this.Size, false);
            if (qdockPoint.DockContainer != null)
            {
              qdockPoint.DockContainer.ResumeLayout(false);
              qdockPoint.DockContainer.PerformLayout((Control) this, "");
              break;
            }
            break;
          case QDockControl.QWindowDockStyle.Undocked:
            this.UndockControl(rectangle.Left, rectangle.Top);
            break;
        }
      }
      finally
      {
        this.m_bIsLoadingPersistence = false;
      }
      this.PerformLayout();
      return true;
    }

    [Browsable(false)]
    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void PutDockBar(QDockBar dockBar)
    {
      base.PutDockBar(dockBar);
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QDockControl)
          ((QDockControl) this.Controls[index]).PutDockBar(dockBar);
      }
    }

    [Browsable(false)]
    internal bool IsPerformingLayout => this.m_bIsPerformingLayout;

    [Browsable(false)]
    internal bool IsLoadingPersistence => this.m_bIsLoadingPersistence;

    internal QDockControl CurrentSizingChildControl
    {
      get => this.m_oCurrentSizingChildControl;
      set => this.m_oCurrentSizingChildControl = value;
    }

    internal bool CanChangeOrientation(QDockControl forControl)
    {
      if (this.Orientation == QDockOrientation.None || this.Controls.Count <= 1)
        return true;
      if (this.Controls.Count == 2)
      {
        if (this.Controls.Contains((Control) forControl))
          return true;
        if (this.ThisOrChildControlContains(forControl))
        {
          QDockControl qdockControl = forControl;
          while (qdockControl != null && qdockControl.Parent != this && qdockControl.IsSingleChildControl)
            qdockControl = (QDockControl) qdockControl.Parent;
          if (qdockControl != null && qdockControl.Parent == this)
            return true;
        }
      }
      return false;
    }

    internal QTabStrip TabStrip => this.m_oTabStrip;

    private Size GetMinimumClientSize(bool returnZeroWhenParentIsOwner)
    {
      Size minimumClientSize = this.m_oCachedMinimumClientSize;
      if (minimumClientSize == Size.Empty)
      {
        if (this.Controls.Count > 0)
        {
          for (int index = 0; index < this.Controls.Count; ++index)
          {
            if (this.Controls[index] is QDockControl && ((QDockControl) this.Controls[index]).VisibleWhenParentVisible)
            {
              QDockControl control = (QDockControl) this.Controls[index];
              if (this.Orientation == QDockOrientation.Tabbed)
              {
                minimumClientSize.Width = Math.Max(minimumClientSize.Width, control.MinimumSize.Width);
                minimumClientSize.Height = Math.Max(minimumClientSize.Height, control.MinimumSize.Height);
              }
              else if (this.Orientation == QDockOrientation.Horizontal)
              {
                minimumClientSize.Width += control.MinimumSize.Width;
                minimumClientSize.Height = Math.Max(minimumClientSize.Height, control.MinimumSize.Height);
              }
              else if (this.Orientation == QDockOrientation.Vertical || this.Orientation == QDockOrientation.None)
              {
                minimumClientSize.Width = Math.Max(minimumClientSize.Width, control.MinimumSize.Width);
                minimumClientSize.Height += control.MinimumSize.Height;
              }
            }
          }
          if (this.IsTabbed && this.Controls.Count > 1)
          {
            if (this.m_oTabStrip.Bounds.IsEmpty)
            {
              this.m_oTabStrip.CalculateLayout(new Size(this.ClientSize.Width, this.m_iTabStripHeight), true);
              minimumClientSize.Height += this.m_oTabStrip.CalculatedSize.Height;
            }
            else
              minimumClientSize.Height += this.m_oTabStrip.Bounds.Height;
          }
        }
        else
          minimumClientSize = base.MinimumClientSize;
        this.m_oCachedMinimumClientSize = minimumClientSize;
      }
      if (returnZeroWhenParentIsOwner && this.Parent == this.Owner)
      {
        if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
          minimumClientSize.Height = 0;
        else if (this.Dock == DockStyle.Bottom || this.Dock == DockStyle.Top)
          minimumClientSize.Width = 0;
      }
      return minimumClientSize;
    }

    private Size GetTotalControlSize(Control exceptControl)
    {
      Size totalControlSize = new Size(0, 0);
      if (this.Controls.Count > 0)
      {
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          if (this.Controls[index] is QDockControl control && control.VisibleWhenParentVisible && control != exceptControl)
          {
            if (this.Orientation == QDockOrientation.Tabbed)
            {
              totalControlSize.Width = Math.Max(totalControlSize.Width, control.Width);
              totalControlSize.Height = Math.Max(totalControlSize.Height, control.Height);
            }
            else if (this.Orientation == QDockOrientation.Horizontal)
            {
              totalControlSize.Width += control.Width;
              totalControlSize.Height = Math.Max(totalControlSize.Height, control.Height);
            }
            else if (this.Orientation == QDockOrientation.Vertical || this.Orientation == QDockOrientation.None)
            {
              totalControlSize.Width = Math.Max(totalControlSize.Width, control.Width);
              totalControlSize.Height += control.Height;
            }
          }
        }
      }
      totalControlSize.Width = Math.Max(totalControlSize.Width, 1);
      totalControlSize.Height = Math.Max(totalControlSize.Height, 1);
      return totalControlSize;
    }

    internal override bool DockModifierKeysPressed
    {
      get
      {
        QDockControlCollection collectionToFill = new QDockControlCollection();
        this.GetCurrentDockingWindows(collectionToFill);
        for (int index = 0; index < collectionToFill.Count; ++index)
        {
          if (collectionToFill[index] is QDockingWindow qdockingWindow && !qdockingWindow.DockModifierKeysPressed)
            return false;
        }
        return true;
      }
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void SetToDockPoint(QDockPoint point)
    {
      if (point.DockContainer != null && point.DockContainerOrientation == QDockOrientation.Tabbed)
      {
        this.ParentSetToDockPoint(point);
        int insertIndex = point.InsertIndex;
        for (int index = this.Controls.Count - 1; index >= 0; --index)
        {
          QDockPoint point1 = new QDockPoint(point);
          point1.InsertIndex = insertIndex;
          if (this.Controls[index] is QDockControl)
          {
            ((QDockControl) this.Controls[index]).SetToDockPoint(point1);
            ++insertIndex;
          }
        }
        this.ClearParent();
      }
      else
        base.SetToDockPoint(point);
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void UndockControl(int x, int y)
    {
      if (!this.IsUndocked)
      {
        this.ParentSetToDockPoint(new QDockPoint());
        this.PutWindowDockStyle(QDockControl.QWindowDockStyle.Undocked);
        this.PutDockBar((QDockBar) null);
        this.SetCanSizeProperties(false, false, false, false);
        if (this.DockForm == null || this.DockForm.IsDisposed)
          this.DockForm = new QDockForm(this.Owner);
        this.DockForm.InitDockForm(this, x, y);
        this.DockForm.Show();
      }
      else
      {
        if (this.Location.X == x && this.Location.Y == y)
          return;
        this.DockForm.SetBounds(x, y, 0, 0, BoundsSpecified.Location);
      }
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override QDockContainer GetDockContainerForOrientation(
      QDockControl control,
      QDockOrientation orientation,
      ref int controlIndex)
    {
      if (this.Orientation == orientation || this.CanChangeOrientation(control))
        return this;
      return this.DockContainer != null && (this.DockContainer.Orientation == orientation || this.DockContainer.CanChangeOrientation(control)) ? this.DockContainer : this.CreateNewChildControlContainer(control);
    }

    internal QDockContainer CreateNewChildControlContainer(QDockControl forControl)
    {
      Control control = this.ActiveControl != null ? this.ActiveControl : (Control) this.CurrentWindow;
      ArrayList arrayList = new ArrayList();
      if (this.IsTabbed)
      {
        for (int index = 0; index < this.TabStrip.TabButtons.Count; ++index)
        {
          if (this.TabStrip.TabButtons[index].Control is QDockControl && this.TabStrip.TabButtons[index].Control != forControl)
            arrayList.Add((object) this.TabStrip.TabButtons[index].Control);
        }
      }
      else
      {
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          if (this.Controls[index] is QDockControl && this.Controls[index] != forControl)
            arrayList.Add((object) this.Controls[index]);
        }
      }
      this.SuspendLayout();
      QDockContainer containerInstance = this.CreateDockContainerInstance((QDockControl) this, this.Size, 0, this.DockPosition, (Control) this, this.Orientation, false);
      containerInstance.SuspendLayout();
      for (int index = 0; index < arrayList.Count; ++index)
        ((QDockControl) arrayList[index]).SetParent((Control) containerInstance, -1, DockStyle.None, this.Size, false);
      containerInstance.ResumeLayout(false);
      control?.Select();
      this.SetOrientation(QDockOrientation.None, false);
      this.ResumeLayout(false);
      return this;
    }

    Control IContainerControl.ActiveControl
    {
      get => this.ActiveControl;
      set => this.ActiveControl = value;
    }

    public new Control ActiveControl
    {
      get => base.ActiveControl;
      set
      {
        base.ActiveControl = value;
        if (!this.Controls.Contains(value))
          return;
        if (this.IsTabbed)
        {
          this.SetActiveTabButton(this.ActiveControl, false, false, false);
          this.ActiveControl.BringToFront();
        }
        this.OnActiveControlChanged(EventArgs.Empty);
      }
    }

    internal void AddWindowsToDockBar(QDockBar dockBar, QDockingWindow expandedWindow)
    {
      QDockingWindow[] windows = new QDockingWindow[this.Controls.Count];
      if (this.IsTabbed)
      {
        for (int index = 0; index < this.TabStrip.TabButtons.Count; ++index)
          windows[index] = this.TabStrip.TabButtons[index].Control as QDockingWindow;
      }
      else
      {
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          if (this.Controls[index] is QDockingWindow control)
            windows[index] = control;
        }
      }
      dockBar.AddWindows(windows, expandedWindow);
    }

    internal void SetOrientation(QDockOrientation orientation, bool performLayout)
    {
      if (this.m_eOrientation == orientation)
        return;
      this.m_ePreviousOrientation = this.m_eOrientation;
      this.m_eOrientation = orientation;
      if (this.m_eOrientation == QDockOrientation.Tabbed)
        this.FillTabStrip();
      else
        this.ClearTabStrip();
      this.SetCanSizePropertiesOnOrientation();
      if (!performLayout)
        return;
      this.PerformLayout();
    }

    private void FillTabStrip()
    {
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QDockingWindow && ((QDockControl) this.Controls[index]).VisibleWhenParentVisible)
          this.AddTabButton((QDockControl) this.Controls[index]);
      }
    }

    private void ConfigureTabStrip(QTabStrip tabStrip)
    {
      QShape qshape1 = new QShape(QBaseShapeType.SquareTab)
      {
        Items = {
          [0] = {
            LineVisible = false
          },
          [1] = {
            LineVisible = false
          }
        }
      };
      qshape1.ContentBounds = new Rectangle(Point.Empty, qshape1.Size);
      QShape qshape2 = new QShape(QBaseShapeType.SquareTab);
      qshape2.ContentBounds = new Rectangle(Point.Empty, qshape1.Size);
      tabStrip.Configuration.ButtonConfiguration.Appearance.Shape = qshape1;
      tabStrip.Configuration.ButtonConfiguration.AppearanceActive.Shape = qshape2;
      tabStrip.Configuration.SizeBehavior = QTabStripSizeBehaviors.Shrink;
      tabStrip.Configuration.ButtonAreaMargin = new QMargin(5, 3, 0, 5);
      tabStrip.Configuration.ButtonConfiguration.Padding = new QPadding(3, 2, 1, 3);
      tabStrip.Configuration.FontStyleActive = QFontStyle.Regular;
      tabStrip.Configuration.FontStyleHot = QFontStyle.Regular;
      tabStrip.Configuration.ButtonSpacing = -1;
    }

    private void ClearTabStrip() => this.m_oTabStrip.TabButtons.Clear();

    private void AddTabButton(QDockControl control)
    {
      if (!this.IsTabbed)
        return;
      this.m_bChangingTabButtons = true;
      this.m_oTabStrip.TabButtons.Add(control.TabButton, control.ControlIndex, this.ActiveControl == control);
      this.m_bChangingTabButtons = false;
    }

    private void RemoveTabButton(QDockControl control)
    {
      if (this.m_oTabStrip == null || !control.TabButtonCreated)
        return;
      this.m_bChangingTabButtons = true;
      this.m_oTabStrip.TabButtons.Remove(control.TabButton, false);
      this.m_bChangingTabButtons = false;
    }

    private void SetActiveTabButton(
      Control control,
      bool deactivateCurrent,
      bool activateNew,
      bool redraw)
    {
      QDockControl qdockControl = control as QDockControl;
      if (this.m_oTabStrip == null || qdockControl == null)
        return;
      this.m_oTabStrip.SetActiveButton(qdockControl.TabButton, deactivateCurrent, activateNew, redraw);
    }

    internal void HandleChildControlVisibilityChanged(QDockControl control)
    {
      if (control != null)
      {
        if (this.Controls.Contains((Control) control) && control.VisibleWhenParentVisible)
          this.AddTabButton(control);
        else
          this.RemoveTabButton(control);
      }
      int childControlsCount = this.VisibleChildControlsCount;
      if (childControlsCount == 0)
      {
        this.Visible = false;
      }
      else
      {
        this.Visible = true;
        if (childControlsCount == 1)
          this.Orientation = QDockOrientation.None;
        else if (childControlsCount == 2 && this.m_ePreviousOrientation != QDockOrientation.None)
          this.Orientation = this.m_ePreviousOrientation;
        this.SetCanSizePropertiesOnOrientation();
        this.PerformLayout();
        this.Invalidate();
        if (control == null || !this.Controls.Contains((Control) control) || !control.Visible)
          return;
        control.Select();
      }
    }

    private void ChildControl_UserStartsSizing(object sender, EventArgs e) => this.m_oCurrentSizingChildControl = (QDockControl) sender;

    private void ChildControl_UserEndsSizing(object sender, EventArgs e)
    {
      this.PerformLayout();
      this.m_oCurrentSizingChildControl = (QDockControl) null;
    }

    private void ChildControl_UserSizing(object sender, QUserSizingEventArgs e)
    {
      Size newSize = e.NewSize;
      this.CalculateNewSizeForSizingChildControl((QContainerControlBase) this.m_oCurrentSizingChildControl, ref newSize);
      e.NewSize = newSize;
    }

    private void ChildControl_AppearanceChanged(object sender, EventArgs e)
    {
      this.PerformLayout();
      this.RefreshNoClientArea();
      this.OnAppearanceChanged(e);
    }

    private void ChildControl_ColorsChanged(object sender, EventArgs e)
    {
      this.RefreshNoClientArea();
      this.OnColorsChanged(e);
    }

    private void ChildControl_ChildControlsChanged(object sender, EventArgs e) => this.OnChildControlsChanged(e);

    internal bool IsLastVisibleChildControl(Control control)
    {
      int num = this.Controls.IndexOf(control);
      if (num < 0)
        return false;
      for (int index = num + 1; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QDockControl && ((QDockControl) control).VisibleWhenParentVisible)
          return false;
      }
      return true;
    }

    internal int VisibleChildControlsCount
    {
      get
      {
        int childControlsCount = 0;
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          if (this.Controls[index] is QDockControl && ((QDockControl) this.Controls[index]).VisibleWhenParentVisible)
            ++childControlsCount;
        }
        return childControlsCount;
      }
    }

    internal override void ClearCachedMinimumClientSize()
    {
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QDockControl)
          ((QDockControl) this.Controls[index]).ClearCachedMinimumClientSize();
      }
      this.m_oCachedMinimumClientSize = Size.Empty;
    }

    private void ClearCachedObjects() => this.ClearCachedMinimumClientSize();

    private void CalculateNewSizeForSizingChildControl(
      QContainerControlBase control,
      ref Size newSize)
    {
      QContainerControlBase nextSibling = control.NextSibling;
      if (nextSibling == null)
        return;
      if (this.Orientation == QDockOrientation.Horizontal)
      {
        int num = nextSibling.Right - control.Left;
        if (num - newSize.Width >= nextSibling.MinimumSize.Width)
          return;
        newSize.Width = num - nextSibling.MinimumSize.Width - 1;
      }
      else
      {
        if (this.Orientation != QDockOrientation.Vertical)
          return;
        int num = nextSibling.Bottom - control.Top;
        if (num - newSize.Height >= nextSibling.MinimumSize.Height)
          return;
        newSize.Height = num - nextSibling.MinimumSize.Height - 1;
      }
    }

    private void LayoutChildControlsWhenSizingChildControl(QDockControl control)
    {
      QContainerControlBase nextSibling = control.NextSibling;
      if (nextSibling == null)
        return;
      if (this.Orientation == QDockOrientation.Horizontal)
      {
        int width = nextSibling.Right - control.Left - control.Width;
        nextSibling.SetBounds(control.Right, this.ClientRectangle.Top, width, this.ClientRectangle.Height);
      }
      else
      {
        if (this.Orientation != QDockOrientation.Vertical)
          return;
        int height = nextSibling.Bottom - control.Top - control.Height;
        nextSibling.SetBounds(this.ClientRectangle.Left, control.Bottom, this.ClientRectangle.Width, height);
      }
    }

    private void LayoutChildControlsProportional(
      QDockControl changedControl,
      Size newSize,
      bool letChildsDetermineSize)
    {
      if (newSize.Width - (this.ClientAreaMarginLeft + this.ClientAreaMarginRight) <= 0 || newSize.Height - (this.ClientAreaMarginTop + this.ClientAreaMarginBottom) <= 0)
        return;
      uint uFlags = 268;
      Size minimumClientSize1 = this.GetMinimumClientSize(true);
      Size minimumClientSize2 = this.GetMinimumClientSize(false);
      Size size1 = Size.Empty;
      if (letChildsDetermineSize)
      {
        size1 = this.GetTotalControlSize((Control) null);
        if (changedControl != null)
        {
          if (this.Orientation == QDockOrientation.Horizontal)
            size1.Height = changedControl.Height;
          else if (this.Orientation == QDockOrientation.Vertical)
            size1.Width = changedControl.Width;
          else if (this.Orientation == QDockOrientation.Tabbed)
          {
            size1 = changedControl.Size;
            size1.Height += this.m_oTabStrip.Bounds.Height;
          }
          else if (this.Orientation == QDockOrientation.None)
            size1 = changedControl.Size;
        }
      }
      else
        size1 = new Size(newSize.Width - (this.ClientAreaMarginLeft + this.ClientAreaMarginRight), newSize.Height - (this.ClientAreaMarginBottom + this.ClientAreaMarginTop));
      size1.Width = Math.Max(minimumClientSize1.Width, size1.Width);
      size1.Height = Math.Max(minimumClientSize1.Height, size1.Height);
      newSize = new Size(size1.Width + this.ClientAreaMarginLeft + this.ClientAreaMarginRight, size1.Height + this.ClientAreaMarginTop + this.ClientAreaMarginBottom);
      Size size2 = this.CurrentBounds.Size;
      if (size2 != newSize)
      {
        BoundsSpecified specified = BoundsSpecified.None;
        if (newSize.Width != size2.Width)
          specified |= BoundsSpecified.Width;
        if (newSize.Height != size2.Height)
          specified |= BoundsSpecified.Height;
        this.SetBounds(0, 0, newSize.Width, newSize.Height, specified);
        newSize = this.Size;
        size1 = new Size(this.Width - (this.ClientAreaMarginLeft + this.ClientAreaMarginRight), this.Height - (this.ClientAreaMarginBottom + this.ClientAreaMarginTop));
      }
      Size totalControlSize = this.GetTotalControlSize((Control) changedControl);
      if (this.Orientation == QDockOrientation.Tabbed)
      {
        this.m_oTabStrip.CalculateLayout(new Size(size1.Width, this.m_iTabStripHeight), true);
        Size calculatedSize = this.m_oTabStrip.CalculatedSize;
        this.m_oTabStrip.PutBounds(new Rectangle(this.ClientRectangle.Left, size1.Height - calculatedSize.Height, size1.Width, calculatedSize.Height));
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          if (this.Controls[index] is QDockingWindow control)
          {
            NativeMethods.SetWindowPos(control.Handle, IntPtr.Zero, this.ClientRectangle.Left, this.ClientRectangle.Top, size1.Width, size1.Height - this.m_oTabStrip.Bounds.Height, uFlags);
            control.RequestedSize = (SizeF) new Size(size1.Width, size1.Height - this.m_oTabStrip.Bounds.Height);
            control.UpdateBounds();
          }
        }
      }
      else if (this.Orientation == QDockOrientation.Horizontal)
      {
        int iX = 0;
        float a = 0.0f;
        if (changedControl != null)
          a = Math.Min(Math.Max(!letChildsDetermineSize ? Math.Min((float) size1.Width / (float) this.Controls.Count, (float) changedControl.Width) : changedControl.RequestedWidth, (float) changedControl.MinimumSize.Width), (float) (size1.Width - (minimumClientSize2.Width - changedControl.MinimumSize.Width)));
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          if (this.Controls[index] is QDockControl)
          {
            QDockControl control = (QDockControl) this.Controls[index];
            if (control.VisibleWhenParentVisible)
            {
              minimumClientSize2.Width -= control.MinimumSize.Width;
              int val2 = Math.Max(size1.Width - iX - minimumClientSize2.Width, control.MinimumSize.Width);
              float num1 = Math.Max(control.RequestedWidth, 1f);
              float val1;
              if (this.IsLastVisibleChildControl((Control) control))
                val1 = (float) (size1.Width - iX);
              else if (control == changedControl)
              {
                val1 = a;
              }
              else
              {
                int num2 = size1.Width - (int) Math.Round((double) a);
                val1 = num1 / (float) totalControlSize.Width * (float) num2;
              }
              float num3 = Math.Max(Math.Min(val1, (float) val2), (float) control.MinimumSize.Width);
              NativeMethods.SetWindowPos(control.Handle, IntPtr.Zero, iX, this.ClientRectangle.Top, (int) Math.Round((double) num3), size1.Height, uFlags);
              control.RequestedSize = new SizeF(num3, (float) size1.Height);
              control.UpdateBounds();
              iX += control.Bounds.Width;
            }
          }
        }
      }
      else
      {
        if (this.Orientation != QDockOrientation.Vertical && this.Orientation != QDockOrientation.None)
          return;
        int iY = 0;
        float a = 0.0f;
        if (changedControl != null)
          a = Math.Min(Math.Max(!letChildsDetermineSize ? Math.Min((float) size1.Height / (float) this.Controls.Count, (float) changedControl.Height) : changedControl.RequestedHeight, (float) changedControl.MinimumSize.Height), (float) (size1.Height - (minimumClientSize2.Height - changedControl.MinimumSize.Height)));
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          if (this.Controls[index] is QDockControl)
          {
            QDockControl control = (QDockControl) this.Controls[index];
            if (control.VisibleWhenParentVisible)
            {
              minimumClientSize2.Height -= control.MinimumSize.Height;
              int val2 = Math.Max(size1.Height - iY - minimumClientSize2.Height, control.MinimumSize.Height);
              float num4 = Math.Max(control.RequestedHeight, 1f);
              float val1;
              if (this.IsLastVisibleChildControl((Control) control))
                val1 = (float) (size1.Height - iY);
              else if (control == changedControl)
              {
                val1 = a;
              }
              else
              {
                int num5 = size1.Height - (int) Math.Round((double) a);
                val1 = num4 / (float) totalControlSize.Height * (float) num5;
              }
              float num6 = Math.Max(Math.Min(val1, (float) val2), (float) control.MinimumSize.Height);
              NativeMethods.SetWindowPos(control.Handle, IntPtr.Zero, this.ClientRectangle.Left, iY, size1.Width, (int) Math.Round((double) num6), uFlags);
              control.RequestedSize = new SizeF((float) size1.Width, num6);
              control.UpdateBounds();
              iY += control.Bounds.Height;
            }
          }
        }
      }
    }

    private void StartDraggingButton(QTabButton button)
    {
      this.m_oDraggingButton = button;
      this.Cursor = Cursors.SizeAll;
      this.Capture = true;
    }

    private void EndDraggingButton()
    {
      this.m_oBoundsOfLastRepositionedButton = Rectangle.Empty;
      this.m_oDraggingButton = (QTabButton) null;
      this.Cursor = Cursors.Default;
      this.Capture = false;
    }

    internal void HandleControlAdded(Control control)
    {
      if (control is QDockControl control1)
      {
        if (this.Controls.Count == 1 && (this.Height == 0 || this.Width == 0))
          this.Size = this.DockedSize;
        control1.UserStartsSizing += this.m_oUserStartsSizingHandler;
        control1.UserEndsSizing += this.m_oUserEndsSizingHandler;
        control1.UserSizing += this.m_oUserSizingHandler;
        control1.ColorsChanged += this.m_oChildControlColorsChanged;
        control1.AppearanceChanged += this.m_oChildControlAppearanceChanded;
        control1.ChildControlsChanged += this.m_oChildControlsChanged;
        this.HandleChildControlVisibilityChanged(control1);
      }
      this.OnChildControlsChanged(EventArgs.Empty);
    }

    internal void HandleControlRemoved(Control control)
    {
      if (control is QDockControl control1)
      {
        control1.UserStartsSizing -= this.m_oUserStartsSizingHandler;
        control1.UserEndsSizing -= this.m_oUserEndsSizingHandler;
        control1.UserSizing -= this.m_oUserSizingHandler;
        control1.ColorsChanged -= this.m_oChildControlColorsChanged;
        control1.AppearanceChanged -= this.m_oChildControlAppearanceChanded;
        control1.ChildControlsChanged -= this.m_oChildControlsChanged;
        if (this.Controls.Count == 0)
        {
          this.ClearParent();
          this.ClearTabStrip();
          this.Dispose();
        }
        else
        {
          this.HandleChildControlVisibilityChanged(control1);
          this.PerformLayout();
          this.Invalidate();
        }
      }
      this.OnChildControlsChanged(EventArgs.Empty);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.IsIdle && this.IsTabbed)
      {
        QTabButton accessibleButtonAtPoint = this.m_oTabStrip.GetAccessibleButtonAtPoint(new Point(e.X, e.Y));
        if (accessibleButtonAtPoint != null)
        {
          this.m_oMouseDownAtPoint = new Point(e.X, e.Y);
          this.m_oMouseDownAtButton = accessibleButtonAtPoint;
          accessibleButtonAtPoint.TabButtonSource.ActivateSource();
        }
        else
          this.m_oMouseDownAtButton = (QTabButton) null;
      }
      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.IsIdle && this.IsTabbed && e.Button == MouseButtons.Left)
      {
        if (!this.UserIsDraggingTabButton)
        {
          if (this.m_oMouseDownAtButton != null && (!QMath.ValueInMargin(e.X, this.m_oMouseDownAtPoint.X, this.m_iMouseMoveBeforeDrag) || !QMath.ValueInMargin(e.Y, this.m_oMouseDownAtPoint.Y, this.m_iMouseMoveBeforeDrag)))
            this.StartDraggingButton(this.m_oMouseDownAtButton);
        }
        else if (this.m_oTabStrip.Bounds.Contains(e.X, e.Y))
        {
          QTabButton accessibleButtonAtPoint = this.m_oTabStrip.GetAccessibleButtonAtPoint(new Point(e.X, e.Y));
          if (accessibleButtonAtPoint != null && accessibleButtonAtPoint != this.m_oDraggingButton)
          {
            if (!this.m_oBoundsOfLastRepositionedButton.Contains(e.X, e.Y))
            {
              this.m_oBoundsOfLastRepositionedButton = accessibleButtonAtPoint.BoundsToControl;
              this.m_oDraggingButton.ButtonOrder = accessibleButtonAtPoint.ButtonOrder >= this.m_oDraggingButton.ButtonOrder ? accessibleButtonAtPoint.ButtonOrder + 1 : accessibleButtonAtPoint.ButtonOrder;
            }
          }
          else
            this.m_oBoundsOfLastRepositionedButton = Rectangle.Empty;
        }
        else
        {
          QTabButton oDraggingButton = this.m_oDraggingButton;
          if (oDraggingButton.Control is QDockingWindow && !((QDockControl) oDraggingButton.Control).DockModifierKeysPressed)
          {
            base.OnMouseMove(e);
            return;
          }
          this.EndDraggingButton();
          if (oDraggingButton.Control is QDockingWindow)
          {
            QDockingWindow control = (QDockingWindow) oDraggingButton.Control;
            control.StartDragging(new Point(this.m_oMouseDownAtPoint.X - oDraggingButton.Bounds.Left, control.Height - (this.m_oMouseDownAtPoint.Y - oDraggingButton.Bounds.Bottom)));
          }
        }
      }
      base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.UserIsDraggingTabButton)
        this.EndDraggingButton();
      this.m_oMouseDownAtButton = (QTabButton) null;
      base.OnMouseUp(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (!this.IsTabbed)
        return;
      this.m_oTabStrip.Draw(e.Graphics);
    }

    protected override void OnUserSizing(QUserSizingEventArgs e)
    {
      this.m_oBoundsBeforeUserSize = this.Parent.RectangleToClient(this.CurrentBounds);
      base.OnUserSizing(e);
    }

    protected override void OnUserSized(QUserSizedEventArgs e)
    {
      base.OnUserSized(e);
      this.m_bIsPerformingLayout = true;
      this.LayoutChildControlsProportional((QDockControl) null, e.NewSize, false);
      this.m_bIsPerformingLayout = false;
      this.RefreshNoClientArea(true);
      if (this.Parent != null)
      {
        Region region = new Region(this.Parent.RectangleToClient(this.CurrentBounds));
        region.Union(this.m_oBoundsBeforeUserSize);
        this.Parent.Invalidate(region, true);
        this.Parent.Update();
        region.Dispose();
      }
      else
        this.Refresh();
    }

    protected override void OnUserEndsSizing(EventArgs e)
    {
      base.OnUserEndsSizing(e);
      if (this.Parent == null)
        return;
      this.Parent.Invalidate((Region) null, true);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      if (this.m_bIsLoadingPersistence)
        return;
      this.m_bIsPerformingLayout = true;
      this.ClearCachedObjects();
      Control affectedControl = levent.AffectedControl;
      if (this.UserIsSizingChildControl)
        this.LayoutChildControlsWhenSizingChildControl(this.m_oCurrentSizingChildControl);
      else if (!this.IsUserSizing)
      {
        bool letChildsDetermineSize = false;
        QDockControl changedControl = (QDockControl) null;
        if (levent.AffectedControl != null && levent.AffectedControl != this && this.Controls.Contains(levent.AffectedControl) && levent.AffectedControl is QDockControl && ((QDockControl) levent.AffectedControl).VisibleWhenParentVisible)
        {
          changedControl = (QDockControl) levent.AffectedControl;
          letChildsDetermineSize = levent.AffectedProperty == "Bounds";
        }
        this.LayoutChildControlsProportional(changedControl, this.CurrentBounds.Size, letChildsDetermineSize);
        this.RefreshNoClientArea(true);
        this.Refresh();
      }
      this.m_bIsPerformingLayout = false;
    }

    protected virtual void OnActiveControlChanged(EventArgs e)
    {
      if (this.Parent is QDockContainer)
        ((QDockContainer) this.Parent).OnActiveControlChanged(EventArgs.Empty);
      this.m_oActiveControlChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oActiveControlChangedDelegate, (object) this, (object) e);
    }

    protected virtual void OnTabStripHostPropertyChanged(EventArgs e) => this.m_oTabStripHostPropertyChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oTabStripHostPropertyChangedDelegate, (object) this, (object) e);

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.m_oTabStrip != null && !this.m_oTabStrip.IsDisposed)
        this.m_oTabStrip.Dispose();
      base.Dispose(disposing);
    }

    void IQTabStripHost.StartAnimateTimer(QTabStrip sender) => throw new NotImplementedException();

    void IQTabStripHost.StopAnimateTimer(QTabStrip sender) => throw new NotImplementedException();

    bool IQTabStripHost.UserIsDragging => throw new NotImplementedException();

    bool IQTabStripHost.AllowDrag
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    bool IQTabStripHost.AllowDrop
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    bool IQTabStripHost.AllowExternalDrop
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    QTabStripPaintParams IQTabStripHost.RetrieveTabStripPaintParams()
    {
      if (this.m_oTabStripPaintParams == null)
        this.m_oTabStripPaintParams = new QTabStripPaintParams();
      this.m_oTabStripPaintParams.Border = (Color) this.ColorScheme.DockingWindowTabButtonBorder;
      this.m_oTabStripPaintParams.Background1 = (Color) this.ColorScheme.DockingWindowTabStrip1;
      this.m_oTabStripPaintParams.Background2 = (Color) this.ColorScheme.DockingWindowTabStrip2;
      return this.m_oTabStripPaintParams;
    }

    void IQTabStripHost.HandleTabStripUiRequest(
      QTabStrip sender,
      QCommandUIRequest request,
      Rectangle invalidateBounds)
    {
      if (this.m_bChangingTabButtons)
        return;
      if (request == QCommandUIRequest.PerformLayout)
      {
        this.PerformLayout();
      }
      else
      {
        if (request != QCommandUIRequest.Redraw)
          return;
        this.Refresh();
      }
    }

    protected override void OnPaintNonClientArea(PaintEventArgs e)
    {
      base.OnPaintNonClientArea(e);
      QRectanglePainter.Default.Paint(e.ClipRectangle, (IQAppearance) this.Appearance, new QColorSet(this.BackColor, this.BackColor2, this.BorderColor), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, e.Graphics);
    }
  }
}
