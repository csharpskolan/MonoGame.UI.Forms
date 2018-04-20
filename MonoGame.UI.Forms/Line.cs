using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoGame.UI.Forms
{
    public class Line : Control
    {
        public Vector2 StartPosition
        {
            get { return _startPosition; }
            set { _startPosition = value; UpdateCorners(); }
        }

        public Vector2 EndPosition
        {
            get { return _endPosition; }
            set { _endPosition = value; UpdateCorners(); }
        }

        public int LineThickness
        {
            get { return _lineThickness; }
            set { _lineThickness = value; UpdateCorners(); }
        }

        private List<Vector2> _transformedCorners;
        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private int _lineThickness;

        public Line(Vector2 from, Vector2 to)
        {
            _lineThickness = 1;
            _startPosition = from;
            _endPosition = to;
            BackgroundColor = Color.White;
            UpdateCorners();
        }

        internal override void Draw(DrawHelper helper, Vector2 offset)
        {
            helper.DrawLine(StartPosition + offset, EndPosition + offset, BackgroundColor, LineThickness);
        }

        public override bool Contains(Point point)
        {
            for (int i = 0; i < _transformedCorners.Count; i++)
            {
                var norm = point.ToVector2() - _transformedCorners[i];
                var line = _transformedCorners[(i + 1) % _transformedCorners.Count] - _transformedCorners[i];
                var normal = new Vector2(-line.Y, line.X);
                if ((normal.X * norm.X + normal.Y * norm.Y) < 0)
                    return false;
            }

            return true;
        }

        private void UpdateCorners()
        {
            _transformedCorners = new List<Vector2>();
            var line1 = EndPosition - StartPosition;
            var line2 = new Vector2(line1.Y, -line1.X);
            line2.Normalize();
            _transformedCorners.Add(StartPosition + (LineThickness * 0.5f) * line2);
            _transformedCorners.Add(_transformedCorners[0] + line1);
            _transformedCorners.Add(_transformedCorners[1] - (line2 * LineThickness));
            _transformedCorners.Add(_transformedCorners[2] - line1);
        }

    }
}
