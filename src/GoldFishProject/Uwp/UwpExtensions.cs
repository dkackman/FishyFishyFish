using System;
using System.Threading.Tasks;

using Windows.System;
using Windows.ApplicationModel;

namespace FishTank.Uwp
{
    static class UwpExtensions
    {
        public static async Task LaunchAppReviewPageInStore()
        {
            try // this will except for unpublished apps
            {
                await Task.Run(() =>
                    Launcher.LaunchUriAsync(new Uri($"ms-windows-store:review?pfn={Package.Current.Id.FamilyName}")));
            }
            catch
            {

            }
        }
    }
}
