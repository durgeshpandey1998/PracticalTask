using BusinessLogicLayer;
using DataAccessLayer.DataBaseTable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PracticalTask.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace PracticalTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRegisterUserService _registerUserService;

        public HomeController(ILogger<HomeController> logger, IRegisterUserService registerUserService)
        {
            _logger = logger;
            _registerUserService = registerUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        #region Register Data
        public async Task<IActionResult> RegisterData()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterData(TblUserRegistration tblUserRegistration)
        {
            var context = new ValidationContext(tblUserRegistration, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(tblUserRegistration, context, validationResults, true);

            if (isValid)
            {
                var registerUser = await _registerUserService.AddTblUserRegisterationAsync(tblUserRegistration);
                return Json(new { success = true });
            }
            else
            {
                var errors = validationResults
                    .SelectMany(vr => vr.MemberNames.Select(memberName => new { Property = memberName, ErrorMessage = vr.ErrorMessage }))
                    .ToList();

                return Json(new { success = false, errors = errors });
            }
        }
        #endregion

        #region Delete Data
        [HttpPost]
        public async Task<bool> DeleUser(int Id)
        {
            bool isDeleted = false;
            if (Id > 0)
            {
                await _registerUserService.UserDeleteAsync(Id);
                isDeleted = true;
                return isDeleted;
            }
            else
            {
                return isDeleted;
            }
        }
        #endregion

        #region Display Data
        public async Task<IActionResult> DisplayUser()
        {
            var registerUser = await _registerUserService.GetTblUserRegistrationsAsync();
            return View(registerUser);
        }
        #endregion

        #region Get All State and City

        [HttpPost("GetAllCity")]
        public async Task<JsonResult> GetAllCity(int stateId)
        {
            var cityData = await _registerUserService.GetTblCityAsync(stateId);
            return Json(cityData);
        }
        [HttpGet("GetAllState")]
        public async Task<JsonResult> GetAllState()
        {
            var cityData = await _registerUserService.GetTblStateAsync();
            return Json(cityData);
        }

        #endregion

        #region Get All User By Id
        [HttpPost("GetUserById")]
        public async Task<JsonResult> GetUserById(int userId)
        {
            var userData = await _registerUserService.GetUserById(userId);
            return Json(userData);
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}