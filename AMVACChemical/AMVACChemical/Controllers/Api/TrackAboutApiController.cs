using AMVACChemical.Entities.TrackAbout;
using AMVACChemical.Interfaces.Repository;
using AMVACChemical.ViewModels.TrackAbout.Assets;
using AMVACChemical.ViewModels.TrackAbout.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AMVACChemical.Controllers.Api
{
    public class TrackAboutApiController : Controller
    {
        // Declare global configurations for inject
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _config;
        private readonly ITrackAboutRepository _trackAboutRepo;
        private readonly ILogger<TrackAboutApiController> _logger;

        // Create constructor
        public TrackAboutApiController(IHostingEnvironment env, IConfigurationRoot config, ITrackAboutRepository trackAboutRepo,
            ILogger<TrackAboutApiController> logger)
        {
            _env = env;
            _config = config;
            _trackAboutRepo = trackAboutRepo;
            _logger = logger;
        }

        #region-- Assets
        /// <summary>
        /// Method used to get a list of all assets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetAssetsList()
        {
            AssetsVM assetsDetail = new AssetsVM();
            try
            {
                assetsDetail = await _trackAboutRepo.GetAssetList();              
                return Json(assetsDetail);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new Response<object> { TotalRows = ex.Message });
            }
        }
        #endregion

        #region-- IDENTITY
        [HttpPost]
        public async Task<JsonResult> SaveStudent(StudentRegistrationVM model)
        {
            try
            {
                UspBaseSaveResult result = new UspBaseSaveResult();               
                result = await _trackAboutRepo.SaveStudent(model);
                return Json(result);
            }
            catch
            {
                return Json(new UspBaseSaveResult());
            }
        }

        [HttpPost]
        public async Task<JsonResult> LoginStudent(LoginViewModel model)
        {
            try
            {
                UspBaseSaveResult result = new UspBaseSaveResult();
                result = await _trackAboutRepo.LoginStudent(model);
                return Json(result);
            }
            catch
            {
                return Json(new UspBaseSaveResult());
            }
        }
        #endregion
    }
}
