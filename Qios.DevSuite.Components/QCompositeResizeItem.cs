// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeResizeItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QCompositeResizeItem : QCompositeItem
  {
    public QCompositeResizeItem()
      : base(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateColorScheme | QCompositeItemCreationOptions.CreateConfiguration)
    {
      this.CloseMenuOnActivate = false;
    }

    [Description("Gets or sets whether the Menu must be closed when the Item is activated")]
    [DefaultValue(false)]
    [Category("QAppearance")]
    public override bool CloseMenuOnActivate
    {
      get => base.CloseMenuOnActivate;
      set => base.CloseMenuOnActivate = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("QBehavior")]
    [DefaultValue(null)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Description("Gets or sets the mouse cursor for this QCompositeItem")]
    public override Cursor Cursor
    {
      get
      {
        if (this.Site == null && base.Cursor == (Cursor) null)
        {
          switch (this.Configuration.ResizeBorder)
          {
            case QCompositeResizeBorder.None:
              return (Cursor) null;
            case QCompositeResizeBorder.Left:
            case QCompositeResizeBorder.Right:
              return Cursors.SizeWE;
            case QCompositeResizeBorder.Top:
            case QCompositeResizeBorder.Bottom:
              return Cursors.SizeNS;
            case QCompositeResizeBorder.TopLeft:
            case QCompositeResizeBorder.BottomRight:
              return Cursors.SizeNWSE;
            case QCompositeResizeBorder.TopRight:
            case QCompositeResizeBorder.BottomLeft:
              return Cursors.SizeNESW;
          }
        }
        return base.Cursor;
      }
      set => base.Cursor = value;
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QCompositeResizeItemConfiguration();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override QPartCollection ChildItems => base.ChildItems;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Contains the Configuration.")]
    public QCompositeResizeItemConfiguration Configuration
    {
      get => base.Configuration as QCompositeResizeItemConfiguration;
      set => this.Configuration = value;
    }
  }
}
