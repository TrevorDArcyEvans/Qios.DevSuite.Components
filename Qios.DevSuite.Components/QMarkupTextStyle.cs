// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextStyle
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Xml;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QMarkupTextStyleConverter))]
  public class QMarkupTextStyle : ICloneable
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QMarkupText m_oParentMarkupText;
    private string m_sDefaultTag;
    private string[] m_aAdditionalTags = new string[0];
    private QFontDefinition m_oFontDefintion = QFontDefinition.Empty;
    private QFontDefinition m_oFontDefintionHot = QFontDefinition.Empty;
    private QFontDefinition m_oFontDefintionActive = QFontDefinition.Empty;
    private Color m_oTextColor = Color.Empty;
    private Color m_oTextColorHot = Color.Empty;
    private Color m_oTextColorActive = Color.Empty;
    private Color m_oTextColorDisabled = Color.Empty;
    private string m_sTextColorProperty;
    private string m_sTextColorHotProperty;
    private string m_sTextColorActiveProperty;
    private string m_sTextColorDisabledProperty;
    private bool m_bNewLineBefore;
    private bool m_bNewLineAfter;
    private bool m_bAddEmptyPart;
    private bool m_bCanFocus;
    private QTristateBool m_eWrapText;
    private Type m_oElementType = typeof (QMarkupTextElement);
    private bool m_bReapplyAttributesOnStateChange;
    private bool m_bCanBeSourceForEvents = true;

    public QMarkupTextStyle()
    {
    }

    public QMarkupTextStyle(string defaultTag) => this.m_sDefaultTag = defaultTag;

    public QMarkupTextStyle(string defaultTag, Type elementType)
    {
      this.m_sDefaultTag = defaultTag;
      this.m_oElementType = elementType;
    }

    [Category("QBehavior")]
    [Description("Gets or sets the default tag that this style handles. For additional tags use AdditionalTags.")]
    [DefaultValue(null)]
    public virtual string DefaultTag
    {
      get => this.m_sDefaultTag;
      set
      {
        this.m_sDefaultTag = value;
        this.NotifyParentMarkupTextOfChange(true, true);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual string[] AdditionalTags
    {
      get => this.m_aAdditionalTags;
      set
      {
        this.m_aAdditionalTags = value;
        this.NotifyParentMarkupTextOfChange(true, true);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Type ElementType
    {
      get => this.m_oElementType;
      set
      {
        if (value == null)
          value = typeof (QMarkupTextElement);
        this.m_oElementType = value == typeof (QMarkupTextElement) || value.IsSubclassOf(typeof (QMarkupTextElement)) ? value : throw new InvalidOperationException(QResources.GetException("QMarkupTextStyle_ElementTypeNotQMarkupTextElement", (object) value.ToString()));
      }
    }

    [Description("Gets or sets the TextColor to use. Set it to null to inherit it. This can be a string containing a QColorScheme property or a color. Because the color is serialized to code as a string it is parsed with the InvariantCulture.")]
    [Editor(typeof (QColorStringEditor), typeof (UITypeEditor))]
    [Category("QBehavior")]
    [DefaultValue(null)]
    public string TextColorProperty
    {
      get => this.m_sTextColorProperty;
      set
      {
        this.m_sTextColorProperty = value;
        this.NotifyParentMarkupTextOfChange();
      }
    }

    public Color GetUsedTextColor(QColorScheme colorScheme, Color defaultColor) => QMisc.GetAsColor(this.m_sTextColorProperty, colorScheme, defaultColor);

    [DefaultValue(null)]
    [Editor(typeof (QColorStringEditor), typeof (UITypeEditor))]
    [Category("QBehavior")]
    [Description("Gets or sets the TextColor to use for a hot element Set it to null to inherit it. This can be a string containing a QColorScheme property or a color. Because the color is serialized to code as a string it is parsed with the InvariantCulture.")]
    public string TextColorHotProperty
    {
      get => this.m_sTextColorHotProperty;
      set
      {
        this.m_sTextColorHotProperty = value;
        this.NotifyParentMarkupTextOfChange();
      }
    }

    public Color GetUsedTextColorHot(QColorScheme colorScheme, Color defaultColor) => QMisc.GetAsColor(this.m_sTextColorHotProperty, colorScheme, defaultColor);

    [Description("Gets or sets the TextColor to use for an active element. Set it to null to inherit it. This can be a string containing a QColorScheme property or a color. Because the color is serialized to code as a string it is parsed with the InvariantCulture.")]
    [Editor(typeof (QColorStringEditor), typeof (UITypeEditor))]
    [DefaultValue(null)]
    [Category("QBehavior")]
    public string TextColorActiveProperty
    {
      get => this.m_sTextColorActiveProperty;
      set
      {
        this.m_sTextColorActiveProperty = value;
        this.NotifyParentMarkupTextOfChange();
      }
    }

    public Color GetUsedTextColorActive(QColorScheme colorScheme, Color defaultColor) => QMisc.GetAsColor(this.m_sTextColorActiveProperty, colorScheme, defaultColor);

    [Category("QBehavior")]
    [Description("Gets or sets the TextColor to use for an active element Set it to null to inherit it. This can be a string containing a QColorScheme property or a color. Because the color is serialized to code as a string it is parsed with the InvariantCulture.")]
    [DefaultValue(null)]
    [Editor(typeof (QColorStringEditor), typeof (UITypeEditor))]
    public string TextColorDisabledProperty
    {
      get => this.m_sTextColorDisabledProperty;
      set
      {
        this.m_sTextColorDisabledProperty = value;
        this.NotifyParentMarkupTextOfChange();
      }
    }

    public Color GetUsedTextColorDisabled(QColorScheme colorScheme, Color defaultColor) => QMisc.GetAsColor(this.m_sTextColorDisabledProperty, colorScheme, defaultColor);

    [Description("Gets or sets the default FontDefinition")]
    [Category("QBehavior")]
    [DefaultValue(typeof (QFontDefinition), "Empty")]
    public QFontDefinition FontDefinition
    {
      get => this.m_oFontDefintion;
      set
      {
        this.m_oFontDefintion = value;
        this.NotifyParentMarkupTextOfChange();
      }
    }

    [DefaultValue(typeof (QFontDefinition), "Empty")]
    [Description("Gets or sets the FontDefinition for a hot element")]
    [Category("QBehavior")]
    public QFontDefinition FontDefinitionHot
    {
      get => this.m_oFontDefintionHot;
      set
      {
        this.m_oFontDefintionHot = value;
        this.NotifyParentMarkupTextOfChange();
      }
    }

    [DefaultValue(typeof (QFontDefinition), "Empty")]
    [Category("QBehavior")]
    [Description("Gets or sets the FontDefinition for an active element")]
    public QFontDefinition FontDefinitionActive
    {
      get => this.m_oFontDefintionActive;
      set
      {
        this.m_oFontDefintionActive = value;
        this.NotifyParentMarkupTextOfChange();
      }
    }

    [Description("Gets or sets whether the user can focus the element.")]
    [Category("QBehavior")]
    [DefaultValue(false)]
    public bool CanFocus
    {
      get => this.m_bCanFocus;
      set => this.m_bCanFocus = value;
    }

    [DefaultValue(false)]
    [Category("QBehavior")]
    [Description("Gets or sets whether this style inserts a new line before it is rendered.")]
    public bool NewLineBefore
    {
      get => this.m_bNewLineBefore;
      set
      {
        this.m_bNewLineBefore = value;
        this.NotifyParentMarkupTextOfChange();
      }
    }

    [Description("Gets or sets whether this style inserts a new line after it is rendered.")]
    [Category("QBehavior")]
    [DefaultValue(false)]
    public bool NewLineAfter
    {
      get => this.m_bNewLineAfter;
      set
      {
        this.m_bNewLineAfter = value;
        this.NotifyParentMarkupTextOfChange();
      }
    }

    [DefaultValue(false)]
    [Category("QBehavior")]
    [Description("Gets or sets whether this style adds an empty part with the height of the currentFont to the currentLine. This can be usefull to render empty lines like the 'BR' tag. Always combine this property with NewLineAfter")]
    public bool AddEmptyPart
    {
      get => this.m_bAddEmptyPart;
      set
      {
        this.m_bAddEmptyPart = value;
        this.NotifyParentMarkupTextOfChange();
      }
    }

    [Description("Gets or sets whether this style should wrap text.")]
    [Category("QBehavior")]
    [DefaultValue(QTristateBool.Undefined)]
    public QTristateBool WrapText
    {
      get => this.m_eWrapText;
      set
      {
        this.m_eWrapText = value;
        this.NotifyParentMarkupTextOfChange();
      }
    }

    [Description("Indicates if the the attributes should be reapplied when the state of an element changes. This should be set to true when an element is interactive and changes when it is active or hot.")]
    [Category("QBehavior")]
    [DefaultValue(false)]
    public bool ReapplyAttributesOnStateChange
    {
      get => this.m_bReapplyAttributesOnStateChange;
      set => this.m_bReapplyAttributesOnStateChange = value;
    }

    [Category("QBehavior")]
    [Description("Indicates that this element can be source for events. If this is not true and an event takes place on this element the parent is used as the source (if that has CanBeSourceForEvents set to true).")]
    [DefaultValue(true)]
    public virtual bool CanBeSourceForEvents
    {
      get => this.m_bCanBeSourceForEvents;
      set => this.m_bCanBeSourceForEvents = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QMarkupText ParentMarkupText => this.m_oParentMarkupText;

    internal void PutParentMarkupText(QMarkupText value) => this.m_oParentMarkupText = value;

    public QColorScheme ColorScheme => this.ParentMarkupText == null ? (QColorScheme) null : this.ParentMarkupText.ColorScheme;

    public void ApplyAllFontDefinitions(QFontDefinition definition)
    {
      this.m_oFontDefintion = definition;
      this.m_oFontDefintionHot = definition;
      this.m_oFontDefintionActive = definition;
    }

    public virtual QMarkupTextElement CreateElement(XmlNode node) => Activator.CreateInstance(this.m_oElementType, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, new object[2]
    {
      (object) this,
      (object) node.Name
    }, (CultureInfo) null) as QMarkupTextElement;

    public virtual bool CanRenderNode(XmlNode node)
    {
      string strA = node != null ? node.Name : string.Empty;
      if (string.Compare(strA, this.DefaultTag, true, CultureInfo.InvariantCulture) == 0)
        return true;
      foreach (string additionalTag in this.AdditionalTags)
      {
        if (string.Compare(strA, additionalTag, true, CultureInfo.InvariantCulture) == 0)
          return true;
      }
      return false;
    }

    private void NotifyParentMarkupTextOfChange() => this.NotifyParentMarkupTextOfChange(false, true);

    private void NotifyParentMarkupTextOfChange(bool processMarkup, bool applyAttributes)
    {
      if (this.m_oParentMarkupText == null)
        return;
      if (processMarkup)
        this.m_oParentMarkupText.ProcessMarkup();
      if (applyAttributes)
        this.m_oParentMarkupText.ApplyAttributes();
      this.m_oParentMarkupText.RaiseUpdateRequested(QCommandUIRequest.PerformLayout);
    }

    public object Clone() => QObjectCloner.CloneObject((object) this);
  }
}
