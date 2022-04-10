// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDockException
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Runtime.Serialization;

namespace Qios.DevSuite.Components
{
  [Serializable]
  public class QDockException : Exception
  {
    public QDockException()
    {
    }

    public QDockException(string message)
      : base(message)
    {
    }

    public QDockException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    protected QDockException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
