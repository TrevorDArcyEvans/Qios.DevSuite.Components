// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QObjectCloner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  internal class QObjectCloner
  {
    private QObjectCloner()
    {
    }

    internal static QCloneBehaviorType GetCloneBehaviorType(FieldInfo fieldInfo)
    {
      object[] customAttributes = fieldInfo.GetCustomAttributes(typeof (QCloneBehaviorAttribute), true);
      for (int index = 0; index < customAttributes.Length; ++index)
      {
        if (customAttributes[index] is QCloneBehaviorAttribute)
          return ((QCloneBehaviorAttribute) customAttributes[index]).BehaviorType;
      }
      return QCloneBehaviorType.Default;
    }

    public static object ShallowCloneObject(object value) => QObjectCloner.CloneObject(value, true);

    public static object CloneObject(object value) => QObjectCloner.CloneObject(value, false);

    internal static object CreateNewObjectInstance(object sourceObject)
    {
      BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
      Type type = sourceObject.GetType();
      object[] parameters = (object[]) null;
      ConstructorInfo constructor = type.GetConstructor(bindingAttr, (Binder) null, new Type[2]
      {
        typeof (object),
        typeof (QObjectClonerConstructOptions)
      }, (ParameterModifier[]) null);
      if (constructor != null)
      {
        parameters = new object[2]
        {
          sourceObject,
          (object) QObjectClonerConstructOptions.None
        };
      }
      else
      {
        constructor = type.GetConstructor(bindingAttr, (Binder) null, new Type[0], (ParameterModifier[]) null);
        if (constructor != null)
          parameters = new object[0];
      }
      return constructor != null ? constructor.Invoke(parameters) : throw new Exception(QResources.GetException("QObjectCloner_CannotFindConstructors", (object) sourceObject.GetType().ToString()));
    }

    internal static void CopyToObject(object source, object target, bool shallow) => QObjectCloner.CopyToObject(source, target, shallow, -1);

    internal static void CopyToObject(object source, object target, bool shallow, int levels)
    {
      for (Type type = source.GetType(); type != null && (levels < 0 || levels > 0); --levels)
      {
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        for (int index = 0; index < fields.Length; ++index)
        {
          QCloneBehaviorType cloneBehaviorType = QObjectCloner.GetCloneBehaviorType(fields[index]);
          object obj = fields[index].GetValue(source);
          if ((object) (obj as Delegate) == null && cloneBehaviorType != QCloneBehaviorType.DoNotClone)
          {
            ICloneable cloneable = obj as ICloneable;
            if (!shallow && cloneable != null && cloneBehaviorType == QCloneBehaviorType.Default)
              fields[index].SetValue(target, cloneable.Clone());
            else
              fields[index].SetValue(target, obj);
          }
        }
        type = type.BaseType;
      }
    }

    public static object CloneObject(object value, bool shallow) => QObjectCloner.CloneObject(value, shallow, -1);

    public static object CloneObject(object value, bool shallow, int levels)
    {
      object newObjectInstance = QObjectCloner.CreateNewObjectInstance(value);
      QObjectCloner.CopyToObject(value, newObjectInstance, shallow, levels);
      return newObjectInstance;
    }
  }
}
