// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDiff
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QDiff
  {
    private int[] m_iArrayA;
    private int[] m_iArrayB;
    private bool[,] m_bMatchPoints;
    private ArrayList m_oMatchPointsPath = new ArrayList();

    internal QDiff(int[] arrayA, int[] arrayB)
    {
      this.m_iArrayA = arrayA;
      this.m_iArrayB = arrayB;
    }

    private void CreateMatchPoints()
    {
      this.m_bMatchPoints = new bool[this.m_iArrayA.Length + 1, this.m_iArrayB.Length + 1];
      for (int index1 = 0; index1 < this.m_iArrayA.Length; ++index1)
      {
        for (int index2 = 0; index2 < this.m_iArrayB.Length; ++index2)
        {
          if (this.m_iArrayA[index1] == this.m_iArrayB[index2])
            this.m_bMatchPoints[index1 + 1, index2 + 1] = true;
        }
      }
    }

    internal QDiffChangeCollection CalculateDifferences()
    {
      QSubArray subA = new QSubArray(this.m_iArrayA);
      QSubArray subB = new QSubArray(this.m_iArrayB);
      this.m_oMatchPointsPath.Clear();
      this.CreateMatchPoints();
      this.FindMatchPointsPath(subA, subB);
      this.m_bMatchPoints = new bool[0, 0];
      return this.ConvertMatchPointsToDiffChangeCollection();
    }

    private QDiffChangeCollection ConvertMatchPointsToDiffChangeCollection()
    {
      QDiffChangeCollection changeCollection = new QDiffChangeCollection();
      int num1 = 1;
      int num2 = 1;
      this.m_oMatchPointsPath.Add((object) new Point(this.m_iArrayA.Length + 1, this.m_iArrayB.Length + 1));
      for (int index = 0; index < this.m_oMatchPointsPath.Count; ++index)
      {
        if (num1 < ((Point) this.m_oMatchPointsPath[index]).X && num2 < ((Point) this.m_oMatchPointsPath[index]).Y)
        {
          QDiffChange change = new QDiffChange(QDiffChangeType.Change, num1 - 1, num2 - 1, ((Point) this.m_oMatchPointsPath[index]).X - num1, ((Point) this.m_oMatchPointsPath[index]).Y - num2);
          changeCollection.Add(change);
        }
        else if (num1 < ((Point) this.m_oMatchPointsPath[index]).X)
        {
          QDiffChange change = new QDiffChange(QDiffChangeType.Delete, num1 - 1, num2 - 1, ((Point) this.m_oMatchPointsPath[index]).X - num1, 0);
          changeCollection.Add(change);
        }
        else if (num2 < ((Point) this.m_oMatchPointsPath[index]).Y)
        {
          QDiffChange change = new QDiffChange(QDiffChangeType.Insert, num1 - 1, num2 - 1, 0, ((Point) this.m_oMatchPointsPath[index]).Y - num2);
          changeCollection.Add(change);
        }
        num1 = ((Point) this.m_oMatchPointsPath[index]).X + 1;
        num2 = ((Point) this.m_oMatchPointsPath[index]).Y + 1;
      }
      return changeCollection;
    }

    private void FindMatchPointsPath(QSubArray subA, QSubArray subB)
    {
      Point[] matchPointRecursive = this.FindLongestMatchPointRecursive(subA, subB);
      for (int index = 1; index < matchPointRecursive.Length; index += 2)
        this.AddMatchPointsPath(matchPointRecursive[index - 1], matchPointRecursive[index]);
    }

    private void AddMatchPointsPath(Point start, Point end)
    {
      Point point = start;
      while (point != end)
      {
        ++point.X;
        ++point.Y;
        this.m_oMatchPointsPath.Add((object) point);
      }
    }

    private Point[] FindLongestMatchPointRecursive(QSubArray subA, QSubArray subB)
    {
      Point[] longestMatchPoint = this.FindLongestMatchPoint(subA, subB);
      Point[] pointArray1 = new Point[0];
      Point[] pointArray2 = new Point[0];
      if (longestMatchPoint.Length == 2)
      {
        pointArray1 = this.FindLongestMatchPointRecursive(new QSubArray(subA, 0, longestMatchPoint[0].X - subA.Offset), new QSubArray(subB, 0, longestMatchPoint[0].Y - subB.Offset));
        pointArray2 = this.FindLongestMatchPointRecursive(new QSubArray(subA, longestMatchPoint[1].X - subA.Offset, subA.Length - (longestMatchPoint[1].X - subA.Offset)), new QSubArray(subB, longestMatchPoint[1].Y - subB.Offset, subB.Length - (longestMatchPoint[1].Y - subB.Offset)));
      }
      Point[] matchPointRecursive = new Point[pointArray1.Length + longestMatchPoint.Length + pointArray2.Length];
      int index1 = 0;
      for (int index2 = 0; index2 < pointArray1.Length; ++index2)
      {
        matchPointRecursive[index1] = pointArray1[index2];
        ++index1;
      }
      for (int index3 = 0; index3 < longestMatchPoint.Length; ++index3)
      {
        matchPointRecursive[index1] = longestMatchPoint[index3];
        ++index1;
      }
      for (int index4 = 0; index4 < pointArray2.Length; ++index4)
      {
        matchPointRecursive[index1] = pointArray2[index4];
        ++index1;
      }
      return matchPointRecursive;
    }

    private Point[] FindLongestMatchPoint(QSubArray subA, QSubArray subB)
    {
      Point point = Point.Empty;
      int num1 = 0;
      int num2 = 0;
      int offset1 = subA.Offset;
      int offset2 = subB.Offset;
      for (int index1 = 0; index1 < subA.Length; ++index1)
      {
        for (int index2 = 0; index2 < subB.Length; ++index2)
        {
          if (this.m_bMatchPoints[index1 + offset1 + 1, index2 + offset2 + 1])
          {
            int num3 = 1;
            while (index1 - num3 >= 0 && index2 - num3 >= 0 && this.m_bMatchPoints[index1 + 1 - num3 + offset1, index2 + 1 - num3 + offset2])
              ++num3;
            int num4 = Math.Abs(index1 + offset1 + 1 - (index2 + offset2 + 1));
            if (num3 > num1 || num3 == num1 && num4 < num2)
            {
              point = new Point(index1 + 1, index2 + 1);
              num1 = num3;
              num2 = num4;
            }
          }
        }
      }
      if (!(point != Point.Empty))
        return new Point[0];
      return new Point[2]
      {
        new Point(offset1 + point.X - num1, offset2 + point.Y - num1),
        new Point(offset1 + point.X, offset2 + point.Y)
      };
    }
  }
}
