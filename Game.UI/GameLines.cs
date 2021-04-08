using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Game.Logic;

namespace Game.UI
{
    // TG: rename the file as well
    public partial class GameLines : Form
    {
        // private IPathStrategy pathStrategy;
        private readonly Field _field = new Field(9, 9, 3, 4, 4, new GetPathOriginal());
        private Position _selectedPosition;

        private int BallSize { get; set; }

        public GameLines()
        {
            InitializeComponent();
        }

        private int CellSize => Math.Min(ClientRectangle.Height, ClientRectangle.Width) / _field.Height;
        private int LineWidth => 5;

        private void GameLines_Paint_1(object sender, PaintEventArgs e)
        {
            PaintGrid(e.Graphics);
            PaintBalls(e.Graphics);
            PaintBordersSelectedBall(e.Graphics);
        }

        private void PaintGrid(Graphics graphics)
        {
            var cellSize = CellSize;
            var gridWidth = cellSize * _field.Height;
            var gridHeight = cellSize * _field.Height;

            using var pen = new Pen(Color.SandyBrown, LineWidth);

            // Draw horizontal lines
            var rowLine = 0;
            for (int row = 0; row <= _field.Width; row++)
            {
                graphics.DrawLine(pen, 0, rowLine, gridWidth, rowLine);
                rowLine += cellSize;
            }

            // Draw vertical lines
            var columnLine = 0;
            for (int col = 0; col <= _field.Height; col++)
            {
                graphics.DrawLine(pen, columnLine, 0, columnLine, gridHeight);
                columnLine += cellSize;
            }
        }

        private void PaintBalls(Graphics graphics)
        {
            var indent = LineWidth;

            var cellSize = CellSize;
            BallSize = CellSize - 2 * indent;

            for (int row = 0; row < _field.Height; row++)
            {
                for (int col = 0; col < _field.Width; col++)
                {
                    var ballColor = _field.GetBallColorAt(row, col);
                    if (ballColor != BallColor.Empty)
                    {
                        DrawingBall(graphics, row, col, cellSize, indent, ballColor);
                    }
                }
            }
        }

        private void DrawingBall(Graphics graphics, int col, int row, int cellSize, int indent, BallColor color)
        {
            var colorPan = MapColor(color);
            using var pen = new Pen(colorPan);
            using var brush = new SolidBrush(colorPan);
            graphics.DrawEllipse(pen, col * cellSize + indent, row * cellSize + indent, BallSize, BallSize);
            graphics.FillEllipse(brush, col * cellSize + indent, row * cellSize + indent, BallSize, BallSize);
        }

        private void BlurOutBall(Graphics graphics, int row, int col, int cellSize, int indent)
        {
            DrawingBall(graphics, row, col, cellSize, indent, BallColor.White);
        }

        private Color MapColor(BallColor color)
        {
            switch (color)
            {
                case BallColor.Empty: return Color.SandyBrown;
                case BallColor.White: return Color.White;
                case BallColor.Black: return Color.Black;
                case BallColor.Blue: return Color.Blue;
                case BallColor.Red: return Color.Red;
                case BallColor.Yellow: return Color.Yellow;
            }

            return Color.SandyBrown;
        }

        private void GameLines_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void GameLines_Load(object sender, EventArgs e)
        {
            _field.PlaceBalls();
        }

        private void GameLines_Click(object sender, EventArgs e)
        {
            var mouseEventArgs = (MouseEventArgs) e;
            var clickedPosition = CalculatePositionByCoordinates(mouseEventArgs.X, mouseEventArgs.Y);
            var colorClickedPosition = _field.GetBallColorAt(clickedPosition);

            if (_selectedPosition == null)
            {
                if (colorClickedPosition != BallColor.Empty)
                {
                    _selectedPosition = clickedPosition;
                }
            }
            else
            {
                if (colorClickedPosition != BallColor.Empty)
                {
                    _selectedPosition = clickedPosition;
                }

                else if (colorClickedPosition == BallColor.Empty)
                {
                    if (_field.PathStrategy.GetPath(_selectedPosition, clickedPosition, new bool[_field.Height, _field.Width]) != null)
                    {
                        using var graphics = CreateGraphics();
                        var path = _field.PathStrategy.GetPath(_selectedPosition, clickedPosition,
                            new bool[_field.Height, _field.Width]);

                        var ballColor = _field.GetBallColorAt(_selectedPosition);
                        foreach (var position in path)
                        {
                            DrawingBall(graphics, position.Row, position.Column, CellSize, LineWidth, ballColor);
                            Thread.Sleep(40);
                            BlurOutBall(graphics, position.Row, position.Column, CellSize, LineWidth);
                        }
                        _field.MoveBall(_selectedPosition, clickedPosition);
                        _field.PlaceBalls();
                        var line = _field.RemoveLines(clickedPosition);
                        if (line != null)
                        {
                            RemoveLines(graphics, line);
                        }

                    }
                    _selectedPosition = null;
                }
            }
            Invalidate();
        }

        private void RemoveLines(Graphics graphics, IEnumerable<Position> balls)
        {
            Thread.Sleep(50);
            var cellSize = CellSize;
            foreach (var ball in balls)
            {
                var color = MapColor(_field.GetBallColorAt(ball));
                using var pen = new Pen(color, 5);
                graphics.DrawRectangle(pen, _selectedPosition.Row * cellSize, _selectedPosition.Column * cellSize,
                    cellSize, cellSize);
            }
        }

        private Position CalculatePositionByCoordinates(int x, int y)
        {
            var cellSize = CellSize;

            var positionX = x / cellSize;
            var positionY = y / cellSize;

            return new Position(positionX, positionY);
        }

        private void PaintBordersSelectedBall(Graphics graphics)
        {
            if (_selectedPosition == null) return;
            var cellSize = CellSize;
            using var pen = new Pen(Color.Black, 5);

            graphics.DrawRectangle(pen, _selectedPosition.Row * cellSize, _selectedPosition.Column * cellSize,
                cellSize, cellSize);
        }
    }
}
