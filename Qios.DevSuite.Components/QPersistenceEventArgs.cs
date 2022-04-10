// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPersistenceEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  public class QPersistenceEventArgs : EventArgs
  {
    private string m_sPersistableObjectName;
    private Type m_oPersistableObjectType;
    private Guid m_oPersistableObjectGuid;
    private IQPersistableObject m_oPersistableObject;
    private Guid m_oPersistableHostGuid;
    private IQPersistableHost m_oPersistableHost;
    private IXPathNavigable m_oPersistableObjectElement;

    public QPersistenceEventArgs(
      string persistableObjectName,
      Type persistableObjectType,
      Guid persistableObjectGuid,
      Guid persistableHostGuid)
    {
      this.m_sPersistableObjectName = persistableObjectName;
      this.m_oPersistableObjectType = persistableObjectType;
      this.m_oPersistableObjectGuid = persistableObjectGuid;
      this.m_oPersistableHostGuid = persistableHostGuid;
    }

    public Guid PersistableHostGuid => this.m_oPersistableHostGuid;

    public string PersistableObjectName => this.m_sPersistableObjectName;

    public Type PersistableObjectType => this.m_oPersistableObjectType;

    public Guid PersistableObjectGuid => this.m_oPersistableObjectGuid;

    public IQPersistableObject PersistableObject
    {
      get => this.m_oPersistableObject;
      set => this.m_oPersistableObject = value;
    }

    public IQPersistableHost PersistableHost
    {
      get => this.m_oPersistableHost;
      set => this.m_oPersistableHost = value;
    }

    public IXPathNavigable PersistableObjectElement => this.m_oPersistableObjectElement;

    internal void PutPersistableObjectElement(IXPathNavigable element) => this.m_oPersistableObjectElement = element;
  }
}
