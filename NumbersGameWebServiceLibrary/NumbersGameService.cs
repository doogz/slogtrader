using NumbersGameWebServiceLibrary;

namespace ScottLogic.NumbersGame.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "NumbersGameService" in both code and config file together.
    public class NumbersGameService : INumbersGameService
    {
        public string GetNewGame(int numValues)
        {
            return "1,1,1,1,1,1=6";
        }

        /*
         * public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
         * */

    }
}
