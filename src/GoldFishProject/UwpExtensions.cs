using System.Globalization;
using System.Threading.Tasks;
using System.Reflection;

using Windows.System;
using Windows.ApplicationModel;

namespace FishTank
{
    static class UwpExtensions
    {
        public static async Task LaunchAppReviewPageInStore()
        {
            try // this will except for unpublished apps
            {
                await Task.Run(() =>
                    Launcher.LaunchUriAsync(new System.Uri($"ms-windows-store:review?ProductId={Package.Current.Id.FamilyName}")));
            }
            catch
            {

            }
        }

        public static string GetVersion()
        {
            try
            {
                var currentPackage = Package.Current;
                if (currentPackage?.Id != null)
                {
                    return string.Format(
                                        CultureInfo.InvariantCulture,
                                        "{0}.{1}.{2}.{3}",
                                        currentPackage.Id.Version.Major,
                                        currentPackage.Id.Version.Minor,
                                        currentPackage.Id.Version.Build,
                                        currentPackage.Id.Version.Revision);
                }
            }
            catch
            {

            }

            return Assembly.GetEntryAssembly().GetName().Version.ToString(4);
        }

        public static string GetStoreRegion()
        {
            return new Windows.Globalization.GeographicRegion().CodeTwoLetter;
        }
    }
}
