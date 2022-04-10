// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCommandException
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Runtime.Serialization;

namespace Qios.DevSuite.Components
{
  [Serializable]
  public class QCommandException : Exception
  {
    public QCommandException()
    {
    }

    public QCommandException(string message)
      : base(message)
    {
    }

    public QCommandException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    protected QCommandException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
