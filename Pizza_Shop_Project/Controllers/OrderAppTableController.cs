using BLL.Interface;
using DAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizza_Shop_Project.Authorization;

namespace Pizza_Shop_Project.Controllers;


[PermissionAuthorize("AccountManager")]
public class OrderAppTableController : Controller
{
    private readonly ITableSectionService _sectionService;
    private readonly IOrderAppTableService _orderAppTableService;
    public OrderAppTableController(ITableSectionService sectionService, IOrderAppTableService orderAppTableService)
    {
        _sectionService = sectionService;
        _orderAppTableService = orderAppTableService;
    }

    public async Task<IActionResult> OrderAppTable()
    {
        OrderAppTableMainViewModel TableMainVM = new();
        TableMainVM.sectionListVM = _orderAppTableService.GetAllSectionList();
        ViewData["orderApp-Active"] = "Table";
        ViewData["Icon"] = "fa-table";
        return View(TableMainVM);
    }

    public async Task<IActionResult> GetTablesBySection(long SectionId){
        var tableList = _orderAppTableService.GetTablesBySection(SectionId);
        return PartialView("_TableList",tableList);
    }

    //     public async Task<IActionResult> WaitingTokenDetails(OrderAppTableViewModel orderappTablevm){
    //     string token = Request.Cookies["AuthToken"];
    //     var userData = _userService.getUserFromEmail(token);
    //     long userId = _userLoginSerivce.GetUserId(userData[0].Userlogin.Email);

    //     long customerIdIfPresent = _orderAppTableService.IsCustomerPresent(orderappTablevm.waitingTokenDetailsViewModel.Email);
    //     if(customerIdIfPresent == 0){
    //         bool createCustomer =await _orderAppTableService.AddCustomer(orderappTablevm.waitingTokenDetailsViewModel, userId);
    //         if(!createCustomer){
    //             return Json(new {success= false, text="Error While Adding Customer. Try Again!"});
    //         }
    //     }
    //     bool customerAddToWaitingList =await _orderAppTableService.AddCustomerToWaitingList(orderappTablevm.waitingTokenDetailsViewModel, userId);
    //     if(customerAddToWaitingList){
    //         return Json(new {success= true, text="Customer Added In Waiting List"});
    //     }
    //     return Json(new {success= false, text="Error While Adding Customer to waiting List. Try Again!"});
    // }


}
