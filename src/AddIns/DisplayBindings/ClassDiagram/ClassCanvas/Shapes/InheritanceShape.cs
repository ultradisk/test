// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ClassDiagram
{
	public class InheritanceShape : VectorShape
	{
		static GraphicsPath path = InitializePath();
		
		static GraphicsPath InitializePath ()
		{
			GraphicsPath path = new GraphicsPath();
			path.StartFigure();
			path.AddPolygon(new PointF[]{
				new PointF(0.0f, 1.9f),
				new PointF(2.0f, 1.9f),
				new PointF(2.0f, 1.0f),
				new PointF(3.0f, 2.0f),
				new PointF(2.0f, 3.0f),
				new PointF(2.0f, 2.1f),
				new PointF(0.0f, 2.1f)
			});
			path.CloseFigure();
			path.StartFigure();
			path.AddPolygon(new PointF[]{
				new PointF(2.2f, 1.4f),
				new PointF(2.7f, 2.0f),
				new PointF(2.2f, 2.6f)
			});
			path.FillMode = FillMode.Alternate;
			path.CloseFigure();
			return path;
		}
		
		public override float ShapeWidth
		{
			get { return 3.0f; }
		}
		
		public override float ShapeHeight
		{
			get { return 4.0f; }
		}
		
		public override void Draw(Graphics graphics)
		{
			if (graphics == null) return;
			graphics.FillPath(Brushes.Black, path);
		}
	}
}
