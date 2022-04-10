// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeItemTemplate
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QCompositeItemTemplateConverter))]
  [DesignTimeVisible(true)]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (QCompositeControl), "Resources.ControlImages.QCompositeTemplate.bmp")]
  [Designer(typeof (QCompositeItemTemplateDesigner), typeof (IDesigner))]
  public class QCompositeItemTemplate : QCompositeItem
  {
    public void Apply(QCompositeItem item)
    {
      QDesignerHelper.NotifyDesignerOfCreation((object) this, true, (IServiceProvider) this.Site);
      if (item is QCompositeItemTemplate)
        return;
      QObjectCloner.CopyToObject((object) this, (object) item, false);
      QDesignerHelper.NotifyDesignerOfCreation((object) this, false, (IServiceProvider) this.Site);
      item.HandleChange(true);
    }
  }
}
