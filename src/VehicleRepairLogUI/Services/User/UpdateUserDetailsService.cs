namespace VehicleRepairLogUI.Services.User
{
    public class UpdateUserDetailsService
    {
        public bool ShowEditDetailsForm { get; set; }
        public bool ShowPasswordChangeForm { get; private set; }

        public void ShowUpdateUserDetailsForm()
        {
            ShowEditDetailsForm = true;
        }

        public void ShowUserPasswordChangeForm()
        {
            ShowPasswordChangeForm = true;
        }

        public void ReturnToUserProfilePage()
        {
            ShowEditDetailsForm = false;
            ShowPasswordChangeForm = false;
        }
    }
}
