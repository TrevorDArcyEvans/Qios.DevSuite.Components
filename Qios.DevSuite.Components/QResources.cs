// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QResources
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  internal class QResources
  {
    private static QResourceManager m_oResourceManager;

    private QResources()
    {
    }

    private static void SecureResourceManager()
    {
      if (QResources.m_oResourceManager != null)
        return;
      QResources.m_oResourceManager = new QResourceManager(typeof (QResources).Assembly, "Qios.DevSuite.Components.Resources.Strings");
    }

    public static string GetNiner(string niner, params object[] args)
    {
      QResources.SecureResourceManager();
      return QResources.m_oResourceManager.GetString("Niners", niner, args);
    }

    public static string GetException(string exception, params object[] args)
    {
      QResources.SecureResourceManager();
      return QResources.m_oResourceManager.GetString("Exceptions", exception, args);
    }

    public static string GetGeneral(string name, params object[] args)
    {
      QResources.SecureResourceManager();
      return QResources.m_oResourceManager.GetString("General", name, args);
    }

    public static Icon LoadIconFromResource(string resourceString) => QResources.LoadObjectFromResource(typeof (Icon), resourceString) as Icon;

    public static Image LoadImageFromResource(string resourceString) => QResources.LoadObjectFromResource(typeof (Bitmap), resourceString) as Image;

    public static object LoadObjectFromResource(Type objectType, string resourceString)
    {
      if (resourceString == null)
        return (object) null;
      string[] strArray = resourceString.Split(',');
      Assembly assembly;
      if (strArray.Length == 2)
      {
        try
        {
          assembly = Assembly.Load(strArray[1].Trim());
        }
        catch (FileNotFoundException ex)
        {
          return (object) null;
        }
      }
      else
        assembly = typeof (QResources).Assembly;
      Stream stream = (Stream) null;
      try
      {
        stream = assembly.GetManifestResourceStream(strArray[0].Trim());
      }
      catch (ArgumentNullException ex)
      {
      }
      catch (ArgumentException ex)
      {
      }
      if (stream == null)
        return (object) null;
      return Activator.CreateInstance(objectType, (object) stream);
    }
  }
}
