﻿using System;
using System.Collections.Generic;
using System.Drawing;

using Foundation;
using UIKit;

using TelerikUI;

namespace Examples
{
	public class CustomPointLabels : ExampleViewController
	{
		TKChart chart;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			chart = new TKChart (this.ExampleBounds);
			chart.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			chart.Delegate = new ChartDelegate (0, 3);
			this.View.AddSubview (chart);

			int[] values = new int[] { 58, 59, 61, 64, 66, 69, 72, 72, 69 };
			int[] values1 = new int[] { 42, 44, 47, 51, 56, 61, 62, 64, 62 };
			List<TKChartDataPoint> dataPoints = new List<TKChartDataPoint> ();
			List<TKChartDataPoint> dataPoints1 = new List<TKChartDataPoint> ();
			for (int i = 0; i < values.Length; i++) {
				TKChartDataPoint point = new TKChartDataPoint (new NSNumber (i), new NSNumber (values [i]));
				TKChartDataPoint point1 = new TKChartDataPoint (new NSNumber (i), new NSNumber (values1 [i]));
				dataPoints.Add (point);
				dataPoints1.Add (point1);
			}

			TKChartLineSeries lineSeries = new TKChartLineSeries (dataPoints.ToArray ());
			lineSeries.SelectionMode = TKChartSeriesSelectionMode.DataPoint;
			lineSeries.Style.PointShape = new TKPredefinedShape (TKShapeType.Circle, new SizeF (8, 8));
			lineSeries.Style.PointLabelStyle.TextHidden = false;
			lineSeries.Style.PointLabelStyle.LabelOffset = new UIOffset (0, -24);
			lineSeries.Style.PointLabelStyle.Insets = new UIEdgeInsets (-1, -5, -1, -5);
			lineSeries.Style.PointLabelStyle.LayoutMode = TKChartPointLabelLayoutMode.Manual;
			lineSeries.Style.PointLabelStyle.Font = UIFont.SystemFontOfSize (10);
			lineSeries.Style.PointLabelStyle.TextAlignment = UITextAlignment.Center;
			lineSeries.Style.PointLabelStyle.TextColor = UIColor.White;
			lineSeries.Style.PointLabelStyle.Fill = new TKSolidFill (new UIColor ((float)(108 / 255.0), (float)(181 / 255.0), (float)(250 / 255.0), (float)1.0));
			lineSeries.Style.PointLabelStyle.ClipMode = TKChartPointLabelClipMode.Hidden;

			TKChartLineSeries lineSeries1 = new TKChartLineSeries (dataPoints1.ToArray ());
			lineSeries1.SelectionMode = TKChartSeriesSelectionMode.DataPoint;
			lineSeries1.Style.PointShape = new TKPredefinedShape (TKShapeType.Circle, new SizeF (8, 8));
			lineSeries1.Style.PointLabelStyle.TextHidden = false;
			lineSeries1.Style.PointLabelStyle.LabelOffset = new UIOffset (0, -24);
			lineSeries1.Style.PointLabelStyle.Insets = new UIEdgeInsets (-1, -5, -1, -5);
			lineSeries1.Style.PointLabelStyle.LayoutMode = TKChartPointLabelLayoutMode.Manual;
			lineSeries1.Style.PointLabelStyle.Font = UIFont.SystemFontOfSize (10);
			lineSeries1.Style.PointLabelStyle.TextAlignment = UITextAlignment.Center;
			lineSeries1.Style.PointLabelStyle.TextColor = UIColor.White;
			lineSeries1.Style.PointLabelStyle.Fill = new TKSolidFill (new UIColor ((float)(241 / 255.0), (float)(140 / 255.0), (float)(133 / 255.0), (float)1.0));
			lineSeries1.Style.PointLabelStyle.ClipMode = TKChartPointLabelClipMode.Hidden;

			TKChartNumericAxis yAxis = new TKChartNumericAxis (new NSNumber (40), new NSNumber (80));
			yAxis.MajorTickInterval = new NSNumber (10);
			chart.YAxis = yAxis;

			chart.AddSeries (lineSeries);
			chart.AddSeries (lineSeries1);
		}

		class ChartDelegate : TKChartDelegate
		{
			int selectedSeriesIndex;
			int selectedDataPointIndex;

			public ChartDelegate(int selectedSeriesIndex, int selectedDataPointIndex)
			{
				this.selectedSeriesIndex = selectedSeriesIndex;
				this.selectedDataPointIndex = selectedDataPointIndex;
			}

			public override TKChartPointLabel LabelForDataPoint (TKChart chart, TKChartData dataPoint, TKChartSeries series, nuint dataIndex)
			{
				TKChartDataPoint point = (TKChartDataPoint)dataPoint;
				if (series.Index == (nuint)this.selectedSeriesIndex && dataIndex == (nuint)this.selectedDataPointIndex) {
					return new MyPointLabel (point, series.Style.PointLabelStyle, String.Format ("{0}", point.DataYValue));
				}

				
				return new TKChartPointLabel (point, series.Style.PointLabelStyle, String.Format ("{0}", point.DataYValue));
			}
				
			public override TKChartPaletteItem PaletteItemForPoint (TKChart chart, nuint index, TKChartSeries series)
			{
				if (series.Index == (nuint)this.selectedSeriesIndex && index == (nuint)this.selectedDataPointIndex) {
					return new TKChartPaletteItem (new TKStroke (UIColor.Black, 2.0f), new TKSolidFill (UIColor.White));
				}

				if (series.Index == 0) {
					return new TKChartPaletteItem (new TKSolidFill (new UIColor ((float)(108 / 255.0), (float)(181 / 255.0), (float)(250 / 255.0), (float)1.0)));
				}

				return new TKChartPaletteItem (new TKSolidFill (new UIColor ((float)(241 / 255.0), (float)(140 / 255.0), (float)(133 / 255.0), (float)1.0)));
			}

			public override void PointSelected (TKChart chart, TKChartData point, TKChartSeries series, nint index)
			{
				this.selectedSeriesIndex = (int)series.Index;
				this.selectedDataPointIndex = (int)index;
			}
		}
	}
}

