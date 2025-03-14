using BLL.Interface;
using DAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizza_Shop_Project.Authorization;

namespace Pizza_Shop_Project.Controllers
{
    public class RolePermissionController : Controller
    {
        private readonly IRolePermission _rolePermission;

        #region RolePermission Constructor
        public RolePermissionController(IRolePermission rolePermission)
        {
            _rolePermission = rolePermission;
        }
        #endregion

        #region RoleDashboard
        //Fetching roles
        [Authorize(Roles = "Admin")]
        [PermissionAuthorize("Role.View")]
        public IActionResult RoleDashboard()
        {
            ViewData["sidebar-active"] = "Role";
            var Roles = _rolePermission.GetAllRoles();
            return View(Roles);
        }
        #endregion

        #region Permission
        [Authorize(Roles = "Admin")]
        [PermissionAuthorize("Role.AddEdit")]
        public IActionResult Permission(string name)
        {
            ViewData["sidebar-active"] = "Role";
            List<RolesPermissionViewModel> permissions = _rolePermission.GetPermissionByRole(name);
            return View(permissions);
        }

        [Authorize(Roles = "Admin")]
        [PermissionAuthorize("Role.AddEdit")]
        [HttpPost]
        public IActionResult Permission(List<RolesPermissionViewModel> rolesPermissionViewModel)
        {
            for (int i = 0; i < rolesPermissionViewModel.Count; i++)
            {
                RolesPermissionViewModel rolesPermissionvm = new RolesPermissionViewModel();
                rolesPermissionvm.RolepermissionmappingId = rolesPermissionViewModel[i].RolepermissionmappingId;
                rolesPermissionvm.Canview = rolesPermissionViewModel[i].Canview;
                rolesPermissionvm.Canaddedit = rolesPermissionViewModel[i].Canaddedit;
                rolesPermissionvm.Candelete = rolesPermissionViewModel[i].Candelete;
                rolesPermissionvm.Permissioncheck = rolesPermissionViewModel[i].Permissioncheck;
                _rolePermission.EditPermissionMapping(rolesPermissionvm);
            }
            TempData["SuccessMessage"] = "Permissions Updated Successfully";
            return RedirectToAction("Permission", "RolePermission", new { name = rolesPermissionViewModel[0].RoleName });// 3rd para ma obj create krvopade bcoz
                                                                                                                         //  redirectToAction ma 3rd para obj accept kre string nai..nd get method ma name pass krva mate ahiyathi name no ob banavvi moklvu
        }
        #endregion
    
    }
}