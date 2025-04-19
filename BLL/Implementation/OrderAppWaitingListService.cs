using BLL.Interface;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BLL.Implementation;

public class OrderAppWaitingListService : IOrderAppWaitingListService
{
    private readonly PizzaShopDbContext _context;

    #region Constructor
    public OrderAppWaitingListService(PizzaShopDbContext context)
    {
        _context = context;
    }
    #endregion

    public List<WaitingTokenDetailViewModel> GetWaitingList(long sectionid)
    {
        try
        {
            var waitingData = _context.Waitinglists.Include(x => x.Customer).Where(waiting => !waiting.Isdelete && !waiting.Isassign);

            if (sectionid == 0)
            {
                List<WaitingTokenDetailViewModel>? waiting = waitingData
                        .Select(waiting => new WaitingTokenDetailViewModel
                        {
                            WaitingId = waiting.WaitingId,
                            CustomerId = waiting.CustomerId,
                            CustomerName = waiting.Customer.CustomerName,
                            PhoneNo = waiting.Customer.PhoneNo,
                            Email = waiting.Customer.Email,
                            NoOfPerson = waiting.NoOfPerson,
                            CreatedAt = waiting.CreatedAt,
                            SectionId = waiting.SectionId,
                            SectionName = waiting.Section.SectionName
                        }).OrderBy(w => w.WaitingId).ToList();
                if (waiting == null)
                {
                    return null;
                }
                return waiting;

            }
            else
            {
                var waitingList = waitingData.Where(waiting => waiting.SectionId == sectionid)
                    .Select(waiting => new WaitingTokenDetailViewModel
                    {
                        WaitingId = waiting.WaitingId,
                        CustomerId = waiting.CustomerId,
                        CustomerName = waiting.Customer.CustomerName,
                        PhoneNo = waiting.Customer.PhoneNo,
                        Email = waiting.Customer.Email,
                        NoOfPerson = waiting.NoOfPerson,
                        CreatedAt = waiting.CreatedAt,
                        SectionId = waiting.SectionId,
                        SectionName = waiting.Section.SectionName

                    }).OrderBy(w => w.WaitingId).ToList();

                if (waitingList == null)
                {
                    return null;
                }
                return waitingList;
            }
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public WaitingTokenDetailViewModel GetWaitingToken(long waitingid)
    {
        try
        {
            WaitingTokenDetailViewModel? waitingList = _context.Waitinglists
           .Include(w => w.Customer)
           .Include(wc => wc.Section)
           .Where(wcs => wcs.WaitingId == waitingid && !wcs.Isdelete && !wcs.Isassign)
           .Select(w => new WaitingTokenDetailViewModel
           {
               WaitingId = waitingid,
               CustomerId = w.CustomerId,
               CustomerName = w.Customer.CustomerName,
               PhoneNo = w.Customer.PhoneNo,
               Email = w.Customer.Email,
               NoOfPerson = w.NoOfPerson,
               SectionId = w.SectionId,
               SectionName = w.Section.SectionName,
               CreatedAt = w.CreatedAt
           }).ToList().FirstOrDefault();

            return waitingList == null ? null : waitingList;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }


    // #region DeleteWaitingToken
    // public async Task<bool> DeleteWaitingToken(long waitingId)
    // {
    //     Waitinglist? waitingList = await _context.Waitinglists.FirstOrDefaultAsync(x => x.WaitingId == waitingId && x.Isassign == false && x.Isdelete == false);
    //     if (waitingList != null)
    //     {
    //         waitingList.Isdelete = true;
    //         _context.Update(waitingList);
    //         await _context.SaveChangesAsync();
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }
    // #endregion


    // #region GetTableBySection
    // public List<TablesViewModel> GetTableBySection(long sectionID)
    // {
    //     return _context.Tables.Where(x => x.SectionId == sectionID && x.Isdelete == false && x.Status == "Available")
    //             .Select(t => new TablesViewModel
    //             {
    //                 TableId = t.TableId,
    //                 TableName = t.TableName,
    //                 SectionId = t.SectionId,
    //                 Capacity = t.Capacity,
    //             }).ToList();
    // }
    // #endregion

    // #region AssignTable
    // public async Task<bool> AssignTable(int[] TableIds, long waitingId, long sectionId, long userId)
    // {
    //     try
    //     {
    //         Waitinglist waitinglist = await _context.Waitinglists.Include(x => x.Customer).FirstOrDefaultAsync(x => x.WaitingId == waitingId && x.Isdelete == false && x.Isassign == false);
    //         if (waitinglist == null) { return false; }
    //         waitinglist.Isassign = true;
    //         waitinglist.SectionId = sectionId;
    //         waitinglist.AssignedAt = DateTime.Now;
    //         waitinglist.ModifiedAt = DateTime.Now;
    //         waitinglist.ModifiedBy = userId;

    //         for (int i = 0; i < TableIds.Length; i++)
    //         {
    //             AssignTable assigntable = new();
    //             assigntable.CustomerId = waitinglist.CustomerId;
    //             assigntable.TableId = TableIds[i];
    //             assigntable.NoOfPerson = waitinglist.NoOfPerson;
    //             await _context.AddAsync(assigntable);

    //             Table table = await _context.Tables.FirstOrDefaultAsync(x => x.TableId == TableIds[i] && x.Isdelete == false);
    //             table.Status = "Assigned";
    //             table.ModifiedAt = DateTime.Now;
    //             table.ModifiedBy = userId;
    //             _context.Update(table);
    //             await _context.SaveChangesAsync();
    //         }

    //         _context.Update(waitinglist);
    //         await _context.SaveChangesAsync();

    //         return true;
    //     }
    //     catch (Exception e)
    //     {
    //         return false;
    //     }
    // }
    // #endregion


}