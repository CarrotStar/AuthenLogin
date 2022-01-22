using AuthenLoginDeploy.Entities;
using System;
using System.Linq;
using System.Web.Helpers;

namespace AuthenLoginDeploy.Models.DAO
{
    public static class UserLoginDAO
    {
        public static resultinfo GetUserLogin(this AuthenDbContext dbContext, TblUsersInfo userinfo = null)
        {
            resultinfo resulted = new resultinfo();

            var query = (from u in dbContext.TblUsers
                         select new TblUsersInfo
                         {
                             UserId = u.UserId,
                             UserName = u.UserName,
                             Password = u.Password,
                             CreateDate = u.CreateDate,
                             Salt = u.Salt,
                             UpdateDate = u.UpdateDate,
                             FirstName = u.FirstName == null ? "" : u.FirstName,
                             LastName = u.LastName == null ? "" : u.LastName,
                             Profileimg = u.Profileimg == null ? "" : u.Profileimg,
                         });

            if (userinfo != null && userinfo.UserName != "")
                query = query.Where(item => item.UserName == userinfo.UserName);

            var list = query.ToList();

            if (list.Count == 0)
            {
                resultinfo ResultFail = new resultinfo
                {

                    Result = new result
                    {
                        Status = false,
                        UserId = null
                    }
                };

                return ResultFail;
            }
            else
            {
                foreach (var item in list)
                {
                    var Password = userinfo.Password + item.Salt;

                    var verified = Crypto.VerifyHashedPassword(item.Password, Password);
                    if (verified)
                    {
                        resultinfo resultedg = new resultinfo
                        {

                            Result = new result
                            {
                                Status = true,
                                UserId = item.UserId,
                                FirstName = item.FirstName,
                                LastName = item.LastName,
                                Profileimg = item.Profileimg
                            }
                        };
                        return resultedg;

                        //break;
                    }
                    else
                    {
                        resultinfo ResultFail = new resultinfo
                        {

                            Result = new result
                            {
                                Status = false,
                                UserId = null
                            }
                        };

                        return ResultFail;
                    }
                }
            }


            return resulted;

        }

        public static bool UpdateUser(this AuthenDbContext dbContext, TblUsersInfo UserInfo = null)
        {
            TblUsersInfo job = new TblUsersInfo();
            TblHistoryUser TblHistoryUserInfo = new TblHistoryUser();
            try
            {

                bool IsUpdate = true;

                var Update = (from s in dbContext.TblUsers
                              where s.UserId == UserInfo.UserId
                              select s).FirstOrDefault();


                var selecthis = (from s in dbContext.TblHistoryUsers
                                 where s.UserId == UserInfo.UserId
                                 orderby s.Id descending
                                 select s);

                var listResult = selecthis.ToList().Take(5);



                foreach (var item in listResult)
                {
                    var Password = UserInfo.Password + item.Salt;

                    var verified = Crypto.VerifyHashedPassword(item.Password, Password);
                    if (verified)
                    {

                        IsUpdate = false;

                        break;
                    }
                }



                if (IsUpdate)
                {
                    var salt = Crypto.GenerateSalt();
                    var Passwprd = UserInfo.Password + salt;

                    var hash = Crypto.HashPassword(Passwprd);

                    Update.Password = hash;
                    Update.Salt = salt;
                    Update.UserName = UserInfo.UserName;
                    Update.FirstName = UserInfo.FirstName;
                    Update.LastName = UserInfo.LastName;
                    Update.Profileimg = UserInfo.Profileimg;
                    Update.EditCount = Update.EditCount + 1;
                    Update.UpdateDate = DateTime.Now;

                    dbContext.TblUsers.Update(Update);
                    dbContext.SaveChanges();

                    var a = insertHistoryUser(dbContext, Update);

                }
                return IsUpdate;
            }
            catch (Exception ex)
            {

                return false;
            }
        }



        public static bool insertUser(this AuthenDbContext dbContext, TblUsersInfo Userinfo = null)
        {

            var query = (from u in dbContext.TblUsers
                         select new TblUsersInfo
                         {
                             UserId = u.UserId,
                             UserName = u.UserName
                         });

            if (Userinfo != null && Userinfo.UserId > 0)
                query = query.Where(item => item.UserId == Userinfo.UserId);

            if (Userinfo.UserName != null && Userinfo.UserName != "")
                query = query.Where(item => item.UserName.Equals(Userinfo.UserName));

            var list = query.ToList();

            if (list.Count >= 1)
                return false;

            TblUsers TblUsersInfo = new TblUsers();

            TblHistoryUser TblHistoryUserInfo = new TblHistoryUser();

            var salt = Crypto.GenerateSalt();

            var Passwprd = Userinfo.Password + salt;
            var hashedPass = Crypto.HashPassword(Passwprd);

            TblUsersInfo.UserName = Userinfo.UserName;
            TblUsersInfo.Password = hashedPass;
            TblUsersInfo.Salt = salt;
            TblUsersInfo.FirstName = Userinfo.FirstName;
            TblUsersInfo.LastName = Userinfo.LastName;
            TblUsersInfo.Profileimg = Userinfo.Profileimg;
            TblUsersInfo.EditCount = 0;
            TblUsersInfo.CreateDate = DateTime.Now;



            try
            {

                dbContext.TblUsers.Add(TblUsersInfo);
                dbContext.SaveChanges();

                var a = insertHistoryUser(dbContext, TblUsersInfo);


                return true;
            }

            catch (Exception ex)
            {
                return false;



            }
        }

        public static bool insertHistoryUser(this AuthenDbContext dbContext, TblUsers Userinfo = null)
        {

            TblHistoryUser TblHistoryUserInfo = new TblHistoryUser();
            TblHistoryUserInfo.UserId = Userinfo.UserId;
            TblHistoryUserInfo.UserName = Userinfo.UserName;
            TblHistoryUserInfo.Password = Userinfo.Password;
            TblHistoryUserInfo.Salt = Userinfo.Salt;
            TblHistoryUserInfo.FirstName = Userinfo.FirstName;
            TblHistoryUserInfo.LastName = Userinfo.LastName;
            TblHistoryUserInfo.Profileimg = Userinfo.Profileimg;
            TblHistoryUserInfo.UpdateDate = DateTime.Now;

            try
            {

                dbContext.TblHistoryUsers.Add(TblHistoryUserInfo);
                dbContext.SaveChanges();
                return true;
            }

            catch (Exception ex)
            {
                return false;



            }
        }
    }
}
