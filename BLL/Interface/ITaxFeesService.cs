using DAL.ViewModels;

namespace BLL.Interface;

public interface ITaxFeesService
{
    public PaginationViewModel<TaxViewModel> GetTaxList(int pageNumber = 1,string search = "",  int pageSize = 3);  
    public Task<bool> DeleteTax(long taxid);

}
