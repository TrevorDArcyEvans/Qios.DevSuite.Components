// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQItemStatesImplementation
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  public interface IQItemStatesImplementation
  {
    QItemStates ItemState { get; }

    QItemStates GetState(QItemStates checkForStates, bool includeParentStates);

    QItemStates HasStatesDefined(QItemStates checkForStates, QTristateBool stateValue);

    bool HasStatesDefined(QItemStates state);

    void AdjustState(QItemStates state, bool setValue, object additionalInfo);
  }
}
