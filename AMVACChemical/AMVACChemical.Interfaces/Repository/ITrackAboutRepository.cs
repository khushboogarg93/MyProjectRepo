using AMVACChemical.Entities.TrackAbout;
using AMVACChemical.ViewModels.TrackAbout.Assets;
using AMVACChemical.ViewModels.TrackAbout.Identity;
using System.Threading.Tasks;

namespace AMVACChemical.Interfaces.Repository
{
    public interface ITrackAboutRepository
    {
        #region -- ASSETS
        /// <summary>
        /// Declaration for getting list of assets
        /// </summary>
        /// <returns></returns>
        Task<AssetsVM> GetAssetList();
        #endregion

        #region
        Task<UspBaseSaveResult> SaveStudent(StudentRegistrationVM model);

        Task<UspBaseSaveResult> LoginStudent(LoginViewModel model);
        #endregion

    }
}
