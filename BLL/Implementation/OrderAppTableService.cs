using BLL.Interface;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BLL.Implementation;

public class OrderAppTableService : IOrderAppTableService
{
    private readonly PizzaShopDbContext _context;

    #region Constructor
    public OrderAppTableService(PizzaShopDbContext context)
    {
        _context = context;
    }
    #endregion

    public List<OrderAppSectionVM> GetAllSectionList()
    {
        List<OrderAppSectionVM> sectionList = _context.Sections
            .Where(x => !x.Isdelete).OrderBy(x => x.SectionId)
            .Select(x => new OrderAppSectionVM
            {
                SectionId = x.SectionId,
                SectionName = x.SectionName,
                AvailableCount = x.Tables.Count(y => y.Status == "Available"),
                AssignedCount = x.Tables.Count(y => y.Status == "Assigned"),
                RunningCount = x.Tables.Count(y => y.Status == "Running"),
            }).ToList();

            if(sectionList == null){
                return null;
            }

        return sectionList;
    }

    public List<OrderAppTableVM> GetTablesBySection(long SectionId)
    {
        List<OrderAppTableVM>? tableListVM = _context.Tables.Where(x => x.Section.SectionId == SectionId && !x.Isdelete)
        .Select(y => new OrderAppTableVM
        {
            TableId = y.TableId,
            SectionId = y.SectionId,
            TableName = y.TableName,
            Capacity = y.Capacity,
            Status = y.Status,
            TableTime = (DateTime)y.CreatedAt,
            OrderAmount = (decimal)205.00
            //  _context.Orders
            //     .Where(o => o.SectionId == x.SectionId && o.TableId == y.TableId && !o.Isdelete)
            //     .Sum(o => o.Orderdetails.Sum(od => od.Quantity * od.Item.Rate))
        }).ToList();

        if(tableListVM == null)
        {
            return null;
        }

        return tableListVM;
    }


//  #region IsCustomerPresent
//     public long IsCustomerPresent(string Email)
//     {
//         return _context.Customers.FirstOrDefault(x => x.Email == Email && x.Isdelete == false).CustomerId;
//     }
//     #endregion

// #region AddCustomer
//     public async Task<bool> AddCustomer(WaitingTokenDetailsViewModel waitingTokenvm, long userId)
//     {
//         Customer customer = new();
//         customer.CustomerName = waitingTokenvm.Name;
//         customer.Email = waitingTokenvm.Email;
//         customer.Phoneno = waitingTokenvm.Mobileno;
//         customer.CreatedBy = userId;
//         await _context.AddAsync(customer);
//         await _context.SaveChangesAsync();
//         return true;
//     }
//     #endregion

//     #region AddCustomerToWaitingList
//     public async Task<bool> AddCustomerToWaitingList(WaitingTokenDetailsViewModel waitingTokenvm, long userId)
//     {
//         try{
//             long customerId = IsCustomerPresent(waitingTokenvm.Email);

//         Waitinglist waitinglist = new();
//         waitinglist.CustomerId = customerId;
//         waitinglist.NoOfPerson = waitingTokenvm.NoOfPerson;
//         waitinglist.SectionId = waitingTokenvm.SectionID;
//         await _context.AddAsync(waitinglist);
//         await _context.SaveChangesAsync();
//         return true;

//         }catch(Exception e){
//             return false;
//         }
//     }

//     #endregion
}