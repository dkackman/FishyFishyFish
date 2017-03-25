using Windows.Foundation;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;

namespace FishTank.Uwp
{
    class SimpleNotification
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public void Show(TypedEventHandler<ToastNotification, object> callback)
        {
            // Construct the visuals of the toast
            var visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = Title
                        },

                        new AdaptiveText()
                        {
                            Text = Content
                        }
                    }
                }
            };

            // Construct the actions for the toast (inputs and buttons)
            var actions = new ToastActionsCustom()
            {
                Buttons =
                {
                    new ToastButton("Got it", new QueryString()
                    {
                        { "action", "gotit" }
                    }.ToString()),

                    new ToastButton("Show me", new QueryString()
                    {
                        { "action", "showme" }
                    }.ToString())
                }
            };

            // Now we can construct the final toast content
            var toastContent = new ToastContent()
            {
                Visual = visual,
                Actions = actions,
                ActivationType = ToastActivationType.Protocol,
                Launch = "fishyfish"
            };

            var xml = new XmlDocument();
            xml.LoadXml(toastContent.GetContent());

            // And create the toast notification
            var notification = new ToastNotification(xml);
            notification.Activated += callback;

            // And then send the toast
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }
    }
}
