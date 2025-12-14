namespace BaoXia.ImageUtils.Models;

public class Padding(int left, int top, int right, int bottom)
{
	////////////////////////////////////////////////
	// @自身属性
	////////////////////////////////////////////////

	#region 自身属性

	public int Left { get; set; } = left;

	public int Right { get; set; } = right;

	public int Width
	{
		get
		{
			return Left + Right;
		}
	}

	public int Top { get; set; } = top;

	public int Bottom { get; set; } = bottom;

	public int Height
	{
		get
		{
			return Top + Bottom;
		}
	}

	#endregion
}