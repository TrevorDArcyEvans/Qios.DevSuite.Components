// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QAppearance
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  public class QAppearance : QAppearanceBase, IQBorderedAppearance, IQBorderedAdvancedAppearance
  {
    protected const int PropShowBorders = 14;
    protected const int PropShowBorderLeft = 15;
    protected const int PropShowBorderRight = 16;
    protected const int PropShowBorderTop = 17;
    protected const int PropShowBorderBottom = 18;
    protected const int PropBorderWidth = 19;
    protected new const int CurrentPropertyCount = 6;
    protected new const int TotalPropertyCount = 20;

    public QAppearance()
    {
      this.Properties.DefineProperty(14, (object) true);
      this.Properties.DefineProperty(15, (object) true);
      this.Properties.DefineProperty(16, (object) true);
      this.Properties.DefineProperty(17, (object) true);
      this.Properties.DefineProperty(18, (object) true);
      this.Properties.DefineProperty(19, (object) 1);
    }

    protected override int GetRequestedCount() => 20;

    [QPropertyIndex(14)]
    [Description("Gets or sets whether the borders should be drawn")]
    public virtual bool ShowBorders
    {
      get => (bool) this.Properties.GetPropertyAsValueType(14);
      set => this.Properties.SetProperty(14, (object) value);
    }

    [Description("Gets or sets whether the left border should be drawn")]
    [QPropertyIndex(15)]
    public virtual bool ShowBorderLeft
    {
      get => (bool) this.Properties.GetPropertyAsValueType(15);
      set => this.Properties.SetProperty(15, (object) value);
    }

    [Description("Gets or sets whether the right border should be drawn")]
    [QPropertyIndex(16)]
    public virtual bool ShowBorderRight
    {
      get => (bool) this.Properties.GetPropertyAsValueType(16);
      set => this.Properties.SetProperty(16, (object) value);
    }

    [Description("Gets or sets whether the top border should be drawn")]
    [QPropertyIndex(17)]
    [DefaultValue(true)]
    public virtual bool ShowBorderTop
    {
      get => (bool) this.Properties.GetPropertyAsValueType(17);
      set => this.Properties.SetProperty(17, (object) value);
    }

    [QPropertyIndex(18)]
    [Description("Gets or sets whether the bottom border should be drawn")]
    public virtual bool ShowBorderBottom
    {
      get => (bool) this.Properties.GetPropertyAsValueType(18);
      set => this.Properties.SetProperty(18, (object) value);
    }

    [Description("Gets or sets the borderWidth")]
    [QPropertyIndex(19)]
    public virtual int BorderWidth
    {
      get => (int) this.Properties.GetPropertyAsValueType(19);
      set => this.Properties.SetProperty(19, (object) value);
    }

    public QDrawControlBackgroundOptions GetDrawControlBackgroundOptions(
      bool horizontal)
    {
      if (!this.ShowBorders)
        return QDrawControlBackgroundOptions.NeverDrawBorders;
      QDrawControlBackgroundOptions backgroundOptions = QDrawControlBackgroundOptions.None;
      if (horizontal)
      {
        if (this.ShowBorderLeft)
          backgroundOptions |= QDrawControlBackgroundOptions.DrawLeftBorder;
        if (this.ShowBorderTop)
          backgroundOptions |= QDrawControlBackgroundOptions.DrawTopBorder;
        if (this.ShowBorderBottom)
          backgroundOptions |= QDrawControlBackgroundOptions.DrawBottomBorder;
        if (this.ShowBorderRight)
          backgroundOptions |= QDrawControlBackgroundOptions.DrawRightBorder;
      }
      else
      {
        if (this.ShowBorderLeft)
          backgroundOptions |= QDrawControlBackgroundOptions.DrawTopBorder;
        if (this.ShowBorderTop)
          backgroundOptions |= QDrawControlBackgroundOptions.DrawRightBorder;
        if (this.ShowBorderBottom)
          backgroundOptions |= QDrawControlBackgroundOptions.DrawLeftBorder;
        if (this.ShowBorderRight)
          backgroundOptions |= QDrawControlBackgroundOptions.DrawBottomBorder;
      }
      return backgroundOptions;
    }
  }
}
