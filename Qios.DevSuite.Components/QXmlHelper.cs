// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QXmlHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  public sealed class QXmlHelper
  {
    private QXmlHelper()
    {
    }

    public static bool ValidateXmlFragment(string fragment, bool throwException)
    {
      if (QMisc.IsEmpty((object)fragment))
        return true;
      XmlReader xmlReader = (XmlReader)new XmlTextReader((TextReader)new StringReader("<value>" + fragment + "</value>"));
      try
      {
        do
          ;
        while (xmlReader.Read());
      }
      catch (Exception ex)
      {
        if (throwException)
          throw new InvalidOperationException(QResources.GetException("QXmlHelper_InvalidXml"), ex);
        return false;
      }
      finally
      {
        xmlReader.Close();
      }
      return true;
    }

    public static void LoadObjectFromXmlElement(
      IXPathNavigable element,
      object destination,
      PropertyDescriptorCollection propertiesToSet)
    {
      if (destination == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object)nameof(destination)));
      if (propertiesToSet == null)
        propertiesToSet = TypeDescriptor.GetProperties(destination);
      for (int index = 0; index < propertiesToSet.Count; ++index)
      {
        PropertyDescriptor propertiesTo = propertiesToSet[index];
        if (!propertiesTo.IsReadOnly)
        {
          string str = "";
          if (propertiesTo.Name.Length > 1)
            str = char.ToLower(propertiesTo.Name[0], CultureInfo.InvariantCulture).ToString() + propertiesTo.Name.Substring(1);
          else if (propertiesTo.Name.Length == 1)
            str = propertiesTo.Name.ToLower(CultureInfo.InvariantCulture);
          if (QXmlHelper.ContainsChildElement(element, str))
          {
            object obj = null;
            obj = QMisc.GetViaTypeConverter((object)QXmlHelper.GetChildElementString(element, str), propertiesTo.PropertyType) ?? Convert.ChangeType(obj, propertiesTo.PropertyType, (IFormatProvider)CultureInfo.InvariantCulture);
            try
            {
              propertiesTo.SetValue(destination, obj);
            }
            catch (Exception ex)
            {
              throw new InvalidOperationException(QResources.GetException("QXmlHelper_InitializeObjectFromXmlSetThrewException", (object)destination.GetType().Name, (object)propertiesTo.Name, obj), ex);
            }
          }
        }
      }
    }

    public static void SaveObjectToXml(
      IXPathNavigable objectElement,
      object source,
      PropertyDescriptorCollection propertiesToRead)
    {
      if (propertiesToRead == null)
        propertiesToRead = TypeDescriptor.GetProperties(source);
      for (int index = 0; index < propertiesToRead.Count; ++index)
      {
        PropertyDescriptor property = propertiesToRead[index];
        if (!property.IsReadOnly && !property.DesignTimeOnly && QXmlSaveAttribute.ShouldSaveProperty(property, source))
        {
          string name = "";
          if (property.Name.Length > 1)
            name = char.ToLower(property.Name[0], CultureInfo.InvariantCulture).ToString() + property.Name.Substring(1);
          else if (property.Name.Length == 1)
            name = property.Name.ToLower(CultureInfo.InvariantCulture);
          QXmlHelper.AddElement(objectElement, name, property.GetValue(source));
        }
      }
    }

    public static IXPathNavigable AddAttribute(
      IXPathNavigable parentElement,
      string name,
      object value)
    {
      switch (parentElement)
      {
        case XmlElement:
        label_3:
          var documentElement = (XmlElement)parentElement;
          string asString = QMisc.GetAsString(value);
          if (asString == null && asString.Length == 0)
            return (IXPathNavigable)null;
          XmlAttribute attribute = documentElement.OwnerDocument.CreateAttribute(name);
          attribute.Value = asString;
          documentElement.Attributes.Append(attribute);
          return (IXPathNavigable)attribute;
        case XmlDocument xmlDocument:
          documentElement = xmlDocument.DocumentElement;
          goto label_3;
        default:
          throw new XmlException(QResources.GetException("QXml_XPathNavigableIsNotAnXmlElementOrDocument"));
      }
    }

    public static IXPathNavigable AddElement(IXPathNavigable parentNode, string name)
    {
      XmlElement xmlElement = parentNode as XmlElement;
      XmlDocument xmlDocument = parentNode as XmlDocument;
      if (xmlElement == null && xmlDocument == null)
        throw new XmlException(QResources.GetException("QXml_XPathNavigableIsNotAnXmlElementOrDocument"));
      XmlElement element;
      if (xmlDocument != null)
      {
        element = xmlDocument.CreateElement(name);
        xmlDocument.AppendChild((XmlNode)element);
      }
      else
      {
        element = xmlElement.OwnerDocument.CreateElement(name);
        xmlElement.AppendChild((XmlNode)element);
      }
      return (IXPathNavigable)element;
    }

    public static IXPathNavigable AddElement(
      IXPathNavigable parentNode,
      string name,
      object innerValue)
    {
      var xmlElement = QXmlHelper.AddElement(parentNode, name) as XmlElement;
      if (xmlElement != null && innerValue != null)
        xmlElement.InnerText = QMisc.GetAsString(innerValue);
      return (IXPathNavigable)xmlElement;
    }

    public static void RemoveAll(IXPathNavigable element)
    {
      if (!(element is XmlNode xmlNode))
        return;
      xmlNode.RemoveAll();
    }

    public static string GetAttributeString(
      IXPathNavigable parentElement,
      string name,
      string defaultValue)
    {
      string str = parentElement != null ? parentElement.CreateNavigator().GetAttribute(name, "") : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object)nameof(parentElement)));
      return QMisc.IsEmpty((object)str) ? defaultValue : str;
    }

    public static string GetAttributeString(IXPathNavigable parentElement, string name) => QXmlHelper.GetAttributeString(parentElement, name, string.Empty);

    public static Guid GetAttributeGuid(IXPathNavigable parentElement, string name) => QMisc.GetAsGuid((object)QXmlHelper.GetAttributeString(parentElement, name));

    public static Guid GetAttributeGuid(
      IXPathNavigable parentElement,
      string name,
      Guid defaultValue)
    {
      return QMisc.GetAsGuid((object)QXmlHelper.GetAttributeString(parentElement, name), defaultValue);
    }

    public static bool GetAttributeBool(IXPathNavigable parentElement, string name) => QMisc.GetAsBool((object)QXmlHelper.GetAttributeString(parentElement, name));

    public static bool GetAttributeBool(
      IXPathNavigable parentElement,
      string name,
      bool defaultValue)
    {
      return QMisc.GetAsBool((object)QXmlHelper.GetAttributeString(parentElement, name), defaultValue);
    }

    public static Type GetAttributeType(IXPathNavigable parentElement, string name) => Type.GetType(QXmlHelper.GetAttributeString(parentElement, name), false, true);

    public static int GetAttributeInt(IXPathNavigable parentElement, string name) => QMisc.GetAsInt((object)QXmlHelper.GetAttributeString(parentElement, name));

    public static int GetAttributeInt(IXPathNavigable parentElement, string name, int defaultValue) => QMisc.GetAsInt((object)QXmlHelper.GetAttributeString(parentElement, name), defaultValue);

    public static Enum GetAttributeEnum(
      IXPathNavigable parentElement,
      string name,
      Type enumType,
      Enum defaultValue)
    {
      string attributeString = QXmlHelper.GetAttributeString(parentElement, name);
      try
      {
        return (Enum)Enum.Parse(enumType, attributeString, true);
      }
      catch (ArgumentException ex)
      {
        return defaultValue;
      }
    }

    public static Enum GetAttributeEnum(
      IXPathNavigable parentElement,
      string name,
      Type enumType)
    {
      return QXmlHelper.GetAttributeEnum(parentElement, name, enumType, (Enum)null);
    }

    public static XPathNavigator SelectChildNavigator(
      IXPathNavigable parentElement,
      string xpath)
    {
      XPathNodeIterator xpathNodeIterator = parentElement != null ? parentElement.CreateNavigator().Select(xpath) : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object)nameof(parentElement)));
      return xpathNodeIterator.MoveNext() ? xpathNodeIterator.Current : (XPathNavigator)null;
    }

    public static IXPathNavigable SelectChildNavigable(
      IXPathNavigable parentElement,
      string xpath)
    {
      XPathNavigator navigator = QXmlHelper.SelectChildNavigator(parentElement, xpath);
      return navigator != null ? QXmlHelper.GetNavigableFromNavigator(navigator) : (IXPathNavigable)null;
    }

    public static IXPathNavigable GetNavigableFromNavigator(XPathNavigator navigator) => navigator is IHasXmlNode hasXmlNode ? (IXPathNavigable)hasXmlNode.GetNode() : (IXPathNavigable)null;

    public static bool ContainsChildElement(IXPathNavigable parentElement, string childName) => QXmlHelper.SelectChildNavigator(parentElement, childName) != null;

    public static string GetChildElementString(
      IXPathNavigable element,
      string name,
      string defaultValue)
    {
      if (element == null)
        return defaultValue;
      XPathNavigator xpathNavigator = QXmlHelper.SelectChildNavigator(element, name);
      return xpathNavigator != null && xpathNavigator.MoveToFirstChild() && xpathNavigator.NodeType == XPathNodeType.Text ? xpathNavigator.Value : defaultValue;
    }

    public static string GetChildElementString(IXPathNavigable element, string name) => QXmlHelper.GetChildElementString(element, name, string.Empty);

    public static Guid GetChildElementGuid(IXPathNavigable element, string name) => QMisc.GetAsGuid((object)QXmlHelper.GetChildElementString(element, name));

    public static Guid GetChildElementGuid(
      IXPathNavigable element,
      string name,
      Guid defaultValue)
    {
      return QMisc.GetAsGuid((object)QXmlHelper.GetChildElementString(element, name), defaultValue);
    }

    public static bool GetChildElementBool(IXPathNavigable element, string name) => QMisc.GetAsBool((object)QXmlHelper.GetChildElementString(element, name));

    public static bool GetChildElementBool(IXPathNavigable element, string name, bool defaultValue) => QMisc.GetAsBool((object)QXmlHelper.GetChildElementString(element, name), defaultValue);

    public static Type GetChildElementType(IXPathNavigable element, string name) => Type.GetType(QXmlHelper.GetChildElementString(element, name), false, true);

    public static int GetChildElementInt(IXPathNavigable element, string name) => QMisc.GetAsInt((object)QXmlHelper.GetChildElementString(element, name));

    public static int GetChildElementInt(IXPathNavigable element, string name, int defaultValue) => QMisc.GetAsInt((object)QXmlHelper.GetChildElementString(element, name), defaultValue);

    public static Size GetChildElementSize(IXPathNavigable element, string name) => QMisc.GetAsSize((object)QXmlHelper.GetChildElementString(element, name));

    public static Size GetChildElementSize(
      IXPathNavigable element,
      string name,
      Size defaultValue)
    {
      return QMisc.GetAsSize((object)QXmlHelper.GetChildElementString(element, name), defaultValue);
    }

    public static Rectangle GetChildElementRectangle(IXPathNavigable element, string name) => QMisc.GetAsRectangle((object)QXmlHelper.GetChildElementString(element, name));

    public static Rectangle GetChildElementRectangle(
      IXPathNavigable element,
      string name,
      Rectangle defaultValue)
    {
      return QMisc.GetAsRectangle((object)QXmlHelper.GetChildElementString(element, name), defaultValue);
    }

    public static Enum GetChildElementEnum(IXPathNavigable element, string name, Type enumType) => QXmlHelper.GetChildElementEnum(element, name, enumType, (Enum)null);

    public static Enum GetChildElementEnum(
      IXPathNavigable element,
      string name,
      Type enumType,
      Enum defaultValue)
    {
      string childElementString = QXmlHelper.GetChildElementString(element, name);
      try
      {
        return (Enum)Enum.Parse(enumType, childElementString, true);
      }
      catch (ArgumentException ex)
      {
        return defaultValue;
      }
    }
  }
}
