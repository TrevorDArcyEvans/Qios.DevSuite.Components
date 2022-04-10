// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPersistenceUnloadOptions
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  public enum QPersistenceUnloadOptions
  {
    WhereNotPersistObject = 1,
    WherePersistObject = 2,
    WhereRequiresUnload = 4,
    Default = WhereRequiresUnload | WherePersistObject, // 0x00000006
    All = 8,
  }
}
