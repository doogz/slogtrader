using System.Runtime.Serialization;
using System.ServiceModel;

namespace NumbersGameWebServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INumbersGameService" in both code and config file together.
    [ServiceContract(Namespace = "http://scottlogic.co.uk")]
    public interface INumbersGameService
    {
        [OperationContract]
        string GetNewGame(int numValues);

        /*
        [OperationContract]
        bool VerifySolution(string game, string solution);
        */


        //  Wizard-inserted stuff on CompositeTypes
        /*
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        */
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "NumbersGameWebServiceLibrary.ContractType".
    
    /*
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
    */
}
