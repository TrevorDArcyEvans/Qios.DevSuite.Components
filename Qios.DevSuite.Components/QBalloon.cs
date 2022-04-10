// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QBalloon
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [DesignerSerializer(typeof (QBalloonCodeSerializer), typeof (CodeDomSerializer))]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (QBalloon), "Resources.ControlImages.QBalloon.bmp")]
  [Designer(typeof (QBalloonDesigner), typeof (IDesigner))]
  public class QBalloon : QControlComponent
  {
    private QWeakEventConsumerCollection m_oEventConsumers;
    private bool m_bRecreateBalloonWindow;
    private QBalloonControlListener m_oLastListener;
    private bool m_bIgnoreNextMouseEnter;
    private int m_iReShowDelayTickCount;
    private QBalloonConfiguration m_oBalloonConfiguration;
    private EventHandler m_oBalloonConfigurationChangedEventHandler;
    private EventHandler m_oBalloonConfigurationBalloonWindowShapeChangedEventHander;
    private EventHandler m_oBalloonWindowConfigurationChangedEventHandler;
    private EventHandler m_oBalloonWindowAppearanceChangedEventHandler;
    private QBalloon.QBalloonAnimationStatus m_eAnimationStatus;
    private int m_iAnimationTimerInterval = 30;
    private int m_iTimerInterval = 50;
    private int m_iTimerID;
    private int m_iAnimationTimerID;
    private ArrayList m_aListeners;
    private QBalloonWindow m_oBalloonWindow;
    private static QBalloon CurrentVisibleBalloon;
    private QWeakDelegate m_oBalloonVisibleChangedDelegate;
    private QWeakDelegate m_oElementMouseDownDelegate;
    private QWeakDelegate m_oElementMouseEnterDelegate;
    private QWeakDelegate m_oElementMouseLeaveDelegate;
    private QWeakDelegate m_oElementMouseUpDelegate;
    private QWeakDelegate m_oElementMouseClickDelegate;
    private QWeakDelegate m_oElementLinkClickDelegate;

    public QBalloon()
    {
      this.m_aListeners = new ArrayList();
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      new InitializationDelegate(this.Initialize)();
    }

    internal QBalloon.QBalloonAnimationStatus AnimationStatus => this.m_eAnimationStatus;

    internal QBalloonControlListener LastListener
    {
      get => this.m_oLastListener;
      set => this.m_oLastListener = value;
    }

    internal bool IgnoreNextMouseEnter
    {
      get => this.m_bIgnoreNextMouseEnter;
      set => this.m_bIgnoreNextMouseEnter = value;
    }

    [Description("Occurs when the QBalloonWindow is changes visibility (after any animation)")]
    [QWeakEvent]
    [Category("QEvents")]
    public event EventHandler BalloonVisibleChanged
    {
      add => this.m_oBalloonVisibleChangedDelegate = QWeakDelegate.Combine(this.m_oBalloonVisibleChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oBalloonVisibleChangedDelegate = QWeakDelegate.Remove(this.m_oBalloonVisibleChangedDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Occurs when the mouse enters an element.")]
    public event QMarkupTextElementEventHandler ElementMouseEnter
    {
      add => this.m_oElementMouseEnterDelegate = QWeakDelegate.Combine(this.m_oElementMouseEnterDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseEnterDelegate = QWeakDelegate.Remove(this.m_oElementMouseEnterDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Occurs when the mouse leaves this element.")]
    [QWeakEvent]
    public event QMarkupTextElementEventHandler ElementMouseLeave
    {
      add => this.m_oElementMouseLeaveDelegate = QWeakDelegate.Combine(this.m_oElementMouseLeaveDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseLeaveDelegate = QWeakDelegate.Remove(this.m_oElementMouseLeaveDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Occurs when the user presses the mousebutton on this element.")]
    [QWeakEvent]
    public event QMarkupTextElementEventHandler ElementMouseDown
    {
      add => this.m_oElementMouseDownDelegate = QWeakDelegate.Combine(this.m_oElementMouseDownDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseDownDelegate = QWeakDelegate.Remove(this.m_oElementMouseDownDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the user releases the mousebutton on this element.")]
    public event QMarkupTextElementEventHandler ElementMouseUp
    {
      add => this.m_oElementMouseUpDelegate = QWeakDelegate.Combine(this.m_oElementMouseUpDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseUpDelegate = QWeakDelegate.Remove(this.m_oElementMouseUpDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the user clicks on this element.")]
    public event QMarkupTextElementEventHandler ElementMouseClick
    {
      add => this.m_oElementMouseClickDelegate = QWeakDelegate.Combine(this.m_oElementMouseClickDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseClickDelegate = QWeakDelegate.Remove(this.m_oElementMouseClickDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Occurs when the user clicks on a link.")]
    [Category("QEvents")]
    public event QMarkupTextElementEventHandler ElementLinkClick
    {
      add => this.m_oElementLinkClickDelegate = QWeakDelegate.Combine(this.m_oElementLinkClickDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementLinkClickDelegate = QWeakDelegate.Remove(this.m_oElementLinkClickDelegate, (Delegate) value);
    }

    public bool Visible => this.m_oBalloonWindow != null && this.m_oBalloonWindow.Visible && this.m_oBalloonWindow.OpacityAsByte == byte.MaxValue;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the current QBalloonWindow used for showing the balloon text.")]
    [Category("QAppearance")]
    [Browsable(false)]
    [DefaultValue(null)]
    public QBalloonWindow BalloonWindow => this.m_oBalloonWindow;

    [Category("QAppearance")]
    [Description("Gets or sets the QBalloonConfiguration for the QBalloon.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QBalloonConfiguration Configuration
    {
      get => this.m_oBalloonConfiguration;
      set
      {
        if (this.m_oBalloonConfiguration == value)
          return;
        if (this.m_oBalloonConfiguration != null)
        {
          this.m_oBalloonConfiguration.ConfigurationChanged -= this.m_oBalloonConfigurationChangedEventHandler;
          this.m_oBalloonConfiguration.BalloonWindowConfigurationChanged -= this.m_oBalloonWindowConfigurationChangedEventHandler;
          this.m_oBalloonConfiguration.BalloonWindowAppearanceChanged -= this.m_oBalloonWindowAppearanceChangedEventHandler;
          this.m_oBalloonConfiguration.BalloonWindowShapeChanged -= this.m_oBalloonConfigurationBalloonWindowShapeChangedEventHander;
        }
        this.m_oBalloonConfiguration = value;
        if (this.m_oBalloonConfiguration == null)
          return;
        this.m_oBalloonConfiguration.ConfigurationChanged += this.m_oBalloonConfigurationChangedEventHandler;
        this.m_oBalloonConfiguration.BalloonWindowConfigurationChanged += this.m_oBalloonWindowConfigurationChangedEventHandler;
        this.m_oBalloonConfiguration.BalloonWindowAppearanceChanged += this.m_oBalloonWindowAppearanceChangedEventHandler;
        this.m_oBalloonConfiguration.BalloonWindowShapeChanged += this.m_oBalloonConfigurationBalloonWindowShapeChangedEventHander;
        this.Balloon_ConfigurationChanged((object) this, EventArgs.Empty);
      }
    }

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing)
        return;
      if (this.m_oBalloonWindow != null && !this.m_oBalloonWindow.IsDisposed)
        this.m_oBalloonWindow.Dispose();
      this.m_oBalloonWindow = (QBalloonWindow) null;
    }

    public bool ShowOnNotifyIcon(NotifyIcon notifyIcon, string markupText)
    {
      Rectangle empty = Rectangle.Empty;
      Rectangle rectangle = QNotifyIconHelper.RetrieveNotifyIconBounds(notifyIcon);
      if (!(rectangle != Rectangle.Empty))
        return false;
      if (!this.Configuration.BalloonWindowConfiguration.TopMost)
        this.Configuration.BalloonWindowConfiguration.TopMost = true;
      this.Show(new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top + rectangle.Height / 2), markupText, true);
      return true;
    }

    public void Show(Point screenLocation, string markupText) => this.Show(screenLocation, markupText, false);

    public void Show(Point screenLocation, string markupText, bool ignoreWorkingArea)
    {
      QBalloonControlListener qballoonControlListener = new QBalloonControlListener(this, (Control) null, markupText, true);
      this.m_aListeners.Add((object) qballoonControlListener);
      qballoonControlListener.Positioning = QBalloonWindowPositioning.SpecifiedLocation;
      qballoonControlListener.SpecifiedLocation = screenLocation;
      qballoonControlListener.IgnoreWorkingArea = ignoreWorkingArea;
      qballoonControlListener.Show(false);
      this.SecureTimer();
    }

    public void Show(Control targetControl, string markupText)
    {
      QBalloonControlListener qballoonControlListener = new QBalloonControlListener(this, targetControl, markupText, true);
      this.m_aListeners.Add((object) qballoonControlListener);
      qballoonControlListener.Positioning = QBalloonWindowPositioning.ControlBounds;
      qballoonControlListener.Show(false);
      this.SecureTimer();
    }

    public void Show(Control targetControl)
    {
      if (!this.HasListenerForControl(targetControl))
        return;
      QBalloonControlListener listenerForControl = this.GetListenerForControl(targetControl);
      listenerForControl.Positioning = QBalloonWindowPositioning.ControlBounds;
      listenerForControl.Show(false);
      this.SecureTimer();
    }

    public bool HasListenerForControl(Control control) => this.GetListenerForControl(control) != null;

    public void SetMarkupText(Control control, string markupText)
    {
      if (!QMisc.IsEmpty((object) markupText))
        this.AddListener(control, markupText);
      else
        this.RemoveListener(control);
    }

    public string GetMarkupText(Control control) => this.GetListenerForControl(control)?.Text;

    [Localizable(true)]
    public void AddListener(Control control, string markupText)
    {
      if (!this.HasListenerForControl(control))
      {
        QBalloonControlListener qballoonControlListener = new QBalloonControlListener(this, control, markupText);
        qballoonControlListener.Connect();
        this.m_aListeners.Add((object) qballoonControlListener);
        this.SecureTimer();
      }
      else
      {
        QBalloonControlListener listenerForControl = this.GetListenerForControl(control);
        listenerForControl.RemoveAfterHide = false;
        listenerForControl.Enabled = true;
        listenerForControl.Text = markupText;
        listenerForControl.Connect();
      }
      QBalloonDesigner.GetExtenderProviderOnSite(this.Site, true)?.SetQBalloon(control, this);
    }

    public void RemoveListener(Control control)
    {
      QBalloonControlListener listenerForControl = this.GetListenerForControl(control);
      if (listenerForControl == null)
        return;
      if (this.m_oLastListener == listenerForControl && this.m_oBalloonWindow != null && this.m_oBalloonWindow.Visible && !this.Configuration.AutoHide)
      {
        this.m_oLastListener.RemoveAfterHide = true;
        this.m_oLastListener.Enabled = false;
      }
      else
      {
        if (listenerForControl.Connected)
          listenerForControl.Disconnect();
        this.m_aListeners.Remove((object) listenerForControl);
        this.SecureTimer();
      }
    }

    internal void RemoveListener(QBalloonControlListener listener, bool force)
    {
      if (listener == null)
        return;
      if (this.m_oLastListener == listener && this.m_oBalloonWindow != null && this.m_oBalloonWindow.Visible && !this.Configuration.AutoHide && !force)
      {
        this.m_oLastListener.RemoveAfterHide = true;
        this.m_oLastListener.Enabled = false;
      }
      else
      {
        if (listener.Connected)
          listener.Disconnect();
        this.m_aListeners.Remove((object) listener);
      }
    }

    protected virtual QBalloonWindow CreateDefaultBalloonWindow() => new QBalloonWindow();

    protected virtual QBalloonWindow CreateBalloonWindow()
    {
      QBalloonWindow balloonWindow = (QBalloonWindow) null;
      if (balloonWindow == null && this.Configuration.BalloonWindowClassReference != null)
      {
        if (this.Configuration.BalloonWindowClassReference != "")
        {
          try
          {
            System.Type type = System.Type.GetType(this.Configuration.BalloonWindowClassReference);
            if (type != null)
              balloonWindow = (QBalloonWindow) Activator.CreateInstance(type);
          }
          catch (Exception ex)
          {
            throw new InvalidOperationException(QResources.GetException("QBalloon_BalloonWindowClassReference"), ex);
          }
        }
      }
      if (balloonWindow == null)
        balloonWindow = this.CreateDefaultBalloonWindow();
      if (balloonWindow.Appearance.Shape == null)
        balloonWindow.Appearance.Shape = new QShape(QBaseShapeType.RectangleShape);
      balloonWindow.BalloonManaged = true;
      if (this.Configuration.ColorSchemeSource == QBalloonWindowPropertySource.QBalloon)
        balloonWindow.OverrideColorScheme(this.ColorScheme);
      if (this.Configuration.FontSource == QBalloonWindowPropertySource.QBalloon)
        balloonWindow.OverrideFont(this.Font);
      balloonWindow.Configuration.Properties.BaseProperties = this.Configuration.BalloonWindowConfiguration.Properties;
      balloonWindow.Appearance.Properties.BaseProperties = this.Configuration.BalloonWindowAppearance.Properties;
      balloonWindow.ElementMouseEnter += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseEnter);
      balloonWindow.ElementMouseLeave += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseLeave);
      balloonWindow.ElementMouseDown += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseDown);
      balloonWindow.ElementMouseUp += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseUp);
      balloonWindow.ElementMouseClick += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseClick);
      balloonWindow.ElementLinkClick += new QMarkupTextElementEventHandler(this.MarkupTextRoot_LinkClick);
      this.m_bRecreateBalloonWindow = false;
      return balloonWindow;
    }

    protected virtual QBalloonConfiguration CreateBalloonConfigurationInstance() => new QBalloonConfiguration();

    protected virtual void OnElementMouseEnter(QMarkupTextElementEventArgs e) => this.m_oElementMouseEnterDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseEnterDelegate, (object) this, (object) e);

    protected virtual void OnElementMouseLeave(QMarkupTextElementEventArgs e) => this.m_oElementMouseLeaveDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseLeaveDelegate, (object) this, (object) e);

    protected virtual void OnElementMouseDown(QMarkupTextElementEventArgs e) => this.m_oElementMouseDownDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseDownDelegate, (object) this, (object) e);

    protected virtual void OnElementMouseUp(QMarkupTextElementEventArgs e) => this.m_oElementMouseUpDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseUpDelegate, (object) this, (object) e);

    protected virtual void OnElementMouseClick(QMarkupTextElementEventArgs e) => this.m_oElementMouseClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseClickDelegate, (object) this, (object) e);

    protected virtual void OnElementLinkClick(QMarkupTextElementEventArgs e) => this.m_oElementLinkClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementLinkClickDelegate, (object) this, (object) e);

    protected virtual void OnBalloonVisibleChanged(EventArgs e) => this.m_oBalloonVisibleChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oBalloonVisibleChangedDelegate, (object) this, (object) e);

    protected override void OnFontChanged(EventArgs e)
    {
      base.OnFontChanged(e);
      if (this.Configuration == null || this.Configuration.FontSource != QBalloonWindowPropertySource.QBalloon || this.m_oBalloonWindow == null)
        return;
      this.m_oBalloonWindow.OverrideFont(this.Font);
    }

    protected override void OnTimerElapsed(QControlTimerEventArgs e)
    {
      base.OnTimerElapsed(e);
      if (this.IsDisposed)
        return;
      if (e.TimerId == this.m_iTimerID)
      {
        for (int index = 0; index < this.m_aListeners.Count; ++index)
        {
          if (this.m_aListeners[index] is QBalloonControlListener aListener)
            aListener.TimerTick();
        }
      }
      else
      {
        if (e.TimerId != this.m_iAnimationTimerID || this.m_oBalloonWindow == null)
          return;
        int num1 = this.Configuration.AnimateTime / this.m_iAnimationTimerInterval;
        if (num1 != 0)
        {
          if (this.m_eAnimationStatus == QBalloon.QBalloonAnimationStatus.FadingIn)
          {
            int num2 = (int) this.m_oBalloonWindow.OpacityAsByte + (int) byte.MaxValue / num1;
            if (num2 < (int) byte.MaxValue)
            {
              this.m_oBalloonWindow.OpacityAsByte = (byte) num2;
              return;
            }
          }
          else if (this.m_eAnimationStatus == QBalloon.QBalloonAnimationStatus.FadingOut)
          {
            int num3 = (int) this.m_oBalloonWindow.OpacityAsByte - (int) byte.MaxValue / num1;
            if (num3 > 0)
            {
              this.m_oBalloonWindow.OpacityAsByte = (byte) num3;
              return;
            }
          }
        }
        if (this.m_eAnimationStatus == QBalloon.QBalloonAnimationStatus.FadingIn)
        {
          this.m_oBalloonWindow.OpacityAsByte = byte.MaxValue;
          if (this.m_oBalloonWindow.ControlContainer != null)
          {
            this.m_oBalloonWindow.ControlContainer.Visible = true;
            this.m_oBalloonWindow.PaintControlContainer = false;
          }
          this.OnBalloonVisibleChanged(EventArgs.Empty);
          if (this.m_oLastListener != null)
          {
            this.m_oLastListener.Hide(true);
          }
          else
          {
            this.HideBalloonWindow((QBalloonControlListener) null);
            return;
          }
        }
        else if (this.m_eAnimationStatus == QBalloon.QBalloonAnimationStatus.FadingOut)
          this.HideBalloonWindowNow();
        if (this.m_iAnimationTimerID == 0)
          return;
        this.StopTimer(this.m_iAnimationTimerID);
        this.m_iAnimationTimerID = 0;
        this.m_eAnimationStatus = QBalloon.QBalloonAnimationStatus.None;
      }
    }

    internal QBalloonControlListener GetListenerForControl(Control control)
    {
      for (int index = 0; index < this.m_aListeners.Count; ++index)
      {
        QBalloonControlListener aListener = (QBalloonControlListener) this.m_aListeners[index];
        if (aListener.Control == control)
          return aListener;
      }
      return (QBalloonControlListener) null;
    }

    private Control GetControlFromListeners(IntPtr handle)
    {
      for (int index = this.m_aListeners.Count - 1; index >= 0; --index)
      {
        Control control = ((QBalloonControlListener) this.m_aListeners[index]).Control;
        if (control.IsDisposed)
          this.RemoveListener(control);
        else if (control.IsHandleCreated && control.Handle == handle)
          return control;
      }
      return (Control) null;
    }

    private void SecureTimer()
    {
      if (this.Configuration.Enabled && this.m_aListeners.Count > 0)
      {
        if (this.m_iTimerID != 0)
          return;
        this.m_iTimerID = this.StartTimer(this.m_iTimerInterval);
      }
      else
      {
        if (this.m_iTimerID == 0)
          return;
        this.StopTimer(this.m_iTimerID);
        this.m_iTimerID = 0;
      }
    }

    private void Initialize()
    {
      this.m_oBalloonConfigurationChangedEventHandler = new EventHandler(this.Balloon_ConfigurationChanged);
      this.m_oBalloonConfiguration = this.CreateBalloonConfigurationInstance();
      this.m_oBalloonConfiguration.ConfigurationChanged += this.m_oBalloonConfigurationChangedEventHandler;
      this.m_oBalloonWindowConfigurationChangedEventHandler = new EventHandler(this.Balloon_BalloonWindowConfigurationChanged);
      this.m_oBalloonConfiguration.BalloonWindowConfigurationChanged += this.m_oBalloonWindowConfigurationChangedEventHandler;
      this.m_oBalloonWindowAppearanceChangedEventHandler = new EventHandler(this.BalloonWindowAppearance_AppearanceChanged);
      this.m_oBalloonConfiguration.BalloonWindowAppearanceChanged += this.m_oBalloonWindowAppearanceChangedEventHandler;
      this.m_oBalloonConfigurationBalloonWindowShapeChangedEventHander = new EventHandler(this.Configuration_BalloonWindowShapeChanged);
      this.m_oBalloonConfiguration.BalloonWindowShapeChanged += this.m_oBalloonConfigurationBalloonWindowShapeChangedEventHander;
      this.SecureTimer();
    }

    private void Balloon_ConfigurationChanged(object sender, EventArgs e)
    {
      this.SecureTimer();
      if (this.m_oBalloonWindow == null)
        return;
      if (this.Configuration.ColorSchemeSource == QBalloonWindowPropertySource.QBalloon && !this.m_oBalloonWindow.ColorSchemeOverridden)
        this.m_oBalloonWindow.OverrideColorScheme(this.ColorScheme);
      if (this.Configuration.ColorSchemeSource != QBalloonWindowPropertySource.QBalloon && this.m_oBalloonWindow.ColorSchemeOverridden)
        this.m_oBalloonWindow.RestoreColorScheme();
      if (this.Configuration.FontSource == QBalloonWindowPropertySource.QBalloon && !this.m_oBalloonWindow.FontOverridden)
        this.m_oBalloonWindow.OverrideFont(this.Font);
      if (this.Configuration.FontSource == QBalloonWindowPropertySource.QBalloon || !this.m_oBalloonWindow.FontOverridden)
        return;
      this.m_oBalloonWindow.RestoreFont();
    }

    private void Balloon_BalloonWindowConfigurationChanged(object sender, EventArgs e)
    {
      if (this.m_oBalloonWindow == null || this.m_oBalloonWindow.IsDisposed)
        return;
      this.m_oBalloonWindow.Configuration.Properties.BaseProperties = this.Configuration.BalloonWindowConfiguration.Properties;
      this.m_oBalloonWindow.Configuration.RaiseConfigurationChanged();
    }

    private void Configuration_BalloonWindowShapeChanged(object sender, EventArgs e) => this.m_bRecreateBalloonWindow = true;

    private void BalloonWindowAppearance_AppearanceChanged(object sender, EventArgs e)
    {
      if (this.m_oBalloonWindow == null || this.m_oBalloonWindow.IsDisposed)
        return;
      this.m_oBalloonWindow.Appearance.Properties.BaseProperties = this.Configuration.BalloonWindowAppearance.Properties;
      this.m_oBalloonWindow.Appearance.RaiseAppearanceChanged();
    }

    private void MarkupTextRoot_MouseEnter(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseEnter(e);

    private void MarkupTextRoot_MouseLeave(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseLeave(e);

    private void MarkupTextRoot_MouseDown(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseDown(e);

    private void MarkupTextRoot_MouseUp(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseUp(e);

    private void MarkupTextRoot_MouseClick(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseClick(e);

    private void MarkupTextRoot_LinkClick(object sender, QMarkupTextElementEventArgs e) => this.OnElementLinkClick(e);

    internal int ReShowDelayTickCount
    {
      get => this.m_iReShowDelayTickCount;
      set => this.m_iReShowDelayTickCount = value;
    }

    internal void ShowBalloonWindow(
      Point screenLocation,
      QBalloonControlListener listener,
      Control target,
      string text,
      QBalloonWindowPositioning positioning,
      bool ignoreWorkingArea)
    {
      if (QBalloon.CurrentVisibleBalloon != null)
      {
        if (QBalloon.CurrentVisibleBalloon == this && QBalloon.CurrentVisibleBalloon.BalloonWindow.TargetControl == target && QBalloon.CurrentVisibleBalloon.BalloonWindow.Visible && (QBalloon.CurrentVisibleBalloon.BalloonWindow.MarkupText ?? "") == (text ?? ""))
        {
          if (this.m_eAnimationStatus != QBalloon.QBalloonAnimationStatus.FadingOut)
            return;
          this.m_eAnimationStatus = QBalloon.QBalloonAnimationStatus.FadingIn;
          if (listener != null)
          {
            this.m_oLastListener = listener;
            listener.Enabled = true;
            listener.RemoveAfterHide = false;
          }
          if (this.m_iAnimationTimerID != 0)
            return;
          this.m_iAnimationTimerID = this.StartTimer(this.m_iAnimationTimerInterval);
          return;
        }
        QBalloon.CurrentVisibleBalloon.HideBalloonWindowNow();
      }
      this.m_oLastListener = listener;
      if (this.m_bRecreateBalloonWindow && this.m_oBalloonWindow != null)
      {
        this.m_oBalloonWindow.Closing -= new CancelEventHandler(this.m_oBalloonWindow_Closing);
        this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.m_oBalloonWindow_Disposed));
        this.m_oBalloonWindow.Close();
        this.m_oBalloonWindow.Dispose();
        this.m_oBalloonWindow = (QBalloonWindow) null;
      }
      if (this.m_oBalloonWindow == null)
      {
        this.m_oBalloonWindow = this.CreateBalloonWindow();
        this.m_oBalloonWindow.Closing += new CancelEventHandler(this.m_oBalloonWindow_Closing);
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.m_oBalloonWindow_Disposed), (object) this.m_oBalloonWindow, "Disposed"));
      }
      this.m_oBalloonWindow.MarkupText = text;
      if (this.m_oBalloonWindow.OwnerControl != null)
      {
        this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.OwnerControl_LocationChanged));
        this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.OwnerControl_VisibleChanged));
      }
      if (target != null)
        this.m_oBalloonWindow.OwnerControl = target.TopLevelControl;
      this.m_oBalloonWindow.RepositionToOwnerControl(false);
      if (this.m_oBalloonWindow.OwnerControl != null)
      {
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.OwnerControl_LocationChanged), (object) this.m_oBalloonWindow.OwnerControl, "LocationChanged"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.OwnerControl_VisibleChanged), (object) this.m_oBalloonWindow.OwnerControl, "VisibleChanged"));
      }
      this.m_oBalloonWindow.CalculateBounds(screenLocation, target, this.Configuration.FlipWindow, positioning, ignoreWorkingArea);
      this.m_oBalloonWindow.AnimateTime = this.Configuration.AnimateTime;
      this.m_oBalloonWindow.AnimateWindow = this.Configuration.UsedAnimateWindow;
      this.m_oBalloonWindow.PaintControlContainer = true;
      bool flag = this.Configuration.UsedAnimateWindow && !this.m_oBalloonWindow.Visible;
      if (flag)
        this.m_oBalloonWindow.OpacityAsByte = (byte) 0;
      else
        this.m_oBalloonWindow.OpacityAsByte = byte.MaxValue;
      this.m_oBalloonWindow.Visible = true;
      if (flag)
      {
        this.m_eAnimationStatus = QBalloon.QBalloonAnimationStatus.FadingIn;
        if (this.m_iAnimationTimerID == 0)
          this.m_iAnimationTimerID = this.StartTimer(this.m_iAnimationTimerInterval);
      }
      else
      {
        if (this.m_oBalloonWindow.ControlContainer != null)
          this.m_oBalloonWindow.ControlContainer.Visible = true;
        this.OnBalloonVisibleChanged(EventArgs.Empty);
        this.m_oLastListener.Hide(true);
      }
      QBalloon.CurrentVisibleBalloon = this;
    }

    internal void HideBalloonWindow(QBalloonControlListener listener)
    {
      if (this.m_oBalloonWindow == null)
        return;
      this.m_oLastListener = listener;
      if (this.Configuration.UsedAnimateWindow)
      {
        if (this.m_oBalloonWindow.ControlContainer != null)
        {
          this.m_oBalloonWindow.PaintControlContainer = true;
          this.m_oBalloonWindow.ControlContainer.Hide();
          if (this.m_oBalloonWindow.OwnerControl != null)
            this.m_oBalloonWindow.OwnerControl.Refresh();
        }
        this.m_eAnimationStatus = QBalloon.QBalloonAnimationStatus.FadingOut;
        if (this.m_iAnimationTimerID != 0)
          return;
        this.m_iAnimationTimerID = this.StartTimer(this.m_iAnimationTimerInterval);
      }
      else
        this.HideBalloonWindowNow();
    }

    private void HideBalloonWindowNow()
    {
      if (this.m_oBalloonWindow == null)
        return;
      this.m_oBalloonWindow.Hide();
      this.m_oBalloonWindow.PaintControlContainer = false;
      this.m_oBalloonWindow.TargetControl = (Control) null;
      if (this.m_oBalloonWindow.OwnerControl != null)
      {
        this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.OwnerControl_LocationChanged));
        this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.OwnerControl_VisibleChanged));
      }
      this.m_oBalloonWindow.OwnerControl = (Control) null;
      if (this.m_oBalloonWindow.ControlContainer != null)
        this.m_oBalloonWindow.ControlContainer.Hide();
      if (QBalloon.CurrentVisibleBalloon == this)
        QBalloon.CurrentVisibleBalloon = (QBalloon) null;
      this.StopTimer(this.m_iAnimationTimerID);
      this.m_iAnimationTimerID = 0;
      this.m_eAnimationStatus = QBalloon.QBalloonAnimationStatus.None;
      this.OnBalloonVisibleChanged(EventArgs.Empty);
    }

    private bool IsTopMost(Control target) => target != null && target.IsHandleCreated && NativeHelper.IsTopMost(target.Handle);

    private void OwnerControl_LocationChanged(object sender, EventArgs e)
    {
      if (this.m_oBalloonWindow == null)
        return;
      this.m_oBalloonWindow.RepositionToOwnerControl(true);
    }

    private void OwnerControl_VisibleChanged(object sender, EventArgs e)
    {
      Control control = sender as Control;
      if (this.m_oBalloonWindow == null || control == null || this.m_oLastListener == null || QBalloon.CurrentVisibleBalloon != this || this.m_oBalloonWindow.TargetControl != control || !this.m_oBalloonWindow.Visible || control.Visible)
        return;
      this.m_oLastListener.Hide(false);
    }

    private void m_oBalloonWindow_Closing(object sender, CancelEventArgs e)
    {
      if (e.Cancel)
        return;
      e.Cancel = true;
      if (this.m_oLastListener != null)
        this.m_oLastListener.Hide(false);
      else
        this.m_oBalloonWindow.Hide();
    }

    private void m_oBalloonWindow_Disposed(object sender, EventArgs e)
    {
      if (sender != this.m_oBalloonWindow)
        return;
      this.m_oBalloonWindow = (QBalloonWindow) null;
    }

    internal enum QBalloonAnimationStatus
    {
      None,
      FadingIn,
      FadingOut,
    }
  }
}
