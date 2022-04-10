// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QColorCodeSerializer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.CodeDom;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace Qios.DevSuite.Components.Design
{
  public class QColorCodeSerializer : CodeDomSerializer
  {
    public override object Serialize(IDesignerSerializationManager manager, object value)
    {
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      QColor qcolor = (QColor) value;
      CodeStatementCollection statementCollection = ((CodeDomSerializer) manager.GetSerializer(typeof (Component), typeof (CodeDomSerializer))).Serialize(manager, (object) qcolor) as CodeStatementCollection;
      CodeExpression expression1 = this.SerializeToExpression(manager, (object) qcolor);
      if (statementCollection == null)
      {
        statementCollection = new CodeStatementCollection();
      }
      else
      {
        for (int index = statementCollection.Count - 1; index >= 0; --index)
        {
          if (statementCollection[index] is CodeCommentStatement)
            statementCollection.Remove(statementCollection[index]);
        }
      }
      for (int index = 0; index < qcolor.ColorScheme.Themes.Count; ++index)
      {
        if (qcolor.ShouldSerializeColor(qcolor.ColorScheme.Themes[index].ThemeName))
        {
          CodeExpression expression2 = this.SerializeToExpression(manager, (object) qcolor[qcolor.ColorScheme.Themes[index].ThemeName]);
          statementCollection.Add((CodeExpression) new CodeMethodInvokeExpression(expression1, "SetColor", new CodeExpression[3]
          {
            (CodeExpression) new CodePrimitiveExpression((object) qcolor.ColorScheme.Themes[index].ThemeName),
            expression2,
            (CodeExpression) new CodePrimitiveExpression((object) false)
          }));
        }
      }
      return (object) statementCollection;
    }

    public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
    {
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      return ((CodeDomSerializer) manager.GetSerializer(typeof (QColor).BaseType, typeof (CodeDomSerializer))).Deserialize(manager, codeObject);
    }
  }
}
