// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonCollapsedItemIconConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonCollapsedItemIconConfiguration : QCompositeIconConfiguration
  {
    protected const int PropAppearance = 16;
    protected new const int CurrentPropertyCount = 1;
    protected new const int TotalPropertyCount = 17;
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QRibbonCollapsedItemIconConfiguration()
    {
      this.Properties.DefineProperty(1, (object) new QPadding(8, 3, 10, 8));
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineResettableProperty(16, (IQResettableValue) new QRibbonCollapsedItemIconAppearance());
      this.Appearance.AppearanceChanged += this.m_oChildObjectsChangedEventHandler;
    }

    [QPropertyIndex(16)]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the appearance of a QCompositeItem.")]
    public QRibbonCollapsedItemIconAppearance Appearance => this.Properties.GetProperty(16) as QRibbonCollapsedItemIconAppearance;

    protected override int GetRequestedCount() => 17;

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

    public override IQPadding[] GetPaddings(IQPart part) => QPartHelper.GetDefaultPaddings(part, this.Padding, (QShapeAppearance) this.Appearance);
  }
}
