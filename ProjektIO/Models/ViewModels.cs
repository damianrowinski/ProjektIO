using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektIO.Models
{
    public class ViewModels
    {
        public ExternalLoginConfirmationViewModel ExternalLoginConfirmation { get; set; }
        public ExternalLoginListViewModel ExternalLoginList { get; set; }
        public SendCodeViewModel SendCode { get; set; }
        public VerifyCodeViewModel VerifyCode { get; set; }
        public ForgotViewModel Forgot { get; set; }
        public LoginViewModel Login { get; set; }
        public RegisterViewModel Register { get; set; }
        public ResetPasswordViewModel GetResetPassword { get; set; }
        public ForgotPasswordViewModel ForgotPassword { get; set; }
        public GroupViewModel GroupModel { get; set; }
        public GroupListViewModel GroupList { get; set; }
        public MembersViewModel Members { get; set; }
        public PostViewModel PostModel { get; set; }
        public PostListViewModel PostList { get; set; }
        public AddCommentViewModel AddComment { get; set; }
        public AddPostViewModel AddPost { get; set; }
        public IndexViewModel Index { get; set; }
        public ManageLoginsViewModel ManageLogins { get; set; }
        public FactorViewModel Factor { get; set; }
        public SetPasswordViewModel SetPassword { get; set; }
        public ChangePasswordViewModel ChangePassword { get; set; }
        public AddPhoneNumberViewModel AddPhoneNumber { get; set; }
        public VerifyPhoneNumberViewModel VerifyPhoneNumber { get; set; }
        public ConfigureTwoFactorViewModel ConfigureTwoFactor { get; set; }
        public Czlonkowie Member { get; set; }
        public Portfolio Portfolio { get; set; }
        public KoloNaukowe Group { get; set; }
        public Post Post { get; set; }
        public Komentarz Comment { get; set; }
    }
}