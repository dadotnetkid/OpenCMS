using System.ComponentModel;

namespace OpenCMS.Shared.Common
{
    public enum CardFileType
    {
        Customer=0,
        Supplier=1
    }

    public enum SalesStatus
    {
        Quotation,
        Order,
        Invoice
    }

    public enum TransactionTypes
    {
        Purchase = 1,
        Sales =2
        
    }
}
