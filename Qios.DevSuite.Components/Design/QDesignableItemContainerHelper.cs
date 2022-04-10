// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QDesignableItemContainerHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  internal class QDesignableItemContainerHelper
  {
    internal QDesignableItemContainerHelper()
    {
    }

    public static bool ComponentIsDesignTimeVisible(IComponent component) => TypeDescriptor.GetAttributes((object) component).Contains((Attribute) DesignTimeVisibleAttribute.Yes);

    public static bool ShoudAddDesignTimeVisibleComponents(ISite site)
    {
      if (site == null || !(site.GetService(typeof (ISelectionService)) is ISelectionService service1))
        return true;
      ICollection selectedComponents = service1.GetSelectedComponents();
      if (selectedComponents == null)
        return true;
      foreach (IComponent component in (IEnumerable) selectedComponents)
      {
        if (component != null && component.Site != null && component.Site.GetService(typeof (IDesignerHost)) is IDesignerHost service2 && service2.GetDesigner(component) is IQDragControlDesigner designer && designer.IsDragging)
          return false;
      }
      return true;
    }

    public static ICollection AddAssociatedComponents(
      IComponent component,
      ICollection associatedComponents)
    {
      bool flag = QDesignableItemContainerHelper.ShoudAddDesignTimeVisibleComponents(component.Site);
      if (component is IQDesignableItemContainer qdesignableItemContainer)
      {
        IList associatedComponents1 = qdesignableItemContainer.AssociatedComponents;
        if (associatedComponents1 != null && associatedComponents1.Count > 0)
        {
          if (!(associatedComponents is IList list) || list.IsFixedSize || list.IsReadOnly)
            list = associatedComponents == null ? (IList) new ArrayList() : (IList) new ArrayList(associatedComponents);
          for (int index = 0; index < associatedComponents1.Count; ++index)
          {
            if (associatedComponents1[index] is IComponent component1 && component1.Site != null && (flag || !QDesignableItemContainerHelper.ComponentIsDesignTimeVisible(component1)))
              list.Add((object) component1);
          }
          return (ICollection) list;
        }
      }
      return associatedComponents;
    }

    public static void RemoveFromPossibleHost(IComponent component)
    {
      if (!(component is IQHostedComponent qhostedComponent))
        return;
      qhostedComponent.SetComponentHost((IQComponentHost) null, true);
    }

    public static void DestroyAssociatedComponents(
      IComponent component,
      ICollection associatedComponents,
      bool designTimeVisibleComponentsAlso)
    {
      IDesignerHost service = component.Site.GetService(typeof (IDesignerHost)) as IDesignerHost;
      foreach (IComponent associatedComponent in (IEnumerable) associatedComponents)
      {
        if (designTimeVisibleComponentsAlso || !QDesignableItemContainerHelper.ComponentIsDesignTimeVisible(associatedComponent))
          service.DestroyComponent(associatedComponent);
      }
    }
  }
}
