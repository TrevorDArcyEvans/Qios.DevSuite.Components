// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQMouseHookClient
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal interface IQMouseHookClient
  {
    bool SuppressMessageToDestination(int code, ref NativeMethods.MOUSEHOOKSTRUCT mouseHookStruct);

    void HandleExitMessage(ref bool cancelMessage);

    void HandleMouseWheelMessage(ref bool cancelMessage, MouseEventArgs e);

    Point PointToClient(Point point);
  }
}
