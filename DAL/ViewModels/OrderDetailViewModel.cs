using DAL.Models;

namespace DAL.ViewModels;

public class OrderDetailViewModel
{
    // Order Details
    public long InvoiceId { get; set; }
    public string InvoiceNo { get; set; } = null!;
    public long OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = null!;
    public string? OrderInstruction { get; set; }

    // Customer Details
    public long CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public long? PhoneNo { get; set; }
    public string? Email { get; set; }
    public int NoOfPerson { get; set; }

    // Tables Section Details
    public List<Table> tableList { get; set; } = null!;
    public long SectionId { get; set; }
    public string SectionName { get; set; } = null!;

    // List of View Models
    public List<ItemOrderViewModel> itemOrderVM { get; set; } = null!;
    public List<TaxInvoiceViewModel> taxInvoiceVM { get; set; } = null!;

    // Extra Fields
    public decimal SubTotalAmountOrder { get; set; }
    public decimal TotalAmountOrder { get; set; }

}

/*
using DAL.Models;

namespace DAL.ViewModels;

public class OrderDetaIlsInvoiceViewModel
{

    //order details

    public long InvoiceId { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public long OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public string OrderStatus { get; set; } = null!;

    public string OtherInstruction{get;set;}



    //customer details
     public long CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public long? Phoneno { get; set; }

    public long NumberOfPerson{get;set;}

    public string? Email { get; set; }


    //table details
    public List<Table> tableList{get;set;}
    public long SectionId { get; set; }
    public string SectionName { get; set; } = null!;

    public List<ItemForInvoiceOrderDetails> ItemsInOrderDetails { get; set; }

    public List<TaxForOrderDetailsInvoice> TaxesInOrderDetails { get; set; }

    public decimal SubTotalAmountOfOrder { get; set; }

    public decimal TotalAmountOfOrderMain { get; set; }



    

    
}
*/