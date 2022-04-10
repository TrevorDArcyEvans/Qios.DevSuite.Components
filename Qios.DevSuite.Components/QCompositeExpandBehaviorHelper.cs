// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeExpandBehaviorHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  internal class QCompositeExpandBehaviorHelper
  {
    public static bool IsSet(
      QCompositeExpandBehavior behaviorToCheck,
      QCompositeExpandBehavior behavior1,
      QCompositeExpandBehavior behavior2)
    {
      QCompositeExpandBehavior qcompositeExpandBehavior = behavior1 | behavior2;
      return (behaviorToCheck & qcompositeExpandBehavior) == behaviorToCheck && ((QCompositeExpandBehavior) ((int) behaviorToCheck << 1) & qcompositeExpandBehavior) == QCompositeExpandBehavior.None;
    }
  }
}
