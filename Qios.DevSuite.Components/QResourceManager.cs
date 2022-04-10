// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QResourceManager
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Qios.DevSuite.Components
{
  internal class QResourceManager
  {
    private ArrayList m_aManagers;
    private Assembly m_oAssembly;
    private string m_sResourceFilesNameSpace;

    public QResourceManager(Assembly assembly, string resourceFilesNameSpace)
    {
      this.m_aManagers = new ArrayList();
      this.m_oAssembly = assembly;
      this.m_sResourceFilesNameSpace = resourceFilesNameSpace.EndsWith(".") ? resourceFilesNameSpace : resourceFilesNameSpace + ".";
    }

    public string GetString(string resourceFile, string name, params object[] args)
    {
      ResourceManager resourceManager = this.GetResourceManager(resourceFile);
      string format = resourceManager.GetString(name);
      if (format == null)
        throw new InvalidOperationException("Cannot find resource with name '" + name + "' in '" + resourceManager.BaseName + "'");
      return args != null && args.Length > 0 ? string.Format((IFormatProvider) CultureInfo.InvariantCulture, format, args) : format;
    }

    private ResourceManager GetResourceManager(string name)
    {
      string baseName = this.m_sResourceFilesNameSpace + name;
      for (int index = 0; index < this.m_aManagers.Count; ++index)
      {
        ResourceManager aManager = (ResourceManager) this.m_aManagers[index];
        if (aManager.BaseName == baseName)
          return aManager;
      }
      ResourceManager resourceManager = new ResourceManager(baseName, this.m_oAssembly);
      this.m_aManagers.Add((object) resourceManager);
      return resourceManager;
    }
  }
}
