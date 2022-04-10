// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeGroupConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QCompositeGroupConfiguration : QGroupPartConfiguration
  {
    protected internal const int PropAppearance = 15;
    protected internal const int PropLayout = 16;
    protected internal const int PropScrollConfiguration = 17;
    protected new const int CurrentPropertyCount = 3;
    protected new const int TotalPropertyCount = 18;
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QCompositeGroupConfiguration()
    {
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(1, (object) new QPadding(1, 1, 1, 1));
      this.Properties.DefineProperty(16, (object) QCompositeItemLayout.Auto);
      this.Properties.DefineResettableProperty(17, (IQResettableValue) this.CreateScrollConfiguration());
      if (this.ScrollConfiguration != null)
        this.ScrollConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(15, (IQResettableValue) this.CreateAppearance());
      if (this.Appearance == null)
        return;
      this.Appearance.AppearanceChanged += this.m_oChildObjectsChangedEventHandler;
    }

    protected virtual QShapeAppearance CreateAppearance() => (QShapeAppearance) new QCompositeGroupAppearance();

    protected virtual QCompositeScrollConfiguration CreateScrollConfiguration() => new QCompositeScrollConfiguration();

    protected override int GetRequestedCount() => 18;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string ContentLayoutOrder
    {
      get => base.ContentLayoutOrder;
      set => base.ContentLayoutOrder = value;
    }

    [QPropertyIndex(16)]
    [Description("Gets or set the layout of the QCompositeItem. If this is auto the layout is automatically determined. For example when the item is located in a Table-Layed-out parent the Auto value specifies that the item should be in a RowLayout.")]
    [Category("QAppearance")]
    public virtual QCompositeItemLayout Layout
    {
      get => (QCompositeItemLayout) this.Properties.GetPropertyAsValueType(16);
      set => this.Properties.SetProperty(16, (object) value);
    }

    [Description("Gets the scroll configuration")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [QPropertyIndex(17)]
    public virtual QCompositeScrollConfiguration ScrollConfiguration => this.Properties.GetProperty(17) as QCompositeScrollConfiguration;

    [QPropertyIndex(15)]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the appearance of a QCompositeItem.")]
    public virtual QShapeAppearance Appearance => this.Properties.GetProperty(15) as QShapeAppearance;

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

    public override Size GetMinimumSize(IQPart part)
    {
      Size minimumSize = base.GetMinimumSize(part);
      if (this.Appearance != null)
      {
        minimumSize.Width = Math.Max(this.Appearance.Shape.MinimumSize.Width, minimumSize.Width);
        minimumSize.Height = Math.Max(this.Appearance.Shape.MinimumSize.Height, minimumSize.Height);
      }
      return minimumSize;
    }

    public override IQPadding[] GetPaddings(IQPart part) => QPartHelper.GetDefaultPaddings(part, this.Padding, this.Appearance);

    public virtual QShapeAppearance GetAppearance(IQPart part) => this.Appearance;

    public virtual QCompositeItemLayout GetLayout(IQPart part) => this.Layout;

    public QCompositeScrollConfiguration GetScrollConfiguration(
      IQPart part)
    {
      return this.ScrollConfiguration;
    }
  }
}
