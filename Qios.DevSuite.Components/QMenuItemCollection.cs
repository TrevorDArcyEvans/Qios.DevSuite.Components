// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMenuItemCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  public sealed class QMenuItemCollection : QCommandCollection
  {
    public QMenuItemCollection()
    {
    }

    public QMenuItemCollection(IQCommandContainer parentContainer, QCommand parentCommand)
      : base(parentContainer, parentCommand)
    {
    }

    public void Add(QMenuItem menuItem) => this.AddCommand((QCommand) menuItem);

    public void Remove(QMenuItem menuItem) => this.RemoveCommand((QCommand) menuItem);

    public new void RemoveAt(int index) => this.RemoveCommandAt(index);

    public void Insert(int index, QMenuItem menuItem) => this.InsertCommand(index, (QCommand) menuItem);

    public void AddRange(QMenuItem[] menuItems)
    {
      for (int index = 0; index < menuItems.Length; ++index)
        this.Add(menuItems[index]);
    }

    public void AddRange(QMenuItemCollection collection)
    {
      if (collection == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (collection)));
      for (int index = 0; index < collection.Count; ++index)
        this.Add(collection[index]);
    }

    public bool Contains(QMenuItem menuItem) => this.ContainsCommand((QCommand) menuItem);

    public QMenuItem this[int index]
    {
      get => (QMenuItem) this.GetCommand(index);
      set => this.SetCommandAtIndex(index, (QCommand) value);
    }

    public QMenuItem this[string name] => (QMenuItem) this.GetCommand(name) ?? (QMenuItem) null;

    public QMenuItem GetMenuItemAtPosition(Point position) => (QMenuItem) this.GetCommandAtPosition(position) ?? (QMenuItem) null;

    internal QMenuItem GetMenuItemWithOuterBoundsOn(Point position)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].OuterBounds.Contains(position))
          return this[index];
      }
      return (QMenuItem) null;
    }

    internal QMenuItem GetNextVisibleMenuItem(QMenuItem sourceItem, bool directionDown)
    {
      int num1 = this.IndexOf(sourceItem);
      if (num1 < 0)
        return (QMenuItem) null;
      int num2 = directionDown ? 1 : -1;
      for (int index = num1 + num2; index >= 0 && index < this.Count; index += num2)
      {
        if (this[index].IsVisible)
          return this[index];
      }
      return (QMenuItem) null;
    }

    internal QMenuItem GetFirstVisibleItem() => this.Count == 0 ? (QMenuItem) null : this.GetNextVisibleMenuItem(this[0], true);

    internal QMenuItem GetLastVisibleItem() => this.Count == 0 ? (QMenuItem) null : this.GetNextVisibleMenuItem(this[this.Count - 1], false);

    public int IndexOf(QMenuItem menuItem) => this.IndexOfCommand((QCommand) menuItem);

    public int IndexOf(string name) => this.IndexOfCommand(name);

    internal int GetMenuItemsWithHotkeyCount(
      Keys hotkey,
      bool mustBeAccessible,
      int recursiveLevels)
    {
      int itemsWithHotkeyCount = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Hotkey == hotkey && (!mustBeAccessible || this[index].IsAccessible))
          ++itemsWithHotkeyCount;
        if ((!mustBeAccessible || this[index].IsAccessible) && recursiveLevels > 0 && this[index].IsExpanded)
          itemsWithHotkeyCount += this[index].MenuItems.GetMenuItemsWithHotkeyCount(hotkey, mustBeAccessible, recursiveLevels - 1);
      }
      return itemsWithHotkeyCount;
    }

    public int GetMenuItemsWithHotkeyCount(Keys hotkey, bool mustBeAccessible) => this.GetMenuItemsWithHotkeyCount(hotkey, mustBeAccessible, 0);

    public QMenuItem GetMenuItemWithHotkey(Keys hotkey) => this.GetNextMenuItemWithHotkey((QMenuItem) null, hotkey, false, true);

    public QMenuItem GetNextMenuItemWithHotkey(
      QMenuItem afterItem,
      Keys hotkey,
      bool mustBeAccessible,
      bool loopAround)
    {
      return this.GetNextMenuItemWithHotkey(afterItem, hotkey, mustBeAccessible, loopAround, 0);
    }

    internal QMenuItem GetNextMenuItemWithHotkey(
      QMenuItem afterItem,
      Keys hotkey,
      bool mustBeAccessible,
      bool loopAround,
      int recursiveLevels)
    {
      int num = 0;
      bool flag = false;
      if (afterItem != null && this.Contains(afterItem))
      {
        num = this.IndexOf(afterItem);
        flag = true;
      }
      else if (afterItem != null && recursiveLevels > 0)
      {
        num = this.IndexOf(afterItem, recursiveLevels);
        flag = true;
      }
      for (int index = num; index < this.Count; ++index)
      {
        if (!flag && this[index].Hotkey == hotkey && (!mustBeAccessible || this[index].IsAccessible))
          return this[index];
        if (recursiveLevels > 0 && this[index].IsExpanded)
        {
          QMenuItem menuItemWithHotkey = this[index].MenuItems.GetNextMenuItemWithHotkey(afterItem, hotkey, mustBeAccessible, false, recursiveLevels - 1);
          if (menuItemWithHotkey != null)
            return menuItemWithHotkey;
        }
        flag = false;
      }
      return loopAround ? this.GetNextMenuItemWithHotkey((QMenuItem) null, hotkey, mustBeAccessible, false, recursiveLevels) : (QMenuItem) null;
    }

    internal int IndexOf(QMenuItem menuItem, int recursiveLevels)
    {
      if (this.Contains(menuItem))
        return this.IndexOf(menuItem);
      if (recursiveLevels > 0)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].MenuItems.Contains(menuItem, recursiveLevels - 1))
            return index;
        }
      }
      return -1;
    }

    internal bool Contains(QMenuItem menuItem, int recursiveLevels)
    {
      if (this.Contains(menuItem))
        return true;
      if (recursiveLevels > 0)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].MenuItems.Contains(menuItem, recursiveLevels - 1))
            return true;
        }
      }
      return false;
    }

    public QMenuItem GetMenuItemWithHotkey(Keys hotkey, int recursiveLevels)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Hotkey == hotkey)
          return this[index];
        if (this[index].HasChildItems && recursiveLevels > 0)
        {
          QMenuItem menuItemWithHotkey = this[index].MenuItems.GetMenuItemWithHotkey(hotkey, recursiveLevels - 1);
          if (menuItemWithHotkey != null)
            return menuItemWithHotkey;
        }
      }
      return (QMenuItem) null;
    }

    public QMenuItem GetMenuItemWithShortcut(Keys shortcut)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        QMenuItem itemWithShortcut = this[index].GetMenuItemWithShortcut(shortcut);
        if (itemWithShortcut != null)
          return itemWithShortcut;
      }
      return (QMenuItem) null;
    }

    public QMenuItem FindMenuItemWithRelativeName(string relativeName) => (QMenuItem) this.FindCommandWithRelativeName(relativeName) ?? (QMenuItem) null;

    public bool HasPersonalizedItems(bool includeChildMenus)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (!this[index].VisibleWhenPersonalized || includeChildMenus && this[index].HasChildItems && this[index].MenuItems.HasPersonalizedItems(true))
          return true;
      }
      return false;
    }

    public void SetVisibleWhenPersonalizedOfAllMenuItems(bool value, bool includeChildItems) => this.SetPropertyOfAllMenuItems(typeof (QMenuItem).GetProperty("VisibleWhenPersonalized"), (object) value, includeChildItems);

    private static QMenuItem CreateMenuItem(string reference)
    {
      System.Type type = System.Type.GetType(reference, false, true);
      if (type == null)
        throw new QCommandException(QResources.GetException("QMenuItemCreation_CannotFindObjectWithType", (object) reference));
      object instance;
      try
      {
        instance = Activator.CreateInstance(type, new object[0]);
      }
      catch (Exception ex)
      {
        throw new QCommandException(QResources.GetException("QMenuItemCreation_ConstructorThrewException", (object) reference), ex);
      }
      return instance is QMenuItem qmenuItem ? qmenuItem : throw new QCommandException(QResources.GetException("QMenuItemCreation_ObjectNotAMenuItem", (object) reference));
    }

    private void SetPropertyOfAllMenuItems(
      PropertyInfo property,
      object value,
      bool includeChildItems)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        property.SetValue((object) this[index], value, (object[]) null);
        if (includeChildItems && this[index].HasChildItems)
          this[index].MenuItems.SetPropertyOfAllMenuItems(property, value, true);
      }
    }

    public void LoadFromXml(string fileName, QMenuItemLoadType loadOptions)
    {
      XmlDocument xmlDocument;
      try
      {
        xmlDocument = new XmlDocument();
        xmlDocument.Load(fileName);
      }
      catch (Exception ex)
      {
        throw new XmlException(QResources.GetException("QMenuItemCollectionLoad_FileCannotBeLoaded", (object) fileName), ex);
      }
      if (!(xmlDocument.SelectSingleNode("menuItemCollection") is XmlElement collectionElement))
        throw new XmlException(QResources.GetException("QMenuItemCollectionLoad_MenuItemCollectionNotFound"));
      this.LoadFromXml((IXPathNavigable) collectionElement, loadOptions);
    }

    public void LoadFromXml(IXPathNavigable collectionElement, QMenuItemLoadType loadOptions)
    {
      if (collectionElement == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (collectionElement)));
      bool flag = loadOptions == QMenuItemLoadType.CreateNewItems;
      object attributeEnum = (object) QXmlHelper.GetAttributeEnum(collectionElement, "saveOptions", typeof (QMenuItemSaveType));
      QMenuItemSaveType qmenuItemSaveType = attributeEnum != null ? (QMenuItemSaveType) attributeEnum : QMenuItemSaveType.CompleteMenuItem;
      if (flag)
      {
        this.Clear();
      }
      else
      {
        switch (qmenuItemSaveType)
        {
          case QMenuItemSaveType.CompleteMenuItem:
            for (int index = 0; index < this.Count; ++index)
              QMisc.SetToDefaultValues((object) this[index]);
            break;
          case QMenuItemSaveType.PersonalizedStateOnly:
            this.SetVisibleWhenPersonalizedOfAllMenuItems(true, true);
            break;
        }
      }
      if (!flag)
      {
        for (int index = 0; index < this.Count; ++index)
          this[index].VisibleWhenPersonalized = true;
      }
      XPathNodeIterator xpathNodeIterator = collectionElement.CreateNavigator().SelectChildren("item", "");
      while (xpathNodeIterator.MoveNext())
      {
        IXPathNavigable navigableFromNavigator = QXmlHelper.GetNavigableFromNavigator(xpathNodeIterator.Current);
        if (navigableFromNavigator != null)
        {
          string attributeString = QXmlHelper.GetAttributeString(navigableFromNavigator, "name");
          QMenuItem menuItem;
          if (flag)
          {
            menuItem = QMenuItemCollection.CreateMenuItem(QXmlHelper.GetAttributeString(navigableFromNavigator, "type"));
            menuItem.ItemName = attributeString;
            this.Add(menuItem);
          }
          else
            menuItem = this[attributeString];
          menuItem?.LoadFromXml(navigableFromNavigator, loadOptions);
        }
      }
    }

    public IXPathNavigable SaveToXml(string fileName, QMenuItemSaveType saveType)
    {
      XmlDocument parentNode = new XmlDocument();
      this.SaveToXml((IXPathNavigable) parentNode, saveType);
      try
      {
        parentNode.Save(fileName);
      }
      catch (Exception ex)
      {
        throw new XmlException(QResources.GetException("QMenuItemCollectionLoad_FileCannotBeSaved", (object) fileName), ex);
      }
      return (IXPathNavigable) parentNode.DocumentElement;
    }

    public IXPathNavigable SaveToXml(
      IXPathNavigable parentNode,
      QMenuItemSaveType saveOptions)
    {
      IXPathNavigable xml = QXmlHelper.AddElement(parentNode, "menuItemCollection");
      QXmlHelper.AddAttribute(xml, nameof (saveOptions), (object) saveOptions);
      for (int index = 0; index < this.Count; ++index)
        this[index].SaveToXml(xml, saveOptions);
      return xml;
    }
  }
}
