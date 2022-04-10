// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartObjectPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public abstract class QPartObjectPainter : IQPartObjectPainter
  {
    private bool m_bEnabled = true;
    private IQPartObjectPainter m_oNextPainter;

    public bool Enabled
    {
      get => this.m_bEnabled;
      set => this.m_bEnabled = value;
    }

    public IQPartObjectPainter NextPainter
    {
      get => this.m_oNextPainter;
      set => this.m_oNextPainter = value;
    }

    public bool ContainsPainter(IQPartObjectPainter painter)
    {
      IQPartObjectPainter qpartObjectPainter = (IQPartObjectPainter) this;
      while (qpartObjectPainter != null && qpartObjectPainter != painter)
        qpartObjectPainter = qpartObjectPainter.NextPainter;
      return qpartObjectPainter == painter;
    }

    public IQPartObjectPainter GetPainter(Type painterType)
    {
      if (painterType == null)
        return (IQPartObjectPainter) this;
      IQPartObjectPainter o = (IQPartObjectPainter) this;
      while (o != null && !painterType.IsInstanceOfType((object) o))
        o = o.NextPainter;
      return o;
    }

    public void AddPainter(IQPartObjectPainter painter)
    {
      if (painter == null)
        return;
      this.RemovePainter(painter);
      IQPartObjectPainter qpartObjectPainter = (IQPartObjectPainter) this;
      while (qpartObjectPainter.NextPainter != null)
        qpartObjectPainter = qpartObjectPainter.NextPainter;
      qpartObjectPainter.NextPainter = painter;
    }

    public void RemovePainter(IQPartObjectPainter painter)
    {
      if (painter == null)
        return;
      IQPartObjectPainter qpartObjectPainter = (IQPartObjectPainter) this;
      IQPartObjectPainter nextPainter;
      for (nextPainter = qpartObjectPainter.NextPainter; nextPainter != null && nextPainter != painter; nextPainter = nextPainter.NextPainter)
        qpartObjectPainter = nextPainter;
      if (nextPainter != painter)
        return;
      qpartObjectPainter.NextPainter = painter.NextPainter;
      painter.NextPainter = (IQPartObjectPainter) null;
    }

    public abstract void PaintObject(IQPart part, QPartPaintContext paintContext);

    public static bool ContainsPainter(
      IQPartObjectPainter[] painters,
      QPartPaintLayer paintLayer,
      IQPartObjectPainter painter)
    {
      IQPartObjectPainter objectPainter = QPartObjectPainter.GetObjectPainter(painters, paintLayer);
      if (objectPainter == null)
        return false;
      return objectPainter == painter || objectPainter.ContainsPainter(painter);
    }

    public static IQPartObjectPainter[] SetObjectPainter(
      IQPartObjectPainter[] painters,
      QPartPaintLayer paintLayer,
      IQPartObjectPainter painter)
    {
      if (painters == null)
        painters = new IQPartObjectPainter[Enum.GetValues(typeof (QPartPaintLayer)).Length];
      painters[(int) paintLayer] = painter;
      return painters;
    }

    public static IQPartObjectPainter GetObjectPainter(
      IQPartObjectPainter[] painters,
      QPartPaintLayer paintLayer)
    {
      return painters?[(int) paintLayer];
    }

    public static IQPartObjectPainter GetObjectPainter(
      IQPartObjectPainter[] painters,
      QPartPaintLayer paintLayer,
      Type painterType)
    {
      if (painters != null)
      {
        IQPartObjectPainter painter = painters[(int) paintLayer];
        if (painter != null)
          return painter.GetPainter(painterType);
      }
      return (IQPartObjectPainter) null;
    }

    public static IQPartObjectPainter[] AddObjectPainter(
      IQPartObjectPainter[] painters,
      QPartPaintLayer paintLayer,
      IQPartObjectPainter painter)
    {
      IQPartObjectPainter objectPainter = QPartObjectPainter.GetObjectPainter(painters, paintLayer);
      if (objectPainter != null)
        objectPainter.AddPainter(painter);
      else
        painters = QPartObjectPainter.SetObjectPainter(painters, paintLayer, painter);
      return painters;
    }

    public static IQPartObjectPainter[] RemoveObjectPainter(
      IQPartObjectPainter[] painters,
      QPartPaintLayer paintLayer,
      IQPartObjectPainter painter)
    {
      IQPartObjectPainter objectPainter = QPartObjectPainter.GetObjectPainter(painters, paintLayer);
      if (objectPainter != null)
      {
        if (objectPainter == painter)
          painters[(int) paintLayer] = painter.NextPainter;
        else
          objectPainter.RemovePainter(painter);
      }
      return painters;
    }
  }
}
