using BaoXia.Utils.Extensions;
using SkiaSharp;

namespace BaoXia.ImageUtils.Extensions;

public static class SKCanvasExtension
{
	////////////////////////////////////////////////
	// @类方法
	////////////////////////////////////////////////

	#region 类方法

	public static SKRect Bounds(this SKCanvas canvas)
	{
		canvas.GetDeviceClipBounds(out var boundsI);
		var bounds = new SKRect(
		    0,
		    0,
		    boundsI.Width,
		    boundsI.Height);
		{ }
		return bounds;
	}

	public static SKRectI BoundsI(this SKCanvas canvas)
	{
		canvas.GetDeviceClipBounds(out var boundsI);
		var bounds = new SKRectI(
		    0,
		    0,
		    boundsI.Width,
		    boundsI.Height);
		{ }
		return bounds;
	}

	public static void DrawTextToLeftTop(
	    this SKCanvas canvas,
	    string? text,
	    float left,
	    float top,
	    SKFont skFont,
	    SKPaint skPaint)
	{
		if (string.IsNullOrEmpty(text))
		{
			return;
		}

		var textDrawX = left;
		var textDrawY = top;

		// !!!
		textDrawY += skFont.Metrics.CapHeight;
		// !!!
		canvas.DrawText(
		    text,
		    textDrawX,
		    textDrawY,
		    skFont,
		    skPaint);
	}

	public static void DrawTextToLeftTop(
	    this SKCanvas canvas,
	    string? text,
	    SKPoint point,
	    SKFont skFont,
	    SKPaint skPaint)
	{
		SKCanvasExtension.DrawTextToLeftTop(
			canvas,
			text,
			point.X,
			point.Y,
			skFont,
			skPaint);
	}


	public static SKRect DrawTextToLeftTop(
	    this SKCanvas canvas,
	    string? text,
	    float left,
	    float top,
	    SKFont skFont,
	    SKPaint skPaint,
	    uint textColorARGBHex)
	{
		var textLocation = new SKPoint(left, top);
		skPaint = skPaint.Clone();
		skPaint.Color = new SKColor(textColorARGBHex);
		skFont.MeasureText(text, out var textBounds);
		// !!!!
		canvas.DrawTextToLeftTop(
			text,
			textLocation,
			skFont,
			(SKPaint)skPaint);
		// !!!
		return SKRect.Create(
			textLocation.X,
			textLocation.Y,
			textBounds.Width,
			textBounds.Height);
	}

	public static SKRect DrawTextInRect(
		this SKCanvas canvas,
		string? text,
		SKRect rect,
		float lineHeight,
		SKTextAlign textHorizontalAlign,
		SKTextAlign textVerticalAlign,
		SKPaint skPaint,
		SKFont skFont)
	{
		var textBounds = SKRect.Create(
				rect.Left,
				rect.Top,
				rect.Width,
				0);
		if (string.IsNullOrEmpty(text))
		{
			return textBounds;
		}

		var lineLeft = textBounds.Left;
		var lineTop = textBounds.Top;
		var lineBottom = lineTop;
		var lineWidth = textBounds.Width;
		var lineTextHeight = skFont.Metrics.XHeight;
		if (lineHeight < 0)
		{
			lineHeight = lineTextHeight + skFont.Spacing;
		}

		var textWillDraw = text;
		while (textWillDraw?.Length > 0)
		{
			var lineTextLength
				= skFont.BreakText(
					textWillDraw,
					lineWidth,
					out var lineTextWidth);
			var lineTextLeft = lineLeft;
			string lineText;
			if (lineTextLength > 0)
			{
				lineText = textWillDraw.Left(lineTextLength);
				textWillDraw = textWillDraw.Right(textWillDraw.Length - (int)lineTextLength);
			}
			else
			{
				lineText = textWillDraw;
				textWillDraw = null;
			}

			if (textHorizontalAlign == SKTextAlign.Center)
			{
				lineTextLeft = lineLeft + (lineWidth - lineTextWidth) / 2.0F;
			}
			else if (textHorizontalAlign == SKTextAlign.Right)
			{
				lineTextLeft = lineLeft + lineWidth - lineTextWidth;
			}
			var lineTextTop = lineTop;
			if (textVerticalAlign == SKTextAlign.Center)
			{
				lineTextTop = lineTop + (lineHeight - lineTextHeight) / 2.0F;
			}
			else if (textVerticalAlign == SKTextAlign.Right)
			{
				lineTextTop = lineTop + lineHeight - lineTextHeight;
			}
			canvas.DrawTextToLeftTop(
				lineText,
				lineTextLeft,
				lineTextTop,
				skFont,
				skPaint);
			// !!!
			lineBottom = lineTop + lineHeight;
			lineTop = lineBottom;
			// !!!
		}
		// !!!
		textBounds.Bottom = lineBottom;
		// !!!

		return textBounds;
	}

	#endregion
}