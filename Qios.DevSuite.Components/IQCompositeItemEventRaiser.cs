// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQCompositeItemEventRaiser
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  internal interface IQCompositeItemEventRaiser
  {
    void RaisePaintItem(QCompositePaintStageEventArgs e);

    void RaiseItemActivating(QCompositeCancelEventArgs e);

    void RaiseItemActivated(QCompositeEventArgs e);

    void RaiseItemSelected(QCompositeEventArgs e);

    void RaiseItemExpanding(QCompositeExpandingCancelEventArgs e);

    void RaiseItemExpanded(QCompositeExpandedEventArgs e);

    void RaiseItemCollapsing(QCompositeCancelEventArgs e);

    void RaiseItemCollapsed(QCompositeEventArgs e);
  }
}
