using System.ComponentModel;

namespace OpenCMS.Shared.Common
{
    public enum CardFileType
    {
        Customer = 0,
        Supplier = 1
    }

    public enum TransactionStatus
    {
        Quotation = 1,
        Order = 2,
        Invoice = 3
    }

    public enum TransactionType
    {
        Purchases = 1,
        Sales = 2

    }

    public enum PaymentMethod
    {
        Cash = 1,
        Check = 2,
        Credit,
        Debit
    }

    public enum PaymentTerm
    {
        [Description("Full Payment")]
        Full=1,
        [Description("Partial Payment")]
        Partial=2
    }
}
