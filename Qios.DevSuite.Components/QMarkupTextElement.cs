// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextElement
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;

namespace Qios.DevSuite.Components
{
  public class QMarkupTextElement : IQWeakEventPublisher
  {
    public const string AttributeName = "name";
    public const string AttributeEnabled = "enabled";
    public const string AttributeWrapText = "wrapText";
    private bool m_bWeakEventHandlers = true;
    private string m_sTag;
    private Font m_oCurrentFont;
    private Color m_oCurrentColor;
    private bool m_bFocused;
    private bool m_bActive;
    private bool m_bHot;
    private bool m_bCanFocus;
    private bool m_bReapplyAttributesOnStateChange;
    private bool m_bCanBeSourceForEvents = true;
    private XmlNode m_oMarkupNode;
    private QMarkupTextStyle m_oOwningStyle;
    private QMarkupTextElement m_oParentElement;
    private QMarkupTextElementCollection m_oElements;
    private QMarkupTextRenderedPartCollection m_oParts;
    private QWeakDelegate m_oMouseEnterDelegate;
    private QWeakDelegate m_oMouseLeaveDelegate;
    private QWeakDelegate m_oMouseDownDelegate;
    private QWeakDelegate m_oMouseUpDelegate;
    private QWeakDelegate m_oMouseClickDelegate;
    private QWeakDelegate m_oLinkClickDelegate;
    private QWeakDelegate m_oGotFocusDelegate;
    private QWeakDelegate m_oLostFocusDelegate;

    protected QMarkupTextElement(QMarkupTextStyle owningStyle, string tag)
    {
      this.m_sTag = tag;
      this.m_oOwningStyle = owningStyle;
      this.m_bReapplyAttributesOnStateChange = this.m_oOwningStyle.ReapplyAttributesOnStateChange;
      this.m_bCanBeSourceForEvents = this.m_oOwningStyle.CanBeSourceForEvents;
      this.m_bCanFocus = this.m_oOwningStyle.CanFocus;
    }

