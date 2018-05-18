using System;
using System.Linq;
using System.Security.Principal;
using ProjektIO.Models;
using ProjektIO.Libraries;
using System.Collections.Generic;
using System.Web;

namespace ProjektIO.Auth
{
    public class Principal : IPrincipal
    {

        private Uzytkownik userData { get; set; }
        private Czlonkowie memberData { get; set; }
        private List<string> urlSegments = new List<string>();

        public IIdentity Identity
        {
            get; private set;
        }

        public Principal(string username)
        {
            Identity = new GenericIdentity(username);
            
        }

        public Principal(int userId)
        {
            using (var db = new DatabaseContext())
            {
                userData = db.Uzytkownik.FirstOrDefault(t => t.Id == userId);
                Identity = new GenericIdentity(userData.Login);
            }
        }

        public Uzytkownik GetUserData()
        {
            return userData;
        }


        private void SetCurrentMember(int groupId)
        {
            using (var db = new DatabaseContext())
            {
                Czlonkowie temp = new Czlonkowie();
                temp = db.Czlonkowie.FirstOrDefault(p => p.IdUzytkownika == userData.Id && p.IdKola == groupId);
                memberData = temp;
            }
        }

        private bool CheckPostAuthor(int postId)
        {
            using (var db = new DatabaseContext())
            {
                Post post = new Post();
                post = db.Post.FirstOrDefault(p => p.Id == postId);
                Czlonkowie temp = new Czlonkowie();
                temp = db.Czlonkowie.FirstOrDefault(p => p.Id == post.IdCzlonka);
                if (temp.IdUzytkownika == userData.Id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool CheckCommentAuthor(int commentId)
        {
            using (var db = new DatabaseContext())
            {
                Komentarz comment = new Komentarz();
                comment = db.Komentarz.FirstOrDefault(p => p.Id == commentId);
                Czlonkowie temp = new Czlonkowie();
                temp = db.Czlonkowie.FirstOrDefault(p => p.Id == comment.IdCzlonka);
                if (temp.IdUzytkownika == userData.Id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private string ScanUrl()
        {
            UrlLibrary library = new UrlLibrary();
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            urlSegments = url.Split('/').ToList();
            urlSegments.RemoveAt(0);
            if (library.GroupFirst.Contains(urlSegments[0]))
            {
                if (library.GroupSecond.Find(p => p == urlSegments[1]) != null)
                {
                    return urlSegments[2];
                }
            }
            if (library.PostFirst.Contains(urlSegments[0]))
            {
                if (library.PostSecond.Find(p => p == urlSegments[1]) != null)
                {
                    return urlSegments[2];
                }
            }
            return null;
        }

        public bool IsInRole(string role)
        {
            switch (role)
            {
                default:
                    return false;
                case RoleLibrary.ADMIN:
                    SetCurrentMember(int.Parse(ScanUrl()));
                    if (memberData == null)
                        return false;
                    return (memberData.Rola & 1) > 0;
                case RoleLibrary.MEMBER:
                    SetCurrentMember(int.Parse(ScanUrl()));
                    if (memberData == null)
                        return false;
                    return (memberData.Rola & 2) > 0;
                case RoleLibrary.POST_AUTHOR:
                    return CheckPostAuthor(int.Parse(ScanUrl()));
                case RoleLibrary.COMMENT_AUTHOR:
                    return CheckCommentAuthor(int.Parse(ScanUrl()));
            }
        }
    }
}