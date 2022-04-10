// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQCompositeContainer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal interface IQCompositeContainer : IQDesignableItemContainer
  {
    QComposite Composite { get; }

    IQCompositeContainer ParentContainer { get; }

    bool CanClose { get; }

    bool Close(QCompositeActivationType closeType);

    Cursor Cursor { get; set; }

    bool IsFocused { get; }

    Rectangle RectangleToScreen(Rectangle rectangle);

    Size Size { get; }

    Control Control { get; }

    bool ContainsControl(Control control);

    QPartCollection Items { get; }
  }
}
