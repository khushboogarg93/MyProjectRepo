using AMVACChemical.Entities.TrackAbout;
using AMVACChemical.Entities.TrackAbout.Identity;
using AMVACChemical.Interfaces.Repository;
using AMVACChemical.ViewModels.TrackAbout.Assets;
using AMVACChemical.ViewModels.TrackAbout.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AMVACChemical.Repositories.TrackAbout
{
    #region Enums declaration
    enum EnumProjectTest { Id, Name };
    #endregion

    public class TrackAboutRepository : ITrackAboutRepository
    {
        // Declare global configurations for inject
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _config;
        private readonly AMVAC_TrackaboutContext _trackAboutContext;
        private readonly string _username;
        private readonly string _password;
        private readonly string _apiKey;
        private HttpClient client = new HttpClient();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // create constructor
        public TrackAboutRepository(IHostingEnvironment env, IConfigurationRoot config, AMVAC_TrackaboutContext trackAboutContext,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _env = env;
            _config = config;
            _trackAboutContext = trackAboutContext;
            _username = _config["TrackAboutApi:Username"];
            _password = _config["TrackAboutApi:Password"];
            _apiKey = _config["TrackAboutApi:ApiKey"];
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region-- ASSETS
        /// <summary>
        /// HttpHandlerMethod Method used to pass and create http client request for trackabout api's 
        /// </summary>
        public void HttpHandlerMethod()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                Credentials = new
                    System.Net.NetworkCredential(_username, _password)
            };
            try
            {
                string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_username + ":" + _password));
                client.DefaultRequestHeaders.Add(RepositoryResource.RepoResource.apikey, _apiKey);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(RepositoryResource.RepoResource.applicationjson));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(RepositoryResource.RepoResource.Basic, credentials);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// GetAssetList Method used to get list of assets from trackAbout Api
        /// </summary>
        /// <returns></returns>
        public async Task<AssetsVM> GetAssetList()
        {
            // Calling function for getting http-client response
            HttpHandlerMethod();
            try
            {
                var apiResponse = await client.GetAsync(RepositoryResource.RepoResource.assetsAPI).ConfigureAwait(false);
                var contentResult = await apiResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var assetResult = JsonConvert.DeserializeObject<AssetsVM>(contentResult);
                return assetResult;
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Dispose();
            }
        }
        #endregion

        #region-- IDENTITY
        public async Task<UspBaseSaveResult> SaveStudent(StudentRegistrationVM model)
        {
            try
            {
                UspBaseSaveResult result = new UspBaseSaveResult();
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var signUpResult = await _userManager.CreateAsync(user, model.Password);
                if (signUpResult.Succeeded)
                {
                    // Add role to user
                    await _userManager.AddToRoleAsync(user, "User");

                    // for signIn after register
                    await _signInManager.SignInAsync(user, false); // false means lost info after leaving page                    
                    result.OperationStatus = "SUCCESS";
                    result.OperationMessage = "Registration successfull!!";
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UspBaseSaveResult> LoginStudent(LoginViewModel model)
        {
            try
            {
                UspBaseSaveResult result = new UspBaseSaveResult();
                var signInresult = await _signInManager.PasswordSignInAsync(model.Email, model.Password,model.RememberMe,false); // logout the user if fail true:false
                if(signInresult.Succeeded)
                {
                    result.OperationStatus = "SUCCESS";
                }
                else
                {
                    result.OperationStatus = "FAIL";
                    result.OperationMessage = "Invalid Login Attempt";
                }

                return result;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
