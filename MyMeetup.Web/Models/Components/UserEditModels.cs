namespace MyMeetup.Web.Models.Components
{
    public class UserEditModel
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsOkToGetMeetupsInfo { get; set; }

        public int Id { get; set; }

        /// <summary>
        /// Where to get back
        /// </summary>
        public string BackUrl { get; set; }
    }
}
