// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QExplorerBarConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QCommandConfigurationTypeConverter))]
  public class QExplorerBarConfiguration : IQWeakEventPublisher
  {
    private bool m_bWeakEventHandlers = true;
    private bool m_bFocusRectangleVisible;
    private QPadding m_oExplorerBarPadding;
    private QShortcutScope m_eShortcutScope = QShortcutScope.Application;
    private QWeakDelegate m_oConfigurationChangedDelegate;

    public QExplorerBarConfiguration() => this.SetToDefaultValues();

    [Description("Gets raised when a property of the configuration is changed")]
    [Category("QEvents")]
    [QWeakEvent]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    [DefaultValue(QShortcutScope.Application)]
    [Description("Gets or sets when Shortcuts must react. This can be on the complete application or parent Form.")]
    [Category("QAppearance")]
    public virtual QShortcutScope ShortcutScope
    {
      get => this.m_eShortcutScope;
      set
      {
        this.m_eShortcutScope = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Determines if the focus rectangle is visible to indicate which menuitem has the keyboard focus")]
    [DefaultValue(true)]
    public bool FocusRectangleVisible
    {
      get => this.m_bFocusRectangleVisible;
      set
      {
        this.m_bFocusRectangleVisible = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the padding between the edge and the contents of a QExplorerBar")]
    [DefaultValue(typeof (QPadding), "10,10,10,10")]
    [Category("QAppearance")]
    public QPadding ExplorerBarPadding
    {
      get => this.m_oExplorerBarPadding;
      set
      {
        this.m_oExplorerBarPadding = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    public void SetToDefaultValues() => QMisc.SetToDefaultValues((object) this);

    public bool IsSetToDefaultValues() => QMisc.IsSetToDefaultValues((object) this);

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);
  }
}
