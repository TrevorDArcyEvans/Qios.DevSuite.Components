// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Qios.DevSuite.Components
{
  public class QCompositeConfiguration : QGroupPartConfiguration
  {
    protected internal const int PropAppearance = 15;
    protected internal const int PropLayout = 16;
    protected internal const int PropExpandBehavior = 17;
    protected internal const int PropExpandingDelay = 18;
    protected internal const int PropPressedBehaviour = 19;
    protected internal const int PropInheritWindowsSettings = 20;
    protected internal const int PropHotkeyPrefixVisibilityType = 21;
    protected internal const int PropExpandDirection = 22;
    protected internal const int PropIconBackgroundVisible = 23;
    protected internal const int PropIconBackgroundMargin = 24;
    protected internal const int PropIconBackgroundSize = 25;
    protected internal const int PropHotkeyWindowConfiguration = 26;
    protected internal const int PropHotkeyWindowShowBehavior = 27;
    protected internal const int PropScrollConfiguration = 28;
    protected new const int CurrentPropertyCount = 14;
    protected new const int TotalPropertyCount = 29;
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QCompositeConfiguration()
    {
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(4, (object) true);
      this.Properties.DefineProperty(5, (object) true);
      this.Properties.DefineProperty(2, (object) true);
      this.Properties.DefineProperty(3, (object) true);
      this.Properties.DefineProperty(1, (object) new QPadding(2, 2, 2, 2));
      this.Properties.DefineProperty(22, (object) QCompositeExpandDirection.Right);
      this.Properties.DefineProperty(17, (object) (QCompositeExpandBehavior.AutoExpand | QCompositeExpandBehavior.AutoChangeExpand));
      this.Properties.DefineProperty(18, (object) 200);
      this.Properties.DefineProperty(19, (object) QCompositePressedBehaviour.MovePressedItem);
      this.Properties.DefineProperty(20, (object) false);
      this.Properties.DefineProperty(21, (object) QHotkeyVisibilityType.Always);
      this.Properties.DefineProperty(23, (object) false);
      this.Properties.DefineProperty(24, (object) new QMargin(2, 0, 0, 0));
      this.Properties.DefineProperty(25, (object) 23);
      this.Properties.DefineProperty(27, (object) QHotkeyWindowShowBehavior.Automatic);
      this.Properties.DefineProperty(16, (object) QCompositeItemLayout.Auto);
      this.Properties.DefineResettableProperty(15, (IQResettableValue) this.CreateAppearance());
      if (this.Appearance != null)
        this.Appearance.AppearanceChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(28, (IQResettableValue) this.CreateScrollConfiguration());
      if (this.ScrollConfiguration != null)
        this.ScrollConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(26, (IQResettableValue) new QHotkeyWindowConfiguration());
    }

    protected virtual QShapeAppearance CreateAppearance() => (QShapeAppearance) new QCompositeAppearance();

    protected virtual QCompositeScrollConfiguration CreateScrollConfiguration() => new QCompositeScrollConfiguration();

    protected override int GetRequestedCount() => 29;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [QPropertyIndex(12)]
    public override string ContentLayoutOrder
    {
      get => base.ContentLayoutOrder;
      set => base.ContentLayoutOrder = value;
    }

    [Description("Gets or sets the direction a QCompositeItem will expand to when it contains child QCompositeItems. This can be overridden by the QCompositeItems.")]
    [Category("QAppearance")]
    [QPropertyIndex(22)]
    public virtual QCompositeExpandDirection ExpandDirection
    {
      get => (QCompositeExpandDirection) this.Properties.GetPropertyAsValueType(22);
      set => this.Properties.SetProperty(22, (object) value);
    }

    [Description("Gets or sets the behaviour for expanded or expanding items")]
    [Category("QAppearance")]
    [QPropertyIndex(17)]
    [Editor(typeof (QFlagsEditor), typeof (UITypeEditor))]
    public virtual QCompositeExpandBehavior ExpandBehavior
    {
      get => (QCompositeExpandBehavior) this.Properties.GetPropertyAsValueType(17);
      set => this.Properties.SetProperty(17, (object) value);
    }

    [Category("QAppearance")]
    [Description("Gets or sets the delay for expanding items that have CompositeChildItems when hovering above the item")]
    [QPropertyIndex(18)]
    public virtual int ExpandingDelay
    {
      get => (int) this.Properties.GetPropertyAsValueType(18);
      set
      {
        if (value < 0 || value > 10000)
          throw new InvalidOperationException(QResources.GetException("QConfiguration_Between_Invalid", (object) 0, (object) 10000));
        this.Properties.SetProperty(18, (object) value);
      }
    }

    [Description("Gets or sets whether the QMenu should inherit WindowsSettings like showing hotkeys.")]
    [Category("QAppearance")]
    [QPropertyIndex(20)]
    public virtual bool InheritWindowsSettings
    {
      get => (bool) this.Properties.GetPropertyAsValueType(20);
      set => this.Properties.SetProperty(20, (object) value);
    }

    [QPropertyIndex(21)]
    [Description("Gets or sets if the hotkey prefix (underline) should always be visible, only when Alt is pressed or never. This is not used when the InheritWindowsSettings is true.")]
    [Category("QAppearance")]
    public virtual QHotkeyVisibilityType HotkeyPrefixVisibilityType
    {
      get => (QHotkeyVisibilityType) this.Properties.GetPropertyAsValueType(21);
      set => this.Properties.SetProperty(21, (object) value);
    }

    [Browsable(false)]
    public virtual QHotkeyVisibilityType UsedHotkeyPrefixVisibilityType
    {
      get
      {
        if (!this.InheritWindowsSettings)
          return this.HotkeyPrefixVisibilityType;
        return NativeHelper.LettersAlwaysUnderlined ? QHotkeyVisibilityType.Always : QHotkeyVisibilityType.WhenAltIsPressed;
      }
    }

    [Description("Gets or sets the behaviour for pressed items.")]
    [QPropertyIndex(19)]
    [Category("QAppearance")]
    public virtual QCompositePressedBehaviour PressedBehaviour
    {
      get => (QCompositePressedBehaviour) this.Properties.GetPropertyAsValueType(19);
      set => this.Properties.SetProperty(19, (object) value);
    }

    [Description("Gets or sets whether the icon background of the QCompositeMenu is visible")]
    [Category("QAppearance")]
    [QPropertyIndex(23)]
    public virtual bool IconBackgroundVisible
    {
      get => (bool) this.Properties.GetPropertyAsValueType(23);
      set => this.Properties.SetProperty(23, (object) value);
    }

    [Description("Gets or sets the margin of the icon background")]
    [Category("QAppearance")]
    [QPropertyIndex(24)]
    public virtual QMargin IconBackgroundMargin
    {
      get => (QMargin) this.Properties.GetPropertyAsValueType(24);
      set => this.Properties.SetProperty(24, (object) value);
    }

    [QPropertyIndex(25)]
    [Description("Gets or sets the size of the icon background")]
    [Category("QAppearance")]
    public virtual int IconBackgroundSize
    {
      get => (int) this.Properties.GetPropertyAsValueType(25);
      set => this.Properties.SetProperty(25, (object) value);
    }

    [QPropertyIndex(26)]
    [Description("Gets or sets the configuration for the HotkeyWindow. This window is displayed when ALT is pressed")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QHotkeyWindowConfiguration HotkeyWindowConfiguration => this.Properties.GetProperty(26) as QHotkeyWindowConfiguration;

    [QPropertyIndex(27)]
    [Category("QAppearance")]
    [Description("Gets or sets the show behavior of the QHotkeyWindow.")]
    public virtual QHotkeyWindowShowBehavior HotkeyWindowShowBehavior
    {
      get => (QHotkeyWindowShowBehavior) this.Properties.GetPropertyAsValueType(27);
      set => this.Properties.SetProperty(27, (object) value);
    }

    [QPropertyIndex(16)]
    [Description("Gets or set the layout of the QCompositeItem")]
    [Category("QAppearance")]
    public virtual QCompositeItemLayout Layout
    {
      get => (QCompositeItemLayout) this.Properties.GetPropertyAsValueType(16);
      set => this.Properties.SetProperty(16, (object) value);
    }

    [Description("Gets the appearance of a QComposite.")]
    [QPropertyIndex(15)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QShapeAppearance Appearance => this.Properties.GetProperty(15) as QShapeAppearance;

    [Description("Gets the scroll configuration")]
    [QPropertyIndex(28)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QCompositeScrollConfiguration ScrollConfiguration => this.Properties.GetProperty(28) as QCompositeScrollConfiguration;

    public override Size GetMinimumSize(IQPart part)
    {
      Size minimumSize = base.GetMinimumSize(part);
      if (this.Appearance != null)
      {
        minimumSize.Width = Math.Max(this.Appearance.Shape.MinimumSize.Width, minimumSize.Width);
        minimumSize.Height = Math.Max(this.Appearance.Shape.MinimumSize.Height, minimumSize.Height);
      }
      if (part is QComposite qcomposite && qcomposite.ParentContainer is QContainerControl parentContainer)
      {
        Size size = QMargin.InflateSize(parentContainer.MinimumClientSize, this.GetMargins(part), false, true);
        if (size.Width > 0)
          minimumSize.Width = Math.Max(minimumSize.Width, size.Width);
        if (size.Height > 0)
          minimumSize.Height = Math.Max(minimumSize.Height, size.Height);
      }
      return minimumSize;
    }

    public override IQPadding[] GetPaddings(IQPart part) => QPartHelper.GetDefaultPaddings(part, this.Padding, this.Appearance);

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

    public virtual QCompositeExpandDirection GetExpandDirection(IQPart part) => this.ExpandDirection;

    public virtual QCompositeExpandBehavior GetExpandBehavior(IQPart part) => this.ExpandBehavior;

    public virtual int GetExpandingDelay(IQPart part) => this.ExpandingDelay;

    public virtual QCompositePressedBehaviour GetPressedBehavior(
      IQPart part)
    {
      return this.PressedBehaviour;
    }

    public virtual bool GetInheritWindowsSettings(IQPart part) => this.InheritWindowsSettings;

    public virtual QHotkeyVisibilityType GetHotkeyPrefixVisibilityType(
      IQPart part)
    {
      return this.UsedHotkeyPrefixVisibilityType;
    }

    public virtual QHotkeyWindowShowBehavior GetHotkeyWindowShowBehavior(
      IQPart part)
    {
      return this.HotkeyWindowShowBehavior;
    }

    public virtual QHotkeyWindowConfiguration GetHotkeyWindowConfiguration(
      IQPart part)
    {
      return this.HotkeyWindowConfiguration;
    }

    public virtual bool GetIconBackgroundVisible(IQPart part) => this.IconBackgroundVisible;

    public virtual QMargin GetIconBackgroundMargin(IQPart part) => this.IconBackgroundMargin;

    public virtual int GetIconBackgroundSize(IQPart part) => this.IconBackgroundSize;

    public virtual QShapeAppearance GetAppearance(IQPart part) => this.Appearance;

    public virtual QCompositeItemLayout GetLayout(IQPart part) => this.Layout;

    public virtual QCompositeScrollConfiguration GetScrollConfiguration(
      IQPart part)
    {
      return this.ScrollConfiguration;
    }
  }
}
