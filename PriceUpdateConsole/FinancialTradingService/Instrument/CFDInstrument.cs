namespace FinancialTradingService.Instruments
{
    class CfdInstrument : BaseInstrument
    {
        CfdInstrument(string symbol, string description) : base(symbol, Market.CfDs)
        {
            _description = description;
        }

        private readonly string _description;
        public override string Description
        {
            get
            {
                return _description;
            }
        }
        
        public override string MarketName
        {
            get { return "CFD"; }
        }
    }
}
