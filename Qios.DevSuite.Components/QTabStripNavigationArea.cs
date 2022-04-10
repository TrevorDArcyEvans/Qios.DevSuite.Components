// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabStripNavigationArea
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QTabStripNavigationArea : IQWeakEventPublisher
  {
    private const int ScrollLeftIndex = 0;
    private const int ScrollRightIndex = 1;
    private const int CloseIndex = 2;
    protected const int DefaultTotalButtonCount = 3;
    private bool m_bWeakEventHandlers = true;
    private Rectangle m_oBounds;
    private QTabStrip m_oTabStrip;
    private GraphicsPath m_oLastDrawnGraphicsPath;
    private QButtonArea[] m_aButtonAreas;
    private QWeakDelegate m_oButtonStateChangedDelegate;
    private QWeakDelegate m_oButtonStateChangingDelegate;

    internal QTabStripNavigationArea(QTabStrip tabStrip)
    {
      this.m_oTabStrip = tabStrip;
      this.m_aButtonAreas = new QButtonArea[this.RequestedButtonCount];
      for (int index = 0; index < this.RequestedButtonCount; ++index)
      {
        this.m_aButtonAreas[index] = new QButtonArea(MouseButtons.Left);
        this.m_aButtonAreas[index].ButtonStateChanging += new QButtonAreaEventHandler(this.Button_ButtonStateChanging);
        this.m_aButtonAreas[index].ButtonStateChanged += new QButtonAreaEventHandler(this.Button_ButtonStateChanged);
      }
    }

    protected virtual int RequestedButtonCount => 3;

    [QWeakEvent]
    public event QButtonAreaEventHandler ButtonStateChanging
    {
      add => this.m_oButtonStateChangingDelegate = QWeakDelegate.Combine(this.m_oButtonStateChangingDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oButtonStateChangingDelegate = QWeakDelegate.Remove(this.m_oButtonStateChangingDelegate, (Delegate) value);
    }

    [QWeakEvent]
    public event QButtonAreaEventHandler ButtonStateChanged
    {
      add => this.m_oButtonStateChangedDelegate = QWeakDelegate.Combine(this.m_oButtonStateChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oButtonStateChangedDelegate = QWeakDelegate.Remove(this.m_oButtonStateChangedDelegate, (Delegate) value);
    }

    [Browsable(false)]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    public QButtonArea ScrollLeft => this.m_aButtonAreas[0];

    public QButtonArea ScrollRight => this.m_aButtonAreas[1];

    public QButtonArea Close => this.m_aButtonAreas[2];

    public QButtonArea[] ButtonAreas => this.m_aButtonAreas;

    public GraphicsPath LastDrawnGraphicsPath
    {
      get => this.m_oLastDrawnGraphicsPath;
      set
      {
        if (this.m_oLastDrawnGraphicsPath == value)
          return;
        if (this.m_oLastDrawnGraphicsPath != null)
          this.m_oLastDrawnGraphicsPath.Dispose();
        this.m_oLastDrawnGraphicsPath = value;
      }
    }

    public int VisibleButtonCount
    {
      get
      {
        int visibleButtonCount = 0;
        for (int index = 0; index < this.m_aButtonAreas.Length; ++index)
        {
          if (this.m_aButtonAreas[index].Visible)
            ++visibleButtonCount;
        }
        return visibleButtonCount;
      }
    }

    public bool ScrollButtonsVisible
    {
      get => this.ScrollLeft.Visible;
      set
      {
        this.ScrollLeft.Visible = value;
        this.ScrollRight.Visible = value;
      }
    }

    public bool CloseButtonVisible
    {
      get => this.Close.Visible;
      set => this.Close.Visible = value;
    }

    public Rectangle Bounds
    {
      get => this.m_oBounds;
      set => this.m_oBounds = value;
    }

    public Point Location
    {
      get => this.m_oBounds.Location;
      set => this.m_oBounds.Location = value;
    }

    public Size Size
    {
      get => this.m_oBounds.Size;
      set => this.m_oBounds.Size = value;
    }

    public Rectangle BoundsToControl => this.CalculateBoundsToControl(new Rectangle(Point.Empty, this.Size));

    public Rectangle CalculateBoundsToControl(Rectangle bounds) => new Rectangle(this.CalculatePointOnControl(bounds.X, bounds.Y), bounds.Size);

    internal virtual void OnButtonStateChanging(object sender, QButtonAreaEventArgs e) => this.m_oButtonStateChangingDelegate = QWeakDelegate.InvokeDelegate(this.m_oButtonStateChangingDelegate, sender, (object) e);

    internal virtual void OnButtonStateChanged(object sender, QButtonAreaEventArgs e) => this.m_oButtonStateChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oButtonStateChangedDelegate, sender, (object) e);

    public void AddButtonArea(QButtonArea buttonArea)
    {
      if (this.ContainsButtonArea(buttonArea))
        return;
      buttonArea.ButtonStateChanging += new QButtonAreaEventHandler(this.Button_ButtonStateChanging);
      buttonArea.ButtonStateChanged += new QButtonAreaEventHandler(this.Button_ButtonStateChanged);
      QButtonArea[] qbuttonAreaArray = new QButtonArea[this.m_aButtonAreas.Length + 1];
      this.m_aButtonAreas.CopyTo((Array) qbuttonAreaArray, 0);
      qbuttonAreaArray[qbuttonAreaArray.Length - 1] = buttonArea;
      this.m_aButtonAreas = qbuttonAreaArray;
    }

    public void RemoveButtonArea(QButtonArea buttonArea)
    {
      if (!this.ContainsButtonArea(buttonArea))
        return;
      buttonArea.ButtonStateChanging += new QButtonAreaEventHandler(this.Button_ButtonStateChanging);
      buttonArea.ButtonStateChanged += new QButtonAreaEventHandler(this.Button_ButtonStateChanged);
      QButtonArea[] qbuttonAreaArray = new QButtonArea[this.m_aButtonAreas.Length - 1];
      int num = 0;
      for (int index = 0; index < this.m_aButtonAreas.Length; ++index)
      {
        if (this.m_aButtonAreas[index] != buttonArea)
          qbuttonAreaArray[num++] = this.m_aButtonAreas[index];
      }
      this.m_aButtonAreas = qbuttonAreaArray;
    }

    public bool ContainsButtonArea(QButtonArea buttonArea)
    {
      for (int index = 0; index < this.m_aButtonAreas.Length; ++index)
      {
        if (this.m_aButtonAreas[index] == buttonArea)
          return true;
      }
      return false;
    }

    public Point CalculatePointOnControl(int x, int y) => this.m_oTabStrip != null ? this.m_oTabStrip.CalculatePointToControl(this.m_oBounds.Left + x, this.m_oBounds.Top + y, false) : new Point(x, y);

    public Point CalculateControlPointOnThis(int x, int y) => this.m_oTabStrip != null ? this.m_oTabStrip.CalculateControlPointToThis(x - this.m_oBounds.Left, y - this.m_oBounds.Top, false) : new Point(x, y);

    public bool ContainsControlPoint(Point controlPoint)
    {
      if (this.VisibleButtonCount == 0)
        return false;
      return this.m_oLastDrawnGraphicsPath != null ? this.m_oLastDrawnGraphicsPath.IsVisible(controlPoint) : this.BoundsToControl.Contains(controlPoint);
    }

    internal bool HandleMouseMove(MouseEventArgs e)
    {
      Point controlPointOnThis = this.CalculateControlPointOnThis(e.X, e.Y);
      MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, controlPointOnThis.X, controlPointOnThis.Y, e.Delta);
      bool flag = false;
      for (int index = 0; index < this.m_aButtonAreas.Length; ++index)
      {
        if (this.m_aButtonAreas[index].HandleMouseMove(e1))
          flag = true;
      }
      return flag;
    }

    internal bool HandleMouseDown(MouseEventArgs e)
    {
      Point controlPointOnThis = this.CalculateControlPointOnThis(e.X, e.Y);
      MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, controlPointOnThis.X, controlPointOnThis.Y, e.Delta);
      bool flag = false;
      for (int index = 0; index < this.m_aButtonAreas.Length; ++index)
      {
        if (this.m_aButtonAreas[index].HandleMouseDown(e1))
          flag = true;
      }
      return flag;
    }

    internal bool HandleMouseUp(MouseEventArgs e)
    {
      Point controlPointOnThis = this.CalculateControlPointOnThis(e.X, e.Y);
      MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, controlPointOnThis.X, controlPointOnThis.Y, e.Delta);
      bool flag = false;
      for (int index = 0; index < this.m_aButtonAreas.Length; ++index)
      {
        if (this.m_aButtonAreas[index].HandleMouseUp(e1))
          flag = true;
      }
      return flag;
    }

    internal bool HandleMouseLeave()
    {
      bool flag = false;
      for (int index = 0; index < this.m_aButtonAreas.Length; ++index)
      {
        if (this.m_aButtonAreas[index].HandleMouseLeave((MouseEventArgs) null))
          flag = true;
      }
      return flag;
    }

    private void Button_ButtonStateChanging(object sender, QButtonAreaEventArgs e) => this.OnButtonStateChanging(sender, e);

    private void Button_ButtonStateChanged(object sender, QButtonAreaEventArgs e) => this.OnButtonStateChanged(sender, e);
  }
}
