// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeItemConfiguration
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
  public class QCompositeItemConfiguration : QGroupPartConfiguration
  {
    protected const int PropAppearance = 15;
    protected const int PropLayout = 16;
    protected const int PropHasHotState = 17;
    protected const int PropHasPressedState = 18;
    protected const int PropHotkeyWindowRelativeOffset = 19;
    protected const int PropHotkeyWindowAlignment = 20;
    protected const int PropExpandBehavior = 21;
    protected const int PropExpandDirection = 22;
    protected new const int CurrentPropertyCount = 8;
    protected new const int TotalPropertyCount = 23;
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QCompositeItemConfiguration()
    {
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(7, (object) QPartAlignment.Near);
      this.Properties.DefineProperty(6, (object) QPartAlignment.Near);
      this.Properties.DefineProperty(1, (object) new QPadding(1, 1, 1, 1));
      this.Properties.DefineProperty(17, (object) QTristateBool.True);
      this.Properties.DefineProperty(18, (object) QTristateBool.True);
      this.Properties.DefineProperty(19, (object) new Point(0, 5));
      this.Properties.DefineProperty(20, (object) ContentAlignment.BottomCenter);
      this.Properties.DefineProperty(16, (object) QCompositeItemLayout.Auto);
      this.Properties.DefineProperty(21, (object) QCompositeExpandBehavior.None);
      this.Properties.DefineProperty(22, (object) QCompositeExpandDirection.Inherited);
      this.Properties.DefineResettableProperty(15, (IQResettableValue) this.CreateAppearance());
      if (this.Appearance == null)
        return;
      this.Appearance.AppearanceChanged += this.m_oChildObjectsChangedEventHandler;
    }

    protected virtual QShapeAppearance CreateAppearance() => (QShapeAppearance) new QCompositeItemAppearance();

    protected override int GetRequestedCount() => 23;

    [Category("QAppearance")]
    [Description("Gets or set if the QCompositeItem has a hot state.")]
    [QPropertyIndex(17)]
    public QTristateBool HasHotState
    {
      get => (QTristateBool) this.Properties.GetPropertyAsValueType(17);
      set => this.Properties.SetProperty(17, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(18)]
    [Description("Gets or set if the QCompositeItem has a pressed state.")]
    public QTristateBool HasPressedState
    {
      get => (QTristateBool) this.Properties.GetPropertyAsValueType(18);
      set => this.Properties.SetProperty(18, (object) value);
    }

    [Category("QAppearance")]
    [Browsable(false)]
    [Description("Gets or set the relative offset of the HotkeyWindow. This offset is added after the alignment.")]
    [QPropertyIndex(19)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Point HotkeyWindowRelativeOffset
    {
      get => (Point) this.Properties.GetPropertyAsValueType(19);
      set => this.Properties.SetProperty(19, (object) value);
    }

    [Category("QAppearance")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [QPropertyIndex(20)]
    [Description("Gets or set the alignment of the hotkeyWindow.")]
    public ContentAlignment HotkeyWindowAlignment
    {
      get => (ContentAlignment) this.Properties.GetPropertyAsValueType(20);
      set => this.Properties.SetProperty(20, (object) value);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string ContentLayoutOrder
    {
      get => base.ContentLayoutOrder;
      set => base.ContentLayoutOrder = value;
    }

    [Category("QAppearance")]
    [Description("Gets or set the layout of the QCompositeItem")]
    [QPropertyIndex(16)]
    public virtual QCompositeItemLayout Layout
    {
      get => (QCompositeItemLayout) this.Properties.GetPropertyAsValueType(16);
      set => this.Properties.SetProperty(16, (object) value);
    }

    [QPropertyIndex(21)]
    [Editor(typeof (QFlagsEditor), typeof (UITypeEditor))]
    [Browsable(false)]
    [Description("Gets or sets additional behaviors that must be applied to this item. Or sets behaviors off by specifying the 'not' behavior.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Category("QAppearance")]
    public virtual QCompositeExpandBehavior ExpandBehavior
    {
      get => (QCompositeExpandBehavior) this.Properties.GetPropertyAsValueType(21);
      set => this.Properties.SetProperty(21, (object) value);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Description("Gets or sets the direction a QCompositeItem will expand to when it contains child QCompositeItems. If this is inherited it uses the direction of the composite.")]
    [QPropertyIndex(22)]
    [Category("QAppearance")]
    public virtual QCompositeExpandDirection ExpandDirection
    {
      get => (QCompositeExpandDirection) this.Properties.GetPropertyAsValueType(22);
      set => this.Properties.SetProperty(22, (object) value);
    }

    [Description("Gets the appearance of a QCompositeItem.")]
    [Category("QAppearance")]
    [QPropertyIndex(15)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QShapeAppearance Appearance => this.Properties.GetProperty(15) as QShapeAppearance;

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

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
