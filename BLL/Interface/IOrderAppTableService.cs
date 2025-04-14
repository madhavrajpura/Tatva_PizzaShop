using DAL.ViewModels;

namespace BLL.Interface;

public interface IOrderAppTableService
{
    public List<OrderAppSectionVM> GetAllSectionList();
    public List<OrderAppTableVM> GetTablesBySection(long SectionId);
    // Task<bool> AddCustomer(WaitingTokenDetailsViewModel waitingTokenvm, long userId);

    // public long IsCustomerPresent(string Email);

    // Task<bool> AddCustomerToWaitingList(WaitingTokenDetailsViewModel waitingTokenvm, long userId);

}
