namespace ScottLogic.NumbersGame.Game
{
    internal struct OperationDetail
    {
        public OperationDetail(int overwritePos, int erasePos, int overwriteValue, int eraseValue) : this()
        {
            OverwritePos = overwritePos;
            ErasePos = erasePos;
            OverwrittenValue = overwriteValue;
            ErasedValue = eraseValue;
        }

        public int OverwritePos { get; set; }
        public int ErasePos { get; set; }
        public int OverwrittenValue { get; set; }
        public int ErasedValue { get; set; }

    }
}
