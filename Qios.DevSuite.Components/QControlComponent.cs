// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QControlComponent
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  [Designer(typeof (QControlComponentDesigner), typeof (IDesigner))]
  public abstract class QControlComponent : Component, IQWeakEventPublisher
  {
    private bool m_bWeakEventHandlers = true;
    private bool m_bIsDisposed;
    private ArrayList m_aSetTimers;
    private QColorScheme m_oColorScheme;
    private QFontScope m_eFontScope;
    private Font m_oLocalFont;
    private Font m_oFont;
    private EventHandler m_oColorSchemeColorsChangedEventHandler;
    private Container m_oComponents;
    private QWeakDelegate m_oTimerElapsedDelegate;
    private QWeakDelegate m_oFontChangedDelegate;

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when a timer that was set with the StartTimer method elapses")]
    public event QControlTimerEventHandler TimerElapsed
    {
      add => this.m_oTimerElapsedDelegate = QWeakDelegate.Combine(this.m_oTimerElapsedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oTimerElapsedDelegate = QWeakDelegate.Remove(this.m_oTimerElapsedDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when the font changes")]
    [QWeakEvent]
    public event EventHandler FontChanged
    {
      add => this.m_oFontChangedDelegate = QWeakDelegate.Combine(this.m_oFontChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oFontChangedDelegate = QWeakDelegate.Remove(this.m_oFontChangedDelegate, (Delegate) value);
    }

    protected QControlComponent(IContainer container)
    {
      if (container == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (container)));
      container.Add((IComponent) this);
      this.InternalConstruct();
    }

    protected QControlComponent() => this.InternalConstruct();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    [Browsable(false)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    [Browsable(false)]
    public bool IsDisposed => this.m_bIsDisposed;

    public int StartTimer(int interval)
    {
      QTimerCallbackDelegate lpTimerFunc = new QTimerCallbackDelegate(this.QTimerCallback);
      GCHandle handle = GCHandle.Alloc((object) lpTimerFunc);
      int int32 = NativeMethods.SetTimer(IntPtr.Zero, IntPtr.Zero, (uint) interval, lpTimerFunc).ToInt32();
      QTimerDefinition timerWithId = this.FindTimerWithID(int32);
      if (timerWithId != null)
      {
        timerWithId.TimerInteval = interval;
      }
      else
      {
        if (this.m_aSetTimers == null)
          this.m_aSetTimers = new ArrayList();
        this.m_aSetTimers.Add((object) new QTimerDefinition(int32, interval, handle));
      }
      return int32;
    }

    public void StopTimer(int timerId)
    {
      NativeMethods.KillTimer(IntPtr.Zero, new IntPtr(timerId));
      QTimerDefinition timerWithId = this.FindTimerWithID(timerId);
      if (timerWithId == null)
        return;
      if (timerWithId.GCHandle.IsAllocated)
        timerWithId.GCHandle.Free();
      this.m_aSetTimers.Remove((object) timerWithId);
    }

    private void QTimerCallback(IntPtr handle, uint msg, uint idEvent, int dwTime)
    {
      try
      {
        this.OnTimerElapsed(new QControlTimerEventArgs((int) idEvent));
      }
      catch (Exception ex)
      {
        Application.OnThreadException(ex);
      }
    }

    protected virtual void OnFontChanged(EventArgs e) => this.m_oFontChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oFontChangedDelegate, (object) this, (object) e);

    protected virtual void OnTimerElapsed(QControlTimerEventArgs e) => this.m_oTimerElapsedDelegate = QWeakDelegate.InvokeDelegate(this.m_oTimerElapsedDelegate, (object) this, (object) e);

    private QTimerDefinition FindTimerWithID(int timerID)
    {
      if (this.m_aSetTimers == null)
        return (QTimerDefinition) null;
      for (int index = 0; index < this.m_aSetTimers.Count; ++index)
      {
        QTimerDefinition aSetTimer = (QTimerDefinition) this.m_aSetTimers[index];
        if (aSetTimer.TimerID == timerID)
          return aSetTimer;
      }
      return (QTimerDefinition) null;
    }

    private void InternalConstruct()
    {
      this.m_oComponents = new Container();
      this.m_oColorSchemeColorsChangedEventHandler = new EventHandler(this.ColorScheme_ColorsChanged);
      this.m_oColorScheme = new QColorScheme();
      this.m_oColorScheme.ColorsChanged += this.m_oColorSchemeColorsChangedEventHandler;
      QGlobalFont.Instance.FontChanged += new EventHandler(this.QGlobalFontInstance_FontChanged);
      this.m_oLocalFont = Control.DefaultFont;
      this.m_eFontScope = QFontScope.Global;
      this.Font = QGlobalFont.Instance.Font;
    }

    public bool ShouldSerializeColorScheme() => this.ColorScheme.ShouldSerialize();

    public void ResetColorScheme() => this.ColorScheme.Reset();

    [Description("The ColorScheme that is used.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QColorScheme ColorScheme
    {
      get => this.m_oColorScheme;
      set
      {
        if (this.m_oColorScheme == value)
          return;
        if (this.m_oColorScheme != null)
          this.m_oColorScheme.ColorsChanged -= this.m_oColorSchemeColorsChangedEventHandler;
        this.m_oColorScheme = value;
        if (this.m_oColorScheme == null)
          return;
        this.m_oColorScheme.ColorsChanged += this.m_oColorSchemeColorsChangedEventHandler;
      }
    }

    [Localizable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Font Font
    {
      get => this.m_oFont;
      set
      {
        this.m_oFont = value;
        this.OnFontChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Localizable(true)]
    [DefaultValue(QFontScope.Global)]
    [Description("The scope of the font. When the scope is set to Local the LocalFont is used, else the Font is defined by Windows or the QGlobalFont.")]
    public QFontScope FontScope
    {
      get => this.m_eFontScope;
      set
      {
        if (this.m_eFontScope == value)
          return;
        this.m_eFontScope = value;
        this.SetFontToFontScope();
      }
    }

    public bool ShouldSerializeLocalFont() => !object.Equals((object) this.LocalFont, (object) Control.DefaultFont);

    public void ResetLocalFont() => this.LocalFont = Control.DefaultFont;

    [Category("QAppearance")]
    [Localizable(true)]
    [Description("The LocalFont is used when the FontScope is set to Local")]
    public Font LocalFont
    {
      get => this.m_oLocalFont;
      set
      {
        if (this.m_oLocalFont == value)
          return;
        this.m_oLocalFont = value;
        this.SetFontToFontScope();
      }
    }

    private void SetFontToFontScope()
    {
      if (this.m_eFontScope == QFontScope.Windows)
        this.Font = SystemInformation.MenuFont;
      else if (this.m_eFontScope == QFontScope.Global)
      {
        this.Font = QGlobalFont.Instance.Font;
      }
      else
      {
        if (this.m_eFontScope != QFontScope.Local)
          return;
        this.Font = this.m_oLocalFont;
      }
    }

    protected override void Dispose(bool disposing)
    {
      this.m_bIsDisposed = true;
      if (this.m_oColorScheme != null && !this.m_oColorScheme.IsDisposed)
      {
        this.m_oColorScheme.ColorsChanged -= this.m_oColorSchemeColorsChangedEventHandler;
        this.m_oColorScheme.Dispose();
      }
      if (this.m_aSetTimers != null)
      {
        for (int index = this.m_aSetTimers.Count - 1; index >= 0; --index)
        {
          if (this.m_aSetTimers[index] is QTimerDefinition aSetTimer)
            this.StopTimer(aSetTimer.TimerID);
        }
        this.m_aSetTimers = (ArrayList) null;
      }
      if (disposing && this.m_oComponents != null)
        this.m_oComponents.Dispose();
      base.Dispose(disposing);
    }

    private void ColorScheme_ColorsChanged(object sender, EventArgs e)
    {
    }

    private void QGlobalFontInstance_FontChanged(object sender, EventArgs e) => this.SetFontToFontScope();
  }
}