    [Category("QEvents")]
    [Description("Occurs when the mouse enters an element.")]
    [QWeakEvent]
    public event QMarkupTextElementEventHandler MouseEnter
    {
      add => this.m_oMouseEnterDelegate = QWeakDelegate.Combine(this.m_oMouseEnterDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMouseEnterDelegate = QWeakDelegate.Remove(this.m_oMouseEnterDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the mouse leaves this element.")]
    public event QMarkupTextElementEventHandler MouseLeave
    {
      add => this.m_oMouseLeaveDelegate = QWeakDelegate.Combine(this.m_oMouseLeaveDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMouseLeaveDelegate = QWeakDelegate.Remove(this.m_oMouseLeaveDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the user presses the mousebutton on this element.")]
    public event QMarkupTextElementEventHandler MouseDown
    {
      add => this.m_oMouseDownDelegate = QWeakDelegate.Combine(this.m_oMouseDownDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMouseDownDelegate = QWeakDelegate.Remove(this.m_oMouseDownDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Occurs when the user releases the mousebutton on this element.")]
    [Category("QEvents")]
    public event QMarkupTextElementEventHandler MouseUp
    {
      add => this.m_oMouseUpDelegate = QWeakDelegate.Combine(this.m_oMouseUpDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMouseUpDelegate = QWeakDelegate.Remove(this.m_oMouseUpDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the user clicks on this element.")]
    public event QMarkupTextElementEventHandler MouseClick
    {
      add => this.m_oMouseClickDelegate = QWeakDelegate.Combine(this.m_oMouseClickDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMouseClickDelegate = QWeakDelegate.Remove(this.m_oMouseClickDelegate, (Delegate) value);
    }

    [Description("Occurs when the user clicks on a link.")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QMarkupTextElementEventHandler LinkClick
    {
      add => this.m_oLinkClickDelegate = QWeakDelegate.Combine(this.m_oLinkClickDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oLinkClickDelegate = QWeakDelegate.Remove(this.m_oLinkClickDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when an element gets the focus")]
    public event QMarkupTextElementEventHandler GotFocus
    {
      add => this.m_oGotFocusDelegate = QWeakDelegate.Combine(this.m_oGotFocusDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oGotFocusDelegate = QWeakDelegate.Remove(this.m_oGotFocusDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when an element lost the focus")]
    public event QMarkupTextElementEventHandler LostFocus
    {
      add => this.m_oLostFocusDelegate = QWeakDelegate.Combine(this.m_oLostFocusDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oLostFocusDelegate = QWeakDelegate.Remove(this.m_oLostFocusDelegate, (Delegate) value);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    public string Tag => this.m_sTag;

    public string Name
    {
      get => this.GetAttributeAsString("name", string.Empty);
      set => this.SetAttributeValue("name", value, false, false);
    }

    public virtual bool IsAccessible => this.Enabled;

    public virtual bool Enabled
    {
      get => (this.ParentElement == null || this.ParentElement.Enabled) && QMisc.GetAsBool((object) this.GetAttributeAsString("enabled"), true);
      set => this.SetAttributeValue("enabled", QMisc.GetAsString((object) value), true, true);
    }

    public void ResetEnabled() => this.ResetAttributeValue("enabled", true, true);

    public bool ParentWrapText => this.ParentElement == null ? this.OwningMarkupText.Configuration.WrapText : this.ParentElement.WrapText;

    public virtual bool WrapText
    {
      get => QMisc.GetAsBool((object) this.GetAttributeAsString("wrapText"), QMisc.GetTristateAsBool(this.OwningStyle.WrapText, this.ParentWrapText));
      set => this.SetAttributeValue("wrapText", QMisc.GetAsString((object) value), true, true);
    }

    public void ResetWrapText() => this.ResetAttributeValue("wrapText", true, true);

    public bool Active => this.m_bActive;

    internal void PutActive(bool value)
    {
      this.m_bActive = value;
      if (!this.ReapplyAttributesOnStateChange || this.AnyParentReapliesAttributesOnStateChange())
        return;
      this.ApplyAttributes();
      this.OwningMarkupText.RaiseUpdateRequested(QCommandUIRequest.PerformLayout);
    }

    public bool Hot => this.m_bHot;

    internal void PutHot(bool value)
    {
      this.m_bHot = value;
      if (!this.ReapplyAttributesOnStateChange || this.AnyParentReapliesAttributesOnStateChange())
        return;
      this.ApplyAttributes();
      this.OwningMarkupText.RaiseUpdateRequested(QCommandUIRequest.PerformLayout);
    }

    public virtual bool Focused
    {
      get
      {
        if (this.m_bFocused)
          return true;
        return !this.m_bCanFocus && this.ParentElement != null && this.ParentElement.Focused;
      }
    }

    internal virtual void PutFocused(bool value)
    {
      this.m_bFocused = value;
      this.OwningMarkupText.RaiseUpdateRequested(QCommandUIRequest.Redraw);
    }

    public Font ParentFont => this.ParentElement == null ? (Font) null : this.ParentElement.CurrentFont;

    public Font CurrentFont => this.m_oCurrentFont == null ? this.ParentFont : this.m_oCurrentFont;

    protected void PutCurrentFont(Font value) => this.m_oCurrentFont = value;

    public Color ParentColor => this.ParentElement == null ? Color.Empty : this.ParentElement.CurrentColor;

    public Color CurrentColor => !(this.m_oCurrentColor != Color.Empty) ? this.ParentColor : this.m_oCurrentColor;

    protected void PutCurrentColor(Color value) => this.m_oCurrentColor = value;

    public QMarkupTextElement ParentElement => this.m_oParentElement;

    internal void PutParentElement(QMarkupTextElement value) => this.m_oParentElement = value;

    public QMarkupTextStyle OwningStyle => this.m_oOwningStyle;

    public QMarkupText OwningMarkupText => this.m_oOwningStyle.ParentMarkupText;

    public XmlNode MarkupNode => this.m_oMarkupNode;

    public bool HasElements => this.m_oElements != null && this.m_oElements.Count > 0;

    public QMarkupTextElementCollection Elements
    {
      get
      {
        if (this.m_oElements == null)
          this.m_oElements = new QMarkupTextElementCollection(this.m_oOwningStyle.ParentMarkupText, this);
        return this.m_oElements;
      }
    }

    public bool HasParts => this.m_oParts != null && this.m_oParts.Count > 0;

    public QMarkupTextRenderedPartCollection Parts
    {
      get
      {
        if (this.m_oParts == null)
          this.m_oParts = new QMarkupTextRenderedPartCollection(this);
        return this.m_oParts;
      }
    }

    public virtual bool CanFocus
    {
      get
      {
        if (this.m_bCanFocus)
          return true;
        return this.ParentElement != null && this.ParentElement.CanFocus;
      }
      set => this.m_bCanFocus = value;
    }

    internal QMarkupTextElement FocusableParentElement
    {
      get
      {
        if (this.ParentElement == null)
          return (QMarkupTextElement) null;
        return this.m_bCanFocus ? this : this.ParentElement.FocusableParentElement;
      }
    }

    public virtual bool CanBeSourceForEvents
    {
      get => this.m_bCanBeSourceForEvents;
      set => this.m_bCanBeSourceForEvents = value;
    }

    public virtual bool ReapplyAttributesOnStateChange
    {
      get => this.m_bReapplyAttributesOnStateChange;
      set => this.m_bReapplyAttributesOnStateChange = value;
    }

    public bool AnyParentReapliesAttributesOnStateChange()
    {
      if (this.ParentElement == null)
        return false;
      return this.ParentElement.ReapplyAttributesOnStateChange || this.ParentElement.AnyParentReapliesAttributesOnStateChange();
    }

    public bool ContainsOrIsElement(QMarkupTextElement element) => element == this || this.ContainsElement(element);

    public bool ContainsElement(QMarkupTextElement element)
    {
      while (element != null && element != this)
        element = element.ParentElement;
      return element != null;
    }

    public QMarkupTextElement FindElement(string name) => this.FindElement(name, true);

    public QMarkupTextElement FindElement(string name, bool deep) => this.HasElements ? this.m_oElements.FindElement(name, deep) : (QMarkupTextElement) null;

    public bool IsOrHasParentOfType(System.Type type)
    {
      System.Type type1 = this.GetType();
      if (type1 == type || type1.IsSubclassOf(type))
        return true;
      return this.ParentElement != null && this.ParentElement.IsOrHasParentOfType(type);
    }

    internal QMarkupTextElement GetFocusableElement(bool forward, bool parents)
    {
      if (!parents && this.m_bCanFocus)
        return this;
      if (this.ParentElement != null && parents && this.CanFocus)
        return this.FocusableParentElement;
      return this.HasElements ? this.m_oElements.GetFocusableElement(forward, false) : (QMarkupTextElement) null;
    }

    public QMarkupTextElement GetElementThatContainsAbsolutePoint(PointF point)
    {
      if (this.HasParts && this.m_oParts.GetPartThatContainsAbsolutePoint(point) != null)
        return this;
      return this.HasElements ? this.m_oElements.GetElementThatContainsAbsolutePoint(point) : (QMarkupTextElement) null;
    }

    public bool ContainsAbsolutePoint(PointF point) => this.HasParts && this.m_oParts.GetPartThatContainsAbsolutePoint(point) != null || this.HasElements && this.m_oElements.GetElementThatContainsAbsolutePoint(point) != null;

    internal static void SetAttributeValue(XmlNode node, string name, string value) => (QMarkupTextElement.GetAttribute(node, name) ?? node.Attributes.Append(node.OwnerDocument.CreateAttribute(name))).Value = value;

    internal static void ResetAttributeValue(XmlNode node, string name)
    {
      XmlAttribute attribute = QMarkupTextElement.GetAttribute(node, name);
      if (attribute == null)
        return;
      node.Attributes.Remove(attribute);
    }

    internal static XmlAttribute GetAttribute(XmlNode node, string name)
    {
      if (node.Attributes != null)
      {
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) node.Attributes)
        {
          if (string.Compare(attribute.Name, name, true, CultureInfo.InvariantCulture) == 0 && !QMisc.IsEmpty((object) attribute.Value))
            return attribute;
        }
      }
      return (XmlAttribute) null;
    }

    internal static string GetAttributeAsString(XmlNode node, string name, string defaultValue)
    {
      XmlAttribute attribute = QMarkupTextElement.GetAttribute(node, name);
      return attribute != null && !QMisc.IsEmpty((object) attribute.Value) ? attribute.Value : defaultValue;
    }

    public void SetAttributeValue(
      string name,
      string value,
      bool reapplyAttributes,
      bool raiseUpdateRequested)
    {
      QMarkupTextElement.SetAttributeValue(this.m_oMarkupNode, name, value);
      if (reapplyAttributes)
        this.ApplyAttributes();
      if (!raiseUpdateRequested)
        return;
      this.OwningMarkupText.RaiseUpdateRequested(QCommandUIRequest.PerformLayout);
    }

    public void ResetAttributeValue(string name, bool reapplyAttributes, bool raiseUpdateRequested)
    {
      QMarkupTextElement.ResetAttributeValue(this.MarkupNode, name);
      if (reapplyAttributes)
        this.ApplyAttributes();
      if (!raiseUpdateRequested)
        return;
      this.OwningMarkupText.RaiseUpdateRequested(QCommandUIRequest.PerformLayout);
    }

    public string GetAttributeAsString(string name, string defaultValue) => QMarkupTextElement.GetAttributeAsString(this.MarkupNode, name, defaultValue);

    public string GetAttributeAsString(string name) => QMarkupTextElement.GetAttributeAsString(this.MarkupNode, name, string.Empty);

    protected string GetNextTextPart(
      string text,
      ref int startIndex,
      QMarkupTextParams textParams,
      float maximumWidth,
      bool removeStartingSpace,
      out SizeF textSize,
      out int[] stringWidths)
    {
      textSize = (SizeF) Size.Empty;
      stringWidths = new int[0];
      if (startIndex >= text.Length)
        return (string) null;
      int maximumCharactersThatFit = 0;
      while (removeStartingSpace && startIndex < text.Length && char.IsWhiteSpace(text, startIndex))
        ++startIndex;
      int length = text.Length - startIndex;
      int maximumWidth1 = this.WrapText ? (int) Math.Round((double) maximumWidth) : int.MaxValue;
      if (length == 0)
        return (string) null;
      string str = text.Substring(startIndex, length);
      Size textExtent = NativeHelper.CalculateTextExtent(str, this.CurrentFont, maximumWidth1, textParams.Graphics, out maximumCharactersThatFit, out stringWidths);
      if (maximumCharactersThatFit < length)
      {
        int num = maximumCharactersThatFit;
        if (maximumCharactersThatFit >= length || !char.IsWhiteSpace(str, maximumCharactersThatFit))
        {
          while (maximumCharactersThatFit > 0 && !char.IsWhiteSpace(str, maximumCharactersThatFit - 1))
            --maximumCharactersThatFit;
        }
        if (maximumCharactersThatFit == 0 && textParams.CurrentLine.Parts.Count == 0)
          maximumCharactersThatFit = num;
      }
      if (maximumCharactersThatFit == 0 && textParams.CurrentLine.Parts.Count == 0 && str.Length > 0)
      {
        string nextTextPart = str.Substring(0, 1);
        ++startIndex;
        textSize = (SizeF) textExtent;
        return nextTextPart;
      }
      if (maximumCharactersThatFit > 0)
      {
        string nextTextPart = str.Substring(0, maximumCharactersThatFit);
        startIndex += maximumCharactersThatFit;
        textSize = (SizeF) new Size(stringWidths[maximumCharactersThatFit - 1], textExtent.Height);
        return nextTextPart;
      }
      textSize = (SizeF) Size.Empty;
      return string.Empty;
    }

    protected void AddPart(QMarkupTextRenderedPart part, QMarkupTextParams textParams)
    {
      textParams.CurrentLine.Parts.Add(part);
      this.Parts.Add(part);
    }

    protected void RenderString(string text, QMarkupTextParams textParams)
    {
      int startIndex = 0;
      bool flag = false;
      while (!flag)
      {
        string empty = string.Empty;
        int[] stringWidths = (int[]) null;
        SizeF textSize;
        string nextTextPart = this.GetNextTextPart(text, ref startIndex, textParams, (float) textParams.MaximumSize.Width - textParams.CurrentLine.Width, textParams.CurrentLine.Parts.Count == 0, out textSize, out stringWidths);
        if (nextTextPart != null && nextTextPart.Length > 0)
        {
          if (textParams.CurrentLine.Parts.Count > 0 && (double) textParams.MaximumSize.Width < (double) textParams.CurrentLine.Width + (double) textSize.Width)
            textParams.AddLine();
          this.AddPart((QMarkupTextRenderedPart) new QMarkupTextRenderedText((float) NativeHelper.CalculateBaseLine(textParams.Graphics, this.CurrentFont), textSize, nextTextPart, stringWidths), textParams);
          if (startIndex < text.Length)
            textParams.AddLine();
        }
        else if (nextTextPart != null && nextTextPart.Length == 0 && startIndex < text.Length)
          textParams.AddLine();
        else
          flag = true;
      }
    }

    protected void RenderEmptyPart(QMarkupTextParams textParams)
    {
      SizeF size = new SizeF(0.0f, (float) NativeHelper.CalculateFontHeight(textParams.Graphics, this.CurrentFont));
      this.AddPart(new QMarkupTextRenderedPart((float) NativeHelper.CalculateBaseLine(textParams.Graphics, this.CurrentFont), size), textParams);
    }

    protected QFontDefinition RetrieveCurrentFontDefinition() => this.Active ? (!(this.OwningStyle.FontDefinitionActive != QFontDefinition.Empty) ? this.OwningStyle.FontDefinition : this.OwningStyle.FontDefinitionActive) : (this.Hot && this.OwningStyle.FontDefinitionHot != QFontDefinition.Empty ? this.OwningStyle.FontDefinitionHot : this.OwningStyle.FontDefinition);

    protected Color RetrieveCurrentTextColor()
    {
      if (!this.Enabled)
        return this.OwningStyle.GetUsedTextColorDisabled(this.OwningMarkupText.ColorScheme, this.OwningStyle.GetUsedTextColor(this.OwningMarkupText.ColorScheme, Color.Empty));
      if (this.Active)
        return this.OwningStyle.GetUsedTextColorActive(this.OwningMarkupText.ColorScheme, this.OwningStyle.GetUsedTextColor(this.OwningMarkupText.ColorScheme, Color.Empty));
      return this.Hot ? this.OwningStyle.GetUsedTextColorHot(this.OwningMarkupText.ColorScheme, this.OwningStyle.GetUsedTextColor(this.OwningMarkupText.ColorScheme, Color.Empty)) : this.OwningStyle.GetUsedTextColor(this.OwningMarkupText.ColorScheme, Color.Empty);
    }

    public void ProcessMarkup(XmlNode node) => this.ProcessMarkup(node, true);

    internal void ProcessMarkup(XmlNode node, bool firstCall)
    {
      if (firstCall)
        this.m_oMarkupNode = node;
      foreach (XmlNode childNode in node.ChildNodes)
      {
        QMarkupTextStyle styleForNode = this.OwningMarkupText.GetStyleForNode(childNode);
        if (styleForNode != null)
        {
          QMarkupTextElement element = styleForNode.CreateElement(childNode);
          this.Elements.Add(element);
          element.ProcessMarkup(childNode);
        }
        else
          this.ProcessMarkup(childNode, false);
      }
    }

    public void ApplyAttributes()
    {
      this.ApplyElementAttributes();
      this.ApplyChildAttributes();
    }

    protected virtual void ApplyElementAttributes()
    {
      QFontDefinition qfontDefinition = this.RetrieveCurrentFontDefinition();
      Font parentFont = this.ParentFont;
      if (this.ParentFont != null)
        this.PutCurrentFont(qfontDefinition.GetFontFromCache(this.ParentFont));
      Color color = this.RetrieveCurrentTextColor();
      if (!(color != Color.Empty))
        return;
      this.PutCurrentColor(color);
    }

    protected virtual void ApplyChildAttributes()
    {
      if (this.m_oElements == null)
        return;
      for (int index = 0; index < this.m_oElements.Count; ++index)
        this.m_oElements[index].ApplyAttributes();
    }

    public virtual QMarkupTextElement GetSourceForEvents() => this.CanBeSourceForEvents || this.ParentElement == null ? this : this.ParentElement.GetSourceForEvents();

    public virtual void RenderElement(QMarkupTextParams textParams)
    {
      if (this.OwningStyle.NewLineBefore && textParams.CurrentLine.Parts.Count > 0)
        textParams.AddLine();
      if (this.OwningStyle.AddEmptyPart)
        this.RenderEmptyPart(textParams);
      if (this.m_oMarkupNode is XmlText || this.m_oMarkupNode is XmlWhitespace)
        this.RenderString(this.m_oMarkupNode.InnerText, textParams);
      else
        this.RenderChildElements(textParams);
      if (!this.OwningStyle.NewLineAfter)
        return;
      textParams.AddLine();
    }

    public virtual void RenderChildElements(QMarkupTextParams textParams)
    {
      if (this.m_oElements == null)
        return;
      for (int index = 0; index < this.m_oElements.Count; ++index)
        this.m_oElements[index].RenderElement(textParams);
    }

    public virtual void Draw(Graphics graphics)
    {
      if (this.HasParts)
      {
        for (int index = 0; index < this.m_oParts.Count; ++index)
          this.m_oParts[index].Draw(graphics);
      }
      if (!this.HasElements)
        return;
      for (int index = 0; index < this.m_oElements.Count; ++index)
        this.m_oElements[index].Draw(graphics);
    }

    internal QMarkupTextElement NextSibling
    {
      get
      {
        if (this.ParentElement == null)
          return (QMarkupTextElement) null;
        int index = this.ParentElement.Elements.IndexOf(this) + 1;
        return this.ParentElement.Elements.Count > index ? this.ParentElement.Elements[index] : (QMarkupTextElement) null;
      }
    }

    internal QMarkupTextElement PreviousSibling
    {
      get
      {
        if (this.ParentElement == null)
          return (QMarkupTextElement) null;
        int index = this.ParentElement.Elements.IndexOf(this) - 1;
        return index >= 0 ? this.ParentElement.Elements[index] : (QMarkupTextElement) null;
      }
    }

    internal void HandleGotFocus(QMarkupTextElement sourceElement) => this.HandleGotFocus(sourceElement, false);

    internal void HandleGotFocus(QMarkupTextElement sourceElement, bool bubbling)
    {
      if (!bubbling)
        this.PutFocused(true);
      sourceElement = sourceElement == null ? this : sourceElement;
      this.OnGotFocus(new QMarkupTextElementEventArgs(sourceElement, (MouseEventArgs) null));
      if (this.ParentElement == null)
        return;
      this.ParentElement.HandleGotFocus(sourceElement, true);
    }

    internal void HandleLostFocus(QMarkupTextElement sourceElement) => this.HandleLostFocus(sourceElement, false);

    internal void HandleLostFocus(QMarkupTextElement sourceElement, bool bubbling)
    {
      if (!bubbling)
        this.PutFocused(false);
      sourceElement = sourceElement == null ? this : sourceElement;
      this.OnLostFocus(new QMarkupTextElementEventArgs(sourceElement, (MouseEventArgs) null));
      if (this.ParentElement == null)
        return;
      this.ParentElement.HandleLostFocus(sourceElement, false);
    }

    internal void HandleMouseEnter(MouseEventArgs e, QMarkupTextElement sourceElement)
    {
      this.PutHot(true);
      sourceElement = sourceElement != null ? sourceElement : this;
      this.OnMouseEnter(new QMarkupTextElementEventArgs(sourceElement, e));
      if (this.ParentElement == null)
        return;
      this.ParentElement.HandleMouseEnter(e, sourceElement);
    }

    internal void HandleMouseLeave(MouseEventArgs e, QMarkupTextElement sourceElement)
    {
      this.PutHot(false);
      sourceElement = sourceElement != null ? sourceElement : this;
      this.OnMouseLeave(new QMarkupTextElementEventArgs(sourceElement, e));
      if (this.ParentElement == null)
        return;
      this.ParentElement.HandleMouseLeave(e, sourceElement);
    }

    internal void HandleMouseDown(MouseEventArgs e, QMarkupTextElement sourceElement)
    {
      this.PutActive(true);
      sourceElement = sourceElement != null ? sourceElement : this;
      this.OnMouseDown(new QMarkupTextElementEventArgs(sourceElement, e));
      if (this.ParentElement == null)
        return;
      this.ParentElement.HandleMouseDown(e, sourceElement);
    }

    internal void HandleMouseUp(MouseEventArgs e, QMarkupTextElement sourceElement)
    {
      this.PutActive(false);
      sourceElement = sourceElement != null ? sourceElement : this;
      this.OnMouseUp(new QMarkupTextElementEventArgs(sourceElement, e));
      if (this.ParentElement == null)
        return;
      this.ParentElement.HandleMouseUp(e, sourceElement);
    }

    internal void HandleMouseClick(QMarkupTextElement sourceElement)
    {
      sourceElement = sourceElement != null ? sourceElement : this;
      this.OnMouseClick(new QMarkupTextElementEventArgs(sourceElement, (MouseEventArgs) null));
      if (this.ParentElement != null)
        this.ParentElement.HandleMouseClick(sourceElement);
      if (!(this is QMarkupTextElementAnchor))
        return;
      this.HandleLinkClick(this);
    }

    internal void HandleLinkClick(QMarkupTextElement sourceElement)
    {
      sourceElement = sourceElement != null ? sourceElement : this;
      this.OnLinkClick(new QMarkupTextElementEventArgs(sourceElement, (MouseEventArgs) null));
      if (this.ParentElement == null)
        return;
      this.ParentElement.HandleLinkClick(sourceElement);
    }

    protected virtual void OnMouseEnter(QMarkupTextElementEventArgs e) => this.m_oMouseEnterDelegate = QWeakDelegate.InvokeDelegate(this.m_oMouseEnterDelegate, (object) this, (object) e);

    protected virtual void OnMouseLeave(QMarkupTextElementEventArgs e) => this.m_oMouseLeaveDelegate = QWeakDelegate.InvokeDelegate(this.m_oMouseLeaveDelegate, (object) this, (object) e);

    protected virtual void OnMouseDown(QMarkupTextElementEventArgs e) => this.m_oMouseDownDelegate = QWeakDelegate.InvokeDelegate(this.m_oMouseDownDelegate, (object) this, (object) e);

    protected virtual void OnMouseUp(QMarkupTextElementEventArgs e) => this.m_oMouseUpDelegate = QWeakDelegate.InvokeDelegate(this.m_oMouseUpDelegate, (object) this, (object) e);

    protected virtual void OnMouseClick(QMarkupTextElementEventArgs e) => this.m_oMouseClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oMouseClickDelegate, (object) this, (object) e);

    protected virtual void OnLinkClick(QMarkupTextElementEventArgs e) => this.m_oLinkClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oLinkClickDelegate, (object) this, (object) e);

    protected virtual void OnGotFocus(QMarkupTextElementEventArgs e) => this.m_oGotFocusDelegate = QWeakDelegate.InvokeDelegate(this.m_oGotFocusDelegate, (object) this, (object) e);

    protected virtual void OnLostFocus(QMarkupTextElementEventArgs e) => this.m_oLostFocusDelegate = QWeakDelegate.InvokeDelegate(this.m_oLostFocusDelegate, (object) this, (object) e);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public virtual void DisposeAttributes(bool deep)
    {
      if (deep && this.m_oElements != null)
        this.m_oElements.DisposeAttributes(deep);
      if (this.m_oCurrentFont == null)
        return;
      this.m_oCurrentFont = (Font) null;
    }

    public virtual void DisposeRenderedObjects(bool deep)
    {
      if (deep && this.m_oElements != null)
        this.m_oElements.DisposeRenderedObjects(deep);
      if (this.m_oParts == null)
        return;
      this.m_oParts.Dispose();
      this.m_oParts = (QMarkupTextRenderedPartCollection) null;
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      this.DisposeAttributes(true);
      this.DisposeRenderedObjects(true);
    }

    ~QMarkupTextElement() => this.Dispose(false);
  }
}
