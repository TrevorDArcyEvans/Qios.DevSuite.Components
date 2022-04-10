// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QColorSchemeBase
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Xml;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QColorSchemeTypeConverter))]
  public abstract class QColorSchemeBase : IDisposable, IQWeakEventPublisher
  {
    public const string DefaultThemeName = "Default";
    public const string LunaBlueThemeName = "LunaBlue";
    public const string LunaOliveThemeName = "LunaOlive";
    public const string LunaSilverThemeName = "LunaSilver";
    public const string HighContrastThemeName = "HighContrast";
    public const string AeroThemeName = "VistaBlack";
    private int m_iColorsChangedSuspend;
    private bool m_bIsRegistered;
    private QColorSchemeBase m_oBaseColorScheme;
    private QColorSchemeScope m_eColorSchemeScope;
    private bool m_bInheritCurrentThemeFromGlobal = true;
    private bool m_bIsDisposed;
    private int m_iCurrentThemeIndex;
    private QThemeInfoCollection m_aThemes;
    private bool m_bIsGlobalColorScheme;
    private Hashtable m_aColors;
    private bool m_bWeakEventHandlers = true;
    private QWeakDelegate m_oColorsChangedDelegate;
    private QWeakDelegate m_oThemesChangedDelegate;

    internal QColorSchemeBase(bool isGlobalColorScheme, bool register)
    {
      this.m_aThemes = new QThemeInfoCollection(this);
      this.m_aColors = new Hashtable((IEqualityComparer) StringComparer.InvariantCultureIgnoreCase);
      this.m_bIsGlobalColorScheme = isGlobalColorScheme;
      if (isGlobalColorScheme || !register)
        return;
      QColorScheme.Global.RegisterColorScheme(this);
      this.m_bIsRegistered = true;
    }

    [Browsable(false)]
    protected bool IsRegistered => this.m_bIsRegistered;

    protected abstract QColorSchemeBase CreateInstanceForClone();

    public bool ShouldSerializeThemes()
    {
      for (int index = 0; index < this.Themes.Count; ++index)
      {
        if (!this.Themes[index].IsSystemTheme)
          return true;
      }
      return false;
    }

    public void ResetThemes()
    {
      for (int index = this.Themes.Count - 1; index >= 0; --index)
      {
        if (!this.Themes[index].IsSystemTheme)
          this.Themes.Remove(this.Themes[index]);
      }
    }

    [QWeakEvent]
    public event EventHandler ColorsChanged
    {
      add => this.m_oColorsChangedDelegate = QWeakDelegate.Combine(this.m_oColorsChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oColorsChangedDelegate = QWeakDelegate.Remove(this.m_oColorsChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    public event EventHandler ThemesChanged
    {
      add => this.m_oThemesChangedDelegate = QWeakDelegate.Combine(this.m_oThemesChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oThemesChangedDelegate = QWeakDelegate.Remove(this.m_oThemesChangedDelegate, (Delegate) value);
    }

    [DefaultValue(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [RefreshProperties(RefreshProperties.All)]
    [Category("QBehavior")]
    [Description("Gets the Theme information that is defined for this QColorScheme")]
    public QThemeInfoCollection Themes => this.m_aThemes;

    [Description("Indicates if this QColorScheme inherits the CurrentTheme from the GlobalColorScheme")]
    [DefaultValue(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("QBehavior")]
    public virtual bool InheritCurrentThemeFromGlobal
    {
      get => this.m_bInheritCurrentThemeFromGlobal;
      set
      {
        if (value == this.m_bInheritCurrentThemeFromGlobal)
          return;
        this.m_bInheritCurrentThemeFromGlobal = value;
        this.RaiseColorsChanged();
      }
    }

    [Description("Gets or sets the scope of this QColorScheme.")]
    [RefreshProperties(RefreshProperties.All)]
    [Category("QBehavior")]
    [DefaultValue(QColorSchemeScope.Control)]
    public virtual QColorSchemeScope Scope
    {
      get => this.m_eColorSchemeScope;
      set => this.m_eColorSchemeScope = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual QColorSchemeBase BaseColorScheme
    {
      get
      {
        if (this.m_bIsGlobalColorScheme)
          return (QColorSchemeBase) null;
        return this.m_oBaseColorScheme != null ? this.m_oBaseColorScheme : (QColorSchemeBase) QColorScheme.Global;
      }
      set
      {
        if (this.m_bIsGlobalColorScheme)
          return;
        this.SetBaseColorScheme(value, true);
      }
    }

    public void SetBaseColorScheme(QColorSchemeBase baseColorScheme, bool raiseEvent)
    {
      if (this.m_bIsGlobalColorScheme)
        return;
      this.m_oBaseColorScheme = baseColorScheme;
      if (!raiseEvent)
        return;
      this.RaiseColorsChanged();
    }

    public virtual bool ShouldSerializeCurrentTheme() => !this.InheritCurrentThemeFromGlobal && this.CurrentTheme != "Default";

    public virtual void ResetCurrentTheme() => this.CurrentTheme = "Default";

    [TypeConverter(typeof (QColorSchemeCurrentThemeTypeConverter))]
    [Description("Gets or sets the CurrentTheme. When the InheritCurrentThemeFromGlobal property is set, then the CurrentTheme from Global is always returned.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("QAppearance")]
    public virtual string CurrentTheme
    {
      get
      {
        if (this.BaseColorScheme != null && this.InheritCurrentThemeFromGlobal)
          return this.BaseColorScheme.CurrentTheme;
        return this.m_iCurrentThemeIndex < 0 || this.m_iCurrentThemeIndex >= this.Themes.Count ? this.Themes[0].ThemeName : this.Themes[this.m_iCurrentThemeIndex].ThemeName;
      }
      set
      {
        int num = this.Themes.IndexOf(value);
        if (num < 0)
          num = this.Themes.Add(new QThemeInfo(value));
        this.m_iCurrentThemeIndex = num;
        this.RaiseColorsChanged();
      }
    }

    [Browsable(false)]
    public bool IsGlobalColorScheme => this.m_bIsGlobalColorScheme;

    public QColor this[string colorName] => this.GetColor(colorName) ?? throw new InvalidOperationException("Cannot get color with name " + colorName + ". Color is not defined");

    public bool IsValidColor(string colorName) => this.IsGlobalColorScheme ? this.m_aColors.Contains((object) colorName) : QColorScheme.Global.IsValidColor(colorName);

    [Browsable(false)]
    public bool UseHighContrast => this.CurrentTheme == "HighContrast";

    [Browsable(false)]
    public bool IsDisposed => this.m_bIsDisposed;

    public void SetAllThemeColors(string colorName, Color color)
    {
      QColor qcolor = this[colorName];
      for (int index = 0; index < this.Themes.Count; ++index)
        qcolor.SetColor(this.Themes[index].ThemeName, color);
    }

    public bool ShouldSerialize()
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this);
      for (int index = 0; index < properties.Count; ++index)
      {
        if (properties[index].PropertyType == typeof (QColor))
        {
          if (this.m_aColors.Contains((object) properties[index].Name) && this[properties[index].Name].ShouldSerialize())
            return true;
        }
        else if (!properties[index].IsReadOnly && properties[index].SerializationVisibility != DesignerSerializationVisibility.Hidden && properties[index].ShouldSerializeValue((object) this))
          return true;
      }
      return false;
    }

    public virtual void Reset()
    {
      this.SuspendColorsChanged();
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this);
      for (int index = 0; index < properties.Count; ++index)
      {
        if (properties[index].PropertyType == typeof (QColor))
          this[properties[index].Name].Reset();
        else
          properties[index].ResetValue((object) this);
      }
      this.ResumeColorsChanged(true);
    }

    public IXPathNavigable SaveToXml(string fileName, QColorSaveType saveOptions)
    {
      XmlDocument parentNode = new XmlDocument();
      this.SaveToXml((IXPathNavigable) parentNode, saveOptions);
      try
      {
        parentNode.Save(fileName);
      }
      catch (Exception ex)
      {
        throw new XmlException(QResources.GetException("QColorScheme_FileCannotBeSaved", (object) fileName), ex);
      }
      return (IXPathNavigable) parentNode.DocumentElement;
    }

    public void LoadFromXml(string fileName)
    {
      XmlDocument xmlDocument;
      try
      {
        xmlDocument = new XmlDocument();
        xmlDocument.Load(fileName);
      }
      catch (Exception ex)
      {
        throw new XmlException(QResources.GetException("QColorScheme_FileCannotBeLoaded", (object) fileName), ex);
      }
      if (!(xmlDocument.SelectSingleNode("colorScheme") is XmlElement collectionElement))
        throw new XmlException(QResources.GetException("QColorScheme_ColorSchemeNotFound"));
      this.LoadFromXml((IXPathNavigable) collectionElement);
    }

    public void LoadFromXml(IXPathNavigable collectionElement)
    {
      if (collectionElement == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (collectionElement)));
      try
      {
        this.SuspendColorsChanged();
        object attributeEnum = (object) QXmlHelper.GetAttributeEnum(collectionElement, "saveOptions", typeof (QColorSaveType));
        if (attributeEnum != null)
        {
          int num = (int) attributeEnum;
        }
        IXPathNavigable xpathNavigable = QXmlHelper.SelectChildNavigable(collectionElement, "themes");
        if (xpathNavigable != null)
        {
          XPathNodeIterator xpathNodeIterator = xpathNavigable.CreateNavigator().SelectChildren("theme", "");
          while (xpathNodeIterator.MoveNext())
          {
            IXPathNavigable navigableFromNavigator = QXmlHelper.GetNavigableFromNavigator(xpathNodeIterator.Current);
            if (navigableFromNavigator != null && Type.GetType(QXmlHelper.GetAttributeString(navigableFromNavigator, "type"), false, true) == typeof (QThemeInfo))
            {
              QThemeInfo info = new QThemeInfo();
              info.LoadFromXml(navigableFromNavigator);
              if (!this.Themes.Contains(info.ThemeName))
                this.Themes.Add(info);
            }
          }
        }
        XPathNodeIterator xpathNodeIterator1 = collectionElement.CreateNavigator().SelectChildren("color", "");
        while (xpathNodeIterator1.MoveNext())
        {
          IXPathNavigable navigableFromNavigator = QXmlHelper.GetNavigableFromNavigator(xpathNodeIterator1.Current);
          if (navigableFromNavigator != null)
          {
            string attributeString = QXmlHelper.GetAttributeString(navigableFromNavigator, "name");
            if (Type.GetType(QXmlHelper.GetAttributeString(navigableFromNavigator, "type"), false, true) == typeof (QColor))
              this[attributeString].LoadFromXml(navigableFromNavigator);
          }
        }
      }
      finally
      {
        this.ResumeColorsChanged(true);
      }
    }

    public IXPathNavigable SaveToXml(
      IXPathNavigable parentNode,
      QColorSaveType saveOptions)
    {
      IXPathNavigable xml = QXmlHelper.AddElement(parentNode, "colorScheme");
      QXmlHelper.AddAttribute(xml, nameof (saveOptions), (object) saveOptions);
      IXPathNavigable collectionElement = QXmlHelper.AddElement(xml, "themes");
      for (int index = 0; index < this.Themes.Count; ++index)
      {
        if (!this.Themes[index].IsSystemTheme)
          this.Themes[index].SaveToXml(collectionElement);
      }
      IEnumerator enumerator = saveOptions != QColorSaveType.AllColors ? (IEnumerator) this.m_aColors.GetEnumerator() : TypeDescriptor.GetProperties((object) this).GetEnumerator();
      while (enumerator.MoveNext() && enumerator.Current != null)
      {
        QColor qcolor = (QColor) null;
        if (enumerator.Current is DictionaryEntry)
          qcolor = ((DictionaryEntry) enumerator.Current).Value as QColor;
        else if (enumerator.Current is PropertyDescriptor)
        {
          PropertyDescriptor current = enumerator.Current as PropertyDescriptor;
          if (current.PropertyType == typeof (QColor))
            qcolor = this[current.Name];
        }
        if (qcolor != null && (saveOptions == QColorSaveType.AllColors || qcolor.ShouldSerialize()))
          qcolor.SaveToXml(xml, saveOptions);
      }
      return xml;
    }

    internal QColorSchemeBase CloneColorScheme()
    {
      QColorSchemeBase instanceForClone = this.CreateInstanceForClone();
      for (int index = 0; index < this.Themes.Count; ++index)
        instanceForClone.Themes.Add(this.Themes[index].CloneTheme());
      foreach (QColor qcolor in (IEnumerable) this.m_aColors.Values)
      {
        QColor color = qcolor.CloneColor(instanceForClone);
        instanceForClone.AddColor(color);
      }
      return instanceForClone;
    }

    public void SuspendColorsChanged() => ++this.m_iColorsChangedSuspend;

    internal int ColorsChangedSuspendCount => this.m_iColorsChangedSuspend;

    public void ResumeColorsChanged(bool raise)
    {
      if (this.m_iColorsChangedSuspend > 0)
        --this.m_iColorsChangedSuspend;
      if (!raise)
        return;
      this.RaiseColorsChanged();
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void RaiseColorsChanged()
    {
      if (this.m_iColorsChangedSuspend > 0)
        return;
      this.OnColorsChanged(EventArgs.Empty);
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void RaiseThemesChanged()
    {
      if (this.m_iColorsChangedSuspend > 0)
        return;
      this.OnThemesChanged(EventArgs.Empty);
    }

    internal void AddColor(string name, params Color[] defaultColors) => this.AddColor(new QColor(this, name, defaultColors));

    internal void AddColor(string name, string defaultColorReference) => this.AddColor(new QColor(this, name, defaultColorReference));

    internal void AddColor(QColor color) => this.m_aColors.Add((object) color.ColorName, (object) color);

    internal QColor GetColor(string colorName)
    {
      switch (colorName)
      {
        case "":
        case null:
          return QColor.Empty;
        default:
          if (this.IsGlobalColorScheme)
            return (QColor) this.m_aColors[(object) colorName];
          if (!(this.m_aColors[(object) colorName] is QColor color) && QColorScheme.Global.IsValidColor(colorName))
          {
            color = new QColor(this, colorName);
            this.AddColor(color);
          }
          return color;
      }
    }

    protected virtual void OnColorsChanged(EventArgs e) => this.m_oColorsChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oColorsChangedDelegate, (object) this, (object) e);

    protected virtual void OnThemesChanged(EventArgs e) => this.m_oThemesChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oThemesChangedDelegate, (object) this, (object) e);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.IsDisposed || !disposing)
        return;
      if (!this.IsGlobalColorScheme && !QColorScheme.Global.IsDisposed && this.m_bIsRegistered)
      {
        QColorScheme.Global.UnregisterColorScheme(this);
        this.m_bIsRegistered = false;
      }
      this.m_bIsDisposed = true;
    }

    ~QColorSchemeBase() => this.Dispose(false);
  }
}
