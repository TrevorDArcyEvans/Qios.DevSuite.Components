// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QScrollHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  internal class QScrollHelper
  {
    public static bool HandleOneDirectionScrollTimerTick(
      ref int scrollOffset,
      int scrollUntilOffset,
      int scrollStep)
    {
      if (scrollOffset > scrollUntilOffset)
      {
        if (scrollOffset - scrollStep < scrollUntilOffset)
          scrollOffset = scrollUntilOffset;
        else
          scrollOffset -= scrollStep;
        return true;
      }
      if (scrollOffset >= scrollUntilOffset)
        return false;
      if (scrollOffset + scrollStep > scrollUntilOffset)
        scrollOffset = scrollUntilOffset;
      else
        scrollOffset += scrollStep;
      return true;
    }

    public static int TranslateCoordinateToOffset(
      int coordinate,
      int viewPortNear,
      int viewPortFar,
      int currentOffset)
    {
      coordinate += currentOffset;
      if (coordinate < viewPortNear)
        return currentOffset - (coordinate - viewPortNear);
      return coordinate > viewPortFar ? currentOffset + (viewPortFar - coordinate) : currentOffset;
    }

    public static bool CalculateScrollIntoViewAmount(
      int nearBounds,
      int farBounds,
      int nearViewPort,
      int farViewPort,
      int currentOffset,
      out int newValue)
    {
      newValue = 0;
      nearBounds += currentOffset;
      farBounds += currentOffset;
      if ((nearBounds >= nearViewPort || farBounds >= farViewPort) && (farBounds <= farViewPort || nearBounds <= nearViewPort))
        return false;
      if (nearBounds < nearViewPort)
        newValue = nearBounds - currentOffset;
      else if (farBounds > farViewPort)
        newValue = farBounds - currentOffset;
      return true;
    }
  }
}
