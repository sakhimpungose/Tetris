using System.Collections.Generic;

namespace Tetris
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
            this.Offset = new Position(StartOffset.RowIndex, StartOffset.ColumnIndex);

        }

        public IEnumerable<Position> TilePositions()
        {
            foreach(Position p in Tiles[this.RotationState])
            {
                yield return new Position(p.RowIndex + this.Offset.RowIndex, p.ColumnIndex + this.Offset.ColumnIndex);
            }
        }

        public void RotateClockwise()
        {
            this.RotationState = (this.RotationState + 1) % Tiles.Length;
        }

        public void RotateCounterClockwise()
        {
            if (this.RotationState == 0)
            {
                this.RotationState = Tiles.Length - 1;
            }
            else
            {
                this.RotationState--;
            }
        }

        public void Move(int numberOfRows, int numberOfColumns)
        {
            this.Offset.RowIndex += numberOfRows;
            this.Offset.ColumnIndex += numberOfColumns;
        }

        public void Reset()
        {
            this.RotationState = 0;
            this.Offset.RowIndex = this.StartOffset.RowIndex;
            this.Offset.ColumnIndex = this.StartOffset.ColumnIndex;
        }

    }
}
