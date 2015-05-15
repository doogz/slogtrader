using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService.Instrument
{
    public class PriceHistory
    {
        private readonly List<PricePoint> _points = new List<PricePoint>();

        /// <summary>
        /// Fast 'add', using pass by reference (requires a PricePoint already)
        /// </summary>
        /// <param name="p"></param>
        public void AddPoint(ref PricePoint p)
        {
            _points.Add(p);    
        }

        /// <summary>
        /// Fast 'add', using pass by reference, but checking our timepoint is newer than the previous one added
        /// </summary>
        /// <param name="p"></param>
        public void AddPointCheckTime(ref PricePoint p)
        {
            // Check we're adding price updates in sequential time
            // Our assumption is that the price points in our collection are in time order
            if (_points.Any())
                if (_points[_points.Count-1].Timestamp > p.Timestamp )
                    throw new Exception( "The last price point in the sequence is more recent than the price point to add");
            
            AddPoint(ref p);
                        
        }

        /// <summary>
        /// Convenice 'add' using individual values
        /// </summary>
        /// <param name="ask"></param>
        /// <param name="bid"></param>
        /// <param name="time"></param>
        public void AddPricePoint(decimal bid, decimal ask, DateTime time)
        {
            _points.Add(new PricePoint(bid, ask, time));
        }

        /// <summary>
        /// Remove all items from the history that pre-date a certain time.
        /// </summary>
        /// <param name="time"></param>
        public void RemoveBefore(DateTime time)
        {
            _points.RemoveAll(t => t.Timestamp < time);
        }


    }
}
