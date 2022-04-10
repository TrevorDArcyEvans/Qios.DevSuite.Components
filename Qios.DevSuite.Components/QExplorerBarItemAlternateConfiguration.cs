// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QExplorerBarItemAlternateConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  public class QExplorerBarItemAlternateConfiguration : QFastPropertyBagHost
  {
    protected const int PropHasChildItemsMaskReverse = 0;
    protected const int PropHasChildItemsMask = 1;
    protected const int CurrentPropertyCount = 2;
    protected const int TotalPropertyCount = 2;
    private static Image m_oDefaultHasChildItemsMaskReverse;
    private static Image m_oDefaultItemHasChildItemsMask;
    private static Image m_oDefaultGroupItemHasChildItemsMask;
    private Image m_oDefaultHasChildItemsMask;
    private QWeakDelegate m_oConfigurationChangedDelegate;

    internal QExplorerBarItemAlternateConfiguration(QExplorerItemType itemType)
    {
      if (QExplorerBarItemAlternateConfiguration.m_oDefaultHasChildItemsMaskReverse == null)
        QExplorerBarItemAlternateConfiguration.m_oDefaultHasChildItemsMaskReverse = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.ExplorerBarHasChildItemsMaskReverse.png"));
      if (QExplorerBarItemAlternateConfiguration.m_oDefaultGroupItemHasChildItemsMask == null)
        QExplorerBarItemAlternateConfiguration.m_oDefaultGroupItemHasChildItemsMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.ExplorerBarHasChildItemsMask.png"));
      if (QExplorerBarItemAlternateConfiguration.m_oDefaultItemHasChildItemsMask == null)
        QExplorerBarItemAlternateConfiguration.m_oDefaultItemHasChildItemsMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.SmallArrowRightMask.png"));
      this.m_oDefaultHasChildItemsMask = itemType != QExplorerItemType.GroupItem ? QExplorerBarItemAlternateConfiguration.m_oDefaultItemHasChildItemsMask : QExplorerBarItemAlternateConfiguration.m_oDefaultGroupItemHasChildItemsMask;
      this.Properties.DefineProperty(0, (object) null);
      this.Properties.DefineProperty(1, (object) null);
      this.Properties.PropertyChanged += new QFastPropertyChangedEventHandler(this.Properties_PropertyChanged);
    }

    protected override int GetRequestedCount() => 2;

    [Description("Gets raised when a property of the configuration is changed")]
    [Category("QEvents")]
    [QWeakEvent]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    internal void RedefineProperties(QExplorerItemType itemType)
    {
      if (itemType == QExplorerItemType.GroupItem)
      {
        this.m_oDefaultHasChildItemsMask = QExplorerBarItemAlternateConfiguration.m_oDefaultGroupItemHasChildItemsMask;
      }
      else
      {
        if (itemType != QExplorerItemType.MenuItem)
          return;
        this.m_oDefaultHasChildItemsMask = QExplorerBarItemAlternateConfiguration.m_oDefaultItemHasChildItemsMask;
      }
    }

    [QPropertyIndex(0)]
    [Category("QAppearance")]
    [Description("Gets or sets the base image that is used when a QExplorerBar has more items then visible. In the Mask the Color Red is replaced by the ExplorerBarHasMoreChildItemsColor.")]
    public Image HasChildItemsMaskReverse
    {
      get => this.Properties.GetProperty(0) as Image;
      set => this.Properties.SetProperty(0, (object) value);
    }

    [Browsable(false)]
    public virtual Image UsedHasChildItemsMaskReverse => this.HasChildItemsMaskReverse != null ? this.HasChildItemsMaskReverse : QExplorerBarItemAlternateConfiguration.m_oDefaultHasChildItemsMaskReverse;

    [QPropertyIndex(1)]
    [Category("QAppearance")]
    [Description("Gets or sets the base image that is used when a QExplorerBar has more items then visible. In the Mask the Color Red is replaced by the ExplorerBarHasMoreChildItemsColor.")]
    public Image HasChildItemsMask
    {
      get => this.Properties.GetProperty(1) as Image;
      set => this.Properties.SetProperty(1, (object) value);
    }

    [Browsable(false)]
    public virtual Image UsedHasChildItemsMask => this.HasChildItemsMask != null ? this.HasChildItemsMask : this.m_oDefaultHasChildItemsMask;

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    private void Properties_PropertyChanged(object sender, QFastPropertyChangedEventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
