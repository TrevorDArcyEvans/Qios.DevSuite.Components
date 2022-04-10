// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QCompositeHelper
  {
    internal static QNavigationActivationType GetAsNavigationActivationType(
      QCompositeActivationType activationType)
    {
      switch (activationType)
      {
        case QCompositeActivationType.Shortcut:
          return QNavigationActivationType.Shortcut;
        case QCompositeActivationType.Hotkey:
          return QNavigationActivationType.Hotkey;
        case QCompositeActivationType.Keyboard:
          return QNavigationActivationType.Keyboard;
        default:
          return QNavigationActivationType.None;
      }
    }

    internal static QCompositeActivationType GetAsCompositeActivationType(
      QNavigationActivationType activationType)
    {
      switch (activationType)
      {
        case QNavigationActivationType.Keyboard:
          return QCompositeActivationType.Keyboard;
        case QNavigationActivationType.Hotkey:
          return QCompositeActivationType.Hotkey;
        case QNavigationActivationType.Shortcut:
          return QCompositeActivationType.Shortcut;
        default:
          return QCompositeActivationType.None;
      }
    }

    internal static bool IsKeyboardActivationType(QCompositeActivationType type) => type == QCompositeActivationType.Keyboard || type == QCompositeActivationType.Hotkey;

    internal static bool HandleShortcutKey(
      IQPartCollection collection,
      Keys key,
      out bool suppressToSystem)
    {
      suppressToSystem = false;
      QCompositeNavigationFilter filter = new QCompositeNavigationFilter(QCompositeNavigationFilterOptions.VisibleForShortcut | QCompositeNavigationFilterOptions.Enabled | QCompositeNavigationFilterOptions.MatchShortcut, key);
      for (int index = 0; index < collection.Count; ++index)
      {
        QCompositeItemBase firstItemRecursive = QCompositeHelper.GetFirstItemRecursive(collection[index], true, filter);
        if (firstItemRecursive != null)
        {
          firstItemRecursive.Activate(QCompositeItemActivationOptions.Activate, QCompositeActivationType.Shortcut);
          QCompositeItem qcompositeItem = firstItemRecursive as QCompositeItem;
          suppressToSystem = qcompositeItem == null || qcompositeItem.SuppressShortcutToSystem;
          return true;
        }
      }
      return false;
    }

    public static bool AssignMouseLeaveToPossibleCurrentControl(
      Control control,
      EventHandler handlerToAssign)
    {
      Point mousePosition = Control.MousePosition;
      IntPtr windowHandle;
      if (QControlHelper.ControlContainsOrIsWindowOnPoint(control, mousePosition, out windowHandle))
      {
        Control controlFromHandle = QControlHelper.GetFirstControlFromHandle(windowHandle);
        if (controlFromHandle != null && controlFromHandle != control)
        {
          controlFromHandle.MouseLeave += handlerToAssign;
          return true;
        }
      }
      return false;
    }

    internal static QCompositeItem GetItemOrAncestorAsCompositeItem(
      object item,
      IQPart tillPart)
    {
      for (IQPart qpart = item as IQPart; qpart != null && qpart != tillPart; qpart = qpart.ParentPart)
      {
        if (qpart is QCompositeItem ancestorAsCompositeItem)
          return ancestorAsCompositeItem;
      }
      return (QCompositeItem) null;
    }

    internal static bool IsAccessibleRecursive(
      IQPart part,
      QPartVisibilitySelectionTypes visibilitySelection)
    {
      if (part == null)
        return true;
      return (!(part is QCompositeItemBase qcompositeItemBase) || qcompositeItemBase.IsAccessible(visibilitySelection)) && QCompositeHelper.IsAccessibleRecursive(part.ParentPart, visibilitySelection);
    }

    internal static QCompositeItemBase GetFirstItemRecursive(
      IQPart rootItem,
      bool checkChildItems,
      QCompositeNavigationFilter filter)
    {
      if (rootItem == null)
        return (QCompositeItemBase) null;
      if (rootItem.Parts != null)
      {
        if (rootItem is QCompositeItemBase firstItemRecursive1 && firstItemRecursive1.MatchesNavigationFilter(filter))
          return firstItemRecursive1;
        if (rootItem.IsVisible(filter.PartVisibilitySelection))
        {
          for (int index = 0; index < rootItem.Parts.Count; ++index)
          {
            QCompositeItemBase firstItemRecursive2 = QCompositeHelper.GetFirstItemRecursive(rootItem.Parts[index], checkChildItems, filter);
            if (firstItemRecursive2 != null)
              return firstItemRecursive2;
          }
        }
      }
      if (checkChildItems && rootItem is QCompositeItem qcompositeItem && qcompositeItem.HasChildItems)
      {
        for (int index = 0; index < qcompositeItem.ChildItems.Count; ++index)
        {
          QCompositeItemBase firstItemRecursive = QCompositeHelper.GetFirstItemRecursive(qcompositeItem.ChildItems[index], checkChildItems, filter);
          if (firstItemRecursive != null)
            return firstItemRecursive;
        }
      }
      return (QCompositeItemBase) null;
    }

    internal static QCompositeItemBase GetNextItemOrderedRecursive(
      IQPart rootItem,
      IQPart fromItem,
      QCompositeNavigationFilter filter,
      bool forward,
      bool loop)
    {
      if (fromItem == null)
      {
        fromItem = rootItem;
        loop = false;
      }
      QCompositeItemBase orderedRecursive1 = QCompositeHelper.GetFirstChildOrderedRecursive(fromItem, filter, forward);
      if (orderedRecursive1 != null)
        return orderedRecursive1;
      IQPart qpart1 = fromItem;
      IQPart qpart2 = fromItem;
      while (qpart1 != null)
      {
        IQPart siblingOrdered = QCompositeHelper.GetSiblingOrdered(qpart2, forward);
        QCompositeItemBase orderedRecursive2 = siblingOrdered as QCompositeItemBase;
        while (siblingOrdered != null)
        {
          if (siblingOrdered.IsVisible(filter.PartVisibilitySelection))
          {
            if (orderedRecursive2 != null && orderedRecursive2.MatchesNavigationFilter(filter))
              return orderedRecursive2;
            QCompositeItemBase orderedRecursive3 = QCompositeHelper.GetFirstChildOrderedRecursive(siblingOrdered, filter, forward);
            if (orderedRecursive3 != null)
              return orderedRecursive3;
          }
          siblingOrdered = QCompositeHelper.GetSiblingOrdered(siblingOrdered, forward);
          orderedRecursive2 = siblingOrdered as QCompositeItemBase;
        }
        if (qpart1 != rootItem)
        {
          qpart1 = qpart1.ParentPart;
          qpart2 = qpart1;
        }
        else
          break;
      }
      return loop ? QCompositeHelper.GetNextItemOrderedRecursive(rootItem, (IQPart) null, filter, forward, false) : (QCompositeItemBase) null;
    }

    private static QCompositeItemBase GetFirstChildOrderedRecursive(
      IQPart rootItem,
      QCompositeNavigationFilter filter,
      bool first)
    {
      if (rootItem == null)
        return (QCompositeItemBase) null;
      if (rootItem.Parts == null)
        return (QCompositeItemBase) null;
      for (IQPart rootItem1 = QCompositeHelper.GetFirstChildOrdered(rootItem, first); rootItem1 != null; rootItem1 = QCompositeHelper.GetSiblingOrdered(rootItem1, first))
      {
        if (rootItem1.IsVisible(filter.PartVisibilitySelection))
        {
          if (rootItem1 is QCompositeItemBase orderedRecursive1 && orderedRecursive1.MatchesNavigationFilter(filter))
            return orderedRecursive1;
          QCompositeItemBase orderedRecursive2 = QCompositeHelper.GetFirstChildOrderedRecursive(rootItem1, filter, first);
          if (orderedRecursive2 != null)
            return orderedRecursive2;
        }
      }
      return (QCompositeItemBase) null;
    }

    internal static void AddItemsRecursive(
      IQPart rootItem,
      IList list,
      QCompositeNavigationFilter filter)
    {
      QCompositeItemBase qcompositeItemBase = rootItem as QCompositeItemBase;
      if (!rootItem.IsVisible(filter.PartVisibilitySelection))
        return;
      if (qcompositeItemBase != null && qcompositeItemBase.MatchesNavigationFilter(filter))
        list.Add((object) qcompositeItemBase);
      if (rootItem.Parts == null)
        return;
      for (int index = 0; index < rootItem.Parts.Count; ++index)
        QCompositeHelper.AddItemsRecursive(rootItem.Parts[index], list, filter);
    }

    private static IQPart GetFirstChildOrdered(IQPart item, bool first)
    {
      if (item == null)
        return (IQPart) null;
      if (item.Parts == null)
        return (IQPart) null;
      QPartDirection direction = item.Properties.GetDirection(item);
      int index1 = -1;
      int num1 = -1;
      for (int index2 = first ? 0 : item.Parts.Count - 1; index2 != (first ? item.Parts.Count : -1); index2 += first ? 1 : -1)
      {
        IQPart part = item.Parts[index2];
        int num2 = direction == QPartDirection.Horizontal ? (int) part.Properties.GetAlignmentHorizontal(part) : (int) part.Properties.GetAlignmentVertical(part);
        if (num1 < 0)
          num1 = num2;
        if (index1 < 0)
          index1 = index2;
        if (first)
        {
          if (num2 < num1)
          {
            num1 = num2;
            index1 = index2;
          }
        }
        else if (num2 > num1)
        {
          num1 = num2;
          index1 = index2;
        }
      }
      return index1 >= 0 ? item.Parts[index1] : (IQPart) null;
    }

    private static IQPart GetSiblingOrdered(IQPart item, bool forward)
    {
      if (item == null)
        return (IQPart) null;
      IQPart parentPart = item.ParentPart;
      if (parentPart == null || parentPart.Parts == null || !parentPart.Parts.Contains(item))
        return (IQPart) null;
      QPartDirection direction = parentPart.Properties.GetDirection(parentPart);
      for (int alignment = direction == QPartDirection.Horizontal ? (int) item.Properties.GetAlignmentHorizontal(item) : (int) item.Properties.GetAlignmentVertical(item); alignment >= 0 && alignment <= 2; alignment += forward ? 1 : -1)
      {
        IQPart siblingOrdered = QCompositeHelper.GetSiblingOrdered(item, forward, direction, (QPartAlignment) alignment);
        if (siblingOrdered != null)
          return siblingOrdered;
      }
      return (IQPart) null;
    }

    private static IQPart GetSiblingOrdered(
      IQPart item,
      bool forward,
      QPartDirection parentDirection,
      QPartAlignment alignment)
    {
      if (item == null)
        return (IQPart) null;
      if (item.ParentPart == null)
        return (IQPart) null;
      IQPart parentPart = item.ParentPart;
      if (parentPart != null && parentPart.Parts != null && parentPart.Parts.Contains(item))
      {
        int num = parentPart.Parts.IndexOf(item);
        QPartAlignment qpartAlignment = parentDirection == QPartDirection.Horizontal ? item.Properties.GetAlignmentHorizontal(item) : item.Properties.GetAlignmentVertical(item);
        if (num >= 0)
        {
          int index = num + (forward ? 1 : -1);
          if (qpartAlignment != alignment)
            index = forward ? 0 : parentPart.Parts.Count - 1;
          for (; index >= 0 && index < parentPart.Parts.Count; index += forward ? 1 : -1)
          {
            if ((parentDirection == QPartDirection.Horizontal ? parentPart.Parts[index].Properties.GetAlignmentHorizontal(item) : parentPart.Parts[index].Properties.GetAlignmentVertical(item)) == alignment)
              return parentPart.Parts[index];
          }
        }
      }
      return (IQPart) null;
    }

    internal static QCompositeItemBase GetMouseHandlingItemAtPointRecursive(
      IQPart rootPart,
      Point point)
    {
      QCompositeItemBase atPointRecursive = (QCompositeItemBase) null;
      QPartHitTestResult qpartHitTestResult;
      if ((qpartHitTestResult = rootPart.HitTest(point.X, point.Y)) != QPartHitTestResult.Nowhere)
      {
        atPointRecursive = QCompositeHelper.GetMouseHandlingItemAtPointRecursive((IQPartCollection) rootPart.Parts, point);
        if (atPointRecursive == null)
        {
          QCompositeItemBase qcompositeItemBase = rootPart as QCompositeItemBase;
          if (qpartHitTestResult == QPartHitTestResult.ScrollArea || qpartHitTestResult == QPartHitTestResult.BoundsCustom || qcompositeItemBase != null && qcompositeItemBase.HasPressedState == QTristateBool.True && qcompositeItemBase.HasHotState == QTristateBool.True)
            atPointRecursive = qcompositeItemBase;
        }
      }
      return atPointRecursive;
    }

    internal static QCompositeItemBase GetMouseHandlingItemAtPointRecursive(
      IQPartCollection parts,
      Point point)
    {
      if (parts == null)
        return (QCompositeItemBase) null;
      for (int index = 0; index < parts.Count; ++index)
      {
        QCompositeItemBase atPointRecursive = QCompositeHelper.GetMouseHandlingItemAtPointRecursive(parts[index], point);
        if (atPointRecursive != null)
          return atPointRecursive;
      }
      return (QCompositeItemBase) null;
    }

    internal static IQCompositeItemEventRaiser FindParentItemEventRaiser(
      IQPart part)
    {
      if (part == null)
        return (IQCompositeItemEventRaiser) null;
      IQPart parentPart = part.ParentPart;
      while (true)
      {
        switch (parentPart)
        {
          case null:
          case IQCompositeItemEventRaiser _:
            goto label_5;
          default:
            parentPart = parentPart.ParentPart;
            continue;
        }
      }
label_5:
      return parentPart as IQCompositeItemEventRaiser;
    }

    internal static void NotifyChildPartVisibilityChanged(IQPart part)
    {
      if (part is QCompositeItemBase qcompositeItemBase)
        qcompositeItemBase.HandleAncestorVisibilityChanged();
      if (part.Parts == null)
        return;
      for (int index = 0; index < part.Parts.Count; ++index)
        QCompositeHelper.NotifyChildPartVisibilityChanged(part.Parts[index]);
    }

    internal static void NotifyChildPartEnabledChanged(IQPart part)
    {
      if (part is QCompositeItemBase qcompositeItemBase)
        qcompositeItemBase.HandleAncestorEnabledChanged();
      if (part.Parts == null)
        return;
      for (int index = 0; index < part.Parts.Count; ++index)
        QCompositeHelper.NotifyChildPartEnabledChanged(part.Parts[index]);
    }

    internal static void NotifyChildPartScrollingStage(
      IQScrollablePart scrollingPart,
      IQPart part,
      QScrollablePartScrollStage stage)
    {
      if (part is QCompositeItemBase qcompositeItemBase)
        qcompositeItemBase.HandleScrollingStage(scrollingPart, stage);
      if (part.Parts == null)
        return;
      for (int index = 0; index < part.Parts.Count; ++index)
        QCompositeHelper.NotifyChildPartScrollingStage(scrollingPart, part.Parts[index], stage);
    }

    internal static void RemoveCloneLinks(IQPartCollection collection)
    {
      if (collection == null)
        return;
      for (int index = 0; index < collection.Count; ++index)
      {
        IQPart qpart = collection[index];
        if (qpart is QCompositeItemBase qcompositeItemBase)
          qcompositeItemBase.RemoveCloneLink();
        else
          QCompositeHelper.RemoveCloneLinks((IQPartCollection) qpart.Parts);
      }
    }

    internal static void MoveUnclonablesToClones(IQPartCollection collection)
    {
      if (collection == null)
        return;
      for (int index = 0; index < collection.Count; ++index)
      {
        IQPart qpart = collection[index];
        if (qpart is QCompositeItemBase qcompositeItemBase)
          qcompositeItemBase.MoveUnclonablesToClone();
        else
          QCompositeHelper.MoveUnclonablesToClones((IQPartCollection) qpart.Parts);
      }
    }

    internal static void RestoreUnclonablesFromClones(IQPartCollection collection)
    {
      if (collection == null)
        return;
      for (int index = 0; index < collection.Count; ++index)
      {
        IQPart qpart = collection[index];
        if (qpart is QCompositeItemBase qcompositeItemBase)
          qcompositeItemBase.RestoreUnclonablesFromClone();
        else
          QCompositeHelper.RestoreUnclonablesFromClones((IQPartCollection) qpart.Parts);
      }
    }

    internal static void ResetState(IQPart part)
    {
      if (part == null)
        return;
      if (part is IQItemStatesImplementation statesImplementation)
      {
        if (statesImplementation.HasStatesDefined(QItemStates.Hot))
          statesImplementation.AdjustState(QItemStates.Hot, false, (object) QCompositeActivationType.None);
        if (statesImplementation.HasStatesDefined(QItemStates.Pressed))
          statesImplementation.AdjustState(QItemStates.Pressed, false, (object) QCompositeActivationType.None);
      }
      if (!(part.ContentObject is IQPartCollection))
        return;
      IQPartCollection contentObject = part.ContentObject as IQPartCollection;
      for (int index = 0; index < contentObject.Count; ++index)
        QCompositeHelper.ResetState(contentObject[index]);
    }

    internal static bool HasOrContainsFocus(IQCompositeContainer container)
    {
      if (container == null)
        return false;
      Control controlFromHandle = QControlHelper.GetFirstControlFromHandle(NativeMethods.GetFocus());
      return QCompositeHelper.IsOrContainsControl(container, controlFromHandle);
    }

    internal static bool IsOrContainsControl(IQCompositeContainer container, Control control)
    {
      if (container == null || control == null)
        return false;
      if (control == container.Control)
        return true;
      IQCompositeContainer qcompositeContainer;
      for (qcompositeContainer = control as IQCompositeContainer; control != null && qcompositeContainer == null; qcompositeContainer = control as IQCompositeContainer)
        control = control.Parent;
      return qcompositeContainer != null && qcompositeContainer.ParentContainer != null && QCompositeHelper.IsOrContainsControl(container, qcompositeContainer.ParentContainer.Control);
    }

    internal static void HandleMouseDown(
      QComposite composite,
      IQPart part,
      MouseEventArgs e,
      ref bool siblingPressed)
    {
      IQItemStatesImplementation statesImplementation = part as IQItemStatesImplementation;
      IQMouseHandler qmouseHandler = part as IQMouseHandler;
      if (part.HitTest(e.X, e.Y) != QPartHitTestResult.Nowhere)
      {
        bool flag = qmouseHandler == null || qmouseHandler.HandleMouseDown(e);
        if (statesImplementation != null && statesImplementation.HasStatesDefined(QItemStates.Pressed) && (!siblingPressed || composite.AllowMultiplePressedSiblings))
        {
          siblingPressed = true;
          statesImplementation.AdjustState(QItemStates.Pressed, true, (object) QCompositeActivationType.Mouse);
          if (part is QCompositeItem qcompositeItem && flag)
          {
            if (QItemStatesHelper.IsExpanded(qcompositeItem.ItemState) && composite.CloseExpandedItemOnClick((object) qcompositeItem))
            {
              composite.CollapseItem(qcompositeItem, QCompositeActivationType.Mouse);
              composite.CancelActivationForItem = (QCompositeItemBase) qcompositeItem;
            }
            else
              composite.ExpandItem(qcompositeItem, QCompositeActivationType.Mouse);
          }
        }
      }
      if (!(part.ContentObject is IQPartCollection))
        return;
      bool siblingPressed1 = false;
      IQPartCollection contentObject = part.ContentObject as IQPartCollection;
      for (int index = 0; index < contentObject.Count; ++index)
        QCompositeHelper.HandleMouseDown(composite, contentObject[index], e, ref siblingPressed1);
    }

    internal static bool HandleMouseUp(QComposite composite, IQPart part, MouseEventArgs e)
    {
      bool flag1 = false;
      bool flag2 = part.HitTest(e.X, e.Y) != QPartHitTestResult.Nowhere;
      QCompositeItemBase qcompositeItemBase = part as QCompositeItemBase;
      IQItemStatesImplementation statesImplementation = part as IQItemStatesImplementation;
      bool flag3 = !(part is IQMouseHandler) || qcompositeItemBase.HandleMouseUp(e);
      if (statesImplementation != null && statesImplementation.HasStatesDefined(QItemStates.Pressed) && QItemStatesHelper.IsPressed(statesImplementation.ItemState))
      {
        statesImplementation.AdjustState(QItemStates.Pressed, false, (object) QCompositeActivationType.Mouse);
        if (flag2)
        {
          if (flag3 && qcompositeItemBase != null && composite.CancelActivationForItem != qcompositeItemBase)
            qcompositeItemBase.Activate(QCompositeItemActivationOptions.Activate, QCompositeActivationType.Mouse);
          flag1 = true;
        }
      }
      if (part.ContentObject is IQPartCollection)
      {
        IQPartCollection contentObject = part.ContentObject as IQPartCollection;
        for (int index = 0; index < contentObject.Count; ++index)
          flag1 = QCompositeHelper.HandleMouseUp(composite, contentObject[index], e) || flag1;
      }
      return flag1;
    }

    internal static void HandleMouseMove(
      QComposite composite,
      IQPart part,
      MouseEventArgs e,
      QCompositeHelper.QCompositeHelperStorage storage,
      ref bool siblingHot,
      ref bool siblingPressed)
    {
      if (part == null)
        return;
      bool flag = part.HitTest(e.X, e.Y) != QPartHitTestResult.Nowhere;
      QCompositeItemBase qcompositeItemBase = part as QCompositeItemBase;
      IQItemStatesImplementation statesImplementation = part as IQItemStatesImplementation;
      IQMouseHandler qmouseHandler = part as IQMouseHandler;
      if (qcompositeItemBase != null)
      {
        if (flag && storage != null && !QMisc.IsEmpty((object) qcompositeItemBase.ToolTipText))
          storage.ToolTipItem = qcompositeItemBase;
        if (flag && storage != null && qcompositeItemBase.Cursor != (Cursor) null)
          storage.CursorItem = qcompositeItemBase;
      }
      if (statesImplementation != null && statesImplementation.HasStatesDefined(QItemStates.Hot))
      {
        if (flag && (!siblingHot || composite.AllowMultipleHotSiblings))
        {
          siblingHot = true;
          statesImplementation.AdjustState(QItemStates.Hot, true, (object) QCompositeActivationType.Mouse);
        }
        else
          statesImplementation.AdjustState(QItemStates.Hot, false, (object) QCompositeActivationType.Mouse);
      }
      if (storage != null && storage.MouseDownPoint.X >= 0 && storage.MouseDownPoint.Y >= 0 && statesImplementation != null && statesImplementation.HasStatesDefined(QItemStates.Pressed))
      {
        if (storage.PressedBehaviour == QCompositePressedBehaviour.MovePressedItem)
        {
          if (flag && (!siblingPressed || composite.AllowMultiplePressedSiblings))
          {
            siblingPressed = true;
            statesImplementation.AdjustState(QItemStates.Pressed, true, (object) QCompositeActivationType.Mouse);
          }
          else
            statesImplementation.AdjustState(QItemStates.Pressed, false, (object) QCompositeActivationType.Mouse);
        }
        else if (storage.PressedBehaviour == QCompositePressedBehaviour.RememberPressedItem)
        {
          if (QItemStatesHelper.IsHot(statesImplementation.ItemState) && part.HitTest(storage.MouseDownPoint.X, storage.MouseDownPoint.Y) != QPartHitTestResult.Nowhere && (!siblingPressed || composite.AllowMultiplePressedSiblings))
          {
            siblingPressed = true;
            statesImplementation.AdjustState(QItemStates.Pressed, true, (object) QCompositeActivationType.Mouse);
          }
          else
            statesImplementation.AdjustState(QItemStates.Pressed, false, (object) QCompositeActivationType.Mouse);
        }
      }
      if (qmouseHandler != null)
        qcompositeItemBase.HandleMouseMove(e);
      if (!(part.ContentObject is IQPartCollection))
        return;
      bool siblingHot1 = false;
      bool siblingPressed1 = false;
      IQPartCollection contentObject = part.ContentObject as IQPartCollection;
      for (int index = 0; index < contentObject.Count; ++index)
        QCompositeHelper.HandleMouseMove(composite, contentObject[index], e, storage, ref siblingHot1, ref siblingPressed1);
    }

    internal class QCompositeHelperStorage
    {
      public QCompositeItemBase ToolTipItem;
      public QCompositeItemBase CursorItem;
      public Point MouseDownPoint;
      public QCompositePressedBehaviour PressedBehaviour;
    }
  }
}
