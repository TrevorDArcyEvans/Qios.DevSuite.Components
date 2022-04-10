// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeScrollConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QCompositeScrollConfiguration : QScrollBarConfiguration
  {
    protected internal const int PropBarAppearance = 10;
    protected internal const int PropScrollType = 11;
    protected internal const int PropScrollHorizontal = 12;
    protected internal const int PropScrollVertical = 13;
    protected internal const int PropScrollStepSize = 14;
    protected new const int CurrentPropertyCount = 5;
    protected new const int TotalPropertyCount = 15;
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QCompositeScrollConfiguration()
    {
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(2, (object) new QPadding(1, 1, 1, 1));
      this.Properties.DefineProperty(11, (object) QCompositeScrollType.Button);
      this.Properties.DefineResettableProperty(10, (IQResettableValue) this.CreateBarAppearance());
      this.Properties.DefineProperty(12, (object) QCompositeScrollVisibility.None);
      this.Properties.DefineProperty(13, (object) QCompositeScrollVisibility.None);
      this.Properties.DefineProperty(14, (object) 10);
      this.BarAppearance.AppearanceChanged += this.m_oChildObjectsChangedEventHandler;
    }

    [Description("Gets or sets the stepsize to use. When animating this increases the speed, when not animating this increases. the step of one button-click.")]
    [QPropertyIndex(14)]
    [Category("QAppearance")]
    public virtual int ScrollStepSize
    {
      get => (int) this.Properties.GetPropertyAsValueType(14);
      set => this.Properties.SetProperty(14, (object) value);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the direction of the scrollbar.")]
    [Category("QAppearance")]
    [QPropertyIndex(9)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override QScrollBarDirection Direction
    {
      get => base.Direction;
      set => base.Direction = value;
    }

    [Description("Gets or sets when horizontal scroll must be visible.")]
    [QPropertyIndex(12)]
    [Category("QAppearance")]
    public virtual QCompositeScrollVisibility ScrollHorizontal
    {
      get => (QCompositeScrollVisibility) this.Properties.GetPropertyAsValueType(12);
      set => this.Properties.SetProperty(12, (object) value);
    }

    [Description("Gets or sets when vertical scroll must be visible.")]
    [QPropertyIndex(13)]
    [Category("QAppearance")]
    public virtual QCompositeScrollVisibility ScrollVertical
    {
      get => (QCompositeScrollVisibility) this.Properties.GetPropertyAsValueType(13);
      set => this.Properties.SetProperty(13, (object) value);
    }

    [QPropertyIndex(11)]
    [Description("Gets or sets what type of scrolling to use.")]
    [Category("QAppearance")]
    public virtual QCompositeScrollType ScrollType
    {
      get => (QCompositeScrollType) this.Properties.GetPropertyAsValueType(11);
      set
      {
        switch (value)
        {
          case QCompositeScrollType.Button:
            this.Properties.DefineProperty(2, (object) new QPadding(1, 1, 1, 1));
            break;
          case QCompositeScrollType.ScrollBar:
            this.Properties.DefineProperty(2, (object) new QPadding(4, 4, 4, 4));
            break;
        }
        this.Properties.SetProperty(11, (object) value);
      }
    }

    [QPropertyIndex(10)]
    [Description("Gets the appearance of a scrollbar on a QComposite or QCompositeGroup.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QCompositeScrollBarAppearance BarAppearance => this.Properties.GetProperty(10) as QCompositeScrollBarAppearance;

    protected override QScrollBarButtonAppearance CreateTrackButtonAppearance() => (QScrollBarButtonAppearance) new QCompositeScrollButtonAppearance();

    protected override QScrollBarButtonAppearance CreateButtonAppearance() => (QScrollBarButtonAppearance) new QCompositeScrollButtonAppearance();

    protected virtual QCompositeScrollBarAppearance CreateBarAppearance() => new QCompositeScrollBarAppearance();

    protected override int GetRequestedCount() => 15;

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
