// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMessagesListener
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal sealed class QMessagesListener : NativeWindow, IQWeakEventPublisher
  {
    private bool m_bWeakEventHandlers = true;
    private static QMessagesListener m_oListener = new QMessagesListener();
    private QWeakDelegate m_oWindowsXPThemeChangedDelegate;
    private QWeakDelegate m_oSystemColorsChangedDelegate;

    internal QMessagesListener() => this.CreateHandle(new CreateParams());

    [QWeakEvent]
    public event EventHandler WindowsXPThemeChanged
    {
      add => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.Combine(this.m_oWindowsXPThemeChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.Remove(this.m_oWindowsXPThemeChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    public event EventHandler SystemColorsChanged
    {
      add => this.m_oSystemColorsChangedDelegate = QWeakDelegate.Combine(this.m_oSystemColorsChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oSystemColorsChangedDelegate = QWeakDelegate.Remove(this.m_oSystemColorsChangedDelegate, (Delegate) value);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    public static QMessagesListener Listener => QMessagesListener.m_oListener;

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case 21:
          this.OnSystemColorsChanged(EventArgs.Empty);
          break;
        case 794:
          this.OnWindowsXPThemeChanged(EventArgs.Empty);
          break;
      }
      base.WndProc(ref m);
    }

    private void OnWindowsXPThemeChanged(EventArgs e) => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oWindowsXPThemeChangedDelegate, (object) this, (object) e);

    private void OnSystemColorsChanged(EventArgs e) => this.m_oSystemColorsChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oSystemColorsChangedDelegate, (object) this, (object) e);
  }
}
