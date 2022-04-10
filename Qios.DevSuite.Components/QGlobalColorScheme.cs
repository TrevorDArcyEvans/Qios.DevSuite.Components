// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QGlobalColorScheme
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public sealed class QGlobalColorScheme : QColorScheme
  {
    private QWeakReferenceCollection m_aColorSchemes;
    private bool m_bInheritCurrentThemeFromWindows = true;

    internal QGlobalColorScheme(bool isGlobalColorScheme, bool addDefaultValues)
      : base(isGlobalColorScheme, addDefaultValues, false)
    {
      QMessagesListener.Listener.WindowsXPThemeChanged += new EventHandler(this.MessageListener_WindowsXPThemeChanged);
      QMessagesListener.Listener.SystemColorsChanged += new EventHandler(this.MessageListener_SystemColorsChanged);
      this.m_aColorSchemes = new QWeakReferenceCollection();
      this.SetCurrentThemeToWindows();
      this.Scope = QColorSchemeScope.All;
    }

    protected override QColorSchemeBase CreateInstanceForClone() => (QColorSchemeBase) new QGlobalColorScheme(true, false);

    [Description("Indicates if the CurrentTheme is inherited from Windows. When this is set to true. The theme is matched")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("QAppearance")]
    [DefaultValue(true)]
    public bool InheritCurrentThemeFromWindows
    {
      get => this.m_bInheritCurrentThemeFromWindows;
      set
      {
        this.m_bInheritCurrentThemeFromWindows = value;
        if (!this.m_bInheritCurrentThemeFromWindows)
          return;
        this.SetCurrentThemeToWindows();
      }
    }

    [DefaultValue(QColorSchemeScope.All)]
    public override QColorSchemeScope Scope
    {
      get => base.Scope;
      set => base.Scope = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override bool InheritCurrentThemeFromGlobal
    {
      get => false;
      set
      {
      }
    }

    public override bool ShouldSerializeCurrentTheme() => !this.InheritCurrentThemeFromWindows && this.CurrentTheme != "Default";

    public override void ResetCurrentTheme()
    {
      if (!this.InheritCurrentThemeFromWindows)
        this.CurrentTheme = "Default";
      else
        this.SetCurrentThemeToWindows();
    }

    public void SetCurrentThemeToWindows()
    {
      QThemeInfo currentThemeInfo = NativeHelper.GetCurrentThemeInfo();
      if (currentThemeInfo == null)
      {
        if (SystemInformation.HighContrast)
          this.CurrentTheme = "HighContrast";
        else
          this.CurrentTheme = "Default";
      }
      else
      {
        QThemeInfo matchingTheme = this.Themes.FindMatchingTheme(currentThemeInfo);
        if (matchingTheme != null)
          this.CurrentTheme = matchingTheme.ThemeName;
        else
          this.CurrentTheme = "Default";
      }
    }

    internal void RegisterColorScheme(QColorSchemeBase colorScheme) => this.m_aColorSchemes.AddObject((object) colorScheme);

    internal void UnregisterColorScheme(QColorSchemeBase colorScheme) => this.m_aColorSchemes.RemoveObject((object) colorScheme, true);

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void RaiseColorsChanged()
    {
      base.RaiseColorsChanged();
      if (this.ColorsChangedSuspendCount > 0)
        return;
      for (int index = this.m_aColorSchemes.Count - 1; index >= 0; --index)
      {
        QWeakReference aColorScheme = this.m_aColorSchemes[index];
        if (aColorScheme != null && !aColorScheme.Finalized && aColorScheme.IsAlive)
          ((QColorSchemeBase) aColorScheme.Target).RaiseColorsChanged();
        else
          this.m_aColorSchemes.RemoveAt(index);
      }
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void RaiseThemesChanged()
    {
      base.RaiseThemesChanged();
      if (this.ColorsChangedSuspendCount > 0 || this.m_aColorSchemes == null)
        return;
      for (int index = this.m_aColorSchemes.Count - 1; index >= 0; --index)
      {
        QWeakReference aColorScheme = this.m_aColorSchemes[index];
        if (aColorScheme != null && !aColorScheme.Finalized && aColorScheme.IsAlive)
          ((QColorSchemeBase) aColorScheme.Target).RaiseThemesChanged();
        else
          this.m_aColorSchemes.RemoveAt(index);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (QMessagesListener.Listener != null)
        {
          QMessagesListener.Listener.WindowsXPThemeChanged -= new EventHandler(this.MessageListener_WindowsXPThemeChanged);
          QMessagesListener.Listener.SystemColorsChanged -= new EventHandler(this.MessageListener_SystemColorsChanged);
        }
        this.m_aColorSchemes.Clear();
      }
      base.Dispose(disposing);
    }

    private void MessageListener_WindowsXPThemeChanged(object sender, EventArgs e)
    {
      if (!this.m_bInheritCurrentThemeFromWindows)
        return;
      this.SetCurrentThemeToWindows();
    }

    private void MessageListener_SystemColorsChanged(object sender, EventArgs e)
    {
      if (!this.m_bInheritCurrentThemeFromWindows)
        return;
      this.SetCurrentThemeToWindows();
    }
  }
}
