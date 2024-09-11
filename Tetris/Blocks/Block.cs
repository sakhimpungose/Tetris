using System.Collections.Generic;

namespace Tetris.Blocks
{
    public abstract class Block
    {
        protected abstract Position[][] Tiles { get; }
        protected abstract Position StartOffset { get; }

        public abstract int Id { get; }

        private int RotationState;
        private Position Offset;

        public Block()
        {
            Offset = new Position(StartOffset.RowIndex, StartOffset.ColumnIndex);

        }

        public IEnumerable<Position> TilePositions()
        {
            foreach (Position p in Tiles[RotationState])
            {
                yield return new Position(p.RowIndex + Offset.RowIndex, p.ColumnIndex + Offset.ColumnIndex);
            }
        }

        public void RotateClockwise()
        {
            RotationState = (RotationState + 1) % Tiles.Length;
        }

        public void RotateCounterClockwise()
        {
            if (RotationState == 0)
            {
                RotationState = Tiles.Length - 1;
            }
            else
            {
                RotationState--;
            }
        }

        public void Move(int numberOfRows, int numberOfColumns)
        {
            Offset.RowIndex += numberOfRows;
            Offset.ColumnIndex += numberOfColumns;
        }

        public void Reset()
        {
            RotationState = 0;
            Offset.RowIndex = StartOffset.RowIndex;
            Offset.ColumnIndex = StartOffset.ColumnIndex;
        }

    }
}
