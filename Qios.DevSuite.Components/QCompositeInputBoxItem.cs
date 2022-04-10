// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeInputBoxItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QCompositeInputBoxItem : QCompositeItem
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeText m_oTitle;

    protected QCompositeInputBoxItem(object sourceObject, QObjectClonerConstructOptions options)
      : base(QCompositeItemCreationOptions.None)
    {
    }

    public QCompositeInputBoxItem(QInputBox parent)
      : base(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateConfiguration)
    {
      this.Configuration = parent.ItemConfiguration;
      this.m_oTitle = new QCompositeText(QCompositeItemCreationOptions.CreateConfiguration);
      this.m_oTitle.Configuration.StretchHorizontal = true;
      this.m_oTitle.ItemName = "Text";
      this.Items.Add((IQPart) this.m_oTitle, false);
    }

    [Localizable(true)]
    [Description("Gets or sets the title of the QCompositeInputBoxItem")]
    [DefaultValue(null)]
    [Category("QAppearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public string Title
    {
      get => this.m_oTitle.Title;
      set => this.m_oTitle.Title = value;
    }
  }
}
