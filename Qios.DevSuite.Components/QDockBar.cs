// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDockBar
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
  [ToolboxBitmap(typeof (QDockBar), "Resources.ControlImages.QDockBar.bmp")]
  [Designer(typeof (QNoResizeControlDesigner), typeof (IDesigner))]
  public class QDockBar : QControl, IQPersistableHost
  {
    private const int BeforeSlideTimerId = 17;
    private Guid m_oPersistGuid = Guid.NewGuid();
    private int m_iSize = 22;
    private int m_iTimeBeforeSlideIn = 1000;
    private int m_iTimeBeforeSlideOut = 500;
    private int m_iButtonMargin = 2;
    private int m_iButtonSpacing = 5;
    private int m_iTextMargin = 2;
    private int m_iNoIconSize = 16;
    private bool m_bTimerRunning;
    private int m_iBeforeSlideTimerInterval = 100;
    private bool m_bSlideWindowOnMouseOver = true;
    private QDockingWindowItemCollection m_oDockingWindowItems;
    private QDockingWindowItem m_oCurrentHoveredButton;
    private int m_iTickOnHoverStart;
    private QDockControlCollection m_oCurrentDockControls;
    private QAppearance m_oButtonAppearance;
    private EventHandler m_oButtonAppearanceChangedEventHandler;

    public QDockBar()
    {
      this.SuspendLayout();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.m_oDockingWindowItems = new QDockingWindowItemCollection();
      base.Dock = DockStyle.Left;
      this.m_oCurrentDockControls = new QDockControlCollection();
      this.m_oButtonAppearanceChangedEventHandler = new EventHandler(this.ButtonAppearance_AppearanceChanged);
      this.ButtonAppearance = new QAppearance();
      this.ResumeLayout(false);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the QDockControlCollection that contains all the QDockControls of this QDockBar")]
    public QDockControlCollection CurrentDockControls => this.m_oCurrentDockControls;

    [Category("QPersistence")]
    [Description("Gets or sets the PersistGuid. With this Guid the control is identified in the persistence files.")]
    public Guid PersistGuid
    {
      get => this.m_oPersistGuid;
      set => this.m_oPersistGuid = value;
    }

    public override DockStyle Dock
    {
      get => base.Dock;
      set => base.Dock = value != DockStyle.None && value != DockStyle.Fill ? value : throw new QDockException(QResources.GetException("QDockBar_Dock_Invalid"));
    }

    public bool ShouldSerializeButtonAppearance() => !this.m_oButtonAppearance.IsSetToDefaultValues();

    public void ResetButtonAppearance() => this.ButtonAppearance.SetToDefaultValues();

    [Description("Gets the QAppearance for the Buttons")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QAppearance ButtonAppearance
    {
      get => this.m_oButtonAppearance;
      set
      {
        if (this.m_oButtonAppearance == value)
          return;
        if (this.m_oButtonAppearance != null)
          this.m_oButtonAppearance.AppearanceChanged -= this.m_oButtonAppearanceChangedEventHandler;
        this.m_oButtonAppearance = value;
        if (this.m_oButtonAppearance != null)
          this.m_oButtonAppearance.AppearanceChanged += this.m_oButtonAppearanceChangedEventHandler;
        this.Refresh();
      }
    }

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QAppearance();

    [Description("Gets the QAppearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QAppearance Appearance => (QAppearance) base.Appearance;

    [Browsable(false)]
    public int ControlIndex => this.Parent == null ? -1 : this.Parent.Controls.IndexOf((Control) this);

    [Browsable(false)]
    public int WindowCount
    {
      get
      {
        int windowCount = 0;
        for (int index = 0; index < this.m_oDockingWindowItems.Count; ++index)
          windowCount += this.m_oDockingWindowItems[index].Count;
        return windowCount;
      }
    }

    [Category("QBehavior")]
    [Description("Gets or sets whether the windows must slide when the mouse hovers over a button.")]
    [DefaultValue(true)]
    public bool SlideWindowOnMouseOver
    {
      get => this.m_bSlideWindowOnMouseOver;
      set => this.m_bSlideWindowOnMouseOver = value;
    }

    internal QDockingWindowItemCollection DockingWindowItems => this.m_oDockingWindowItems;

    internal void AddWindow(QDockingWindow window, bool useMotion) => this.AddWindow(window, -1, useMotion);

    internal void AddWindow(QDockingWindow window, int groupIndex, bool useMotion)
    {
      if (groupIndex >= 0 && this.DockingWindowItems.Count > groupIndex)
      {
        QDockingWindow window1;
        if (this.DockingWindowItems[groupIndex].IsGroup)
        {
          QDockingWindowGroup dockingWindowItem = (QDockingWindowGroup) this.DockingWindowItems[groupIndex];
          window1 = ((QDockingWindowItem) dockingWindowItem.Items[dockingWindowItem.Items.Count - 1]).Window;
        }
        else
          window1 = ((QDockingWindowItem) this.DockingWindowItems[groupIndex]).Window;
        this.AddWindowToGoup(window1, window, true, useMotion);
      }
      else
      {
        QDockingWindowItem itemWithGroupName = this.m_oDockingWindowItems.GetFirstWindowItemWithGroupName(window.WindowGroupName);
        if (itemWithGroupName != null)
        {
          this.AddWindowToGoup(itemWithGroupName.Window, window, false, useMotion);
        }
        else
        {
          if (!this.m_oDockingWindowItems.ContainsWindow(window))
          {
            if (window.DockBar != this)
              window.PutDockBar(this);
            this.m_oDockingWindowItems.Add((IQDockingWindowItem) new QDockingWindowItem(window, (QDockingWindowGroup) null));
            this.PerformLayout();
            this.Refresh();
          }
          window.SetToSlidedState(useMotion);
        }
      }
    }

    internal void AddWindow(QDockingWindow window) => this.AddWindow(window, true);

    internal void AddWindowToGoup(
      QDockingWindow destinationWindow,
      QDockingWindow window,
      bool after,
      bool useMotion)
    {
      QDockingWindowItem windowItem = this.m_oDockingWindowItems.GetWindowItem(destinationWindow);
      if (windowItem == null)
        return;
      if (windowItem.IsInGroup)
      {
        QDockingWindowGroup group = windowItem.Group;
        int num = group.Items.IndexOf((IQDockingWindowItem) windowItem);
        group.Items.Insert(after ? num + 1 : num, (IQDockingWindowItem) new QDockingWindowItem(window, group));
        this.PerformLayout();
        this.Refresh();
        window.SetToSlidedState(useMotion);
      }
      else
      {
        this.RemoveWindow(destinationWindow);
        QDockingWindow[] windows = new QDockingWindow[2];
        windows[after ? 0 : 1] = destinationWindow;
        windows[after ? 1 : 0] = window;
        this.AddWindows(windows, useMotion ? window : (QDockingWindow) null);
      }
    }

    internal void AddWindows(QDockingWindow[] windows, QDockingWindow expandedWindow)
    {
      this.m_oDockingWindowItems.Add((IQDockingWindowItem) new QDockingWindowGroup(windows, expandedWindow));
      this.PerformLayout();
      this.Refresh();
      for (int index = 0; index < windows.Length; ++index)
      {
        if (windows[index].DockBar != this)
          windows[index].PutDockBar(this);
        windows[index].SetToSlidedState(windows[index] == expandedWindow);
      }
      this.PerformLayout();
      this.Refresh();
    }

    internal void RemoveWindow(QDockingWindow window)
    {
      for (int index = 0; index < this.m_oDockingWindowItems.Count; ++index)
      {
        if (this.m_oDockingWindowItems[index].IsGroup)
        {
          QDockingWindowGroup dockingWindowItem = (QDockingWindowGroup) this.m_oDockingWindowItems[index];
          QDockingWindowItem windowItem = dockingWindowItem.Items.GetWindowItem(window);
          if (windowItem != null)
          {
            int val1 = windowItem.IsExpanded ? dockingWindowItem.Items.IndexOf((IQDockingWindowItem) windowItem) : -1;
            dockingWindowItem.Items.Remove((IQDockingWindowItem) windowItem);
            if (dockingWindowItem.Items.Count == 0)
              this.m_oDockingWindowItems.Remove((IQDockingWindowItem) dockingWindowItem);
            else if (val1 >= 0)
              dockingWindowItem[Math.Min(val1, dockingWindowItem.Items.Count - 1)].IsExpanded = true;
          }
        }
        else
        {
          QDockingWindowItem dockingWindowItem = (QDockingWindowItem) this.m_oDockingWindowItems[index];
          if (dockingWindowItem.Window == window)
            this.m_oDockingWindowItems.Remove((IQDockingWindowItem) dockingWindowItem);
        }
      }
      this.PerformLayout();
      this.Refresh();
    }

    internal int GetWindowGroupIndex(QDockingWindow window)
    {
      QDockingWindowItem windowItem = this.DockingWindowItems.GetWindowItem(window);
      if (windowItem == null)
        return -1;
      return windowItem.IsInGroup ? this.DockingWindowItems.IndexOf((IQDockingWindowItem) windowItem.Group) : this.DockingWindowItems.IndexOf((IQDockingWindowItem) windowItem);
    }

    internal void SetWindowToDockedState(QDockingWindow window)
    {
      QDockingWindowItem windowItem = this.m_oDockingWindowItems.GetWindowItem(window);
      if (windowItem == null)
        return;
      if (windowItem.IsInGroup)
      {
        QDockContainer container = (QDockContainer) null;
        QDockingWindowGroup group = windowItem.Group;
        IQDockingWindowItem[] qdockingWindowItemArray = new IQDockingWindowItem[group.Items.Count];
        for (int index = 0; index < group.Items.Count; ++index)
          qdockingWindowItemArray[index] = group.Items[index];
        for (int index = qdockingWindowItemArray.Length - 1; index >= 0; --index)
        {
          if (qdockingWindowItemArray[index] is QDockingWindowItem)
          {
            QDockingWindowItem qdockingWindowItem = (QDockingWindowItem) qdockingWindowItemArray[index];
            if (container == null)
            {
              qdockingWindowItem.Window.SetToDockedState((QDockContainer) null, QDockOrientation.Tabbed);
              container = qdockingWindowItem.Window.DockContainer;
            }
            else
              qdockingWindowItem.Window.SetToDockedState(container, QDockOrientation.Tabbed);
          }
        }
        window.Select();
      }
      else
        windowItem.Window.SetToDockedState((QDockContainer) null, QDockOrientation.None);
    }

    internal void HandleChildControlVisibilityChanged(QDockControl dockControl)
    {
      if (!(dockControl is QDockingWindow window))
        return;
      QDockingWindowItem windowItem = this.m_oDockingWindowItems.GetWindowItem(window);
      if (windowItem == null)
        return;
      if (windowItem.IsInGroup)
      {
        if (dockControl.Visible)
        {
          windowItem.Group.SetAllButtonsToNotExpanded();
          windowItem.IsExpanded = true;
        }
        else if (windowItem.IsExpanded && windowItem.Group.Items.Count > 1)
        {
          windowItem.IsExpanded = false;
          int num = windowItem.Group.Items.IndexOf((IQDockingWindowItem) windowItem);
          if (num == 0)
            windowItem.Group[1].IsExpanded = true;
          else
            windowItem.Group[num - 1].IsExpanded = true;
        }
      }
      if (!windowItem.Window.Visible && windowItem.Window.IsSlideOut)
        this.SlideWindow(windowItem, false);
      this.PerformLayout();
      this.Refresh();
    }

    internal void StartTimer()
    {
      if (this.m_bTimerRunning)
        return;
      this.m_bTimerRunning = true;
      this.StartTimer(17, this.m_iBeforeSlideTimerInterval);
    }

    internal void StopTimer()
    {
      if (!this.m_bTimerRunning)
        return;
      this.m_bTimerRunning = false;
      this.StopTimer(17);
    }

    private QDockingWindowItem GetItemAtPos(int x, int y)
    {
      for (int index1 = 0; index1 < this.m_oDockingWindowItems.Count; ++index1)
      {
        if (this.m_oDockingWindowItems[index1].IsGroup)
        {
          QDockingWindowGroup dockingWindowItem = (QDockingWindowGroup) this.m_oDockingWindowItems[index1];
          for (int index2 = 0; index2 < dockingWindowItem.Count; ++index2)
          {
            if (dockingWindowItem[index2].Window.Visible && dockingWindowItem[index2].ButtonBounds.Contains(x, y))
              return dockingWindowItem[index2];
          }
        }
        else
        {
          QDockingWindowItem dockingWindowItem = (QDockingWindowItem) this.m_oDockingWindowItems[index1];
          if (dockingWindowItem.Window.Visible && dockingWindowItem.ButtonBounds.Contains(x, y))
            return dockingWindowItem;
        }
      }
      return (QDockingWindowItem) null;
    }

    private void SlideInAllSlidedOutWindows(bool useMotion)
    {
      for (int index1 = 0; index1 < this.m_oDockingWindowItems.Count; ++index1)
      {
        if (this.m_oDockingWindowItems[index1].IsGroup)
        {
          QDockingWindowGroup dockingWindowItem = (QDockingWindowGroup) this.m_oDockingWindowItems[index1];
          for (int index2 = 0; index2 < dockingWindowItem.Count; ++index2)
          {
            if (dockingWindowItem[index2].Window.IsSlideOut)
              dockingWindowItem[index2].Window.SlideWindow(true, useMotion);
          }
        }
        else
        {
          QDockingWindowItem dockingWindowItem = (QDockingWindowItem) this.m_oDockingWindowItems[index1];
          if (dockingWindowItem.Window.IsSlideOut)
            dockingWindowItem.Window.SlideWindow(true, useMotion);
        }
      }
    }

    private static StringFormat GetButtonStringFormat() => new StringFormat(StringFormat.GenericDefault)
    {
      LineAlignment = StringAlignment.Near,
      Alignment = StringAlignment.Near,
      Trimming = StringTrimming.EllipsisCharacter,
      FormatFlags = StringFormatFlags.NoWrap
    };

    private void LayoutButtonVertical(
      Graphics graphics,
      QDockingWindowItem item,
      int startY,
      SizeF textSize,
      StringFormat format)
    {
      int iButtonMargin = this.m_iButtonMargin;
      SizeF empty = SizeF.Empty;
      int num = 0;
      QDockingWindowItem qdockingWindowItem = item;
      SizeF sizeF = !(textSize != (SizeF) Size.Empty) ? graphics.MeasureString(qdockingWindowItem.Window.Text, this.Font, PointF.Empty, format) : textSize;
      int width = (int) Math.Ceiling((double) sizeF.Width) + this.m_iTextMargin * 2;
      int height = qdockingWindowItem.Window.Icon == null ? num + (this.m_iNoIconSize + this.m_iTextMargin * 2) : num + (qdockingWindowItem.Window.Icon.Height + this.m_iTextMargin * 2);
      if (qdockingWindowItem.IsExpanded)
        height += (int) Math.Ceiling((double) sizeF.Height) + this.m_iTextMargin * 2;
      qdockingWindowItem.ButtonBounds = new Rectangle(iButtonMargin, startY, width, height);
    }

    private int LayoutVertical()
    {
      Graphics graphics = this.CreateGraphics();
      int iButtonSpacing = this.m_iButtonSpacing;
      int val1 = this.DesignMode ? this.m_iSize : 0;
      StringFormat buttonStringFormat = QDockBar.GetButtonStringFormat();
      buttonStringFormat.FormatFlags |= StringFormatFlags.DirectionVertical;
      for (int index1 = 0; index1 < this.m_oDockingWindowItems.Count; ++index1)
      {
        bool flag = false;
        if (this.m_oDockingWindowItems[index1].IsGroup)
        {
          QDockingWindowGroup dockingWindowItem = (QDockingWindowGroup) this.m_oDockingWindowItems[index1];
          SizeF largestTextSize = dockingWindowItem.GetLargestTextSize(graphics, this.Font, buttonStringFormat);
          for (int index2 = 0; index2 < dockingWindowItem.Count; ++index2)
          {
            QDockingWindowItem qdockingWindowItem = dockingWindowItem[index2];
            if (qdockingWindowItem.Window.Visible)
            {
              this.LayoutButtonVertical(graphics, qdockingWindowItem, iButtonSpacing, largestTextSize, buttonStringFormat);
              iButtonSpacing += qdockingWindowItem.ButtonBounds.Height;
              val1 = Math.Max(val1, qdockingWindowItem.ButtonBounds.Width + this.m_iButtonMargin * 2);
              flag = true;
            }
          }
          if (flag)
            iButtonSpacing += this.m_iButtonSpacing;
        }
        else
        {
          QDockingWindowItem dockingWindowItem = (QDockingWindowItem) this.m_oDockingWindowItems[index1];
          if (dockingWindowItem.Window.Visible)
          {
            this.LayoutButtonVertical(graphics, dockingWindowItem, iButtonSpacing, SizeF.Empty, buttonStringFormat);
            iButtonSpacing += dockingWindowItem.ButtonBounds.Height + this.m_iButtonSpacing;
            val1 = Math.Max(val1, dockingWindowItem.ButtonBounds.Width + this.m_iButtonMargin * 2);
          }
        }
      }
      graphics.Dispose();
      buttonStringFormat.Dispose();
      return val1;
    }

    private void LayoutButtonHorizontal(
      Graphics graphics,
      QDockingWindowItem item,
      int startX,
      SizeF textSize,
      StringFormat format)
    {
      int iButtonMargin = this.m_iButtonMargin;
      SizeF empty = SizeF.Empty;
      int num = 0;
      QDockingWindowItem qdockingWindowItem = item;
      SizeF sizeF = !(textSize != (SizeF) Size.Empty) ? graphics.MeasureString(qdockingWindowItem.Window.Text, this.Font, PointF.Empty, format) : textSize;
      int height = (int) Math.Ceiling((double) sizeF.Height) + this.m_iTextMargin * 2;
      int width = qdockingWindowItem.Window.Icon == null ? num + (this.m_iNoIconSize + this.m_iTextMargin * 2) : num + (qdockingWindowItem.Window.Icon.Width + this.m_iTextMargin * 2);
      if (qdockingWindowItem.IsExpanded)
        width += (int) Math.Ceiling((double) sizeF.Width) + this.m_iTextMargin * 2;
      qdockingWindowItem.ButtonBounds = new Rectangle(startX, iButtonMargin, width, height);
    }

    private int LayoutHorizontal()
    {
      Graphics graphics = this.CreateGraphics();
      int iButtonSpacing = this.m_iButtonSpacing;
      int val1 = this.DesignMode ? this.m_iSize : 0;
      StringFormat buttonStringFormat = QDockBar.GetButtonStringFormat();
      for (int index1 = 0; index1 < this.m_oDockingWindowItems.Count; ++index1)
      {
        if (this.m_oDockingWindowItems[index1].IsGroup)
        {
          bool flag = false;
          QDockingWindowGroup dockingWindowItem = (QDockingWindowGroup) this.m_oDockingWindowItems[index1];
          SizeF largestTextSize = dockingWindowItem.GetLargestTextSize(graphics, this.Font, buttonStringFormat);
          for (int index2 = 0; index2 < dockingWindowItem.Count; ++index2)
          {
            QDockingWindowItem qdockingWindowItem = dockingWindowItem[index2];
            if (qdockingWindowItem.Window.Visible)
            {
              this.LayoutButtonHorizontal(graphics, qdockingWindowItem, iButtonSpacing, largestTextSize, buttonStringFormat);
              iButtonSpacing += qdockingWindowItem.ButtonBounds.Width;
              val1 = Math.Max(val1, qdockingWindowItem.ButtonBounds.Height + this.m_iButtonMargin * 2);
              flag = true;
            }
          }
          if (flag)
            iButtonSpacing += this.m_iButtonSpacing;
        }
        else
        {
          QDockingWindowItem dockingWindowItem = (QDockingWindowItem) this.m_oDockingWindowItems[index1];
          if (dockingWindowItem.Window.Visible)
          {
            this.LayoutButtonHorizontal(graphics, dockingWindowItem, iButtonSpacing, (SizeF) Size.Empty, buttonStringFormat);
            iButtonSpacing += dockingWindowItem.ButtonBounds.Width + this.m_iButtonSpacing;
            val1 = Math.Max(val1, dockingWindowItem.ButtonBounds.Height + this.m_iButtonMargin * 2);
          }
        }
      }
      buttonStringFormat.Dispose();
      graphics.Dispose();
      return val1;
    }

    private void LayoutCore()
    {
      if (this.IsDisposed)
        return;
      switch (this.Dock)
      {
        case DockStyle.Top:
        case DockStyle.Bottom:
          this.SetBounds(0, 0, 0, this.LayoutHorizontal(), BoundsSpecified.Height);
          break;
        case DockStyle.Left:
        case DockStyle.Right:
          this.SetBounds(0, 0, this.LayoutVertical(), 0, BoundsSpecified.Width);
          break;
      }
    }

    private void UpdateLastActiveTickCount(
      QDockingWindowItem item,
      bool slideInTimedOutWindows,
      Point clientPosition)
    {
      if (this.IsDisposed)
        return;
      QDockingWindowItem qdockingWindowItem = item;
      if (qdockingWindowItem.Window == null || !qdockingWindowItem.Window.IsSlideOut)
        return;
      if (qdockingWindowItem.Window.ContainsFocus || qdockingWindowItem.Window.ContainsScreenPoint(Control.MousePosition) || qdockingWindowItem.ButtonBounds.Contains(clientPosition) || !qdockingWindowItem.Window.IsIdle)
      {
        qdockingWindowItem.LastActiveTickCount = Environment.TickCount;
      }
      else
      {
        if (!slideInTimedOutWindows || qdockingWindowItem.LastActiveTickCount + this.m_iTimeBeforeSlideIn >= Environment.TickCount)
          return;
        this.SlideWindow(qdockingWindowItem, true);
      }
    }

    private void UpdateLastActiveTickCount(bool slideInTimedOutWindows)
    {
      if (this.IsDisposed)
        return;
      Point client = this.PointToClient(Control.MousePosition);
      for (int index1 = 0; index1 < this.m_oDockingWindowItems.Count; ++index1)
      {
        if (this.m_oDockingWindowItems[index1].IsGroup)
        {
          QDockingWindowGroup dockingWindowItem = (QDockingWindowGroup) this.m_oDockingWindowItems[index1];
          for (int index2 = 0; index2 < dockingWindowItem.Count; ++index2)
            this.UpdateLastActiveTickCount(dockingWindowItem[index2], slideInTimedOutWindows, client);
        }
        else
          this.UpdateLastActiveTickCount((QDockingWindowItem) this.m_oDockingWindowItems[index1], slideInTimedOutWindows, client);
      }
    }

    protected override void OnTimerElapsed(QControlTimerEventArgs e)
    {
      base.OnTimerElapsed(e);
      if (e.TimerId != 17)
        return;
      QDockingWindowItem qdockingWindowItem = (QDockingWindowItem) null;
      Point client = this.PointToClient(Control.MousePosition);
      if (this.Visible && this.Enabled && Control.MouseButtons == MouseButtons.None)
        qdockingWindowItem = this.GetItemAtPos(client.X, client.Y);
      if (this.m_oCurrentHoveredButton != null && qdockingWindowItem == this.m_oCurrentHoveredButton)
      {
        if (this.m_oCurrentHoveredButton.IsSlideOutCandidate && this.m_iTickOnHoverStart + this.m_iTimeBeforeSlideOut < Environment.TickCount)
        {
          this.m_iTickOnHoverStart = -1;
          if (this.m_oCurrentHoveredButton.Window.IsSlideIn)
            this.SlideWindow(this.m_oCurrentHoveredButton, true);
        }
      }
      else
      {
        if (this.m_oCurrentHoveredButton != null)
        {
          this.m_oCurrentHoveredButton.IsSlideOutCandidate = false;
          this.m_oCurrentHoveredButton = (QDockingWindowItem) null;
          this.m_iTickOnHoverStart = -1;
        }
        this.m_oCurrentHoveredButton = qdockingWindowItem;
        if (this.m_oCurrentHoveredButton != null)
        {
          if (this.m_oCurrentHoveredButton.Window.IsIdle && this.m_oCurrentHoveredButton.Window.IsSlideIn)
          {
            this.m_oCurrentHoveredButton.IsSlideOutCandidate = true;
            this.m_iTickOnHoverStart = Environment.TickCount;
          }
        }
        else
        {
          bool flag = true;
          for (int index1 = 0; index1 < this.m_oDockingWindowItems.Count; ++index1)
          {
            if (this.m_oDockingWindowItems[index1].IsGroup)
            {
              QDockingWindowGroup dockingWindowItem = (QDockingWindowGroup) this.m_oDockingWindowItems[index1];
              for (int index2 = 0; index2 < dockingWindowItem.Count; ++index2)
              {
                QDockingWindow window = dockingWindowItem[index2].Window;
                if (window.IsSlideOut || window.IsSliding)
                  flag = false;
              }
            }
            else
            {
              QDockingWindow window = ((QDockingWindowItem) this.m_oDockingWindowItems[index1]).Window;
              if (window.IsSlideOut || window.IsSliding)
                flag = false;
            }
          }
          if (flag)
            this.StopTimer();
        }
      }
      this.UpdateLastActiveTickCount(this.m_oCurrentHoveredButton == null);
    }

    protected override string BackColorPropertyName => "DockBarBackground1";

    protected override string BackColor2PropertyName => "DockBarBackground2";

    protected override string BorderColorPropertyName => "DockBarBorder";

    protected override void Dispose(bool disposing)
    {
      int num = disposing ? 1 : 0;
      base.Dispose(disposing);
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      if (!this.Visible)
        return;
      this.PerformLayout();
      this.Refresh();
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      this.LayoutCore();
      base.OnLayout(levent);
    }

    private void DrawButtonVertical(QDockingWindowItem item, Graphics graphics)
    {
      StringFormat buttonStringFormat = QDockBar.GetButtonStringFormat();
      buttonStringFormat.FormatFlags |= StringFormatFlags.DirectionVertical;
      Brush brush = this.Enabled ? (Brush) new SolidBrush((Color) this.ColorScheme.TextColor) : (Brush) new SolidBrush((Color) this.ColorScheme.DisabledTextColor);
      QDockingWindowItem qdockingWindowItem = item;
      Rectangle buttonBounds = qdockingWindowItem.ButtonBounds;
      QAppearanceWrapper appearance = new QAppearanceWrapper((IQAppearance) this.ButtonAppearance);
      appearance.AdjustBordersForVerticalOrientation();
      appearance.AdjustBackgroundOrientationsToVertical();
      QRectanglePainter.Default.Paint(buttonBounds, (IQAppearance) appearance, new QColorSet((Color) this.ColorScheme.DockBarButtonBackground1, (Color) this.ColorScheme.DockBarButtonBackground2, (Color) this.ColorScheme.DockBarButtonBorder), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
      int num1 = buttonBounds.Top + this.m_iTextMargin;
      if (qdockingWindowItem.Window.Icon != null)
      {
        int num2 = buttonBounds.Left + (int) Math.Ceiling((double) buttonBounds.Width / 2.0 - (double) qdockingWindowItem.Window.Icon.Width / 2.0);
        if (this.Enabled)
          QControlPaint.DrawIcon(qdockingWindowItem.Window.Icon, num2, num1, graphics);
        else
          QControlPaint.DrawIconDisabled(graphics, qdockingWindowItem.Window.Icon, new Rectangle(num2, num1, qdockingWindowItem.Window.Icon.Width, qdockingWindowItem.Window.Icon.Height));
        num1 += qdockingWindowItem.Window.Icon.Height + this.m_iTextMargin * 2;
      }
      if (qdockingWindowItem.IsExpanded)
        graphics.DrawString(qdockingWindowItem.Window.Text, this.Font, brush, (float) (qdockingWindowItem.ButtonBounds.Left + this.m_iTextMargin), (float) num1, buttonStringFormat);
      brush.Dispose();
      buttonStringFormat.Dispose();
    }

    private void DrawVertical(Graphics graphics)
    {
      for (int index1 = 0; index1 < this.m_oDockingWindowItems.Count; ++index1)
      {
        if (this.m_oDockingWindowItems[index1].IsGroup)
        {
          QDockingWindowGroup dockingWindowItem = (QDockingWindowGroup) this.m_oDockingWindowItems[index1];
          for (int index2 = 0; index2 < dockingWindowItem.Items.Count; ++index2)
          {
            if (dockingWindowItem[index2].Window.Visible)
              this.DrawButtonVertical(dockingWindowItem[index2], graphics);
          }
        }
        else
        {
          QDockingWindowItem dockingWindowItem = (QDockingWindowItem) this.m_oDockingWindowItems[index1];
          if (dockingWindowItem.Window.Visible)
            this.DrawButtonVertical(dockingWindowItem, graphics);
        }
      }
    }

    private void DrawButtonHorizontal(QDockingWindowItem item, Graphics graphics)
    {
      StringFormat buttonStringFormat = QDockBar.GetButtonStringFormat();
      Brush brush = this.Enabled ? (Brush) new SolidBrush((Color) this.ColorScheme.TextColor) : (Brush) new SolidBrush((Color) this.ColorScheme.DisabledTextColor);
      QDockingWindowItem qdockingWindowItem = item;
      Rectangle buttonBounds = qdockingWindowItem.ButtonBounds;
      QRectanglePainter.Default.Paint(buttonBounds, (IQAppearance) this.ButtonAppearance, new QColorSet((Color) this.ColorScheme.DockBarButtonBackground1, (Color) this.ColorScheme.DockBarButtonBackground2, (Color) this.ColorScheme.DockBarButtonBorder), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
      int num1 = buttonBounds.Left + this.m_iTextMargin;
      if (qdockingWindowItem.Window.Icon != null)
      {
        int num2 = buttonBounds.Top + (int) Math.Ceiling((double) buttonBounds.Height / 2.0 - (double) qdockingWindowItem.Window.Icon.Height / 2.0);
        if (this.Enabled)
          QControlPaint.DrawIcon(qdockingWindowItem.Window.Icon, num1, num2, graphics);
        else
          QControlPaint.DrawIconDisabled(graphics, qdockingWindowItem.Window.Icon, new Rectangle(num1, num2, qdockingWindowItem.Window.Icon.Width, qdockingWindowItem.Window.Icon.Height));
        num1 += qdockingWindowItem.Window.Icon.Width + this.m_iTextMargin * 2;
      }
      if (qdockingWindowItem.IsExpanded)
        graphics.DrawString(qdockingWindowItem.Window.Text, this.Font, brush, (float) num1, (float) (qdockingWindowItem.ButtonBounds.Top + this.m_iTextMargin), buttonStringFormat);
      brush.Dispose();
      buttonStringFormat.Dispose();
    }

    private void DrawHorizontal(Graphics graphics)
    {
      for (int index1 = 0; index1 < this.m_oDockingWindowItems.Count; ++index1)
      {
        if (this.m_oDockingWindowItems[index1].IsGroup)
        {
          QDockingWindowGroup dockingWindowItem = (QDockingWindowGroup) this.m_oDockingWindowItems[index1];
          for (int index2 = 0; index2 < dockingWindowItem.Items.Count; ++index2)
          {
            if (dockingWindowItem[index2].Window.Visible)
              this.DrawButtonHorizontal(dockingWindowItem[index2], graphics);
          }
        }
        else
        {
          QDockingWindowItem dockingWindowItem = (QDockingWindowItem) this.m_oDockingWindowItems[index1];
          if (dockingWindowItem.Window.Visible)
            this.DrawButtonHorizontal(dockingWindowItem, graphics);
        }
      }
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      base.OnPaintBackground(pevent);
      if (this.IsDisposed)
        return;
      bool flag = false;
      switch (this.Dock)
      {
        case DockStyle.Top:
        case DockStyle.Bottom:
          this.DrawHorizontal(pevent.Graphics);
          break;
        case DockStyle.Left:
        case DockStyle.Right:
          this.DrawVertical(pevent.Graphics);
          flag = true;
          break;
      }
      if (!this.DesignMode)
        return;
      StringFormat format = new StringFormat(StringFormat.GenericDefault);
      if (flag)
        format.FormatFlags |= StringFormatFlags.DirectionVertical;
      format.LineAlignment = StringAlignment.Center;
      Rectangle layoutRectangle = new QPadding(4, 2, 2, 4).InflateRectangleWithPadding(this.ClientRectangle, false, !flag);
      Brush brush = (Brush) new SolidBrush(Color.Gray);
      pevent.Graphics.DrawString("[QDockBar: " + this.Name + "]", this.Font, brush, (RectangleF) layoutRectangle, format);
      brush.Dispose();
      format.Dispose();
    }

    protected override void OnPaint(PaintEventArgs e) => base.OnPaint(e);

    private void SlideWindow(QDockingWindowItem item, bool useMotion)
    {
      if (item.Window.IsSliding)
        return;
      if (item.Window.IsSlideIn)
      {
        this.SlideInAllSlidedOutWindows(false);
        item.Window.SlideWindow(false, useMotion);
        item.IsSlideOutCandidate = false;
        item.IsExpanded = true;
        this.PerformLayout();
        this.Invalidate();
      }
      else
      {
        if (!item.Window.IsSlideOut)
          return;
        item.Window.SlideWindow(true, useMotion);
        item.IsSlideOutCandidate = false;
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        QDockingWindowItem itemAtPos = this.GetItemAtPos(e.X, e.Y);
        if (itemAtPos != null)
          this.SlideWindow(itemAtPos, true);
      }
      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.m_bSlideWindowOnMouseOver && this.GetItemAtPos(e.X, e.Y) != null)
        this.StartTimer();
      base.OnMouseMove(e);
    }

    private void ButtonAppearance_AppearanceChanged(object sender, EventArgs e) => this.Refresh();
  }
}
