// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QToolItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QToolItemDesigner), typeof (IDesigner))]
  [TypeConverter(typeof (QToolItemConverter))]
  [ToolboxItem(false)]
  public class QToolItem : QMenuItem
  {
    private QToolItemType m_eItemType = QToolItemType.TitleIcon;
    private QHasChildItemsImageVisibility m_eHasChildItemsImageVisibility;

    public QToolItem()
    {
    }

    public QToolItem(IContainer container)
      : base(container)
    {
    }

    public QToolItem(bool separator)
      : base(separator)
    {
    }

    public QToolItem(string title)
      : base(title)
    {
    }

    public QToolItem(string title, string name)
      : base(title, name)
    {
    }

    public QToolItem(string title, string itemName, Icon icon, Shortcut shortcut)
      : base(title, itemName, icon, shortcut)
    {
    }

    [DefaultValue(QToolItemType.TitleIcon)]
    [Description("Gets or sets the ItemType of the QToolItem. This indicates what part of the QToolItem is visible")]
    [Category("QBehavior")]
    public QToolItemType ItemType
    {
      get => this.m_eItemType;
      set
      {
        this.m_eItemType = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [DefaultValue(QHasChildItemsImageVisibility.Default)]
    [Description("Gets or sets visibility of the HasChildItems image.")]
    [Category("QBehavior")]
    public QHasChildItemsImageVisibility HasChildItemsImageVisibility
    {
      get => this.m_eHasChildItemsImageVisibility;
      set
      {
        this.m_eHasChildItemsImageVisibility = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }
  }
}
