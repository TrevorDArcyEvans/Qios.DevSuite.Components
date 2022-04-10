// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QGlobalFont
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QGlobalFont : IQWeakEventPublisher
  {
    private static QGlobalFont m_oInstance;
    private bool m_bWeakEventHandlers = true;
    private bool m_bInheritFromWindows = true;
    private Font m_oFont;
    private QWeakDelegate m_oFontChangedDelegate;
    private QWeakDelegate m_oSystemFontChangedDelegate;

    private QGlobalFont()
    {
      this.m_oFont = SystemInformation.MenuFont;
      QMessagesListener.Listener.SystemColorsChanged += new EventHandler(this.Listener_SystemColorsChanged);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    [QWeakEvent]
    public event EventHandler SystemFontChanged
    {
      add => this.m_oSystemFontChangedDelegate = QWeakDelegate.Combine(this.m_oSystemFontChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oSystemFontChangedDelegate = QWeakDelegate.Remove(this.m_oSystemFontChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    public event EventHandler FontChanged
    {
      add => this.m_oFontChangedDelegate = QWeakDelegate.Combine(this.m_oFontChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oFontChangedDelegate = QWeakDelegate.Remove(this.m_oFontChangedDelegate, (Delegate) value);
    }

    private static void SecureGlobalFont()
    {
      if (QGlobalFont.m_oInstance != null)
        return;
      QGlobalFont.m_oInstance = new QGlobalFont();
    }

    public static QGlobalFont Instance
    {
      get
      {
        QGlobalFont.SecureGlobalFont();
        return QGlobalFont.m_oInstance;
      }
    }

    public bool ShouldSerializeFont() => !object.Equals((object) this.m_oFont, (object) SystemInformation.MenuFont);

    public void ResetFont() => this.SetFont(SystemInformation.MenuFont, this.m_bInheritFromWindows);

    [RefreshProperties(RefreshProperties.Repaint)]
    public Font Font
    {
      get => this.m_oFont;
      set => this.SetFont(value, false);
    }

    [DefaultValue(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public bool InheritFromWindows
    {
      get => this.m_bInheritFromWindows;
      set
      {
        if (this.m_bInheritFromWindows == value)
          return;
        this.m_bInheritFromWindows = value;
        if (!this.m_bInheritFromWindows)
          return;
        this.SetFont(SystemInformation.MenuFont, this.m_bInheritFromWindows);
      }
    }

    protected virtual void OnSystemFontChanged(EventArgs e) => this.m_oSystemFontChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oSystemFontChangedDelegate, (object) this, (object) e);

    protected virtual void OnFontChanged(EventArgs e) => this.m_oFontChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oFontChangedDelegate, (object) this, (object) e);

    private void SetFont(Font font, bool inheritFromWindows)
    {
      if (this.m_oFont == font && this.m_bInheritFromWindows == inheritFromWindows)
        return;
      this.m_oFont = font;
      this.m_bInheritFromWindows = inheritFromWindows;
      this.OnFontChanged(EventArgs.Empty);
    }

    private void Listener_SystemColorsChanged(object sender, EventArgs e)
    {
      this.OnSystemFontChanged(EventArgs.Empty);
      if (!this.m_bInheritFromWindows)
        return;
      this.SetFont(SystemInformation.MenuFont, this.m_bInheritFromWindows);
    }
  }
}
