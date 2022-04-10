// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQPersistableObject
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  public interface IQPersistableObject
  {
    Guid PersistGuid { get; set; }

    bool PersistObject { get; set; }

    bool IsPersisted { get; set; }

    bool RequiresUnload { get; }

    bool CreateNew { get; set; }

    string Name { get; set; }

    bool MustBePersistedAfter(IQPersistableObject persistableObject);

    IXPathNavigable SavePersistableObject(
      QPersistenceManager manager,
      IXPathNavigable parentElement);

    bool LoadPersistableObject(
      QPersistenceManager manager,
      IXPathNavigable persistableObjectElement,
      object parentState);

    void UnloadPersistableObject();
  }
}
