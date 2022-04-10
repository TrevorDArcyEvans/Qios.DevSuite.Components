// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QButtonArea
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QButtonArea : IQWeakEventPublisher
  {
    private bool m_bWeakEventHandlers = true;
    private QButtonState m_eState = QButtonState.Normal;
    private Rectangle m_oBounds;
    private Size m_oRequestedSize;
    private bool m_bVisible = true;
    private bool m_bEnabled = true;
    private object m_oAdditionalData;
    private MouseButtons m_eListensToButtons;
    private QWeakDelegate m_oButtonStateChangedDelegate;
    private QWeakDelegate m_oButtonStateChangingDelegate;

    public QButtonArea(MouseButtons listensToButtons) => this.m_eListensToButtons = listensToButtons;

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
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    public MouseButtons ListensToButtons
    {
      get => this.m_eListensToButtons;
      set => this.m_eListensToButtons = value;
    }

    public QButtonState State => this.m_eState;

    public QItemStates ItemState => QItemStatesHelper.FromButtonState(this.m_eState);

    public bool Visible
    {
      get => this.m_bVisible;
      set
      {
        this.m_bVisible = value;
        if (this.m_bVisible)
          return;
        this.m_eState = QButtonState.Normal;
      }
    }

    public bool Enabled
    {
      get => this.m_bEnabled;
      set
      {
        this.m_bEnabled = value;
        if (this.m_bEnabled)
          return;
        this.m_eState = QButtonState.Normal;
      }
    }

    internal void PutButtonState(QButtonState state) => this.SetState(state, (MouseEventArgs) null);

    internal void PutButtonState(QButtonState state, bool raiseEvents) => this.SetState(state, (MouseEventArgs) null, raiseEvents);

    public Rectangle Bounds
    {
      get => this.m_oBounds;
      set => this.m_oBounds = value;
    }

    public Size Size
    {
      get => this.m_oBounds.Size;
      set => this.m_oBounds.Size = value;
    }

    public Size RequestedSize
    {
      get => this.m_oRequestedSize;
      set => this.m_oRequestedSize = value;
    }

    public Point Location
    {
      get => this.m_oBounds.Location;
      set => this.m_oBounds.Location = value;
    }

    public object AdditionalData
    {
      get => this.m_oAdditionalData;
      set => this.m_oAdditionalData = value;
    }

    protected void SetState(QButtonState state, MouseEventArgs mouseEventArgs) => this.SetState(state, mouseEventArgs, true);

    protected void SetState(QButtonState state, MouseEventArgs mouseEventArgs, bool raiseEvents)
    {
      if (this.m_eState == state)
        return;
      QButtonState eState = this.m_eState;
      QButtonAreaEventArgs e = mouseEventArgs != null ? new QButtonAreaEventArgs(eState, state, mouseEventArgs.Button, mouseEventArgs.X, mouseEventArgs.Y) : new QButtonAreaEventArgs(eState, state, MouseButtons.None, int.MinValue, int.MinValue);
      if (raiseEvents)
        this.OnButtonStateChanging(e);
      if (this.m_eState == e.ToState)
        return;
      this.m_eState = e.ToState;
      if (!raiseEvents)
        return;
      if (mouseEventArgs == null)
        this.OnButtonStateChanged(new QButtonAreaEventArgs(eState, this.m_eState, MouseButtons.None, int.MinValue, int.MinValue));
      else
        this.OnButtonStateChanged(new QButtonAreaEventArgs(eState, this.m_eState, mouseEventArgs.Button, mouseEventArgs.X, mouseEventArgs.Y));
    }

    protected bool ListensToButton(MouseButtons mouseButtons) => mouseButtons != MouseButtons.None && (mouseButtons & this.m_eListensToButtons) == mouseButtons;

    public virtual bool HandleMouseMove(MouseEventArgs e)
    {
      if (!this.m_bVisible || !this.m_bEnabled)
        return false;
      if (this.Bounds.Contains(e.X, e.Y))
      {
        if (this.State == QButtonState.Hot && e.Button == MouseButtons.Left)
          this.SetState(QButtonState.Pressed, e);
        else if (this.State == QButtonState.Normal && e.Button == MouseButtons.None)
          this.SetState(QButtonState.Hot, e);
        return true;
      }
      if (this.State != QButtonState.Normal)
      {
        if (this.ListensToButton(e.Button))
          this.SetState(QButtonState.Hot, e);
        else
          this.SetState(QButtonState.Normal, e);
      }
      return false;
    }

    public virtual bool HandleMouseDown(MouseEventArgs e)
    {
      if (!this.m_bVisible || !this.m_bEnabled || this.State != QButtonState.Hot || !this.ListensToButton(e.Button))
        return false;
      this.SetState(QButtonState.Pressed, e);
      return true;
    }

    public virtual bool HandleMouseUp(MouseEventArgs e)
    {
      if (!this.m_bVisible || !this.m_bEnabled || this.State != QButtonState.Pressed || !this.ListensToButton(e.Button))
        return false;
      if (this.Bounds.Contains(e.X, e.Y))
        this.SetState(QButtonState.Hot, e);
      else
        this.SetState(QButtonState.Normal, e);
      return false;
    }

    public virtual bool HandleMouseLeave(MouseEventArgs e)
    {
      if (!this.m_bVisible || !this.m_bEnabled || this.State == QButtonState.Normal)
        return false;
      this.SetState(QButtonState.Normal, e);
      return false;
    }

    internal virtual void OnButtonStateChanging(QButtonAreaEventArgs e) => this.m_oButtonStateChangingDelegate = QWeakDelegate.InvokeDelegate(this.m_oButtonStateChangingDelegate, (object) this, (object) e);

    internal virtual void OnButtonStateChanged(QButtonAreaEventArgs e) => this.m_oButtonStateChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oButtonStateChangedDelegate, (object) this, (object) e);
  }
}
