using CoffeeShop.Common.Exceptions;
using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Service;
using CoffeeShop.Web.App_Start;
using CoffeeShop.Web.Infrastructure.Extensions;
using CoffeeShop.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;


namespace CoffeeShop.Web.Controllers
{
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;
        private IApplicationGroupService _appGroupService;
        private IApplicationRoleService _appRoleService;
        public UserController(
            IApplicationGroupService appGroupService,
            IApplicationRoleService appRoleService,
            ApplicationUserManager userManager
            )
            : base()
        {
            _appRoleService = appRoleService;
            _appGroupService = appGroupService;
            _userManager = userManager;
        }

        // GET: Acountt
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            Setviewbag();
            Setviewbag1();
            return View();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create(HttpRequestMessage request, ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppUser = new ApplicationUser();
                newAppUser.UpdateUser(applicationUserViewModel);
                try
                {
                    //newAppUser.Id = applicationUserViewModel.Id;
                    var result = await _userManager.CreateAsync(newAppUser, applicationUserViewModel.Password);
                    if (result.Succeeded)
                    {
                        var listAppUserGroup = new List<ApplicationUserGroup>();
                        foreach (var group in applicationUserViewModel.Groups)
                        {
                            listAppUserGroup.Add(new ApplicationUserGroup()
                            {
                                GroupId = group.ID,
                                UserId = newAppUser.Id
                            });
                            //add role to user
                            var listRole = _appRoleService.GetListRoleByGroupId(group.ID);
                            foreach (var role in listRole)
                            {
                                await _userManager.RemoveFromRoleAsync(newAppUser.Id, role.Name);
                                await _userManager.AddToRoleAsync(newAppUser.Id, role.Name);
                            }
                        }
                        _appGroupService.AddUserToGroups(listAppUserGroup, newAppUser.Id);
                        _appGroupService.Save();

                      

                        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);

                    }
                 
                    else
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
              

                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
                catch (Exception ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
    
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,"sai rồi");
            }

            Setviewbag();
            Setviewbag1();
        }

        public void Setviewbag(int? selectedId = null)
        {
            //var db = new loaddata();
            var listDropDownList = _appRoleService.GetAll();
            ViewBag.RoleID = new SelectList(listDropDownList, "ID", "Name", selectedId);
        }
        public void Setviewbag1(int? selectedId = null)
        {
            //var db = new loaddata();
            var listDropDownList = _appGroupService.GetAll();
            ViewBag.Groups = new SelectList(listDropDownList, "ID", "Name", selectedId);
        }

    }
}