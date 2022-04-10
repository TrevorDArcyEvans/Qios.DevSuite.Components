// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QThemeInfoCollectionCodeSerializer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.CodeDom;
using System.ComponentModel.Design.Serialization;

namespace Qios.DevSuite.Components.Design
{
  public class QThemeInfoCollectionCodeSerializer : CodeDomSerializer
  {
    public override object Serialize(IDesignerSerializationManager manager, object value)
    {
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      QThemeInfoCollection qthemeInfoCollection = (QThemeInfoCollection) value;
      object obj = ((CodeDomSerializer) manager.GetSerializer(typeof (object), typeof (CodeDomSerializer))).Serialize(manager, (object) qthemeInfoCollection);
      CodeExpression targetObject = obj as CodeExpression;
      CodeStatementCollection statementCollection = obj as CodeStatementCollection;
      if (targetObject == null)
        targetObject = this.SerializeToExpression(manager, (object) qthemeInfoCollection);
      if (statementCollection == null)
        statementCollection = new CodeStatementCollection();
      else
        statementCollection.Clear();
      for (int index = 0; index < qthemeInfoCollection.Count; ++index)
      {
        if (qthemeInfoCollection[index].ShouldSerialize())
        {
          CodeExpression expression = this.SerializeToExpression(manager, (object) qthemeInfoCollection[index]);
          statementCollection.Add((CodeExpression) new CodeMethodInvokeExpression(targetObject, "Add", new CodeExpression[1]
          {
            expression
          }));
        }
      }
      return (object) statementCollection;
    }

    public override object Deserialize(IDesignerSerializationManager manager, object codeObject) => manager != null ? ((CodeDomSerializer) manager.GetSerializer(typeof (object), typeof (CodeDomSerializer))).Deserialize(manager, codeObject) : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
  }
}
